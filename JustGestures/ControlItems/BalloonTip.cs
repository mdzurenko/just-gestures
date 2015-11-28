using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Drawing;

/*
 * 
 *	NOTE 1 : This class and logic will work only and only if the
 *		following key in the registry is set
 *		HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\EnableToolTips\
 * 
 *	NOTE 2 : There needs to be a manifest file in the output location.
 *		This is in order for the close button to work properly.
 *
 *	NOTE 3 : Needs WinXP.
 * 
*/

namespace JustGestures.ControlItems
{
    //public enum TooltipIcon : int
    //{
    //    None,
    //    Info,
    //    Warning,
    //    Error
    //}

   

    internal class MessageTool : NativeWindow
    {
        [DllImport("gdi32.dll")]
        static extern bool PtInRegion(IntPtr hrgn, int x, int y);

        [DllImport("user32")]
        static extern int GetWindowRgn(IntPtr hwnd, IntPtr hRgn);

        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_MOUSELEAVE = 0x02A3;
        private const int WM_NCMOUSELEAVE = 0x02A2;

        bool m_mouseHover = false;
        bool m_enabledBallonTip = true;
        public bool EnabledBallonTip { get { return m_enabledBallonTip; } }

        Timer m_timer;

        public bool IsMouseHover { get { return m_mouseHover; } }

        public delegate void DlgDeActivate();
        public event DlgDeActivate DeActivate;
        private void OnDeActivate() { if (DeActivate != null) DeActivate(); }

        public delegate void DlgMouseLeave();
        public DlgMouseLeave MouseLeave;
        private void OnMouseLeave() { if (MouseLeave != null) MouseLeave(); }

        public MessageTool()
        {
            m_timer = new Timer();
            m_timer.Interval = 200;
            m_timer.Tick += new EventHandler(m_timer_Tick);
            m_enabledBallonTip = BaloonTipEnabled();
        }

        private bool BaloonTipEnabled()
        {
            RegistryKey key = Registry.CurrentUser;
            bool isEnabled = true;
            object val;
            try
            {
                key = key.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced");
                val = key.GetValue("EnableBalloonTips");
                if (val != null)
                {
                    if (val.ToString() == "0")
                        isEnabled = false;
                    else
                        isEnabled = true;
                }
            }
            catch
            {
                // key wasnt found means it is enabled
            }
            finally
            {
                key.Close();
            }
            return isEnabled;
        }

        void m_timer_Tick(object sender, EventArgs e)
        {
            Win32.POINT point;
            Win32.RECT rect = new Win32.RECT();
            Win32.GetCursorPos(out point);
            Win32.GetWindowRect(this.Handle, ref rect);

            if (rect.left > point.x || point.x > rect.right
                || rect.top > point.y || point.y > rect.bottom)            
            {
                m_mouseHover = false;
                OnMouseLeave();
                m_timer.Stop();
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case  WM_MOUSEMOVE:
                    if (!m_mouseHover)
                    {
                        m_timer.Start();                        
                        m_mouseHover = true;
                    }
                    break;
                //case WM_NCMOUSELEAVE:
                //case WM_MOUSELEAVE:
                //    //if (m_mouseHover)
                //    {
                //        System.Diagnostics.Debug.WriteLine("Mouse left tool tip");
                //        m_mouseHover = false;
                //        OnMouseLeave();
                //    }
                //    break;
                case WM_LBUTTONDOWN:
                    m_mouseHover = false;
                    m_timer.Stop();
                    OnDeActivate();
                    break;
            }            
            base.WndProc(ref m);
        }
    }


    class BalloonTip : IDisposable
    {
        #region Declaration & Win32 import

        private MessageTool m_tool = null;
		private Control m_parent;
		private TOOLINFO ti;

		private int m_maxWidth = 350;
		private string m_text = "Balloon Tooltip Control Display Message";
		private string m_title = "Balloon Tooltip Message";
        private ToolTipIcon m_titleIcon = ToolTipIcon.None;
		private BalloonAlignment m_align = BalloonAlignment.TopRight;
		private bool m_absPosn = false;
		private bool m_centerStem = false;

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


        public enum BalloonAlignment
        {
            TopLeft,
            TopMiddle,
            TopRight,
            LeftMiddle,
            RightMiddle,
            BottomLeft,
            BottomMiddle,
            BottomRight,
            MiddleMiddle
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

        
        
        public MessageTool.DlgMouseLeave MouseLeave;
        private void OnMouseLeave() { if (MouseLeave != null) MouseLeave(); }

