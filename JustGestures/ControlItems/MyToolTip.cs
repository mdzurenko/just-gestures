using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Drawing;


namespace JustGestures.ControlItems
{
    class MyToolTip: IDisposable
    {
        #region Declaration & Win32 import

        private NativeWindow m_tool = null;
		private TOOLINFO ti;
        private System.Threading.Timer m_threadTimerHide;

		private int m_maxWidth = 350;
		private string m_text = "Balloon Tooltip Control Display Message";
		private string m_title = "Balloon Tooltip Message";
        private ToolTipIcon m_titleIcon = ToolTipIcon.None;
		private bool m_absPosn = false;
		private bool m_centerStem = false;
        private int m_posX = 0;
        private int m_posY = 0;

		private const string TOOLTIPS_CLASS = "tooltips_class32";
		private const int WS_POPUP = unchecked((int)0x80000000);
		private const int WM_USER = 0x0400;
		private readonly IntPtr HWND_TOPMOST = new IntPtr(-1);		
		private const int SWP_NOSIZE = 0x0001;
		private const int SWP_NOMOVE = 0x0002;
		private const int SWP_NOACTIVATE = 0x0010;
		private const int SWP_NOZORDER = 0x0004;

		
		[DllImport("User32", SetLastError=true)]
		private static extern int SetWindowPos(
			IntPtr hWnd,
			IntPtr hWndInsertAfter,
			int X,
			int Y,
			int cx,
			int cy,
			int uFlags);

		[DllImport("User32", SetLastError=true)]
		private static extern int GetClientRect(
			IntPtr hWnd,
			ref RECT lpRect);

		[DllImport("User32", SetLastError=true)]
		private static extern int ClientToScreen(
			IntPtr hWnd,
			ref RECT lpRect);

		[DllImport("User32", SetLastError=true)]
		private static extern int SendMessage(
			IntPtr hWnd,
			int Msg,
			int wParam,
			IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessageA", SetLastError = true)]
        private static extern int SendMessageDelay(
            IntPtr hWnd, 
            int message, 
            int wParam, 
            int lParam);

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

		[StructLayout(LayoutKind.Sequential)]
		private struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
		}

		private const int TTS_ALWAYSTIP = 0x01;
		private const int TTS_NOPREFIX = 0x02;
		private const int TTS_BALLOON = 0x40;
		private const int TTS_CLOSE = 0x80;
        
        private const int TTM_SETDELAYTIME = WM_USER + 3;
		private const int TTM_TRACKPOSITION = WM_USER + 18;
		private const int TTM_SETMAXTIPWIDTH = WM_USER + 24;
		private const int TTM_TRACKACTIVATE = WM_USER + 17;
		private const int TTM_ADDTOOL = WM_USER + 50;
		private const int TTM_SETTITLE = WM_USER + 33;
        
        private const int TTM_GETBUBBLESIZE = WM_USER + 30;

		private const int TTF_IDISHWND = 0x0001;
		private const int TTF_SUBCLASS = 0x0010;
		private const int TTF_TRACK = 0x0020;
		private const int TTF_ABSOLUTE = 0x0080;
		private const int TTF_TRANSPARENT = 0x0100;
		private const int TTF_CENTERTIP = 0x0002;
		private const int TTF_PARSELINKS = 0x1000;
        
        private const int TTDT_AUTOPOP = 2;
        private const int TTDT_INITIAL = 3;

		[StructLayout(LayoutKind.Sequential)]
		private struct TOOLINFO
		{
			public int cbSize;
			public int uFlags;
			public IntPtr hwnd;
			public IntPtr uId;
			public RECT rect;
			public IntPtr hinst;
			[MarshalAs(UnmanagedType.LPTStr)] 
			public string lpszText;
			public uint lParam;
		}

        public enum BalloonPosition
        {
            /// <summary>
            /// Positions using the exact co-ordinates.
            /// So if the co-ordinates are outside the screen,
            /// tip wont be shown.
            /// </summary>
            Absolute,

            /// <summary>
            /// Positions using the co-ordinates as a reference.
            /// Regardless of the co-ordinates, the tip will 
            /// always be shown on the screen.
            /// </summary>
            Track
        }

