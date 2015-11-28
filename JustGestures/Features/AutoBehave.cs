using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using JustGestures.Properties;

namespace JustGestures.Features
{
    class AutoBehave
    {
        #region Members

        Timer m_threadTimer;
        List<PrgNamePath> m_finalList;
        /// <summary>
        /// Last active window from final list
        /// </summary>
        IntPtr m_activeWnd;
        bool m_isRunning;
        bool m_prevListState;
        bool m_previousState;
        bool m_threadCheck = false;
        /// <summary>
        /// Handle of the top and transparent window
        /// </summary>
        IntPtr m_topWindow;
        /// <summary>
        /// Previous checked window
        /// </summary>
        IntPtr m_prevWindow = IntPtr.Zero;
        /// <summary>
        /// Check counter since the application state was changed
        /// </summary>
        int m_checkCounter = 0;

        public List<PrgNamePath> FinalList 
        { 
            set 
            {
                // reset last active window, because final list was changed
                m_activeWnd = IntPtr.Zero;
                m_checkCounter = 0;
                m_prevWindow = IntPtr.Zero;
                m_finalList = value; 
            } 
        }

        public delegate void DlgDisableMouse(bool prgInList);
        public DlgDisableMouse DisableMouse;

        private void OnDisableMouse(bool disable)
        {
            if (DisableMouse != null)
                DisableMouse(disable);            
        }

        #endregion Members

        public AutoBehave(IntPtr topWnd)
        {
            m_isRunning = false;
            m_topWindow = topWnd;
            m_activeWnd = IntPtr.Zero;
            m_threadTimer = new Timer(m_threadTimer_Tick, null, -1, -1);
        }

        void m_threadTimer_Tick(object state)
        {
            // do not run multiple checks in parallel
            if (m_threadCheck)
                return;

            m_threadCheck = true;

            IntPtr wnd = Win32.GetForegroundWindow();
            if (wnd == IntPtr.Zero || wnd == m_topWindow)
            {
                m_threadCheck = false;
                return;
            }

            m_checkCounter++;
            // check only every second tick or when the previous and current window is different
            if (m_checkCounter < 2 && m_prevWindow == wnd)
            {
                m_threadCheck = false;
                return;
            }

            // reset counter to zero so the state will be passed in second loop
            if (m_prevWindow != wnd)
            {
                m_prevWindow = wnd;
                m_checkCounter = 1;
            }
            
            bool enabled = m_previousState;

            // get state according to default option
            enabled = GetDefaultState(enabled);
            // get state according to fullscreen option
            enabled = GetFullScreenState(enabled, wnd);
            // get state according to black and white list 
            enabled = GetFinalListState(enabled, wnd);           

            if (m_checkCounter >= 2)
            {
                m_checkCounter = 0;
                if (enabled) OnDisableMouse(false);
                else OnDisableMouse(true);
            }

            m_previousState = enabled;
            m_threadCheck = false;
        }

        #region State calculations

        /// <summary>
        /// Get state accroding to default option
        /// </summary>
        /// <returns></returns>
        public bool GetDefaultState(bool currentState)
        {
            bool enabled = currentState;
            switch (Config.User.StateDefault)
            {
                case 1: enabled = true; break;
                case 2: enabled = false; break;
            }
            return enabled;
        }

        /// <summary>
        /// Get state according to fullscreen option
        /// </summary>
        /// <param name="wndHandle"></param>
        /// <returns></returns>
        public bool GetFullScreenState(bool currentState, IntPtr wndHandle)
        {
            bool enabled = currentState;

            System.Drawing.Rectangle screen = System.Windows.Forms.Screen.FromHandle(wndHandle).Bounds;
            Win32.RECT screenSize = new Win32.RECT(screen);

            Win32.WINDOWINFO info = new Win32.WINDOWINFO();
            Win32.GetWindowInfo(wndHandle, ref info);
            //Debug.WriteLine(string.Format("Wnd left: {0}, top: {1}, right: {2}, bottom: {3}",
            //    info.rcWindow.left, info.rcWindow.top, info.rcWindow.right, info.rcWindow.bottom));
            if (info.rcWindow.Equals(screenSize))
            {
                switch (Config.User.StateFullScreen)
                {
                    case 1: enabled = true; break;
                    case 2: enabled = false; break;
                }
            }
            else
            {
                switch (Config.User.StateFullScreen)
                {
                    case 1: enabled = false; break;
                    case 2: enabled = true; break;
                }
            }
            return enabled;
        }

        /// <summary>
        /// Get state according to merged black and white list
        /// </summary>
        /// <param name="wndHandle"></param>
        /// <returns></returns>
        public bool GetFinalListState(bool currentState, IntPtr wndHandle)
        {
            bool enabled = currentState;

            if (m_finalList.Count > 0)
            {
                if (m_activeWnd != wndHandle)
                {
                    string path = TypeOfAction.AppGroupOptions.GetPathFromHwnd(wndHandle);

                    foreach (PrgNamePath prog in m_finalList)
                    {
                        if (path.Equals(prog.Path))
                        {
                            m_activeWnd = wndHandle;
                            enabled = prog.Active;
                            m_prevListState = enabled;
                        }
                    }
                }
                else
                {
                    enabled = m_prevListState;
                }
            }

            return enabled;
        }

        #endregion State calculations

        public void Start()
        {
            if (Config.User.StateDefault != 0 ||
                    Config.User.StateFullScreen != 0 ||
                    Config.User.StateAuto1 != 0 ||
                    Config.User.StateAuto2 != 0)
            {
                m_threadTimer.Change(0, Config.User.CheckWndLoop / 2);
                m_previousState = Config.User.StateDefault == 1 ? true : false;
            }
            else
                m_threadTimer.Change(-1, -1);
            m_isRunning = true;
        }

        public void Stop()
        {
            if (m_isRunning)
            {
                m_threadTimer.Change(-1, -1);
                m_isRunning = false;
            }
        }

        public bool IsRunning
        {
            get { return m_isRunning; }
        }
    }
}