        public MessageTool.DlgDeActivate DeActivate;
        private void OnDeActivate() 
        {
            this.Hide();
            if (DeActivate != null) DeActivate(); 
        } 
		
		/// <summary>
		/// Creates a new instance of the MessageBalloon.
		/// </summary>
		public BalloonTip()
		{
            m_tool = new MessageTool();
            m_tool.MouseLeave += new MessageTool.DlgMouseLeave(OnMouseLeave);
            m_tool.DeActivate += new MessageTool.DlgDeActivate(OnDeActivate);
			//m_tool.DeActivate += new DeActivateEventHandler(this.Hide);
		}

		/// <summary>
		/// Creates a new instance of the MessageBalloon.
		/// </summary>
		/// <param name="parent">Set the parent control which will display.</param>
		public BalloonTip(Control parent)
		{
			m_parent = parent;
            m_tool = new MessageTool();
            m_tool.MouseLeave += new MessageTool.DlgMouseLeave(OnMouseLeave);
            m_tool.DeActivate += new MessageTool.DlgDeActivate(OnDeActivate);
			//m_tool.DeActivate += new DeActivateEventHandler(this.Hide);

            Form parentForm = parent.FindForm();
            parentForm.Deactivate += new EventHandler(ParentFormDeactivate_EventHandler);
            parentForm.HandleDestroyed += new EventHandler(ParentFormDeactivate_EventHandler);

		}

        private void ParentFormDeactivate_EventHandler(object sender, EventArgs e)
        {
            this.Hide();
        }

        ~BalloonTip()
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
			System.Diagnostics.Debug.Assert(
				m_parent.Handle!=IntPtr.Zero, 
				"parent hwnd is null", "SetToolTip");

			CreateParams cp = new CreateParams();
			cp.ClassName = TOOLTIPS_CLASS;
			cp.Style = 
				WS_POPUP | 
				TTS_NOPREFIX |	
				TTS_ALWAYSTIP |
				TTS_CLOSE;

            // if ballons are enabled in Windows registry then display tool tip as ballon
            if (m_tool.EnabledBallonTip)
                cp.Style |= TTS_BALLOON;


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
			if(m_absPosn)
			{
				ti.uFlags |= TTF_ABSOLUTE;
			}

			if(m_centerStem)
			{
				ti.uFlags |= TTF_CENTERTIP;
			}
            
            ti.uId = m_tool.Handle;
			ti.lpszText = m_text;
			ti.hwnd = m_parent.Handle;

			GetClientRect(m_parent.Handle, ref ti.rect);
			ClientToScreen(m_parent.Handle, ref ti.rect);

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

			IntPtr ptrTitle = Marshal.StringToHGlobalAuto(m_title);

			SendMessage(
				m_tool.Handle, TTM_SETTITLE, 
				(int)m_titleIcon, ptrTitle);

			SetBalloonPosition(ti.rect);

			Marshal.FreeHGlobal(ptrStruct);
			Marshal.FreeHGlobal(ptrTitle);
		}
		
		private void SetBalloonPosition(RECT rect)
		{
			int x = 0, y = 0;

			// calculate cordinates depending upon aligment
			switch(m_align)
			{
				case BalloonAlignment.TopLeft:
					x = rect.left;
					y = rect.top;
					break;
				case BalloonAlignment.TopMiddle:
					x = rect.left + (rect.right / 2);
					y = rect.top;
					break;
				case BalloonAlignment.TopRight:
					x = rect.left + rect.right;
					y = rect.top;
					break;
				case BalloonAlignment.LeftMiddle:
					x = rect.left;
					y = rect.top + (rect.bottom / 2);
					break;
				case BalloonAlignment.RightMiddle:
					x = rect.left + rect.right;
					y = rect.top + (rect.bottom / 2);
					break;
				case BalloonAlignment.BottomLeft:
					x = rect.left;
					y = rect.top + rect.bottom;
					break;
				case BalloonAlignment.BottomMiddle:
					x = rect.left + (rect.right / 2);
					y = rect.top + rect.bottom;
					break;
				case BalloonAlignment.BottomRight:
					x = rect.left + rect.right;
					y = rect.top + rect.bottom;
					break;
                case BalloonAlignment.MiddleMiddle:
                    x = rect.left + (rect.right / 2);
                    y = rect.top + (rect.bottom / 2);
                    break;
				default:
					System.Diagnostics.Debug.Assert(false, "undefined enum", "default case reached");
					break;
			}

            //int pt = MAKELONG(ti.rect.left, ti.rect.top);

            // if tool tips are not enabled in system then manually set the y coordinate
            if (!m_tool.EnabledBallonTip)
            {
                // get the size of screen on which is tooltip located
                Rectangle screen = Screen.FromHandle(m_tool.Handle).Bounds;

                int toolTipHeight = MeasureToolTipHeight();

                if (y + toolTipHeight > screen.Height)
                {
                    y -= toolTipHeight;
                }
            }

			int pt = MAKELONG(x, y);
			IntPtr ptr = new IntPtr(pt);

			SendMessage(
				m_tool.Handle, TTM_TRACKPOSITION,
				0, ptr);
				
		}