        #endregion Declaration & Win32 import

		/// <summary>
		/// Creates a new instance of the MessageBalloon.
		/// </summary>
		public MyToolTip()
		{
            m_tool = new NativeWindow();
            m_threadTimerHide = new System.Threading.Timer(m_threadTimerHide_Tick, null, -1, -1);
		}
       
        ~MyToolTip()
		{
			Dispose(false);
		}

		private bool disposed = false;
		public void Dispose()
		{
			Dispose(true);
			// Take yourself off the Finalization queue 
			// to prevent finalization code for this object
			// from executing a second time.
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if(!this.disposed)
			{
				if(disposing)
				{
					// release managed resources if any
				}
				
				// release unmanaged resource
				Hide();

				// Note that this is not thread safe.
				// Another thread could start disposing the object
				// after the managed resources are disposed,
				// but before the disposed flag is set to true.
				// If thread safety is necessary, it must be
				// implemented by the client.
			}
			disposed = true;
		}

		private void CreateTool()
		{
            //System.Diagnostics.Debug.Assert(
            //    m_parent.Handle!=IntPtr.Zero, 
            //    "parent hwnd is null", "SetToolTip");

			CreateParams cp = new CreateParams();
			cp.ClassName = TOOLTIPS_CLASS;
			cp.Style = 
				WS_POPUP | 
				TTS_NOPREFIX |	
				TTS_ALWAYSTIP |
				TTS_CLOSE;         

			// create the tool
			m_tool.CreateHandle(cp);

			// create and fill in the tool tip info
			ti = new TOOLINFO();
			ti.cbSize = Marshal.SizeOf(ti);

			ti.uFlags = TTF_TRACK |
				TTF_IDISHWND |
				TTF_TRANSPARENT | 
				TTF_SUBCLASS |
				TTF_PARSELINKS;

			// absolute is used tooltip maynot be shown 
			// if coords exceed the corners of the screen
            //if(m_absPosn)
            //{
            //    ti.uFlags |= //TTF_ABSOLUTE;
            //}

			if(m_centerStem)
			{
				ti.uFlags |= TTF_CENTERTIP;
			}
            
            ti.uId = m_tool.Handle;
			ti.lpszText = m_text;
            //ti.hwnd = m_parent.Handle;

            //GetClientRect(m_parent.Handle, ref ti.rect);
            //ClientToScreen(m_parent.Handle, ref ti.rect);

			// make sure we make it the top level window
			SetWindowPos(
				m_tool.Handle, 
				HWND_TOPMOST, 
				0, 0, 0, 0,
				SWP_NOACTIVATE | 
				SWP_NOMOVE | 
				SWP_NOSIZE);

            //int scopeInitInfoSize = Marshal.SizeOf(typeof(DSOP_SCOPE_INIT_INFO));
            //int offset = scopeInitInfoSize;
            //IntPtr scopeInitInfo = (IntPtr)(refScopeInitInfo.ToInt64() + offset); 


			// add the tool tip
			IntPtr ptrStruct = Marshal.AllocHGlobal(Marshal.SizeOf(ti));
			Marshal.StructureToPtr(ti, ptrStruct, false);

			SendMessage(
				m_tool.Handle, TTM_ADDTOOL, 0, ptrStruct);

			ti = (TOOLINFO)Marshal.PtrToStructure(ptrStruct, 
				typeof(TOOLINFO));

			SendMessage(
				m_tool.Handle, TTM_SETMAXTIPWIDTH, 
				0, new IntPtr(m_maxWidth));

            //IntPtr ptrTitle = Marshal.StringToHGlobalAuto(m_title);

            //SendMessage(
            //    m_tool.Handle, TTM_SETTITLE, 
            //    (int)m_titleIcon, ptrTitle);

			SetBalloonPosition(ti.rect);

			Marshal.FreeHGlobal(ptrStruct);
			//Marshal.FreeHGlobal(ptrTitle);
		}

