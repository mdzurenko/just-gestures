using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Drawing;
using JustGestures.Properties;
using JustGestures.GestureParts;

namespace JustGestures.TypeOfAction
{
    [Serializable]
    class WindowOptions : BaseActionClass
    {
        public const string NAME = "window_name";

        public const string WND_MIN = "window_min";
        public const string WND_MIN_ALL = "window_min_all";
        public const string WND_MIN_TOTRAY = "window_min_totray";
        public const string WND_MAX = "window_max";
        public const string WND_CLOSE = "window_close";
        public const string WND_CLOSE_ALL = "window_close_all";
        public const string WND_FULL_SCREEN = "window_fullscreen";
        public const string WND_RETURN_TO_NORMAL = "Return Window To Normal State";
        public const string WND_TOP_MOST = "window_topmost";
        public const string WND_TRANSPARENT = "window_transparent";
        public const string WND_SHOW_SIDE_BY_SIDE = "window_show_side_by_side";
        public const string WND_SHOW_VERTICALLY = "window_show_vertically";
        public const string WND_REDRAWN = "REDRAWN";
        static Dictionary<IntPtr, Win32.WINDOWINFO> m_wndTray;
        static Dictionary<IntPtr, Win32.WINDOWINFO> m_wndFullscreen;        
        static List<IntPtr> m_wndTopMost;
        static List<IntPtr> m_openedWnds;