        private int MeasureToolTipHeight()
        {
            #region TEST ONLY: when messuring the text in tooltip
            //IntPtr ptrStruct = Marshal.AllocHGlobal(Marshal.SizeOf(ti));
            //Marshal.StructureToPtr(ti, ptrStruct, false);

            //SendMessage(m_tool.Handle, TTM_TRACKACTIVATE, -1, ptrStruct);
            //int dwBS = SendMessage(m_tool.Handle, TTM_GETBUBBLESIZE, 0, ptrStruct);
            ////SendMessage(m_tool.Handle, TTM_TRACKACTIVATE, 0, ptrStruct);

            //int origToolTipHeight = Win32.HIWORD(dwBS);
            //int toolTipWidth = Win32.LOWORD(dwBS);
            #endregion
            
            int BORDER_WIDTH = 22;
            int HEADER_HEIGHT = 22;
            double SPACE_BETWEEN_LINES = 2.6;

            Graphics gp = Graphics.FromHwnd(m_tool.Handle);
            Font defFont = System.Windows.Forms.Control.DefaultFont;
            // measure the string that will be displayed in tooltip
            SizeF strSize = gp.MeasureString(m_text, defFont, m_maxWidth - BORDER_WIDTH);
            gp.Dispose();

            int toolTipHeight = HEADER_HEIGHT;
            toolTipHeight += (int)strSize.Height;
            toolTipHeight += Convert.ToInt32((strSize.Height / (double)defFont.Height) * SPACE_BETWEEN_LINES);

            return toolTipHeight;
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

            SendMessage(
                m_tool.Handle, TTM_TRACKACTIVATE,
                show, ptrStruct);

            Marshal.FreeHGlobal(ptrStruct);

        }

		/// <summary>
		/// Hides the message if visible.
		/// </summary>
		public void Hide()
		{
			Display(0);
			m_tool.DestroyHandle();
		}

		private int MAKELONG(int loWord, int hiWord)
		{
			return (hiWord << 16) | (loWord & 0xffff);
		}

		/// <summary>
		/// Sets or gets the Title.
		/// </summary>
		public string Title
		{
			get { return m_title; }
			set { m_title = value; }
		}

		/// <summary>
		/// Sets or gets the display icon.
		/// </summary>
		public ToolTipIcon TitleIcon
		{
			get { return m_titleIcon; }
			set { m_titleIcon = value; }
		}

		/// <summary>
		/// Sets or get the display text.
		/// </summary>
		public string Text
		{
			get { return m_text; }
            set { m_text = value; }
		}

		/// <summary>
		/// Sets or gets the parent.
		/// </summary>
		public Control Parent
		{
			get { return m_parent; }
			set	{ m_parent = value;	}
		}

		/// <summary>
		/// Sets or gets the placement of the balloon.
		/// </summary>
		public BalloonAlignment Align
		{
			get { return m_align; }
			set { m_align = value; }
		}

        public bool IsMouseHover { get { return m_tool.IsMouseHover; } }

		/// <summary>
		/// Sets or gets the positioning of the balloon.
		/// TRUE : Positions using the exact co-ordinates,
		/// if the co-ordinates are outside the screen, tip wont be shown.
		/// FALSE : Positions using the co-ordinates as a reference.
		/// Regardless of the co-ordinates, the tip will 
		/// always be shown on the screen.
		/// </summary>
		public bool UseAbsolutePositioning
		{
			get { return m_absPosn; }
			set { m_absPosn = value; }
		}

		/// <summary>
		/// Sets or gets the stem position 
		/// in the tip. 
		/// TRUE : The stem of the tip is set to center.
		/// An attempt is made to show the tip with the stem
		/// centered, if that would make the tip to be 
		/// hidden partly, stem is not centered.
		/// FALSE: Stem is not centered.
		/// </summary>
		public bool CenterStem
		{
			get { return m_centerStem; }
			set { m_centerStem = value; }
		}

		/// <summary>
		/// Show the Message in a balloon tooltip.
		/// </summary>
		public void Show()
		{
			// recreate window always
			Hide();

			CreateTool();
			Display(-1);
		}





    }
}
