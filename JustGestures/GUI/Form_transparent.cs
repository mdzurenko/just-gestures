using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using JustGestures.Properties;
using JustGestures.ControlItems;

namespace JustGestures.GUI
{
    public partial class Form_transparent : Form
    {
        #region Members
        /// <summary>
        /// All points of the drawn curve
        /// </summary>
        List<PointF> m_curvePoints = new List<PointF>();
        /// <summary>
        /// Is true when toggle button was pushed and form should be displayed
        /// </summary>
        bool m_drawingStarted = false;
        /// <summary>
        /// Is true when toggle button was released and form should be hidden
        /// </summary>
        bool m_drawingEnded = false;
        /// <summary>
        /// Is true when drawing is performed in thread timer tick
        /// </summary>
        bool m_threadDrawing = false;
        /// <summary>
        /// Is true when user draws gesture
        /// </summary>
        bool m_gestureDrawing = false;
        /// <summary>
        /// Region on which was drawn
        /// </summary>
        Win32.RECT m_drawnRegion;
        /// <summary>
        /// Is true when the region should be redrawn during the tick
        /// </summary>
        bool m_redrawInTick = false;
        /// <summary>
        /// Indicates whether the form is on top and should be sent to back
        /// </summary>
        bool m_isOnTop = false;
        /// <summary>
        /// Pen that is used for drawing the curve
        /// </summary>
        Pen m_pen;
        /// <summary>
        /// Width of pen (used for thread safe)
        /// </summary>
        int m_penWidth = 0;
        /// <summary>
        /// Graphics made from the form
        /// </summary>
        Graphics m_gp;
        /// <summary>
        /// Handle of this form
        /// </summary>
        IntPtr m_hwndForm;        
        /// <summary>
        /// Handle of context menu
        /// </summary>
        IntPtr m_hwndCms;
        /// <summary>
        /// Handles of all sub-context menus
        /// </summary>
        List<IntPtr> m_hwndCmsList;
        /// <summary>
        /// List of all sub-context menus
        /// </summary>
        List<ContextMenuStrip> m_cmsList = new List<ContextMenuStrip>();
        /// <summary>
        /// Context menu of actions that have same gestures and should be displayed
        /// </summary>
        ContextMenuStrip cMS_MatchedGestures;
        /// <summary>
        /// Tooltip for actioncs associated to current gesture
        /// </summary>     
        MyToolTip m_tooltip;
        /// <summary>
        /// Timer used for asynchronous drawing
        /// </summary>
        System.Threading.Timer m_threadTimerDraw;
        /// <summary>
        /// Indicates that context menu is visible
        /// </summary>
        bool m_contextMenuIsVisible = false;
        /// <summary>
        /// Index of the last point that was drawn
        /// </summary>
        int m_lastIndex = 0;        
        
        /// <summary>
        /// Points of drawn curve
        /// </summary>
        public List<PointF> Points { get { return m_curvePoints; } }
        /// <summary>
        /// Context menu of actions that are associated to same gesture 
        /// </summary>
        public ContextMenuStrip CmsMatchedGestures { get { return cMS_MatchedGestures; } }

        delegate void ShowContextMenuCallBack(ContextMenuStrip cms);
        //delegate void AddItemToCmsCallBack(ToolStripMenuItem item);
        delegate void AddItemToCmsCallBack(ContextMenuStrip cms, ToolStripMenuItem item);
        delegate void RefreshFormCallBack(Form form);
        delegate void ShowToolTipCallBack(string msg);
        delegate void EmptyHandler();
        delegate void DlgMouseHandler(object sender, MouseEventArgs e);

        #endregion Members