        const double WND_DEFAUL_RATIO = 0.66;
        public static double[] TransparencyLvl = new double[] { 1, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
                
        public WindowOptions()
        {
            m_actions = new List<string>
                (new string[] { 
                    WND_MIN, 
                    WND_MIN_ALL,
                    WND_MIN_TOTRAY,
                    WND_MAX,
                    WND_CLOSE, 
                    WND_CLOSE_ALL, 
                    WND_FULL_SCREEN, 
                    //WND_RETURN_TO_NORMAL, 
                    WND_TOP_MOST,
                    WND_TRANSPARENT, 
                    WND_SHOW_SIDE_BY_SIDE,
                    WND_SHOW_VERTICALLY
                });     
        }

        public WindowOptions(string action) : base(action) { }

        public WindowOptions(WindowOptions action) : base(action) { }

        public WindowOptions(SerializationInfo info, StreamingContext context)
            : base (info, context)
        {
        }

        public override object Clone()
        {
            return new WindowOptions(this);
        }

        public override bool IsSensitiveToMySystemWindows()
        {
            switch (m_name)
            {
                case WND_CLOSE_ALL:
                case WND_MIN_ALL:
                case WND_SHOW_SIDE_BY_SIDE:
                case WND_SHOW_VERTICALLY:
                    return false;
                    break;
                default:
                    return true;
                    break;
            }
        }

        delegate void Emptydel();

        public override void ExecuteAction(IntPtr activeWnd, Point location)
        {
            if (m_wndFullscreen == null) m_wndFullscreen = new Dictionary<IntPtr, Win32.WINDOWINFO>();
            if (m_wndTray == null) m_wndTray = new Dictionary<IntPtr, Win32.WINDOWINFO>();
            if (m_wndTopMost == null) m_wndTopMost = new List<IntPtr>();
            if (m_openedWnds == null) m_openedWnds = new List<IntPtr>();
            
            StringBuilder buff = new StringBuilder(256);
            Win32.GetClassName(activeWnd, buff, 256);
            Debug.WriteLine(buff.ToString());
            string wndName = buff.ToString();//.ToUpper();
            if ((AppGroupOptions.IsDesktop(wndName) || AppGroupOptions.IsTaskbar(wndName)) 
                && (this.Name != WND_MIN_ALL && this.Name != WND_CLOSE_ALL && this.Name != WND_SHOW_SIDE_BY_SIDE
                && this.Name != WND_SHOW_VERTICALLY))
                return;
            //Win32.GetWindowText(ActiveWnd, buff, 256);            

            int x1, x2, y1, y2;
            Win32.WINDOWINFO info = new Win32.WINDOWINFO();
            Rectangle screen;
            info.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(info);
            Win32.WINDOWPLACEMENT placement = new Win32.WINDOWPLACEMENT();
            placement.length = System.Runtime.InteropServices.Marshal.SizeOf(placement);

            IntPtr hwndTaskbar = Win32.FindWindow(AppGroupOptions.SYSTEM_TASKBAR, null);            
            switch (this.Name)
            {
                case WND_MIN:
                    Win32.SendMessage(activeWnd, Win32.WM_SYSCOMMAND, Win32.SC_MINIMIZE, 0);                    
                    break;
                case WND_MIN_ALL:
                    List<IntPtr> opendedWnds = new List<IntPtr>(m_openedWnds);
                    m_openedWnds = new List<IntPtr>();
                    Win32.EnumDelegate enumfunc = new Win32.EnumDelegate(EnumWindowsProc);
                    IntPtr hDesktop = IntPtr.Zero; // current desktop
                    bool success = Win32.EnumDesktopWindows(hDesktop, enumfunc, IntPtr.Zero);
                    if (success)
                    {
                        if (m_openedWnds.Count == 0)
                        {
                            opendedWnds.Reverse();
                            foreach (IntPtr hwnd in opendedWnds)
                                Win32.SendMessage(hwnd, Win32.WM_SYSCOMMAND, Win32.SC_RESTORE, 0);
                                //Win32.SetWindowPos(hwnd, Win32.HWND_NOTOPMOST, 0, 0, 0, 0, Win32.SWP_SHOWWINDOW);
                        }
                        else
                        {
                            foreach (IntPtr hwnd in m_openedWnds)
                                Win32.SendMessage(hwnd, Win32.WM_SYSCOMMAND, Win32.SC_MINIMIZE, 0);
                        }
                    }
                //Win32.SendMessage(hwndTaskbar, Win32.WM_COMMAND, Win32.WINS_MIN_ALL, 0);
                    break;
                case WND_MIN_TOTRAY:
                    StringBuilder title = new StringBuilder(256);
                    Win32.GetWindowText(activeWnd, title, 256);
                    Emptydel del = delegate()
                    {
                        Win32.GetWindowInfo(activeWnd, ref info);
                        if (m_wndTray.ContainsKey(activeWnd))
                            m_wndTray[activeWnd] = info;
                        else
                        {
                            m_wndTray.Add(activeWnd, info);
                            NotifyIcon tray = new NotifyIcon();
                            //tray = new NotifyIcon();                    
                            tray.Visible = true;
                            tray.Tag = activeWnd;
                            tray.Icon = GetWindowIcon(activeWnd);
                            tray.Text = title.Length >= 64 ? title.ToString().Substring(0, 60) + "..." : title.ToString();
                            tray.Click += new EventHandler(tray_Click);
                            Form_engine.TrayIcons.Add(tray);
                        }
                        Win32.ShowWindow(activeWnd, 0);//hide
                    };
                    Form_engine.Instance.Invoke(del);
                    //if (string.IsNullOrEmpty(title.ToString()))
                    //    return;
                    break;
                case WND_MAX:
                    Win32.GetWindowPlacement(activeWnd, ref placement);
                    if (placement.showCmd == Win32.SW_SHOWNORMAL)
                        Win32.SendMessage(activeWnd, Win32.WM_SYSCOMMAND, Win32.SC_MAXIMIZE, 0);
                    else if (placement.showCmd == Win32.SW_SHOWMAXIMIZED)
                        Win32.SendMessage(activeWnd, Win32.WM_SYSCOMMAND, Win32.SC_RESTORE, 0);
                    break;
                case WND_CLOSE:
                    //Win32.PostMessage(ActiveWnd, Win32.WM_SYSCOMMAND, Win32.SC_CLOSE, 0);
                    Win32.PostMessage(activeWnd, Win32.WM_CLOSE, 0, 0);
                    break;
                case WND_CLOSE_ALL:
                    int jgProcessId = Form_engine.Instance.ProcessId;
                    foreach (Process p in Process.GetProcesses(System.Environment.MachineName))
                    {
                        if (p.Id != jgProcessId)
                        {
                            // some application which aren't system might have handle zero but still has to be closed! fix is required
                            if (p.MainWindowHandle != IntPtr.Zero)
                                p.CloseMainWindow();
                        }
                    }      
                    break;
                case WND_FULL_SCREEN:
                    screen = Screen.FromHandle(activeWnd).Bounds;
                    Win32.GetWindowInfo(activeWnd, ref info);
                    
                    int style = (int)info.dwStyle;
                    int fullscreen_style = (int)info.dwStyle & ~Win32.WS_CAPTION & ~Win32.WS_THICKFRAME;

                    //RETURN_TO_NORMAL
                    if ((info.rcWindow.left == screen.Left && info.rcWindow.top == screen.Top &&
                        info.rcWindow.right == screen.Right && info.rcWindow.bottom == screen.Bottom
                        && style == fullscreen_style) || m_wndFullscreen.ContainsKey(activeWnd))
                    {
                        goto case WND_RETURN_TO_NORMAL;                     
                    }
                    //MAKE_FULL_SCREEN
                    else
                    {
                        m_wndFullscreen.Add(activeWnd, info);
                        Win32.SetWindowLong(activeWnd, Win32.GWL_STYLE, ((int)info.dwStyle & ~Win32.WS_CAPTION & ~Win32.WS_THICKFRAME));
                        //Win32.SetWindowLong(ActiveWnd, Win32.GWL_EXSTYLE, (int)info.dwExStyle & ~Win32.WS_EX_TOOLWINDOW);
                        Win32.SetWindowPos(activeWnd, Win32.HWND_TOPMOST, screen.Left, screen.Top, screen.Width, screen.Height, Win32.SWP_SHOWWINDOW | Win32.SWP_DRAWFRAME | Win32.SWP_FRAMECHANGED);
                        //Win32.SetWindowPos(ActiveWnd, Win32.HWND_TOPMOST, x1, y1, x2, y2, Win32.SWP_SHOWWINDOW | Win32.SWP_DRAWFRAME | Win32.SWP_FRAMECHANGED);
                        //Win32.MoveWindow(ActiveWnd, x1, y1, info.rcWindow.right - info.rcWindow.left, info.rcWindow.bottom - info.rcWindow.top, true);
                        //Win32.MoveWindow(ActiveWnd, x1, y1, x2, y2, true);                    
                        //Win32.InvalidateRect(ActiveWnd, ref info.rcWindow, true);
                        //Win32.UpdateWindow(ActiveWnd);                    
                    }
                    break;
                case WND_RETURN_TO_NORMAL:
                    if (m_wndFullscreen.ContainsKey(activeWnd))
                    {
                        info = m_wndFullscreen[activeWnd];
                        x1 = info.rcWindow.left;
                        y1 = info.rcWindow.top;
                        x2 = info.rcWindow.right - x1;
                        y2 = info.rcWindow.bottom - y1;
                        Win32.SetWindowLong(activeWnd, Win32.GWL_STYLE, (int)info.dwStyle);
                        Win32.SetWindowLong(activeWnd, Win32.GWL_EXSTYLE, (int)info.dwExStyle & ~Win32.WS_EX_LAYERED);
                        //Win32.SendMessage(ActiveWnd, Win32.WM_SYSCOMMAND, Win32.SC_RESTORE, 0);
                        Win32.SetWindowPos(activeWnd, Win32.HWND_NOTOPMOST, x1, y1, x2, y2, Win32.SWP_SHOWWINDOW | Win32.SWP_DRAWFRAME | Win32.SWP_FRAMECHANGED);
                        m_wndFullscreen.Remove(activeWnd);
                    }
                    else
                    {
                        screen = Screen.FromHandle(activeWnd).Bounds;
                        x1 = screen.Left + (int)(screen.Width * (1 - WND_DEFAUL_RATIO)) / 2;
                        y1 = screen.Top + (int)(screen.Height * (1 - WND_DEFAUL_RATIO)) / 2;
                        x2 = (int)Math.Floor(screen.Width * WND_DEFAUL_RATIO);
                        y2 = (int)Math.Floor(screen.Height * WND_DEFAUL_RATIO);

                        Win32.SetWindowLong(activeWnd, Win32.GWL_STYLE, (int)info.dwStyle | Win32.WS_CAPTION | Win32.WS_THICKFRAME);
                        Win32.SetWindowLong(activeWnd, Win32.GWL_EXSTYLE, (int)info.dwExStyle & ~Win32.WS_EX_LAYERED);
                        //Win32.RedrawWindow(ActiveWnd, ref info.rcWindow, IntPtr.Zero, Win32.RDW_ERASE | Win32.RDW_INVALIDATE | Win32.RDW_FRAME | Win32.RDW_ALLCHILDREN);                        
                        Win32.SetWindowPos(activeWnd, Win32.HWND_NOTOPMOST, x1, y1, x2, y2, Win32.SWP_SHOWWINDOW | Win32.SWP_DRAWFRAME | Win32.SWP_FRAMECHANGED);
                        
                    }
                    break;
                case WND_TOP_MOST:
                    Win32.GetWindowInfo(activeWnd, ref info);
                    x1 = info.rcWindow.left;
                    y1 = info.rcWindow.top;
                    x2 = info.rcWindow.right - x1;
                    y2 = info.rcWindow.bottom - y1;
                    if (!m_wndTopMost.Contains(activeWnd))
                    {
                        m_wndTopMost.Add(activeWnd);
                        Win32.SetWindowPos(activeWnd, Win32.HWND_TOPMOST, x1, y1, x2, y2, Win32.SWP_SHOWWINDOW);
                    }
                    else
                    {
                        m_wndTopMost.Remove(activeWnd);
                        Win32.SetWindowPos(activeWnd, Win32.HWND_NOTOPMOST, x1, y1, x2, y2, Win32.SWP_SHOWWINDOW);
                    }
                       
                    break;                
                case WND_TRANSPARENT:
                    SetWindowTrasparency(activeWnd, Int16.Parse(this.Details));
                    break;
                case WND_SHOW_VERTICALLY:
                    Win32.SendMessage(hwndTaskbar, Win32.WM_COMMAND, Win32.WINS_ARRANGE_VRT, 0);
                    break;
                case WND_SHOW_SIDE_BY_SIDE:
                    Win32.SendMessage(hwndTaskbar, Win32.WM_COMMAND, Win32.WINS_ARRANGE_HRZ, 0);
                    break;
                case WND_REDRAWN:
                    Win32.InvalidateRgn(activeWnd, IntPtr.Zero, true);
                    //Win32.GetWindowInfo(ActiveWnd, ref info);
                    //x1 = info.rcWindow.left;
                    //y1 = info.rcWindow.top;
                    //x2 = info.rcWindow.right - x1;
                    //y2 = info.rcWindow.bottom - y1;

                    //IntPtr screenDc = Win32.GetDC(IntPtr.Zero);
                    //IntPtr memDc = Win32.CreateCompatibleDC(screenDc);
                    //Win32.SIZE size = new Win32.SIZE(Math.Abs(x2 - x1), Math.Abs(y2 - y1));

                    //Win32.POINT pointSource = new Win32.POINT(x1,y1);
                    //Win32.POINT topPos = new Win32.POINT(x1, y1);

                    //Win32.BLENDFUNCTION blend = new Win32.BLENDFUNCTION();
                    //blend.BlendOp = 0;
                    //blend.BlendFlags = 0;
                    //blend.SourceConstantAlpha = 122;
                    //blend.AlphaFormat = 1;

                    //Win32.UpdateLayeredWindow(ActiveWnd, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, Win32.ULW_ALPHA);

                    ////Win32.InvalidateRect(ActiveWnd, ref info.rcWindow, true);
                    ////Win32.MoveWindow(ActiveWnd, x1, y1, x2 - 10, y2 - 10, true);
                    ////Win32.UpdateWindow(ActiveWnd);
                    ////Win32.SetWindowPos(ActiveWnd, Win32.HWND_NOTOPMOST, x1, y1, x2, y2, Win32.SWP_FRAMECHANGED);
                    //Win32.ReleaseDC(IntPtr.Zero, screenDc);
                    //Win32.DeleteDC(memDc);
                    break;
            }
        }

        //private static bool IsPopup(IntPtr hWnd)
        //{
        //    StringBuilder buff = new StringBuilder(256);
        //    Win32.GetClassName(hWnd, buff, 256);
        //    string wndName = buff.ToString();

        //    int wndStyle = Win32.GetWindowLong(hWnd, Win32.GWL_STYLE);
        //    int wndExStyle = Win32.GetWindowLong(hWnd, Win32.GWL_EXSTYLE);

        //    bool isPopup = (wndStyle & Win32.WS_POPUP) != 0;
        //    bool isTooWnd = (wndExStyle & Win32.WS_EX_TOOLWINDOW) != 0;
        //    bool isWindow = Win32.IsWindow(hWnd);
        //    Debug.WriteLine("ClassName: " + wndName + " IsPopup : " + isPopup + " IsToolWnd: " + isTooWnd + " IsWindow: " + isWindow);
        //    return false;
        //}

        /// <summary>
        /// Check if window handle is visible opened window which should be minimized
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private static bool EnumWindowsProc(IntPtr hWnd, int lParam)
        {
            Win32.WINDOWPLACEMENT placement = new Win32.WINDOWPLACEMENT();
            placement.length = System.Runtime.InteropServices.Marshal.SizeOf(placement);            
            Win32.GetWindowPlacement(hWnd, ref placement);


            StringBuilder buff = new StringBuilder(256);
            IntPtr mainWnd = Win32.GetAncestor(hWnd, Win32.GA_ROOT);

            int wndExStyle = Win32.GetWindowLong(hWnd, Win32.GWL_EXSTYLE);

            Win32.GetClassName(mainWnd, buff, 256);
            string className = buff.ToString();

            if (mainWnd != IntPtr.Zero 
                && Win32.IsWindowVisible(mainWnd)   
                && placement.showCmd != Win32.SW_SHOWMINIMIZED
                && placement.showCmd != Win32.SW_MINIMIZE
                && ((wndExStyle & ~Win32.WS_EX_TOOLWINDOW) == wndExStyle) // is not tool window
                && !AppGroupOptions.IsDesktop(className)
                && !AppGroupOptions.IsTaskbar(className)
                && !m_openedWnds.Contains(mainWnd)
                )
            {
                Win32.GetWindowText(mainWnd, buff, 256);
                string wndText = buff.ToString();
                Debug.WriteLine("ADD: " + wndText + " " + placement.showCmd);

                m_openedWnds.Add(hWnd);
            }
            return true;
        }

        public static void ShowAllTrayWindows()
        {          
            //shows all windows which have been minimized to system tray
            while (Form_engine.TrayIcons.Count > 0)
            {
                NotifyIcon tray = Form_engine.TrayIcons[0];
                tray_Click(tray, null);
            }            
        }

        private static void tray_Click(object sender, EventArgs e)
        {
            NotifyIcon tray = sender as NotifyIcon;
            IntPtr hWnd = (IntPtr)tray.Tag;
            Win32.WINDOWINFO info;
            if (m_wndTray.ContainsKey(hWnd))
            {
                info = m_wndTray[hWnd];
                SetWindowFromInfo(hWnd, info);
                m_wndTray.Remove(hWnd);
            }
            else
            {
                Win32.ShowWindow(hWnd, 1);//show
                Win32.SetForegroundWindow(hWnd);
            }
            tray.Click -= new EventHandler(tray_Click);
            Form_engine.TrayIcons.Remove(tray);
            tray.Dispose();
        }

        private static void SetWindowFromInfo(IntPtr hwnd, Win32.WINDOWINFO info)
        {
            int x1 = info.rcWindow.left;
            int y1 = info.rcWindow.top;
            int x2 = info.rcWindow.right - x1;
            int y2 = info.rcWindow.bottom - y1;
            //Win32.SetWindowLong(hwnd, Win32.GWL_STYLE, (int)info.dwStyle);
            //Win32.SetWindowLong(hwnd, Win32.GWL_EXSTYLE, (int)info.dwExStyle);
            Win32.SetWindowPos(hwnd, Win32.HWND_NOTOPMOST, x1, y1, x2, y2, Win32.SWP_SHOWWINDOW);
        }

        public override Bitmap GetIcon(int size)
        {

            switch (this.Name)
            {
                case NAME:
                    return Resources.window;
                    //break;
                case WND_CLOSE:                    
                case WND_CLOSE_ALL:
                    return Resources.window_close;
                    //break;
                case WND_MAX:
                    return Resources.window_max;
                    //break;
                case WND_MIN:
                case WND_MIN_ALL:
                case WND_MIN_TOTRAY:
                    return Resources.window_min;
                    //break;
                case WND_FULL_SCREEN:
                    return Resources.window_fullscreen;
                    //break;
                case WND_TOP_MOST:
                    return Resources.window_topmost;
                    //break;
                case WND_TRANSPARENT:
                    return Resources.window_transparent;
                    //break;
                case WND_SHOW_VERTICALLY:
                    return Resources.window_vertical;
                    //break;
                case WND_SHOW_SIDE_BY_SIDE:
                    return Resources.window_side_by_side;
                    //break;
                default:
                    return Resources.window;
                //break;
            }            
        }

        Icon GetWindowIcon(IntPtr hWnd)
        {
            IntPtr mainWindow = hWnd; //Win32.GetAncestor(hWnd, Win32.GA_ROOTOWNER);
            IntPtr hIcon = Win32.SendMessage(mainWindow, Win32.WM_GETICON, Win32.ICON_BIG, 0);
            if (hIcon == IntPtr.Zero)
            {
                string path = AppGroupOptions.GetPathFromHwnd(hWnd);
                if (path != string.Empty)
                {
                    try
                    {
                        return Icon.ExtractAssociatedIcon(path);
                    }
                    catch
                    {
                        hIcon = Resources.window.GetHicon();
                    }
                }
                else hIcon = Resources.window.GetHicon();
            }
            return Icon.FromHandle(hIcon);
        }

        #region Transparency Methods

        public static int GetWindowTransparency(IntPtr hWnd)
        {
            uint crKey = 0;
            byte bAlpha;
            uint dwFlags;            
            int num = Win32.GetWindowLong(hWnd, Win32.GWL_EXSTYLE);
            if (num == (num & ~Win32.WS_EX_LAYERED)) return -1;

            Win32.GetLayeredWindowAttributes(hWnd, out crKey, out bAlpha, out dwFlags);
            return (bAlpha * 100) / 255;
        }

        public static void SetWindowTrasparency(IntPtr hWnd, int visibility)
        {
            if (visibility == 100)
            {
                Win32.WINDOWINFO info = new Win32.WINDOWINFO();
                Win32.GetWindowInfo(hWnd, ref info);
                Win32.SetWindowLong(hWnd, Win32.GWL_EXSTYLE, (int)info.dwExStyle & ~Win32.WS_EX_LAYERED);
            }
            else
            {
                int num = Win32.GetWindowLong(hWnd, Win32.GWL_EXSTYLE);
                num |= Win32.WS_EX_LAYERED;
                Win32.SetWindowLong(hWnd, Win32.GWL_EXSTYLE, num);
                Win32.SetLayeredWindowAttributes(hWnd, 0, (byte)Math.Round((double)((((double)visibility) / 100) * 255)), Win32.LWA_ALPHA);
            }
        }

        public static void ChangeWndTransparency(IntPtr hWnd, bool increase)
        {
            int original = GetWindowTransparency(hWnd);            
            int newTransparency;
            if (increase)
            {
                if (original == -1) return;
                if (original == 1)
                    newTransparency = 10;
                else
                    newTransparency = original < 90 ? original + 10 : 100;
            }
            else
            {
                if (original == -1)
                    newTransparency = 90;
                else
                    newTransparency = original > 10 ? original - 10 : 1;
            }
            SetWindowTrasparency(hWnd, newTransparency);
        }
        #endregion Transparency Methods
    }
}