        private void SetBalloonPosition(RECT rect)
        {
            int x = m_posX;
            int y = m_posY;
            
            // get the size of screen on which is tooltip located
            Rectangle screen = Screen.FromPoint(new Point(m_posX, m_posY)).Bounds;

            SizeF toolTipSize = MeasureToolTipSize();

            // y cannot be the top pixel on screen otherwise the tooltip will not be properly shown
            y = Math.Max(screen.Top + 1, y);
                        
            // calcuate if the tooltip can fit in screen 
            float yDiff = screen.Bottom - (y + toolTipSize.Height);
            if (yDiff < 0)
                y += (int)yDiff;

            float xDiff = screen.Right - (x + toolTipSize.Width);
            if (xDiff < 0)
                x += (int)xDiff;

            int pt = MAKELONG(x, y);
            IntPtr ptr = new IntPtr(pt);

            SendMessage(
                m_tool.Handle, TTM_TRACKPOSITION,
                0, ptr);

        }

        private SizeF MeasureToolTipSize()
        {
            #region TEST ONLY: when meassuring the text in tooltip
            //IntPtr ptrStruct = Marshal.AllocHGlobal(Marshal.SizeOf(ti));
            //Marshal.StructureToPtr(ti, ptrStruct, false);

            //SendMessage(m_tool.Handle, TTM_TRACKACTIVATE, -1, ptrStruct);
            //int dwBS = SendMessage(m_tool.Handle, TTM_GETBUBBLESIZE, 0, ptrStruct);
            ////SendMessage(m_tool.Handle, TTM_TRACKACTIVATE, 0, ptrStruct);

            //int origToolTipHeight = Win32.HIWORD(dwBS);
            //int toolTipWidth = Win32.LOWORD(dwBS);
            #endregion
            
            int BORDER_WIDTH = 2;
            int HEADER_HEIGHT = 2;
            double SPACE_BETWEEN_LINES = 2.6;

            Graphics gp = Graphics.FromHwnd(m_tool.Handle);
            Font defFont = System.Windows.Forms.Control.DefaultFont;
            // measure the string that will be displayed in tooltip
            SizeF strSize = gp.MeasureString(m_text, defFont, m_maxWidth - BORDER_WIDTH);
            gp.Dispose();

            int toolTipHeight = HEADER_HEIGHT;
            toolTipHeight += (int)strSize.Height;
            toolTipHeight += Convert.ToInt32((strSize.Height / (double)defFont.Height) * SPACE_BETWEEN_LINES);
            
            strSize.Height = toolTipHeight;

            return strSize;
        }

		/// <summary>
		/// Shows or hides the tool.
		/// </summary>
		/// <param name="show">0 to hide, -1 to show</param>
        private void Display(int show)
        {
            if (m_tool.Handle == IntPtr.Zero) return;

            IntPtr ptrStruct = Marshal.AllocHGlobal(Marshal.SizeOf(ti));
            Marshal.StructureToPtr(ti, ptrStruct, false);

            //SendMessageDelay(m_tool.Handle, TTM_SETDELAYTIME, TTDT_AUTOPOP, 5000);
            //SendMessageDelay(m_tool.Handle, TTM_SETDELAYTIME, TTDT_INITIAL, 0);

            SendMessage(
                m_tool.Handle, TTM_TRACKACTIVATE,
                show, ptrStruct);


            Marshal.FreeHGlobal(ptrStruct);

        }

        /// <summary>
        /// Show tooltip with text at specific position
        /// </summary>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Show(string text, int x, int y)
        {
            m_text = text;
            m_posX = x;
            m_posY = y;
            // recreate window always
            Hide();

            CreateTool();
            Display(-1);
        }


        /// <summary>
        /// Show tooltip with text at specific position for a timeout
        /// </summary>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Show(string text, int x, int y, int timeout)
        {
            Show(text, x, y);
            m_threadTimerHide.Change(timeout, -1);
        }

		/// <summary>
		/// Hides the message if visible.
		/// </summary>
		public void Hide()
		{            
			Display(0);
			m_tool.DestroyHandle();
		}

        private void m_threadTimerHide_Tick(object state)
        {
            Hide();
        }

		private int MAKELONG(int loWord, int hiWord)
		{
			return (hiWord << 16) | (loWord & 0xffff);
		}

      
    }
}
