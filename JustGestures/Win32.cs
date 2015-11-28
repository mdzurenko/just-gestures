using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace JustGestures
{
    public struct Win32
    {        

        #region Sturctures

        //[StructLayout(LayoutKind.Sequential)]
        //public struct KEYBOARD_INPUT
        //{
        //    public uint type;
        //    public ushort vk;
        //    public ushort scanCode;
        //    public uint flags;
        //    public uint time;
        //    public uint extrainfo;
        //    public uint padding1;
        //    public uint padding2;

        //    public KEYBOARD_INPUT(ushort key)
        //    {
        //        type = Win32.INPUT_KEYBOARD;
        //        vk = key;
        //        scanCode = 0;
        //        flags = 0;
        //        time = 0;
        //        extrainfo = 0;
        //        padding1 = 0;
        //        padding2 = 0;
        //    }
        //}

        public struct POINT
        {
            public int x;
            public int y;

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public struct SIZE
        {
            public int cx;
            public int cy;

            public SIZE(int cx, int cy)
            {
                this.cx = cx;
                this.cy = cy;
            }
        }

        public static bool IsKeyDown(ushort KeyCode)
        {
            ushort state = GetKeyState(KeyCode);
            return ((state & 0x10000) == 0x10000);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public Int32 left;
            public Int32 top;
            public Int32 right;
            public Int32 bottom;

            public RECT(Rectangle _rect)
            {
                left = _rect.Left;
                top = _rect.Top;
                right = _rect.Right;
                bottom = _rect.Bottom;
            }

            public RECT(RectangleF _rect)
            {
                left = (int)_rect.Left;
                top = (int)_rect.Top;
                right = (int)_rect.Right;
                bottom = (int)_rect.Bottom;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWINFO
        {
            public UInt32 cbSize;
            public RECT rcWindow;
            public RECT rcClient;
            public UInt32 dwStyle;
            public UInt32 dwExStyle;
            public UInt32 dwWindowStatus;
            public UInt32 cxWindowBorders;
            public UInt32 cyWindowBorders;
            public UInt16 atomWindowType;
            public UInt16 wCreatorVersion;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public POINT ptMinPosition;
            public POINT ptMaxPosition;
            public RECT rcNormalPosition;
        }


        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        public enum TernaryRasterOperations
        {
            SRCCOPY = 0x00CC0020, /* dest = source                   */
            SRCPAINT = 0x00EE0086, /* dest = source OR dest           */
            SRCAND = 0x008800C6, /* dest = source AND dest          */
            SRCINVERT = 0x00660046, /* dest = source XOR dest          */
            SRCERASE = 0x00440328, /* dest = source AND (NOT dest )   */
            NOTSRCCOPY = 0x00330008, /* dest = (NOT source)             */
            NOTSRCERASE = 0x001100A6, /* dest = (NOT src) AND (NOT dest) */
            MERGECOPY = 0x00C000CA, /* dest = (source AND pattern)     */
            MERGEPAINT = 0x00BB0226, /* dest = (NOT source) OR dest     */
            PATCOPY = 0x00F00021, /* dest = pattern                  */
            PATPAINT = 0x00FB0A09, /* dest = DPSnoo                   */
            PATINVERT = 0x005A0049, /* dest = pattern XOR dest         */
            DSTINVERT = 0x00550009, /* dest = (NOT dest)               */
            BLACKNESS = 0x00000042, /* dest = BLACK                    */
            WHITENESS = 0x00FF0062, /* dest = WHITE                    */
        };

        public delegate bool EnumDelegate(IntPtr hWnd, int lParam);

        public const int INPUT_MOUSE = 0;
        public const int INPUT_KEYBOARD = 1;
        public const int INPUT_HARDWARE = 2;

#if (!X64)
        [StructLayout(LayoutKind.Explicit)]
        public struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(4)]
            public MOUSEINPUT mi;
            [FieldOffset(4)]
            public KEYBDINPUT ki;
            [FieldOffset(4)]
            public HARDWAREINPUT hi;
        }
#else 
        [StructLayout(LayoutKind.Explicit)]
        public struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(8)]
            public MOUSEINPUT mi;
            [FieldOffset(8)]
            public KEYBDINPUT ki;
            [FieldOffset(8)]
            public HARDWAREINPUT hi;
        }
#endif

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        public static KEYBDINPUT CreateKeybdInput(ushort wVK, uint flag)
        {
            KEYBDINPUT i = new KEYBDINPUT();
            i.wVk = wVK;
            i.wScan = 0;
            i.time = 0;
            i.dwExtraInfo = IntPtr.Zero;
            i.dwFlags = flag;
            return i;
        }

        public static MOUSEINPUT CreateMouseInput(int x, int y, int data, uint t, int flag)
        {
            MOUSEINPUT mi = new MOUSEINPUT();
            mi.dx = x;
            mi.dy = y;
            mi.mouseData = data;
            mi.time = t;
            //mi.dwFlags = MOUSEEVENTF_ABSOLUTE| MOUSEEVENTF_MOVE;
            mi.dwFlags = flag;
            return mi;
        }
        #endregion Sturctures

        #region DllImport USER32.dll

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetTopWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int SetActiveWindow(IntPtr hWnd);

        [DllImport("User32")]
        public static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("User32")]
        public static extern bool CloseWindow(IntPtr hWnd);

        [DllImport("User32")]
        public static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point pt);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int nVirtKey);

        [DllImport("user32.dll")]
        public static extern ushort GetKeyState(int nVirtKey);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

        [DllImport("user32")]
        public static extern int EnumWindows(EnumWindowProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32")]
        public static extern int EnumChildWindows(IntPtr hWndParent, EnumWindowProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction, IntPtr lParam);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr GetAncestor(IntPtr hwnd, uint gaFlags);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);
        
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        [DllImport("kernel32.dll")]
        public static extern int GetLongPathName(string path, StringBuilder longPath, int longPathLength);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, int wFlag);

        [DllImport("user32.dll")]
        public static extern int ExitWindowsEx(int uFlags, int dwReason);

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public static extern int GetSystemMetrics(int which);

        [DllImport("user32.dll")]
        public static extern void SetWindowPos(IntPtr hWnd, int hwndInsertAfter, int X, int Y, int width, int height, uint flags);

        [DllImport("USER32.DLL")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("USER32.DLL")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        [DllImport("user32")]
        public static extern int SetLayeredWindowAttributes(IntPtr Handle, int crKey, byte bAlpha, int dwFlags);

        [DllImport("user32.dll")]
        public static extern bool GetLayeredWindowAttributes(IntPtr hwnd, out uint crKey, out byte bAlpha, out uint dwFlags);

        [DllImport("user32", SetLastError = true)]
        public static extern IntPtr MoveWindow(IntPtr hwnd, int x, int y, int w, int l, bool repaint);

        [DllImport("user32.dll")]
        public static extern bool InvalidateRect(IntPtr hWnd, ref RECT lpRect, bool bErase);

        [DllImport("user32.dll")]
        public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

        [DllImport("user32")]
        public static extern int UpdateWindow(IntPtr hwnd);

        [DllImportAttribute("user32.dll")]
        public extern static bool UpdateLayeredWindow(IntPtr handle, IntPtr hdcDst, ref POINT pptDst,
            ref SIZE psize, IntPtr hdcSrc, ref POINT pprSrc, int crKey, ref BLENDFUNCTION pblend, int dwFlags);

        [DllImportAttribute("user32.dll")]
        public extern static IntPtr GetDC(IntPtr handle);

        [DllImportAttribute("user32.dll", ExactSpelling = true)]
        public extern static int ReleaseDC(IntPtr handle, IntPtr hDC);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern bool OpenIcon(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string lpFileName);

        [DllImport("user32.dll")]
        public static extern bool InvalidateRgn(IntPtr hWnd, IntPtr hRgn, bool bErase);

        [DllImport("user32.dll")]
        public static extern bool RedrawWindow(IntPtr hWnd, [In] ref RECT lprcUpdate, IntPtr hrgnUpdate, int flags);

        [DllImport("user32.dll")]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetClassLong(IntPtr hWnd, int nIndex);
        //public static extern IntPtr GetClassLongPtr(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint GetWindowModuleFileName(IntPtr hwnd,
           [Out] StringBuilder lpszFileName, int cchFileNameMax);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("psapi.dll")]
        public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, [In] [MarshalAs(UnmanagedType.U4)] int nSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("shell32.dll")]
        public static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);

        [DllImport("kernel32.dll", SetLastError = true)]
        [PreserveSig]
        public static extern uint GetModuleFileName
        (
            [In]
    IntPtr hModule,

            [Out]
    StringBuilder lpFilename,

            [In]
    [MarshalAs(UnmanagedType.U4)]
    int nSize
        );


        [DllImport("uxtheme", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern Int32 SetWindowTheme
            (IntPtr hWnd, String textSubAppName, String textSubIdList);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);


        [DllImport("user32", EntryPoint = "LoadCursorFromFile")]
        public static extern int LoadCursorFromFileA(string lpFileName);


        [DllImport("user32.dll")]
        public static extern IntPtr SetCapture(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);        

        #endregion DllImport USER32.dll



        #region DllImport GDI32.dll

        [DllImport("gdi32.dll")]
        public static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest,
            int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc,
            int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
            TernaryRasterOperations dwRop);


        [DllImport("GDI32.dll")]
        public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        [DllImport("GDI32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        [DllImport("GDI32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("GDI32.dll")]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("GDI32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("GDI32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll", EntryPoint = "GdiAlphaBlend")]
        public static extern bool AlphaBlend(IntPtr hdcDest, int nXOriginDest, int nYOriginDest,
            int nWidthDest, int nHeightDest,
            IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
            BLENDFUNCTION blendFunction);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
        int nLeftRect, // x-coordinate of upper-left corner
        int nTopRect, // y-coordinate of upper-left corner
        int nRightRect, // x-coordinate of lower-right corner
        int nBottomRect, // y-coordinate of lower-right corner
        int nWidthEllipse, // height of ellipse
        int nHeightEllipse // width of ellipse
        );

        #endregion DllImport GDI32.dll

        #region Constants

        public const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        public const uint KEYEVENTF_KEYUP = 0x0002;
        public const uint KEYEVENTF_UNICODE = 0x0004;
        public const uint KEYEVENTF_SCANCODE = 0x0008;

        #region Virtual Key Constatns
        public const ushort VK_LBUTTON = 0x01; //Left mouse button
        public const ushort VK_RBUTTON = 0x02; //Right mouse button
        public const ushort VK_CANCEL = 0x03; //Control-break processing
        public const ushort VK_MBUTTON = 0x04; //Middle mouse button (three-button mouse)
        public const ushort VK_XBUTTON1 = 0x05; //Windows 2000/XP: X1 mouse button
        public const ushort VK_XBUTTON2 = 0x06; //Windows 2000/XP: X2 mouse button
        // - (0x07) Undefined
        public const ushort VK_BACK = 0x08; //BACKSPACE key
        public const ushort VK_TAB = 0x09; //TAB key
        //- (0x0A-0B) Reserved
        public const ushort VK_CLEAR = 0x0C; //CLEAR key
        public const ushort VK_RETURN = 0x0D; //ENTER key
        //- (0x0E-0F) Undefined
        public const ushort VK_SHIFT = 0x10; //SHIFT key
        public const ushort VK_CONTROL = 0x11; //CTRL key
        public const ushort VK_MENU = 0x12; //ALT key
        public const ushort VK_PAUSE = 0x13; //PAUSE key
        public const ushort VK_CAPITAL = 0x14; //CAPS LOCK key
        public const ushort VK_KANA = 0x15; //Input Method Editor (IME) Kana mode
        public const ushort VK_HANGUEL = 0x15; //IME Hanguel mode (maintained for compatibility; use VK_HANGUL)
        public const ushort VK_HANGUL = 0x15; //IME Hangul mode
        //- (0x16) Undefined
        public const ushort VK_JUNJA = 0x17; //IME Junja mode
        public const ushort VK_FINAL = 0x18; //IME final mode
        public const ushort VK_HANJA = 0x19; //IME Hanja mode
        public const ushort VK_KANJI = 0x19; //IME Kanji mode
        //- (0x1A) Undefined
        public const ushort VK_ESCAPE = 0x1B; //ESC key
        public const ushort VK_CONVERT = 0x1C; //IME convert
        public const ushort VK_NONCONVERT = 0x1D; //IME nonconvert
        public const ushort VK_ACCEPT = 0x1E; //IME accept
        public const ushort VK_MODECHANGE = 0x1F; //IME mode change request
        public const ushort VK_SPACE = 0x20; //SPACEBAR
        public const ushort VK_PRIOR = 0x21; //PAGE UP key
        public const ushort VK_NEXT = 0x22; //PAGE DOWN key
        public const ushort VK_END = 0x23; //END key
        public const ushort VK_HOME = 0x24; //HOME key
        public const ushort VK_LEFT = 0x25; //LEFT ARROW key
        public const ushort VK_UP = 0x26; //UP ARROW key
        public const ushort VK_RIGHT = 0x27; //RIGHT ARROW key
        public const ushort VK_DOWN = 0x28; //DOWN ARROW key
        public const ushort VK_SELECT = 0x29; //SELECT key
        public const ushort VK_PRINT = 0x2A; //PRINT key
        public const ushort VK_EXECUTE = 0x2B; //EXECUTE key
        public const ushort VK_SNAPSHOT = 0x2C; //PRINT SCREEN key
        public const ushort VK_INSERT = 0x2D; //INS key
        public const ushort VK_DELETE = 0x2E; //DEL key
        public const ushort VK_HELP = 0x2F; //HELP key
        public const ushort VK_0 = 0x30; //0 key
        public const ushort VK_1 = 0x31; //1 key
        public const ushort VK_2 = 0x32; //2 key
        public const ushort VK_3 = 0x33; //3 key
        public const ushort VK_4 = 0x34; //4 key
        public const ushort VK_5 = 0x35; //5 key
        public const ushort VK_6 = 0x36; //6 key
        public const ushort VK_7 = 0x37; //7 key
        public const ushort VK_8 = 0x38; //8 key
        public const ushort VK_9 = 0x39; //9 key
        //- (0x3A-40) Undefined
        public const ushort VK_A = 0x41; //A key
        public const ushort VK_B = 0x42; //B key
        public const ushort VK_C = 0x43; //C key
        public const ushort VK_D = 0x44; //D key
        public const ushort VK_E = 0x45; //E key
        public const ushort VK_F = 0x46; //F key
        public const ushort VK_G = 0x47; //G key
        public const ushort VK_H = 0x48; //H key
        public const ushort VK_I = 0x49; //I key
        public const ushort VK_J = 0x4A; //J key
        public const ushort VK_K = 0x4B; //K key
        public const ushort VK_L = 0x4C; //L key
        public const ushort VK_M = 0x4D; //M key
        public const ushort VK_N = 0x4E; //N key
        public const ushort VK_O = 0x4F; //O key
        public const ushort VK_P = 0x50; //P key
        public const ushort VK_Q = 0x51; //Q key
        public const ushort VK_R = 0x52; //R key
        public const ushort VK_S = 0x53; //S key
        public const ushort VK_T = 0x54; //T key
        public const ushort VK_U = 0x55; //U key
        public const ushort VK_V = 0x56; //V key
        public const ushort VK_W = 0x57; //W key
        public const ushort VK_X = 0x58; //X key
        public const ushort VK_Y = 0x59; //Y key
        public const ushort VK_Z = 0x5A; //Z key
        public const ushort VK_LWIN = 0x5B; //Left Windows key (Microsoft Natural keyboard) 
        public const ushort VK_RWIN = 0x5C; //Right Windows key (Natural keyboard)
        public const ushort VK_APPS = 0x5D; //Applications key (Natural keyboard)
        //- (0x5E) Reserved
        public const ushort VK_SLEEP = 0x5F; //Computer Sleep key
        public const ushort VK_NUMPAD0 = 0x60; //Numeric keypad 0 key
        public const ushort VK_NUMPAD1 = 0x61; //Numeric keypad 1 key
        public const ushort VK_NUMPAD2 = 0x62; //Numeric keypad 2 key
        public const ushort VK_NUMPAD3 = 0x63; //Numeric keypad 3 key
        public const ushort VK_NUMPAD4 = 0x64; //Numeric keypad 4 key
        public const ushort VK_NUMPAD5 = 0x65; //Numeric keypad 5 key   
        public const ushort VK_NUMPAD6 = 0x66; //Numeric keypad 6 key
        public const ushort VK_NUMPAD7 = 0x67; //Numeric keypad 7 key
        public const ushort VK_NUMPAD8 = 0x68; //Numeric keypad 8 key
        public const ushort VK_NUMPAD9 = 0x69; //Numeric keypad 9 key
        public const ushort VK_MULTIPLY = 0x6A; //Multiply key
        public const ushort VK_ADD = 0x6B; //Add key
        public const ushort VK_SEPARATOR = 0x6C; //Separator key
        public const ushort VK_SUBTRACT = 0x6D; //Subtract key
        public const ushort VK_DECIMAL = 0x6E; //Decimal key
        public const ushort VK_DIVIDE = 0x6F; //Divide key
        public const ushort VK_F1 = 0x70; //F1 key
        public const ushort VK_F2 = 0x71; //F2 key
        public const ushort VK_F3 = 0x72; //F3 key
        public const ushort VK_F4 = 0x73; //F4 key
        public const ushort VK_F5 = 0x74; //F5 key
        public const ushort VK_F6 = 0x75; //F6 key
        public const ushort VK_F7 = 0x76; //F7 key
        public const ushort VK_F8 = 0x77; //F8 key
        public const ushort VK_F9 = 0x78; //F9 key
        public const ushort VK_F10 = 0x79; //F10 key
        public const ushort VK_F11 = 0x7A; //F11 key
        public const ushort VK_F12 = 0x7B; //F12 key
        public const ushort VK_F13 = 0x7C; //F13 key
        public const ushort VK_F14 = 0x7D; //F14 key
        public const ushort VK_F15 = 0x7E; //F15 key
        public const ushort VK_F16 = 0x7F; //F16 key
        //public const ushort VK_F17 = 0x80H; //F17 key
        //public const ushort VK_F18 = 0x81H; //F18 key
        //public const ushort VK_F19 = 0x82H; //F19 key
        //public const ushort VK_F20 = 0x83H; //F20 key
        //public const ushort VK_F21 = 0x84H; //F21 key
        //public const ushort VK_F22 = 0x85H; //F22 key
        //public const ushort VK_F23 = 0x86H; //F23 key
        //public const ushort VK_F24 = 0x87H; //F24 key
        //- (0x88-8F) Unassigned
        public const ushort VK_NUMLOCK = 0x90; //NUM LOCK key
        public const ushort VK_SCROLL = 0x91; //SCROLL LOCK key
        //- (0x92-96) OEM specific
        //- (0x97-9F) // Unassigned
        public const ushort VK_LSHIFT = 0xA0; //Left SHIFT key
        public const ushort VK_RSHIFT = 0xA1; //Right SHIFT key
        public const ushort VK_LCONTROL = 0xA2; //Left CONTROL key
        public const ushort VK_RCONTROL = 0xA3; //Right CONTROL key
        public const ushort VK_LMENU = 0xA4; //Left MENU key
        public const ushort VK_RMENU = 0xA5; //Right MENU key
        public const ushort VK_BROWSER_BACK = 0xA6; //Windows 2000/XP: Browser Back key
        public const ushort VK_BROWSER_FORWARD = 0xA7; //Windows 2000/XP: Browser Forward key
        public const ushort VK_BROWSER_REFRESH = 0xA8; //Windows 2000/XP: Browser Refresh key
        public const ushort VK_BROWSER_STOP = 0xA9; //Windows 2000/XP: Browser Stop key
        public const ushort VK_BROWSER_SEARCH = 0xAA; //Windows 2000/XP: Browser Search key 
        public const ushort VK_BROWSER_FAVORITES = 0xAB; //Windows 2000/XP: Browser Favorites key
        public const ushort VK_BROWSER_HOME = 0xAC; //Windows 2000/XP: Browser Start and Home key
        public const ushort VK_VOLUME_MUTE = 0xAD; //Windows 2000/XP: Volume Mute key
        public const ushort VK_VOLUME_DOWN = 0xAE; //Windows 2000/XP: Volume Down key
        public const ushort VK_VOLUME_UP = 0xAF; //Windows 2000/XP: Volume Up key
        public const ushort VK_MEDIA_NEXT_TRACK = 0xB0; //Windows 2000/XP: Next Track key
        public const ushort VK_MEDIA_PREV_TRACK = 0xB1; //Windows 2000/XP: Previous Track key
        public const ushort VK_MEDIA_STOP = 0xB2; //Windows 2000/XP: Stop Media key
        public const ushort VK_MEDIA_PLAY_PAUSE = 0xB3; //Windows 2000/XP: Play/Pause Media key
        public const ushort VK_LAUNCH_MAIL = 0xB4; //Windows 2000/XP: Start Mail key
        public const ushort VK_LAUNCH_MEDIA_SELECT = 0xB5; //Windows 2000/XP: Select Media key
        public const ushort VK_LAUNCH_APP1 = 0xB6; //Windows 2000/XP: Start Application 1 key
        public const ushort VK_LAUNCH_APP2 = 0xB7; //Windows 2000/XP: Start Application 2 key
        //- (0xB8-B9) Reserved
        public const ushort VK_OEM_1 = 0xBA; // Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP: For the US standard keyboard, the ';:' key 
        public const ushort VK_OEM_PLUS = 0xBB; //Windows 2000/XP: For any country/region, the '+' key
        public const ushort VK_OEM_COMMA = 0xBC; //Windows 2000/XP: For any country/region, the ',' key
        public const ushort VK_OEM_MINUS = 0xBD; //Windows 2000/XP: For any country/region, the '-' key
        public const ushort VK_OEM_PERIOD = 0xBE; //Windows 2000/XP: For any country/region, the '.' key
        public const ushort VK_OEM_2 = 0xBF; //Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP: For the US standard keyboard, the '/?' key 
        public const ushort VK_OEM_3 = 0xC0; //Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP: For the US standard keyboard, the '`~' key 
        //- (0xC1-D7) Reserved
        //- (0xD8-DA) Unassigned
        public const ushort VK_OEM_4 = 0xDB; //Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP: For the US standard keyboard, the '[{' key
        public const ushort VK_OEM_5 = 0xDC; //Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP: For the US standard keyboard, the '\|' key
        public const ushort VK_OEM_6 = 0xDD; //Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP: For the US standard keyboard, the ']}' key
        public const ushort VK_OEM_7 = 0xDE; //Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP: For the US standard keyboard, the 'single-quote/double-quote' key
        public const ushort VK_OEM_8 = 0xDF; //Used for miscellaneous characters; it can vary by keyboard.
        //- (0xE0) Reserved
        //- (0xE1) OEM specific
        public const ushort VK_OEM_102 = 0xE2; //Windows 2000/XP: Either the angle bracket key or the backslash key on the RT 102-key keyboard
        //- (0xE3-E4) OEM specific
        public const ushort VK_PROCESSKEY = 0xE5; //Windows 95/98/Me, Windows NT 4.0, Windows 2000/XP: IME PROCESS key
        //- (0xE6) OEM specific 
        public const ushort VK_PACKET = 0xE7; //Windows 2000/XP: Used to pass Unicode characters as if they were keystrokes. The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods. For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP
        //- (0xE8) Unassigned
        //- (0xE9-F5) OEM specific
        public const ushort VK_ATTN = 0xF6; //Attn key
        public const ushort VK_CRSEL = 0xF7; //CrSel key
        public const ushort VK_EXSEL = 0xF8; //ExSel key
        public const ushort VK_EREOF = 0xF9; //Erase EOF key
        public const ushort VK_PLAY = 0xFA; //Play key
        public const ushort VK_ZOOM = 0xFB; //Zoom key
        public const ushort VK_NONAME = 0xFC; //Reserved 
        public const ushort VK_PA1 = 0xFD; //PA1 key
        public const ushort VK_OEM_CLEAR = 0xFE; //Clear key
        #endregion #Virtual Key Constatns

        public const int STRETCH_ANDSCANS = 1;
        public const int STRETCH_ORSCANS = 2;
        public const int STRETCH_DELETESCANS = 3;
        public const int STRETCH_HALFTONE = 4;

        public const int RDW_INVALIDATE = 0x0001;
        public const int RDW_INTERNALPAINT = 0x0002;
        public const int RDW_ERASE = 0x0004;
        public const int RDW_VALIDATE = 0x0008;
        public const int RDW_NOINTERNALPAINT = 0x0010;
        public const int RDW_NOERASE = 0x0020;
        public const int RDW_NOCHILDREN = 0x0040;
        public const int RDW_ALLCHILDREN = 0x0080;
        public const int RDW_UPDATENOW = 0x0100;
        public const int RDW_ERASENOW = 0x0200;
        public const int RDW_FRAME = 0x0400;
        public const int RDW_NOFRAME = 0x0800;

        public const byte AC_SRC_OVER = 0x00;
        public const byte AC_SRC_ALPHA = 0x01;


        public const int ULW_ALPHA = 2;
        //public const int ULW_COLORKEY = 1;
        //public const int ULW_OPAQUE = 0;


        public const int SHFS_SHOWTASKBAR = 0x1;
        public const int SHFS_HIDETASKBAR = 0x2;
        public const int SHFS_SHOWSIPBUTTON = 0x4;
        public const int SHFS_HIDESIPBUTTON = 0x8;
        public const int SHFS_SHOWSTARTICON = 0x10;
        public const int SHFS_HIDESTARTICON = 0x20;


        public const int LWA_ALPHA = 2;
        public const int LWA_COLORKEY = 1;

        public const int WS_CLIPCHILDREN = 0x02000000;
        public const int GWL_EXSTYLE = -20;
        public const int GWL_STYLE = -16;
        public const int WS_EX_LAYERED = 0x80000;
        public const int WS_EX_TRANSPARENT = 0x00000020;
        public const int WS_EX_TOOLWINDOW = 0x00000080;
        public const int WS_EX_NOACTIVATE = 0x08000000;

        public const int WS_OVERLAPPED = 0x00000000;
        public const uint WS_POPUP = 0x80000000;
        public const int WS_CHILD = 0x40000000;
        public const int WS_MINIMIZE = 0x20000000;
        public const int WS_VISIBLE = 0x10000000;
        public const int WS_DISABLED = 0x08000000;
        public const int WS_CLIPSIBLINGS = 0x04000000;
        public const int WS_MAXIMIZE = 0x01000000;
        public const int WS_CAPTION = 0x00C00000;     /* WS_BORDER | WS_DLGFRAME  */
        public const int WS_BORDER = 0x00800000;
        public const int WS_DLGFRAME = 0x00400000;
        public const int WS_VSCROLL = 0x00200000;
        public const int WS_HSCROLL = 0x00100000;
        public const int WS_SYSMENU = 0x00080000;
        public const int WS_THICKFRAME = 0x00040000;
        public const int WS_GROUP = 0x00020000;
        public const int WS_TABSTOP = 0x00010000;


        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;
        public const int SM_SWAPBUTTON = 23;

        public const int SWP_ASYNCWINDOWPOS = 0x4000;
        public const int SWP_DEFERERASE = 0x2000;
        public const int SWP_DRAWFRAME = 0x0020;
        public const int SWP_FRAMECHANGED = 0x0020;
        public const int SWP_HIDEWINDOW = 0x0080;
        public const int SWP_NOACTIVATE = 0x0010;
        public const int SWP_NOCOPYBITS = 0x0100;
        public const int SWP_NOMOVE = 0x0002;
        public const int SWP_NOOWNERZORDER = 0x0200;
        public const int SWP_NOREDRAW = 0x0008;
        public const int SWP_NOREPOSITION = 0x0200;
        public const int SWP_NOSENDCHANGING = 0x0400;
        public const int SWP_NOSIZE = 0x0001;
        public const int SWP_NOZORDER = 0x0004;
        public const int SWP_SHOWWINDOW = 0x0040;

        public const int HWND_TOP = 0;
        public const int HWND_BOTTOM = 1;
        public const int HWND_TOPMOST = -1;
        public const int HWND_NOTOPMOST = -2;

        public const int EWX_FORCE = 4;
        public const int EWX_LOGOFF = 0;
        public const int EWX_REBOOT = 2;
        public const int EWX_SHUTDOWN = 1;

        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDLAST = 1;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GW_OWNER = 4;
        public const int GW_CHILD = 5;

        public const uint GA_PARENT = 1;
        public const uint GA_ROOT = 2;
        public const uint GA_ROOTOWNER = 3;

        public static ushort LOWORD(int l) { return (ushort)(l); }
        public static ushort HIWORD(int l) { return (ushort)(((int)(l) >> 16) & 0xFFFF); }

        public const int MOUSEEVENTF_MOVE = 0x0001;
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const int MOUSEEVENTF_LEFTUP = 0x0004;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        public const int MOUSEEVENTF_XDOWN = 0x0080;
        public const int MOUSEEVENTF_XUP = 0x0100;
        public const int MOUSEEVENTF_WHEEL = 0x0800;
        public const int MOUSEEVENTF_VIRTUALDESK = 0x4000;
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        public const int XBUTTON1 = 0x0001;
        public const int XBUTTON2 = 0x0002;

        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_CANCELMODE = 0x001F;
   

        public const int HC_ACTION = 0;
        public const int HC_NOREMOVE = 3;

        public const int WM_COPY = 0x301;
        public const int WM_PASTE = 0x302;

        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_SYSKEYUP = 0x0105;
        public const int WM_COMMAND = 0x0111;
        public const int WM_SYSCOMMAND = 0x0112;

        public const uint WM_CLOSE = 0x10;
        public const int WM_ACTIVATE = 0x0006;
        public const int WM_ACTIVATEAPP = 0x001C;
        public const int WM_NCACTIVATE = 0x0086;
        public const int WM_MOUSEACTIVATE = 0x0021;
        public const int WA_ACTIVE = 1;
        public const int WA_INACTIVE = 0;
        public const int HTCLIENT = 1;

        public const int WM_QUERYDRAGICON = 0x37;
        public const int WM_GETICON = 0x007F;
        public const int ICON_SMALL = 0;
        public const int ICON_BIG = 1;
        public const int ICON_SMALL2 = 2;

        public const int GCL_HICON = -14;
        public const int GCL_HICONSM = -34;

        public const int DISPLAY_STARTUP = 305;
        public const int DISPLAY_RUN_DIALOG = 401;
        public const int WINS_ARRANGE_HRZ = 404;
        public const int WINS_ARRANGE_VRT = 405;
        public const int WINS_MIN_ALL = 415;
        public const int WINS_MAX_ALL = 416;
        public const int SHOW_DESKTOP = 419;
        public const int SHOW_TASKMNG = 420;
        public const int CONTROL_PANEL = 505;
        public const int TURN_OFF_DIALOG = 506;

        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_CLOSE = 0xF060;
        public const int SC_RESTORE = 0xF120;

        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_NORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;//Visible but no activate
        public const int SW_RESTORE = 9;
        public const int SW_SHOWDEFAULT = 10;
        public const int SW_FORCEMINIMIZE = 11;
        public const int SW_MAX = 11;

        public const int WA_PREVIOUS = 40044;
        public const int WA_PLAY = 40045;
        public const int WA_PAUSE = 40046;
        public const int WA_STOP = 40047;
        public const int WA_NEXT = 40048;
        public const int WA_CLOSE = 40001;

        #endregion Constants

    }
}
