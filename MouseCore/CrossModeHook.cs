using System;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;

namespace MouseCore
{
    /// <summary>
    /// Gets windows under cursor in specific interval, when event LeftMouseUp is called
    /// it automaticaly unistall itself
    /// </summary>
    public class CrossModeHook : WindowsHook, IDisposable
    {        
        IntPtr prevWnd;
        Timer timer;
        Point point;

        public delegate void DlgAppUnderCrossChanged(IntPtr hwnd);

        public DlgAppUnderCrossChanged AppUnderCrossChanged;
        protected void OnAppUnderCrossChanged(IntPtr hwnd)
        {
            if (AppUnderCrossChanged != null)
                AppUnderCrossChanged(hwnd);
        }

        public delegate void DlgEmptyDelegate();

        public DlgEmptyDelegate LeftMouseUp;
        protected void OnLeftMouseUp()
        {
            if (LeftMouseUp != null)
                LeftMouseUp();
        }

        public CrossModeHook()
            : base(HookType.WH_MOUSE_LL)
        {
            m_filterFunc = new HookProc(this.MouseProc);
            timer = new Timer();
            timer.Interval = 250;
            timer.Tick += new EventHandler(timer_Tick);
        }

        void timer_Tick(object sender, EventArgs e)
        {   
            IntPtr wnd = GetWndUnderCursor.FromPoint(point);
            Debug.WriteLine(string.Format("CrossModelHook - Point({0},{1}) Hwnd: {2} PrevHwnd: {3} ", point.X, point.Y, wnd, prevWnd));
            if (wnd != prevWnd)
                OnAppUnderCrossChanged(wnd);
            prevWnd = wnd;
        }
  
        public new void Uninstall()
        {
            timer.Stop();
            base.Uninstall();
            prevWnd = IntPtr.Zero;
        }

        public new void Install()
        {
            base.Install();
            timer.Start();
        }

        ~CrossModeHook()
        {
            Dispose(false);
        }

        protected void Dispose(bool disposing)
        {
            if (IsInstalled)
                Uninstall();

            if (disposing)
                GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        

        protected int MouseProc(int code, IntPtr wParam, IntPtr lParam)
        {
            // by convention a code < 0 means skip
            
            if (code < 0)
                return CallNextHookEx(m_hhook, code, wParam, lParam);
            // Yield to the next hook in the chain
            //LParam = lParam;
            Debug.Write(code.ToString() + " " + wParam.ToString() + " " + lParam.ToString());
            Win32.MouseLLHookStruct mouse = (Win32.MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(Win32.MouseLLHookStruct));
            point = new Point(mouse.pt.x, mouse.pt.y);


            if (wParam.ToInt32() == Win32.WM_LBUTTONUP)
            {
                OnLeftMouseUp();
                this.Uninstall();
            }

            //informacia o mysi sa posiela do dalsich programov (systemu) na spracovanie
            return CallNextHookEx(m_hhook, code, wParam, lParam);
        }
    }
}