        uint WM_NCHITTEST = 0x0084;
        int WM_ERASEBKGND = 0x14;
        int HTTRANSPARENT = -1;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)WM_NCHITTEST)
                m.Result = (IntPtr)HTTRANSPARENT;
            else if (m.Msg != WM_ERASEBKGND)
                base.WndProc(ref m);
        }

        public Form_transparent()
        {
            InitializeComponent();
            //this.Size = new Size(0, 0);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);            


            BackColor = Color.Black;
            TransparencyKey = Color.Black;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;

            m_pen = new Pen(Brushes.Red);
            m_pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            m_pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            m_gp = this.CreateGraphics();

            cMS_MatchedGestures = new ContextMenuStrip();
            cMS_MatchedGestures.Text = "cMS_MatchedGestures";
            // register visibility event so the context menu is closed when it is hidden
            cMS_MatchedGestures.VisibleChanged += new EventHandler(cMS_MatchedGestures_VisibleChanged);
            m_hwndCms = cMS_MatchedGestures.Handle;
            this.ContextMenuStrip = cMS_MatchedGestures;
            m_hwndCmsList = new List<IntPtr>();

            m_tooltip = new MyToolTip();

            m_hwndForm = this.Handle;
            m_threadTimerDraw = new System.Threading.Timer(m_threadTimerDraw_Tick, null, -1, -1);
        }

        private void Form_transparent_Load(object sender, EventArgs e)
        {            
            WindowState = FormWindowState.Normal;
            TopMost = true;
            //DoubleBuffered = true;            
            int wndStyle = Win32.GetWindowLong(this.Handle, Win32.GWL_EXSTYLE);
            Win32.SetWindowLong(this.Handle, Win32.GWL_EXSTYLE, wndStyle | Win32.WS_EX_TOOLWINDOW);
            ResizeWindow();

            //Win32.SetWindowPos(Handle, Win32.HWND_TOPMOST, 280, 0, 1000, 768, Win32.SWP_SHOWWINDOW | Win32.SWP_DRAWFRAME | Win32.SWP_FRAMECHANGED);
            m_gp = this.CreateGraphics();
            //m_gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            ApplySettings();
        }

        /// <summary>
        /// Resize form all over the desktops
        /// </summary>
        public void ResizeWindow()
        {
            Rectangle rect = new Rectangle();
            GetScreenSize(ref rect); //get size of window that will cover all desktops
            //must be rect.Left - 1, otherwise some applications might detect, that this window is fullscreen and will start behave not in usual way
            Win32.SetWindowPos(m_hwndForm, Win32.HWND_TOPMOST, rect.Left - 1, rect.Top, rect.Width, rect.Height, Win32.SWP_SHOWWINDOW | Win32.SWP_DRAWFRAME | Win32.SWP_FRAMECHANGED);
        }

        /// <summary>
        /// Apply changed settings
        /// </summary>
        public void ApplySettings()
        {
            m_pen.Color = Config.User.PenColor;
            m_pen.Width = Config.User.PenWidth;
            m_penWidth = Config.User.PenWidth;
        }

        /// <summary>
        /// Toggle button was pushed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Mouse_ToggleButtonDown(object sender, MouseEventArgs e)
        {
            m_gestureDrawing = true;

            m_lastIndex = 0;
            PointF point = new PointF(e.X - this.Location.X, e.Y - this.Location.Y);
            // add point immediately after the array was cleard
            m_curvePoints.Clear();
            m_curvePoints.Add(point);

            m_drawingEnded = false;
            m_drawingStarted = true;
            m_redrawInTick = false;

            // even when the gesture should be display the top form has to be shown
            m_threadTimerDraw.Change(10, 30);
        }

        /// <summary>
        /// Assynchronous painting via thread timer
        /// </summary>
        /// <param name="state"></param>
        void m_threadTimerDraw_Tick(object state)
        {
            // if the drawing from previous tick wasn't finished then do not start another
            if (m_threadDrawing) return;
            // draw only when necesary 
            if (m_curvePoints.Count < 2 && !m_drawingStarted)// && !m_drawingEnded)
                return;
            // drawing in thread tick has been started
            m_threadDrawing = true;

            if (m_drawingStarted)
            {
                // if drawing was started then show the form
                MakeTopMost();
                m_drawingStarted = false;

                if (!Config.User.DisplayGesture)
                {
                    // if gesture should not be displayed then stop timer and do not continue
                    m_threadTimerDraw.Change(-1, -1);
                    return;
                }
            }
           
            // when toggle button was released do not draw in last tick
            if (!m_drawingEnded)
            {
                /**
                 * m_pointsToDraw points might be changed while DrawLines operation 
                 * therefore copy only current points that should be drawn
                 * */
                //PointF[] points = m_pointsToDraw.ToArray();                
                int lastIndex = m_curvePoints.Count;
                PointF[] points = m_curvePoints.GetRange(m_lastIndex, lastIndex - m_lastIndex).ToArray();
                m_lastIndex = lastIndex - 1;
                if (points.Length > 1)
                    m_gp.DrawLines(m_pen, points);
            }

            // the refresh method has to be done in 
            if (m_redrawInTick)
            {
                Debug.WriteLine(String.Format("##### InvalidateRect in TICK left: {0}, top: {1}, right: {2}, bottom: {3} ######", 
                    m_drawnRegion.left, m_drawnRegion.top, m_drawnRegion.right, m_drawnRegion.bottom));
                //Win32.InvalidateRect(m_hwndForm, ref m_drawnRegion, true);
                Win32.RedrawWindow(m_hwndForm, ref m_drawnRegion, IntPtr.Zero, Win32.RDW_ERASE | Win32.RDW_INVALIDATE | Win32.RDW_UPDATENOW);
                MakeNonTopMost();
            }

            // drawing was finished
            m_threadDrawing = false;
        }

        /// <summary>
        /// Mouse was moved while toggle button pushed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Mouse_Move(object sender, MouseEventArgs e)
        {
            PointF point = new PointF(e.X - this.Location.X, e.Y - this.Location.Y);
            m_curvePoints.Add(point);
        }

        /// <summary>
        /// Toggle button was released
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Mouse_ToggleButtonUp(object sender, MouseEventArgs e)
        {
            // erase the trail and stop redrawing
            RefreshAndStopDrawing();          
        }

        /// <summary>
        /// Erase the trail of the gesture and stop drawing
        /// </summary>
        public void RefreshAndStopDrawing()
        {

            // the drawing of gestures was finished
            m_gestureDrawing = false;
            // drawing in thread timer tick should not continue
            m_drawingEnded = true;
            // stop the thread timer
            m_threadTimerDraw.Change(-1, -1);

            if (!m_threadDrawing)
            {
                int lastIndex = m_curvePoints.Count;
                PointF[] points = m_curvePoints.GetRange(m_lastIndex, lastIndex - m_lastIndex).ToArray();
                m_lastIndex = lastIndex - 1;
                if (points.Length > 1)
                    m_gp.DrawLines(m_pen, points);
            }

            List<PointF> allPoints = new List<PointF>(m_curvePoints.ToArray());
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLines(allPoints.ToArray());
            RectangleF rect = path.GetBounds();
            int rectShift = m_penWidth * 6;
            rect.X -= rectShift;
            rect.Y -= rectShift;
            rect.Width += rectShift * 2;
            rect.Height += rectShift * 2;
            //Win32.RECT r = new Win32.RECT(rect);
            m_drawnRegion = new Win32.RECT(rect);
            //Rectangle r = new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
            if (!m_threadDrawing)
            {
                Debug.WriteLine(String.Format("##### InvalidateRect in MOUSE UP left: {0}, top: {1}, right: {2}, bottom: {3} ######",
                    m_drawnRegion.left, m_drawnRegion.top, m_drawnRegion.right, m_drawnRegion.bottom));
                //Win32.InvalidateRect(m_hwndForm, ref m_drawnRegion, true);
                Win32.RedrawWindow(m_hwndForm, ref m_drawnRegion, IntPtr.Zero, Win32.RDW_ERASE | Win32.RDW_INVALIDATE | Win32.RDW_UPDATENOW);                
                MakeNonTopMost();
            }
            else
                m_redrawInTick = true;
            //this.Invalidate(r);
            //Win32.RedrawWindow(m_hwndForm, ref r, IntPtr.Zero, 0x2);
        }



        /// <summary>
        /// Refresh form (thread safe)
        /// </summary>
        public void RefreshForm()
        {
            EmptyHandler del = delegate()
            {
                //this.Refresh();
                this.Invalidate();
            };
            if (this.InvokeRequired)
                this.Invoke(del);
            else
                del();
        }

        /// <summary>
        /// Make form top most
        /// </summary>
        public void MakeTopMost()
        {
            //if (!m_isOnTop)
            {
                Debug.WriteLine("^^^ MakeTopMost ^^^");
                Win32.SetWindowPos(m_hwndForm, Win32.HWND_TOPMOST, 0, 0, this.Width, this.Height,
                    Win32.SWP_SHOWWINDOW | Win32.SWP_NOACTIVATE| Win32.SWP_NOREDRAW | Win32.SWP_NOSIZE | Win32.SWP_NOMOVE);
                m_isOnTop = true;
            }
        }

        ///// <summary>
        ///// Make form top most but do not activate it
        ///// </summary>
        //public void MakeTopMostInactive()
        //{
        //    // always show form to keep the context menu and tooltip above everything

        //    //if (!m_isOnTop)
        //    //{
        //    Debug.WriteLine("^^^ MakeTopMost - No Active ^^^");
        //    Win32.SetWindowPos(m_hwndForm, Win32.HWND_TOPMOST, 0, 0, this.Width, this.Height,
        //                Win32.SWP_SHOWWINDOW | Win32.SWP_NOACTIVATE | Win32.SWP_NOREDRAW);
        //    //    m_isOnTop = true;
        //    //}
        //}

        /// <summary>
        /// Make form bottom show it is not visible anymore
        /// </summary>
        public void MakeNonTopMost()
        {
            // hide only when gesture is not drawn
            //if (m_isOnTop)
            {
                Debug.WriteLine("____ MakeNonTopMost ____");
                Win32.SetWindowPos(m_hwndForm, Win32.HWND_TOPMOST, 0, 0, this.Width, this.Height,
                  Win32.SWP_HIDEWINDOW | Win32.SWP_NOACTIVATE | Win32.SWP_NOREDRAW | Win32.SWP_NOSIZE | Win32.SWP_NOMOVE);
                //Win32.SetWindowPos(m_hwndForm, Win32.HWND_BOTTOM, 0, 0, this.Width, this.Height,
                //    Win32.SWP_SHOWWINDOW | Win32.SWP_NOACTIVATE | Win32.SWP_NOREDRAW | Win32.SWP_NOSIZE);
                m_isOnTop = false;
            }
        }

        /// <summary>
        /// Show form (thread safe)
        /// </summary>
        public void ShowForm()
        {
            EmptyHandler del = delegate()
            {
                Win32.ShowWindow(m_hwndForm, 1);
                //Win32.ShowWindowAsync(m_hwndForm, 1);
            };
            if (this.InvokeRequired)
                this.Invoke(del);
            else
                del();
        }

        /// <summary>
        /// Hide form (thread safe)
        /// </summary>
        public void HideForm()
        {
            EmptyHandler del = delegate()
            {
                Win32.ShowWindow(m_hwndForm, 0);
                //Win32.ShowWindowAsync(m_hwndForm, 0);
            };
            if (this.InvokeRequired)
                this.Invoke(del);
            else
                del();
        }

        /// <summary>
        /// Show tool tip (thread safe)
        /// </summary>
        /// <param name="msg"></param>
        public void ShowToolTip(string msg)
        {
            if (this.InvokeRequired)
            {
                ShowToolTipCallBack showTip = new ShowToolTipCallBack(ShowToolTip);
                this.Invoke(showTip, new object[] { msg });                
            }
            else
            {           
                m_tooltip.Show(msg, (Cursor.Position.X - this.Location.X) + 10, Cursor.Position.Y - this.Location.Y, 2000);             
            }
        }

        /// <summary>
        /// Is handle equal to form
        /// </summary>
        /// <param name="hwnd">Handle to compare</param>
        /// <returns></returns>
        public bool IsTopForm(IntPtr hwnd)
        {
            return hwnd == m_hwndForm;
        }

        /// <summary>
        /// Is handle equal to context menu
        /// </summary>
        /// <param name="hwnd">Handle to compare</param>
        /// <returns></returns>
        public bool IsContextMenu(IntPtr hwnd)
        {
            if (m_hwndCms == hwnd)
                return true;
            else
            {
                foreach (IntPtr handle in m_hwndCmsList)
                    if (handle == hwnd) return true;
                return false;
            }
        }

        public void PerformClickOnContextMenu()
        {
            EmptyHandler del = delegate()
            {
                Point point = cMS_MatchedGestures.PointToClient(Cursor.Position);
                //Debug.WriteLine(string.Format("PerformClickOnContextMenu X: {0} Y: {1}", point.X, point.Y));
                ToolStripItem item = cMS_MatchedGestures.GetItemAt(point);
                
                if (item == null)
                {
                    point.Y -= 1;

                    foreach (ContextMenuStrip contextMenu in m_cmsList)
                    {
                        point = contextMenu.PointToClient(Cursor.Position);
                        item = contextMenu.GetItemAt(point);
                        if (item != null)
                            break;
                    }                    
                }

                if (item != null)
                {
                    ToolStripMenuItem menuItem = (ToolStripMenuItem)item;
                    if (menuItem.DropDownItems.Count == 0)
                        item.PerformClick();
                }
                    
            };
            if (this.InvokeRequired)
                this.Invoke(del);
            else
                del();
        }

        /// <summary>
        /// Show context menu (thread safe)
        /// </summary>
        public void ShowContextMenu()
        {
            EmptyHandler del = delegate()
            {
                //MakeTopMostInactive();
                //this.TopMost = true;
                Win32.SetWindowPos(m_hwndCms, Win32.HWND_TOPMOST, Cursor.Position.X - this.Location.X - 20, Cursor.Position.Y - this.Location.Y,
                    0, 0, Win32.SWP_SHOWWINDOW | Win32.SWP_NOACTIVATE | Win32.SWP_NOREDRAW);
                //cMS_MatchedGestures.Show(Cursor.Position.X - this.Location.X - 20, Cursor.Position.Y - this.Location.Y);                
                //cMS_MatchedGestures.Visible = true;
                m_tooltip.Hide();
                m_contextMenuIsVisible = true;

                //Win32.ShowWindow(m_hwndCms, 1);
            };

            if (this.InvokeRequired)
                this.Invoke(del);
            else
                del();
        }

        /// <summary>
        /// Clear context menu (thread safe)
        /// </summary>
        public void ClearContextMenu()
        {
            EmptyHandler del = delegate()
            {                
                foreach (ContextMenuStrip cms in m_cmsList)
                    cms.Dispose();
                cMS_MatchedGestures.Items.Clear();
                m_hwndCmsList.Clear();
                m_cmsList.Clear();
            };
            if (cMS_MatchedGestures.InvokeRequired)
                this.Invoke(del);
            else
                del();
        }

        /// <summary>
        /// Close context menu assynhronously (thread safe)
        /// </summary>
        public void CloseContextMenuAsync()
        {
            if (!m_contextMenuIsVisible)
                return;
            m_contextMenuIsVisible = false;

            EmptyHandler del = delegate()
            {
                Win32.ShowWindow(m_hwndCms, 0);
                //Win32.ShowWindowAsync(m_hwndCms, 0);

                foreach (IntPtr hwnd in m_hwndCmsList)
                    Win32.ShowWindow(hwnd, 0);
                m_tooltip.Hide();
                //MakeNonTopMost();

                //Win32.ShowWindowAsync(hwnd, 0);
                //cMS_MatchedGestures.Visible = false;
                //cMS_MatchedGestures.Close();
            };
            if (cMS_MatchedGestures.InvokeRequired)
            {
                this.BeginInvoke(del);
            }
            else
                del();
        }

        /// <summary>
        /// Close context menu (thread safe)
        /// </summary>
        public void CloseContextMenu()
        {
            if (!m_contextMenuIsVisible)
                return;
            m_contextMenuIsVisible = false;

            EmptyHandler del = delegate()
            {
                Win32.ShowWindow(m_hwndCms, 0);
                //Win32.ShowWindowAsync(m_hwndCms, 0);

                foreach (IntPtr hwnd in m_hwndCmsList)
                    Win32.ShowWindow(hwnd, 0);
                m_tooltip.Hide();
                //MakeNonTopMost();

                //Win32.ShowWindowAsync(hwnd, 0);
                //cMS_MatchedGestures.Visible = false;
                //cMS_MatchedGestures.Close();
            };
            if (cMS_MatchedGestures.InvokeRequired)
            {
                this.Invoke(del);
            }
            else
                del();
        }

        /// <summary>
        /// Called when visibility of context menu is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cMS_MatchedGestures_VisibleChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ContextMenuStrip contextMenu = (System.Windows.Forms.ContextMenuStrip)sender;
            // when is not visible then close it
            if (!contextMenu.Visible)
            {
                //contextMenu.Close();
                m_contextMenuIsVisible = false;
            }
        }

       

        /// <summary>
        /// Add new item into context menu
        /// </summary>
        /// <param name="item"></param>
        public void AddItemToCms(ToolStripMenuItem item)
        {
            EmptyHandler del = delegate()
            {                
                cMS_MatchedGestures.Items.Add(item);
                item.MouseHover += new EventHandler(cMS_MatchedGestures_MouseHover);                
                if (item.DropDown.Items.Count > 0)
                {
                    m_hwndCmsList.Add(item.DropDown.Handle);
                    m_cmsList.Add((ContextMenuStrip)item.DropDown);
                    // register visibility event so the context menu is closed when it is hidden
                    //item.DropDown.VisibleChanged += new EventHandler(cMS_MatchedGestures_VisibleChanged);
                }
            };
            if (cMS_MatchedGestures.InvokeRequired)
                this.Invoke(del);
            else
                del();
        }

        void cMS_MatchedGestures_MouseHover(object sender, EventArgs e)
        {
            foreach (IntPtr hwnd in m_hwndCmsList)
                Win32.ShowWindow(hwnd, 0);
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;    
            if (menuItem.DropDownItems.Count > 0)
            {
                ToolStrip parent = menuItem.GetCurrentParent();
                Point position = parent.PointToClient(Cursor.Position);
                position.Y -= 2;
                //Debug.WriteLine(string.Format("cMS_MatchedGestures_MouseHover X: {0} Y: {1}", position.X, position.Y));
                int y = position.Y - (position.Y % menuItem.Height);
                y = y == 0 ? 0 : y + 2;
                int x = menuItem.Width;
                Point newPos = parent.PointToScreen(new Point(x, y));
                IntPtr handle = menuItem.DropDown.Handle;
                Win32.SetWindowPos(handle, Win32.HWND_TOPMOST, newPos.X, newPos.Y, 
                    0, 0, Win32.SWP_SHOWWINDOW | Win32.SWP_NOACTIVATE | Win32.SWP_NOREDRAW);
            }
        }

        public static void GetScreenSize(ref Rectangle rect)
        {
            foreach (Screen desktop in Screen.AllScreens)
            {
                rect.X = Math.Min(rect.X, desktop.Bounds.X);
                rect.Y = Math.Min(rect.Y, desktop.Bounds.Y);
                rect.Width = Math.Max(rect.Width, desktop.Bounds.X + desktop.Bounds.Width);
                rect.Height = Math.Max(rect.Height, desktop.Bounds.Y + desktop.Bounds.Height);
            }
        }

        public IntPtr GetNextWindow()
        {
            IntPtr hwnd = Win32.GetTopWindow(IntPtr.Zero);// Win32.GetForegroundWindow();
            IntPtr hwndMain = Win32.GetAncestor(hwnd, Win32.GA_ROOT);
            StringBuilder str = new StringBuilder(256);
            Win32.GetWindowText(hwndMain, str, 256);
            do
            {
                hwnd = Win32.GetWindow(hwnd, Win32.GW_HWNDNEXT);
                hwndMain = Win32.GetAncestor(hwnd, Win32.GA_ROOT);
                //hwnd = Win32.GetTopWindow(IntPtr.Zero);                
                Win32.GetWindowText(hwndMain, str, 256);
                hwnd = hwndMain;
            } while (str.Length == 0 && hwndMain != IntPtr.Zero);
            Win32.GetWindowText(hwnd, str, 256);
            Debug.WriteLine("GetNextWindow: " + str);
            return hwnd;
        }



    }
}