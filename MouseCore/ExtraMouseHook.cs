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
    /// .NET version of the NCHHITEST Win32 API enum
    /// </summary>
    public enum HitTestCode : int
    {
        HTERROR = -2,
        HTTRANSPARENT = -1,
        HTNOWHERE = 0,
        HTCLIENT = 1,
        HTCAPTION = 2,
        HTSYSMENU = 3,
        HTGROWBOX = 4,
        HTSIZE = HTGROWBOX,
        HTMENU = 5,
        HTHSCROLL = 6,
        HTVSCROLL = 7,
        HTMINBUTTON = 8,
        HTMAXBUTTON = 9,
        HTLEFT = 10,
        HTRIGHT = 11,
        HTTOP = 12,
        HTTOPLEFT = 13,
        HTTOPRIGHT = 14,
        HTBOTTOM = 15,
        HTBOTTOMLEFT = 16,
        HTBOTTOMRIGHT = 17,
        HTBORDER = 18,
        HTREDUCE = HTMINBUTTON,
        HTZOOM = HTMAXBUTTON,
        HTSIZEFIRST = HTLEFT,
        HTSIZELAST = HTBOTTOMRIGHT,
        HTOBJECT = 19,
        HTCLOSE = 20,
        HTHELP = 21,
    }
   
    /// <summary>
    /// A wrapper around the Mouse Hook API
    /// </summary>
    public class ExtraMouseHook : WindowsHook, IDisposable
    {
        #region ThreadHoook

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        public struct MSG
        {
            public IntPtr hwnd;
            public uint message;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public POINT pt;
        }

        [DllImport("user32.dll")]
        public static extern bool TranslateMessage(ref MSG lpMsg);

        [DllImport("user32.dll")]
        public static extern IntPtr DispatchMessage(ref MSG lpMsg);

        [DllImport("user32.dll")]
        public static extern bool GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin,
           uint wMsgFilterMax);

        [DllImport("user32.dll")]
        public static extern void PostQuitMessage(uint nExitCode);

        [DllImport("user32.dll")]
        public static extern bool PostThreadMessage(uint threadId, uint msg, UIntPtr wParam, IntPtr lParam);

        [DllImport("kernel32")]
        public static extern int GetCurrentThreadId();

        private const UInt32 WM_QUIT = 0x0012;

        System.Threading.Thread threadMouse;
        int m_threadId = -1;

        #endregion ThreadHoook

        private enum MouseMode
        {
            None,
            Toggle,
            DoubleBtn,
            WheelBtn
        }

        ExtraMouse m_mouseHoldDownBtn = null;
        ExtraMouse m_mouseExecuteBtn = null;
        MouseMode m_mouseMode = MouseMode.None;
        ExtraMouseBtns m_btnToggle = ExtraMouseBtns.Right;

        bool m_bUsingClassicCurveOnly = true;
        bool m_bUsingClassicCurve = true;
        bool m_bUsingWheelBtn = false;
        bool m_bUsingDoublebtn = false;
        bool m_bShowToolTip = false;
        int m_sensibleZone = 0;        


        bool DEBUG = false;

        #region Mouse Inactivity members
        /// <summary>
        /// Mouse inactivity timer that is used for showing the tooltip while gesture is performed
        /// </summary>
        System.Threading.Timer m_tMouseInactivityTimer;
        /// <summary>
        /// Inidicates that tool tip is visible and timer should not increment the counter
        /// </summary>
        bool m_bToolTipVisible = false;
        /// <summary>
        /// Indicates that tool tip was displayed on the current position
        /// </summary>
        bool m_bToolTipWasVisible = false;
        /// <summary>
        /// Interval when the tool tip should be shown
        /// </summary>
        int m_iToolTipInterval = 0;
        /// <summary>
        /// Mouse inactivity timer
        /// </summary>
        int m_iInactivityCounter = 0;
        /// <summary>
        /// Mouse inactivity timer tick interval
        /// </summary>
        int m_iInactivityTickInterval = 50;
        /// <summary>
        /// Position of the cursor in privious timer's tick
        /// </summary>
        Point m_pPreviousPosition;
        /// <summary>
        /// Current position of the cursor
        /// </summary>
        Point m_pCurrentPosition;
        
        #endregion Mouse Inactivity members

        ///// <summary>
        ///// Wihdow which is located under the mouse, when toggle button is clicked
        ///// </summary>
        //IntPtr m_wndUnderCursor;
        /// <summary>
        /// Amount of simulations calls
        /// </summary>
        int m_iSimulateCount = 0;
        /// <summary>
        /// Timer that is used for enabling the Hold Down button so the other programs in system will be notified
        /// </summary>
        Timer m_cEnableHoldDownBtn;
        /// <summary>
        /// Indicates that mouse hook should be installed 
        /// (but is necessary to uninstal it firstly)
        /// </summary>
        bool m_bShouldInstall = false;
        /// <summary>
        /// Indicates that mouse hook should be uninstalled 
        /// (but is necessary to wait until all mouse buttons are released)
        /// </summary>
        bool m_bShouldUninstall = false;
        /// <summary>
        /// Current LParam of mouse action 
        /// (used for calling the event RaiseMouseHookEvent in m_cEnableModdifier_Tick)
        /// </summary>
        IntPtr m_LParam;
        //WheelAction m_wheelState;
        //WheelAction m_prevWheelState;
        //bool m_bWheelInOutActive = false;
        //Timer m_cWheelInOut;
        /// <summary>
        /// Indicates that mouse hook will be instaled in separate thread
        /// (SHOULD BE ALWAYS TRUE! otherwise mouse freeze while closing JG windows will ocur)
        /// </summary>
        bool m_bThreaded = true;


        bool m_bCancelEvent = false;

        #region Cross Mode members
        /// <summary>
        /// Indicates that mouse hook is in cross mode 
        /// </summary>
        bool m_bCrossMode = false;
        /// <summary>
        /// Position of the cross
        /// </summary>
        Point m_pCrossPosition;
        /// <summary>
        /// Timer used for selecting the programs under the cross 
        /// </summary>
        Timer m_cCrossTimer;
        /// <summary>
        /// Handle of window that was last selected during the cross mode
        /// </summary>
        IntPtr m_hPrevWndUnderCross = IntPtr.Zero;
        /// <summary>
        /// Indicates that mouse hook was installed before the cross selection
        /// (so will be possible to return the state of hook to it's original state)
        /// </summary>
        bool m_bHookInstalledBeforeCross = false;

        #endregion Cross Mode members

        public void StartSimulation()
        {
            m_iSimulateCount++;
        }

        public void StopSimulation()
        {
            m_iSimulateCount--;
        }

        public bool CancelEvent { set { m_bCancelEvent = value; } }

        /// <summary>
        /// Indicates whether the event should be cancelled
        /// (resets cancel state)
        /// </summary>
        /// <returns></returns>
        private bool isEventCancelled()
        {
            bool isCanceled = m_bCancelEvent;
            m_bCancelEvent = false;
            return isCanceled;
        }


        #region Construction
        /// <summary>
        /// This constructor does not AutoInstall istelf.
        /// Client code must call Install in order to begin receiving MouseEvents
        /// </summary>
        public ExtraMouseHook()
            : base(HookType.WH_MOUSE_LL)
        {            
            // we provide our own callback function 
            m_filterFunc = new HookProc(this.MouseProc);

            //m_wndUnderCursor = IntPtr.Zero;

            m_tMouseInactivityTimer = new System.Threading.Timer(m_tMouseInactivityTimer_Tick, null, -1, -1);
            m_cEnableHoldDownBtn = new Timer();
            m_cEnableHoldDownBtn.Tick += new EventHandler(m_cEnableHoldDownBtn_Tick);
            //m_cWheelInOut = new Timer();
            //m_cWheelInOut.Tick += new EventHandler(m_cWheelInOut_Tick);
            //m_cWheelInOut.Interval = 350;
            m_cCrossTimer = new Timer();
            m_cCrossTimer.Tick += new EventHandler(m_cCrossTimer_Tick);
            m_cCrossTimer.Interval = 300;

            //m_wheelState = WheelAction.Stop;
            //m_prevWheelState = WheelAction.Stop;
        }

        public void SetHookValues(MouseButtons toggleBtn, bool classicCurve, bool doubleBtn, bool wheelBtn, 
            int sensibleZone, int timeout, bool showTooltip, int toolTipInt)
        {
            m_cEnableHoldDownBtn.Interval = timeout;
            m_btnToggle = ExtraMouse.MouseBtnToExtraBtn(toggleBtn);
            m_bUsingClassicCurve = classicCurve;
            m_bUsingDoublebtn = doubleBtn;
            m_bUsingWheelBtn = wheelBtn;
            m_sensibleZone = sensibleZone;
            m_bShowToolTip = showTooltip;
            m_iToolTipInterval = toolTipInt;
            m_bUsingClassicCurveOnly = m_bUsingClassicCurve && !m_bUsingDoublebtn && !m_bUsingWheelBtn;
        }

        private bool IsDistanceLessThan(Point a, Point b, int distance)
        {
            int diff_x = Math.Abs(a.X - b.X);
            int diff_y = Math.Abs(a.Y - b.Y);
            if (diff_x < distance && diff_y < distance)
                return true;
            else
                return false;
        }

        public void StartCrossMode()
        {
            m_bCrossMode = true;
            m_bHookInstalledBeforeCross = IsInstalled;
            if (!m_bHookInstalledBeforeCross) this.Install();
            m_cCrossTimer.Start();

        }

        public void StopCrossMode()
        {
            if (m_bCrossMode)
            {
                m_cCrossTimer.Stop();
                m_bCrossMode = false;
                m_hPrevWndUnderCross = IntPtr.Zero;
                if (!m_bHookInstalledBeforeCross) this.Uninstall();
            }
        }

        void m_cCrossTimer_Tick(object sender, EventArgs e)
        {            
            IntPtr wnd = GetWndUnderCursor.FromPoint(m_pCrossPosition);
            if (wnd != m_hPrevWndUnderCross)
                OnAppUnderCrossChanged(wnd);
            m_hPrevWndUnderCross = wnd;
        }

        #region Install and Uninstall

        public void ForceInstall()
        {
            Debug.WriteLineIf(DEBUG, "FORCE MANUAL INSTALL!");
            Install(m_bThreaded);
        }

        public new void Install()
        {
            if (m_bShouldUninstall)
            {
                m_bShouldInstall = true;
                Debug.WriteLineIf(DEBUG, "SHOULD INSTALL value changed to : TRUE");
            }
            else
            {
                Install(m_bThreaded);
                Debug.WriteLineIf(DEBUG, "SUCCESSFULY INSTALLED");
            }
        }

        public void Install(bool threaded)
        {
            if (threaded)
            {
                if (threadMouse != null && threadMouse.IsAlive)
                    threadMouse.Abort();                                        
                threadMouse = new System.Threading.Thread(new System.Threading.ThreadStart(InstallThread));
                threadMouse.Priority = System.Threading.ThreadPriority.AboveNormal;                
                threadMouse.Start();
            }
            else
                base.Install();
            if (!m_bCrossMode)
                OnHookStateChanged(true);
        }

        private void InstallThread()
        {
            m_threadId = GetCurrentThreadId();
            Debug.WriteLine("*** Thread hook STARTED 0x" + m_threadId.ToString("X").ToLower() + " ***");
            MSG msg;
            base.Install();
            while (GetMessage(out msg, IntPtr.Zero, 0, 0) != false)
            {
                TranslateMessage(ref msg);
                DispatchMessage(ref msg);
            }
            Debug.WriteLine("### Thread hook LOOP ENDED 0x" + m_threadId.ToString("X").ToLower() + " ###");
            m_threadId = -1;
        }


        public new bool IsInstalled
        {
            get
            {
                if (!m_bShouldUninstall && !m_bShouldInstall) return base.IsInstalled;
                else if (m_bShouldInstall && m_bShouldUninstall) return base.IsInstalled;
                else if (m_bShouldInstall) return true;
                //else if (m_bShouldUninstall) return false;
                else return false;
            }
        }

        public void ForceUninstall()
        {
            Debug.WriteLineIf(DEBUG, "FORCE MANUAL UNINSTALL!");
            Uninstall(m_bThreaded);
            m_cEnableHoldDownBtn.Stop();
            StopMouseInactivityTimer();
            m_mouseMode = MouseMode.None;
            m_mouseHoldDownBtn = null;
            m_mouseExecuteBtn = null;
            m_bShouldInstall = false;
            m_bShouldUninstall = false;
        }

        /// <summary>
        /// Important for autobehave, when the JG should be disabled but the mouse button is still pressed
        /// </summary>
        public new void Uninstall()
        {
            if (m_mouseHoldDownBtn != null || m_mouseExecuteBtn != null)
            {
                m_bShouldInstall = false;
                m_bShouldUninstall = true;
                Debug.WriteLineIf(DEBUG, "SHOULD UNINSTALL value changed to : TRUE");
            }
            else
            {
                Uninstall(m_bThreaded);
                Debug.WriteLineIf(DEBUG, "SUCCESSFULY UNINSTALLED");
            }
        }


        private bool TryUninstall()
        {
            if (m_mouseHoldDownBtn == null && m_mouseExecuteBtn == null)
            {
                m_mouseMode = MouseMode.None;
                m_bShouldUninstall = false;
                if (!m_bShouldInstall)
                {

                    Uninstall(m_bThreaded);
                    Debug.WriteLineIf(DEBUG, "UNINSTALLED");
                }
                else
                {
                    m_bShouldInstall = false;
                    Debug.WriteLineIf(DEBUG, "SHOULD INSTALL & UNINSTALL => NOTHING CHANGED");
                }
                return true;
            }
            return false;
        }

        
        public void Uninstall(bool threaded)
        {
            if (threaded)
            {
                if (m_threadId != -1)
                    PostThreadMessage((uint)m_threadId, WM_QUIT, UIntPtr.Zero, IntPtr.Zero);
                else
                {
                    if (threadMouse != null && threadMouse.IsAlive)
                        threadMouse.Abort();
                }
                base.Uninstall();                
            }
            else
                base.Uninstall();
            if (!m_bCrossMode)
                OnHookStateChanged(false);
        }
        
        #endregion Install and Uninstall

        /// <summary>
        /// Sends information to the system that mouse button is being pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_cEnableHoldDownBtn_Tick(object sender, EventArgs e)
        {
            //OnHookStateChanged(false);
            m_cEnableHoldDownBtn.Stop();
            Debug.WriteLineIf(DEBUG, "EnableTrigger_Tick Simulate Trigger button action");
            //inform engine that some button was pushed so the context menu of recognized gestures might be hidden
            ExtraMouse tempMouse = new ExtraMouse(m_mouseHoldDownBtn);            
            RaiseMouseEvent(tempMouse);// new ExtraMouse(ExtraMouseBtns.None, ExtraMouseActions.Down, m_mouseHoldDownBtn.Position));
            if (!isEventCancelled())
                SimulateMouseAction(tempMouse); // action down
            m_mouseHoldDownBtn = null;
        }

        private void RestartMouseInactivityTimer()
        {
            StopMouseInactivityTimer();
            StartMouseInactivityTimer();
        }

        private void StartMouseInactivityTimer()
        {
            if (m_bShowToolTip)
            {
                if (m_mouseMode == MouseMode.Toggle)
                {
                    /**
                     * during toggle mode the timer periodicaly checks the position of cursor
                     * and if counter is incremented untill the tool tip interval is reached
                     * */
                    m_iInactivityCounter = 0;
                    m_tMouseInactivityTimer.Change(m_iInactivityTickInterval, m_iInactivityTickInterval);
                }
                else if (m_mouseMode == MouseMode.DoubleBtn)
                {
                    // timer for double button mode is directly set to the specific tool tip interval
                    m_tMouseInactivityTimer.Change(m_iToolTipInterval, m_iToolTipInterval);
                }
            }
        }

        private void StopMouseInactivityTimer()
        {
            if (m_bShowToolTip)
                m_tMouseInactivityTimer.Change(-1, -1);
        }

        private void m_tMouseInactivityTimer_Tick(object state)
        {
            switch (m_mouseMode)
            {
                case MouseMode.Toggle:
                    // OnMouseStoped is still being processed 
                    if (m_bToolTipVisible)
                        return;
                    // mouse cursor was moved therefore do nothing 
                    if (m_pCurrentPosition != m_pPreviousPosition)
                    {
                        m_pPreviousPosition = m_pCurrentPosition;
                        m_iInactivityCounter = 0;
                        m_bToolTipWasVisible = false;
                        return;
                    }
                    // if tool tip was already displayed do not show it again
                    if (m_pCurrentPosition == m_pPreviousPosition && m_bToolTipWasVisible)
                        return;

                    // increment the inactivity counter
                    m_iInactivityCounter += m_iInactivityTickInterval;

                    int delta = m_iInactivityTickInterval / 2;
                    // if counter is near to tool tip interval
                    if (m_iToolTipInterval - delta < m_iInactivityCounter && m_iInactivityCounter <= m_iToolTipInterval + delta)
                    {
                        m_bToolTipVisible = true;

                        Debug.WriteLineIf(DEBUG, "ToolTip_Tick Raise MouseStoped => Show Tool tip ");

                        // raise event that mouse has been stoped
                        OnMouseStoped(new EventArgs());
                        // do not stop the inactivity timer, as the mouse might be moved and stoped again

                        m_bToolTipVisible = false;
                        m_bToolTipWasVisible = true;
                        // reset counter
                        m_iInactivityCounter = 0;
                    }
                    break;
                case MouseMode.DoubleBtn:
                    Debug.WriteLineIf(DEBUG, "ToolTip_Tick Raise MouseStoped => Show Tool tip ");
                    // stop mouse inactivity timer because it should be shown only once
                    StopMouseInactivityTimer();
                    // raise event to show tool tip about double button gesture
                    OnDoubleBtnAction(m_mouseHoldDownBtn.ToMouseEvent(), m_mouseExecuteBtn.ToMouseEvent(), true);
                    break;
            }
        }
       
        //void m_cWheelInOut_Tick(object sender, EventArgs e)
        //{
        //    m_cWheelInOut.Stop();
        //    m_bWheelInOutActive = true;
        //    //Debug.WriteLineIf(DEBUG, "Raise event MouseBrowseWheel " + m_wheelState.ToString());
        //    //OnMouseBrowseWheel(m_wheelState);
        //}


        //public IntPtr ActiveWindow
        //{
        //    get { return m_wndUnderCursor; }
        //}

        #endregion

        #region Disposal

        ~ExtraMouseHook()
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

        #endregion

        #region Events

        public delegate void DlgHookStateChanged(bool active);
        public DlgHookStateChanged HookStateChanged;
        /// <summary>
        /// Invokes event about changed state (enable/disable) of the mouse
        /// </summary>
        /// <param name="active">is mouse active</param>
        protected void OnHookStateChanged(bool active)
        {
            if (HookStateChanged != null)
                HookStateChanged(active);
        }

        public delegate void DlgAppUnderCrossChanged(IntPtr hwnd);

        public DlgAppUnderCrossChanged AppUnderCrossChanged;
        protected void OnAppUnderCrossChanged(IntPtr hwnd)
        {
            if (AppUnderCrossChanged != null)
                AppUnderCrossChanged(hwnd);
        }

        public delegate void DlgEmptyDelegate();

        public DlgEmptyDelegate CrossLeftMouseUp;
        protected void OnCrossLeftMouseUp()
        {
            if (CrossLeftMouseUp != null)
                CrossLeftMouseUp();
        }
        
        public event MouseEventHandler MouseDown;
        protected void OnMouseDown(MouseEventArgs e)
        {
            if (MouseDown != null)
                MouseDown(this, e);
        }

        public event MouseEventHandler MouseDownGesture;
        protected void OnMouseDownGesture(MouseEventArgs e)
        {
            if (MouseDownGesture != null)
                MouseDownGesture(this, e);
        }

        public event MouseEventHandler MouseUp;
        protected void OnMouseUp(MouseEventArgs e)
        {
            if (MouseUp != null)
                MouseUp(this, e);
        }

        public event MouseEventHandler MouseMove;
        protected void OnMouseMove(MouseEventArgs e)
        {
            if (MouseMove != null)
                MouseMove(this, e);
        }       

        
        public event MouseEventHandler MouseClick;
        protected void OnMouseClick(MouseEventArgs e)
        {
            if (MouseClick != null)
                MouseClick(this, e);
        }

        public event MouseEventHandler MouseDoubleClick;
        protected void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (MouseDoubleClick != null)
                MouseDoubleClick(this, e);
        }

        public event MouseEventHandler MouseWheel;
        protected void OnMouseWheel(MouseEventArgs e)
        {
            if (MouseWheel != null)
                MouseWheel(this, e);
        }

        public delegate void DlgDoubleBtnAction(MouseEventArgs trigger, MouseEventArgs modifier, bool notifyOnly);
        public DlgDoubleBtnAction DoubleBtnAction;

        protected void OnDoubleBtnAction(MouseEventArgs trigger, MouseEventArgs modifier, bool notifyOnly)
        {
            if (DoubleBtnAction != null)
                DoubleBtnAction(trigger, modifier, notifyOnly);
        }

        public delegate void DlgWheelBtnAction(MouseEventArgs trigger, ExtraMouseActions wheelAction);
        public DlgWheelBtnAction WheelBtnAction;

        protected void OnWheelBtnAction(MouseEventArgs trigger, ExtraMouseActions wheelAction)
        {
            if (WheelBtnAction != null)
                WheelBtnAction(trigger, wheelAction);
        }

        //public DlgMouseWheelMove MouseBrowseWheel;
        //protected void OnMouseBrowseWheel(WheelAction wheelAction)
        //{
        //    if (MouseBrowseWheel != null)
        //        MouseBrowseWheel(wheelAction);
        //}

        public event EventHandler MouseStoped;
        protected void OnMouseStoped(EventArgs e)
        {
            if (MouseStoped != null)
                MouseStoped(this, e);
        }
        #endregion


        #region Mouse Hook specific code

        private bool HandleEvent(int code, IntPtr wParam, IntPtr lParam)
        {
            //Debug.WriteLineIf(DEBUG, string.Format("Code: {0} wParam: {1} lParam: {2}", code, wParam, lParam));
            ExtraMouse mouseEvent = new ExtraMouse(wParam, lParam);
            switch (mouseEvent.Action)
            {
                case ExtraMouseActions.Down:
                    Debug.WriteLineIf(DEBUG, string.Format("{0} button _DOWN_ X: {1} Y: {2}", mouseEvent.Button, mouseEvent.Position.X, mouseEvent.Position.Y));
                    if (m_mouseMode == MouseMode.Toggle || m_mouseMode == MouseMode.WheelBtn) return false;
                    if (m_bUsingClassicCurveOnly && mouseEvent.Button != m_btnToggle) break; //raise event

                    if (m_mouseHoldDownBtn == null)
                    {
                        m_mouseHoldDownBtn = mouseEvent;
                        m_cEnableHoldDownBtn.Start();
                        return true;
                    }
                    else
                    {
                        if (!m_bUsingDoublebtn) return false;
                        //m_wndUnderCursor = GetWndUnderCursor.FromPoint(mouseEvent.Position);
                        m_mouseMode = MouseMode.DoubleBtn;
                        
                        // second button has not been pushed yet
                        if (m_mouseExecuteBtn == null)
                        {
                            m_cEnableHoldDownBtn.Stop();
                            m_mouseExecuteBtn = mouseEvent;
                            Debug.WriteLineIf(DEBUG, "Start Timer OnDoubleBtnAction Notify Only");
                            RestartMouseInactivityTimer();                    
                            //OnDoubleBtnAction(m_mouseTrigger.ToMouseEvent(), m_mouseModifier.ToMouseEvent(), true);
                            return true;
                        }
                        // second button was already released, use it again
                        else if (m_mouseExecuteBtn.Action == ExtraMouseActions.Up)
                        {
                            m_mouseExecuteBtn = mouseEvent;
                            Debug.WriteLineIf(DEBUG, "Start Timer OnDoubleBtnAction Notify Only");
                            RestartMouseInactivityTimer();
                            //OnDoubleBtnAction(m_mouseTrigger.ToMouseEvent(), m_mouseModifier.ToMouseEvent(), true);
                            return true;
                        }
                    }
                    break;
                case ExtraMouseActions.Up:
                    Debug.WriteLineIf(DEBUG, string.Format("{0} button ^UP^ X: {1} Y: {2}", mouseEvent.Button, mouseEvent.Position.X, mouseEvent.Position.Y));
                    
                    if (m_mouseMode == MouseMode.Toggle && m_mouseHoldDownBtn.Button == mouseEvent.Button)
                    {
                        m_mouseHoldDownBtn = null;
                        m_mouseMode = MouseMode.None;
                        StopMouseInactivityTimer();
                        //SimulateHiddenAction(ExtraMouseActions.Up, mouseEvent.Position);
                        RaiseMouseEvent(mouseEvent);
                        return true;
                    }
                    else if (m_mouseMode == MouseMode.WheelBtn && m_mouseHoldDownBtn.Button == mouseEvent.Button)
                    {
                        Debug.WriteLineIf(DEBUG, "Raise OnWheelBtnAction - Hold-down button released");
                        OnWheelBtnAction(m_mouseHoldDownBtn.ToMouseEvent(), ExtraMouseActions.None);
                        m_mouseHoldDownBtn = null;
                        m_mouseMode = MouseMode.None;                        
                        return true;
                    }

                    //raise event...
                    if (m_mouseHoldDownBtn == null)
                    {
                        RaiseMouseEvent(mouseEvent);
                        if (isEventCancelled())
                            return true;
                        else        
                            return false;
                    }

                    if (m_mouseHoldDownBtn.Button == mouseEvent.Button)
                    {
                        StopMouseInactivityTimer();
                        m_cEnableHoldDownBtn.Stop();
                        
                        if (m_mouseExecuteBtn == null)
                        {
                            // mouse click action is raised
                            lock (this)
                            {
                                //RaiseMouseEvent(m_mouseTrigger); //action down
                                //SimulateMouseAction(m_mouseTrigger);
                                //RaiseMouseEvent(mouseEvent); //action up
                                //SimulateMouseAction(mouseEvent.Button, ExtraMouseActions.Click);

                                RaiseMouseEvent(new ExtraMouse(m_mouseHoldDownBtn.Button, ExtraMouseActions.Click, m_mouseHoldDownBtn.Position));
                                if (!isEventCancelled())
                                {
                                    SimulateMouseAction(mouseEvent.Button, ExtraMouseActions.Down);
                                    System.Threading.Thread.Sleep(10);
                                    SimulateMouseAction(mouseEvent.Button, ExtraMouseActions.Up);
                                }
                            }
                            m_mouseHoldDownBtn = null;
                            m_mouseMode = MouseMode.None;
                            return true;
                        }
                        else if (m_mouseExecuteBtn.Action == ExtraMouseActions.Down)
                        {
                            Debug.WriteLineIf(DEBUG, "Trigger ^UP^ & Modifier _DOWN_ => Swap MODIFIER & TRIGGER");
                            m_mouseHoldDownBtn = m_mouseExecuteBtn;
                            m_mouseExecuteBtn = mouseEvent;
                            //m_mouseMode = MouseMode.DoubleBtn;
                            return true;
                        }
                        else
                        {
                            m_mouseExecuteBtn = null;
                            m_mouseHoldDownBtn = null;
                            m_mouseMode = MouseMode.None;
                            return true;
                        }
                    }
                    else if (m_mouseExecuteBtn != null)
                    {
                        if (m_mouseExecuteBtn.Button == mouseEvent.Button)
                        {
                            StopMouseInactivityTimer();

                            m_mouseExecuteBtn = mouseEvent;
                            if (m_mouseHoldDownBtn != null)
                            {                                
                                //m_wndUnderCursor = GetWndUnderCursor.FromPoint(mouseEvent.Position);

                                Debug.WriteLineIf(DEBUG, "Raise OnDoubleBtnAction");
                                OnDoubleBtnAction(m_mouseHoldDownBtn.ToMouseEvent(), m_mouseExecuteBtn.ToMouseEvent(), false);
                                //m_mouseModifier = null;
                                return true;
                            }
                        }
                    }
                    break;
                case ExtraMouseActions.WheelDown:
                case ExtraMouseActions.WheelUp:
                    if (!m_bUsingWheelBtn) return false;

                    if (m_mouseHoldDownBtn != null && m_mouseMode == MouseMode.None)
                    {
                        m_mouseMode = MouseMode.WheelBtn;
                        m_cEnableHoldDownBtn.Stop();
                        //m_wndUnderCursor = GetWndUnderCursor.FromPoint(m_mouseTrigger.Position);
                        Debug.WriteLineIf(DEBUG, "Raise OnWheelBtnAction");
                        OnWheelBtnAction(m_mouseHoldDownBtn.ToMouseEvent(), mouseEvent.Action);
                        return true;
                    }
                    else if (m_mouseMode == MouseMode.WheelBtn)
                    {
                        Debug.WriteLineIf(DEBUG, "Raise OnWheelBtnAction");
                        OnWheelBtnAction(m_mouseHoldDownBtn.ToMouseEvent(), mouseEvent.Action);
                        return true;
                    }
                    break;
                case ExtraMouseActions.Move:
                    if (m_mouseHoldDownBtn != null && m_mouseMode == MouseMode.None)
                    {
                        if (IsDistanceLessThan(m_mouseHoldDownBtn.Position, mouseEvent.Position, m_sensibleZone))
                            return false;

                        if (m_mouseHoldDownBtn.Button == m_btnToggle && m_bUsingClassicCurve)
                        {
                            m_mouseMode = MouseMode.Toggle;
                            m_cEnableHoldDownBtn.Stop();
                            Debug.WriteLineIf(DEBUG, "Mouse start moving & ToggleBtn _DOWN_ => RaiseMouseHookEvent()");
                            OnMouseDownGesture(m_mouseHoldDownBtn.ToMouseEvent());
                            StartMouseInactivityTimer();
                            //m_wndUnderCursor = GetWndUnderCursor.FromPoint(m_mouseTrigger.Position);
                            //SimulateHiddenAction(ExtraMouseActions.Down, m_mouseTrigger.Position);
                            //return true;
                        }
                        else
                        {
                            m_mouseMode = MouseMode.None;
                            m_cEnableHoldDownBtn.Stop();
                            ExtraMouse mouseTemp = new ExtraMouse(m_mouseHoldDownBtn); //backup mouse trigger so it can be changed during this process
                            RaiseMouseEvent(mouseTemp);
                            if (!isEventCancelled())                                
                                SimulateMouseAction(mouseTemp);                            
                            m_mouseHoldDownBtn = null;
                        }
                    }
                    else if (m_mouseMode == MouseMode.Toggle)
                    {
                        m_pCurrentPosition = mouseEvent.Position;                      
                    }
                    break;
            }

            if (code == Win32.HC_ACTION)
                RaiseMouseEvent(mouseEvent);

            return false;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="button"></param>
        /// <returns>True if button was released & event handled</returns>
        private bool CheckAndRelease(ExtraMouse button, IntPtr wParam)
        {
            if (button != null)
            {
                if (button.Action == ExtraMouseActions.Down)
                {
                    if (wParam.ToInt32() == ExtraMouse.GetWmValue(button.Button, ExtraMouseActions.Up))
                    {
                        Debug.WriteLineIf(DEBUG, string.Format("Button {0} Uninstalled", button.Button));
                        //button = null;                        
                        return true;
                    }
                }
                else
                {
                    Debug.WriteLineIf(DEBUG, string.Format("Button {0} Uninstalled", button.Button));
                    //button = null;
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// The callback passed to SetWindowsHookEx
        /// </summary>
        /// <param name="code">The action code passed from the hook chain</param>
        /// <param name="wParam">The mouse message being received</param>
        /// <param name="lParam">Message paramters</param>
        /// <returns>return code to hook chain</returns>        
        protected int MouseProc(int code, IntPtr wParam, IntPtr lParam)
        {
            // by convention a code < 0 means skip

            if (code < 0)
                return CallNextHookEx(m_hhook, code, wParam, lParam);
            // Yield to the next hook in the chain

            // used in m_cEnableModdifier_Tick
            m_LParam = lParam;

            if (m_iSimulateCount == 0 && !m_bShouldUninstall && !m_bCrossMode)
            {
                bool handled = HandleEvent(code, wParam, lParam);
                if (handled) return 1;
            }
                
            if (m_bCrossMode)
            {
                Win32.MouseLLHookStruct mouse = (Win32.MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(Win32.MouseLLHookStruct));
                m_pCrossPosition = new Point(mouse.pt.x, mouse.pt.y);
                if (wParam.ToInt32() == Win32.WM_LBUTTONUP)
                {
                    OnCrossLeftMouseUp();
                    //StopCrossMode();
                }
            }

            // if uninstall is called and mouse button is still pressed, then wait until it is released
            if (m_bShouldUninstall)
            {
                if (m_mouseHoldDownBtn != null)
                {
                    if (CheckAndRelease(m_mouseHoldDownBtn, wParam))
                    {
                        if (m_mouseMode == MouseMode.WheelBtn)
                        {
                            //necessary to release some pushed buttons...
                            Debug.WriteLineIf(DEBUG, "Raise OnMouseTabWheel");
                            OnWheelBtnAction(m_mouseHoldDownBtn.ToMouseEvent(), ExtraMouseActions.None);
                        }
                        m_mouseHoldDownBtn = null;
                        TryUninstall();
                        return 1;
                    }
                }
                if (m_mouseExecuteBtn != null)
                {
                    if (CheckAndRelease(m_mouseExecuteBtn, wParam))
                    {
                        m_mouseExecuteBtn = null;
                        TryUninstall();
                        return 1;
                    }
                }
            }

            // pass information about mouse activity to other programs in a the system for further processing 
            return CallNextHookEx(m_hhook, code, wParam, lParam);
        }

        //public void SimulateHiddenAction(ExtraMouseActions action, Point pos)
        //{
        //    m_bSimimulate = true;
        //    ExtraMouse.SimateHiddenClick(action, pos);
        //    m_bSimimulate = false;
        //}

        public void SimulateMouseAction(ExtraMouseBtns button, ExtraMouseActions action)
        {
            StartSimulation();
            Debug.WriteLineIf(DEBUG, string.Format("Simulating button: {0}, action: {1}", button, action));
            ExtraMouse.SimulateMouseAction(button, action, m_LParam);
            StopSimulation();
        }
        
        public void SimulateMouseAction(ExtraMouse mouse)
        {
            StartSimulation();
            Debug.WriteLineIf(DEBUG, string.Format("Simulating button: {0}, action: {1} position: ({2},{3})", mouse.Button, mouse.Action, mouse.Position.X, mouse.Position.Y));            
            mouse.SimulateMouseAction();
            StopSimulation();
        }

        private void RaiseMouseEvent(ExtraMouse mouse)
        {
            if (mouse.Action != ExtraMouseActions.Move)
                Debug.WriteLineIf(DEBUG, string.Format("RaiseMouseEvent button: {0}, action {1}", mouse.Button, mouse.Action));
            MouseEventArgs args = mouse.ToMouseEvent();
            if (mouse.Button == ExtraMouseBtns.Wheel)
            {
                OnMouseWheel(args);
            }
            else
            {
                switch (mouse.Action)
                {
                    case ExtraMouseActions.Down:
                        OnMouseDown(args);
                        break;
                    case ExtraMouseActions.Click:
                        OnMouseClick(args);
                        break;
                    case ExtraMouseActions.Up:
                        OnMouseUp(args);
                        break;                    
                    case ExtraMouseActions.DoubleClick:
                        OnMouseDoubleClick(args);
                        break;
                    case ExtraMouseActions.Move:
                        OnMouseMove(args);
                        break;
                }
            }
        }

        
        #endregion

    }
}
