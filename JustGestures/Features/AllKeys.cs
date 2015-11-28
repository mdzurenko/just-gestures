using System;
using System.Collections.Generic;
using System.Text;

namespace JustGestures.Features
{
    struct AllKeys
    {   

        #region KEY INICIALIZATION
        public static MyKey KEY_LBUTTON = new MyKey(0x01, "Left mouse button");
        public static MyKey KEY_RBUTTON = new MyKey(0x02, "Right mouse button");
        public static MyKey KEY_CANCEL = new MyKey(0x03, "Control-break processing");
        public static MyKey KEY_MBUTTON = new MyKey(0x04, "Middle mouse button");
        public static MyKey KEY_XBUTTON1 = new MyKey(0x05, "X1 mouse button");
        public static MyKey KEY_XBUTTON2 = new MyKey(0x06, "X2 mouse button");
        // - (0x07) Undefined
        public static MyKey KEY_BACK = new MyKey(0x08, "BACKSPACE");
        public static MyKey KEY_TAB = new MyKey(0x09, "TAB");
        //- (0x0A-0B) Reserved
        public static MyKey KEY_CLEAR = new MyKey(0x0C, "CLEAR");
        public static MyKey KEY_RETURN = new MyKey(0x0D, "ENTER");
        //- (0x0E-0F) Undefined
        public static MyKey KEY_SHIFT = new MyKey(0x10, "Shift");
        public static MyKey KEY_CONTROL = new MyKey(0x11, "Ctrl");
        public static MyKey KEY_MENU = new MyKey(0x12, "Alt");
        public static MyKey KEY_PAUSE = new MyKey(0x13, "PAUSE");
        public static MyKey KEY_CAPITAL = new MyKey(0x14, "CAPS LOCK");
        public static MyKey KEY_KANA = new MyKey(0x15, "Input Method Editor (IME) Kana mode");
        public static MyKey KEY_HANGUEL = new MyKey(0x15, "IME Hanguel mode (maintained for compatibility; use VK_HANGUL)");
        public static MyKey KEY_HANGUL = new MyKey(0x15, "IME Hangul mode");
        //- (0x16) Undefined
        public static MyKey KEY_JUNJA = new MyKey(0x17, "IME Junja mode");
        public static MyKey KEY_FINAL = new MyKey(0x18, "IME final mode");
        public static MyKey KEY_HANJA = new MyKey(0x19, "IME Hanja mode");
        public static MyKey KEY_KANJI = new MyKey(0x19, "IME Kanji mode");
        //- (0x1A) Undefined
        public static MyKey KEY_ESCAPE = new MyKey(0x1B, "ESC");
        public static MyKey KEY_CONVERT = new MyKey(0x1C, "IME convert");
        public static MyKey KEY_NONCONVERT = new MyKey(0x1D, "IME nonconvert");
        public static MyKey KEY_ACCEPT = new MyKey(0x1E, "IME accept");
        public static MyKey KEY_MODECHANGE = new MyKey(0x1F, "IME mode change request");
        public static MyKey KEY_SPACE = new MyKey(0x20, "SPACEBAR");
        public static MyKey KEY_PRIOR = new MyKey(0x21, "PAGE UP");
        public static MyKey KEY_NEXT = new MyKey(0x22, "PAGE DOWN");
        public static MyKey KEY_END = new MyKey(0x23, "END");
        public static MyKey KEY_HOME = new MyKey(0x24, "HOME");
        public static MyKey KEY_LEFT = new MyKey(0x25, "LEFT ARROW");
        public static MyKey KEY_UP = new MyKey(0x26, "UP ARROW");
        public static MyKey KEY_RIGHT = new MyKey(0x27, "RIGHT ARROW");
        public static MyKey KEY_DOWN = new MyKey(0x28, "DOWN ARROW");
        public static MyKey KEY_SELECT = new MyKey(0x29, "SELECT");
        public static MyKey KEY_PRINT = new MyKey(0x2A, "PRINT");
        public static MyKey KEY_EXECUTE = new MyKey(0x2B, "EXECUTE");
        public static MyKey KEY_SNAPSHOT = new MyKey(0x2C, "PRINT SCREEN");
        public static MyKey KEY_INSERT = new MyKey(0x2D, "INS");
        public static MyKey KEY_DELETE = new MyKey(0x2E, "DEL");
        public static MyKey KEY_HELP = new MyKey(0x2F, "HELP");
        public static MyKey KEY_0 = new MyKey(0x30, "0");
        public static MyKey KEY_1 = new MyKey(0x31, "1");
        public static MyKey KEY_2 = new MyKey(0x32, "2");
        public static MyKey KEY_3 = new MyKey(0x33, "3");
        public static MyKey KEY_4 = new MyKey(0x34, "4");
        public static MyKey KEY_5 = new MyKey(0x35, "5");
        public static MyKey KEY_6 = new MyKey(0x36, "6");
        public static MyKey KEY_7 = new MyKey(0x37, "7");
        public static MyKey KEY_8 = new MyKey(0x38, "8");
        public static MyKey KEY_9 = new MyKey(0x39, "9");
        //- (0x3A-40) Undefined
        public static MyKey KEY_A = new MyKey(0x41, "A");
        public static MyKey KEY_B = new MyKey(0x42, "B");
        public static MyKey KEY_C = new MyKey(0x43, "C");
        public static MyKey KEY_D = new MyKey(0x44, "D");
        public static MyKey KEY_E = new MyKey(0x45, "E");
        public static MyKey KEY_F = new MyKey(0x46, "F");
        public static MyKey KEY_G = new MyKey(0x47, "G");
        public static MyKey KEY_H = new MyKey(0x48, "H");
        public static MyKey KEY_I = new MyKey(0x49, "I");
        public static MyKey KEY_J = new MyKey(0x4A, "J");
        public static MyKey KEY_K = new MyKey(0x4B, "K");
        public static MyKey KEY_L = new MyKey(0x4C, "L");
        public static MyKey KEY_M = new MyKey(0x4D, "M");
        public static MyKey KEY_N = new MyKey(0x4E, "N");
        public static MyKey KEY_O = new MyKey(0x4F, "O");
        public static MyKey KEY_P = new MyKey(0x50, "P");
        public static MyKey KEY_Q = new MyKey(0x51, "Q");
        public static MyKey KEY_R = new MyKey(0x52, "R");
        public static MyKey KEY_S = new MyKey(0x53, "S");
        public static MyKey KEY_T = new MyKey(0x54, "T");
        public static MyKey KEY_U = new MyKey(0x55, "U");
        public static MyKey KEY_V = new MyKey(0x56, "V");
        public static MyKey KEY_W = new MyKey(0x57, "W");
        public static MyKey KEY_X = new MyKey(0x58, "X");
        public static MyKey KEY_Y = new MyKey(0x59, "Y");
        public static MyKey KEY_Z = new MyKey(0x5A, "Z");
        public static MyKey KEY_LWIN = new MyKey(0x5B, "Left Windows key");
        public static MyKey KEY_RWIN = new MyKey(0x5C, "Right Windows key");
        public static MyKey KEY_APPS = new MyKey(0x5D, "APPLICATIONS KEY");
        //- (0x5E) Reserved
        public static MyKey KEY_SLEEP = new MyKey(0x5F, "Computer Sleep");
        public static MyKey KEY_NUMPAD0 = new MyKey(0x60, "NUM 0");
        public static MyKey KEY_NUMPAD1 = new MyKey(0x61, "NUM 1");
        public static MyKey KEY_NUMPAD2 = new MyKey(0x62, "NUM 2");
        public static MyKey KEY_NUMPAD3 = new MyKey(0x63, "NUM 3");
        public static MyKey KEY_NUMPAD4 = new MyKey(0x64, "NUM 4");
        public static MyKey KEY_NUMPAD5 = new MyKey(0x65, "NUM 5");
        public static MyKey KEY_NUMPAD6 = new MyKey(0x66, "NUM 6");
        public static MyKey KEY_NUMPAD7 = new MyKey(0x67, "NUM 7");
        public static MyKey KEY_NUMPAD8 = new MyKey(0x68, "NUM 8");
        public static MyKey KEY_NUMPAD9 = new MyKey(0x69, "NUM 9");
        public static MyKey KEY_MULTIPLY = new MyKey(0x6A, "NUM *");
        public static MyKey KEY_ADD = new MyKey(0x6B, "NUM +");
        public static MyKey KEY_SEPARATOR = new MyKey(0x6C, "Separator");
        public static MyKey KEY_SUBTRACT = new MyKey(0x6D, "NUM -");
        public static MyKey KEY_DECIMAL = new MyKey(0x6E, "NUM .");
        public static MyKey KEY_DIVIDE = new MyKey(0x6F, "NUM /");
        public static MyKey KEY_F1 = new MyKey(0x70, "F1");
        public static MyKey KEY_F2 = new MyKey(0x71, "F2");
        public static MyKey KEY_F3 = new MyKey(0x72, "F3");
        public static MyKey KEY_F4 = new MyKey(0x73, "F4");
        public static MyKey KEY_F5 = new MyKey(0x74, "F5");
        public static MyKey KEY_F6 = new MyKey(0x75, "F6");
        public static MyKey KEY_F7 = new MyKey(0x76, "F7");
        public static MyKey KEY_F8 = new MyKey(0x77, "F8");
        public static MyKey KEY_F9 = new MyKey(0x78, "F9");
        public static MyKey KEY_F10 = new MyKey(0x79, "F10");
        public static MyKey KEY_F11 = new MyKey(0x7A, "F11");
        public static MyKey KEY_F12 = new MyKey(0x7B, "F12");
        public static MyKey KEY_F13 = new MyKey(0x7C, "F13");
        public static MyKey KEY_F14 = new MyKey(0x7D, "F14");
        public static MyKey KEY_F15 = new MyKey(0x7E, "F15");
        public static MyKey KEY_F16 = new MyKey(0x7F, "F16");
        //public static Key KEY_F17 = new Key(0x80H, "F17");
        //public static Key KEY_F18 = new Key(0x81H, "F18");
        //public static Key KEY_F19 = new Key(0x82H, "F19");
        //public static Key KEY_F20 = new Key(0x83H, "F20");
        //public static Key KEY_F21 = new Key(0x84H, "F21");
        //public static Key KEY_F22 = new Key(0x85H, "F22");
        //public static Key KEY_F23 = new Key(0x86H, "F23");
        //public static Key KEY_F24 = new Key(0x87H, "F24");
        //- (0x88-8F) Unassigned
        public static MyKey KEY_NUMLOCK = new MyKey(0x90, "NUM LOCK");
        public static MyKey KEY_SCROLL = new MyKey(0x91, "SCROLL LOCK");
        //- (0x92-96) OEM specific
        //- (0x97-9F) // Unassigned
        public static MyKey KEY_LSHIFT = new MyKey(0xA0, "Left SHIFT");
        public static MyKey KEY_RSHIFT = new MyKey(0xA1, "Right SHIFT");
        public static MyKey KEY_LCONTROL = new MyKey(0xA2, "Left CONTROL");
        public static MyKey KEY_RCONTROL = new MyKey(0xA3, "Right CONTROL");
        public static MyKey KEY_LMENU = new MyKey(0xA4, "Left MENU");
        public static MyKey KEY_RMENU = new MyKey(0xA5, "Right MENU");
        public static MyKey KEY_BROWSER_BACK = new MyKey(0xA6, "Browser Back");
        public static MyKey KEY_BROWSER_FORWARD = new MyKey(0xA7, "Browser Forward");
        public static MyKey KEY_BROWSER_REFRESH = new MyKey(0xA8, "Browser Refresh");
        public static MyKey KEY_BROWSER_STOP = new MyKey(0xA9, "Browser Stop");
        public static MyKey KEY_BROWSER_SEARCH = new MyKey(0xAA, "Browser Search");
        public static MyKey KEY_BROWSER_FAVORITES = new MyKey(0xAB, "Browser Favorites");
        public static MyKey KEY_BROWSER_HOME = new MyKey(0xAC, "Browser Start and Home");
        public static MyKey KEY_VOLUME_MUTE = new MyKey(0xAD, "Volume Mute");
        public static MyKey KEY_VOLUME_DOWN = new MyKey(0xAE, "Volume Down");
        public static MyKey KEY_VOLUME_UP = new MyKey(0xAF, "Volume Up");
        public static MyKey KEY_MEDIA_NEXT_TRACK = new MyKey(0xB0, "Next Track");
        public static MyKey KEY_MEDIA_PREV_TRACK = new MyKey(0xB1, "Previous Track");
        public static MyKey KEY_MEDIA_STOP = new MyKey(0xB2, "Stop Media");
        public static MyKey KEY_MEDIA_PLAY_PAUSE = new MyKey(0xB3, "Play/Pause Media");
        public static MyKey KEY_LAUNCH_MAIL = new MyKey(0xB4, "Start Mai");
        public static MyKey KEY_LAUNCH_MEDIA_SELECT = new MyKey(0xB5, "Select Media");
        public static MyKey KEY_LAUNCH_APP1 = new MyKey(0xB6, "Start Application 1");
        public static MyKey KEY_LAUNCH_APP2 = new MyKey(0xB7, "Start Application 2");
        //- (0xB8-B9) Reserved
        public static MyKey KEY_OEM_1 = new MyKey(0xBA, ";"); //;:
        public static MyKey KEY_OEM_PLUS = new MyKey(0xBB, "="); //=+
        public static MyKey KEY_OEM_COMMA = new MyKey(0xBC, ","); //,<
        public static MyKey KEY_OEM_MINUS = new MyKey(0xBD, "-"); //-_
        public static MyKey KEY_OEM_PERIOD = new MyKey(0xBE, "."); //.>
        public static MyKey KEY_OEM_2 = new MyKey(0xBF, "/"); // /?
        public static MyKey KEY_OEM_3 = new MyKey(0xC0, "`"); //`~
        //- (0xC1-D7) Reserved
        //- (0xD8-DA) Unassigned
        public static MyKey KEY_OEM_4 = new MyKey(0xDB, "["); // [{
        public static MyKey KEY_OEM_5 = new MyKey(0xDC, "\\"); // \|
        public static MyKey KEY_OEM_6 = new MyKey(0xDD, "]"); // ]}
        public static MyKey KEY_OEM_7 = new MyKey(0xDE, "'"); // '"
        public static MyKey KEY_OEM_8 = new MyKey(0xDF, "Used for miscellaneous characters; it can vary by keyboard");
        //- (0xE0) Reserved
        //- (0xE1) OEM specific
        public static MyKey KEY_OEM_102 = new MyKey(0xE2, "angle bracket key");// or the backslash key on the RT 102-key keyboard
        //- (0xE3-E4) OEM specific
        public static MyKey KEY_PROCESSKEY = new MyKey(0xE5, "IME PROCESS");
        //- (0xE6) OEM specific 
        public static MyKey KEY_PACKET = new MyKey(0xE7, "Windows 2000/XP: Used to pass Unicode characters as if they were keystrokes. The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods. For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP");
        //- (0xE8) Unassigned
        //- (0xE9-F5) OEM specific
        public static MyKey KEY_ATTN = new MyKey(0xF6, "Attn");
        public static MyKey KEY_CRSEL = new MyKey(0xF7, "CrSel");
        public static MyKey KEY_EXSEL = new MyKey(0xF8, "ExSel");
        public static MyKey KEY_EREOF = new MyKey(0xF9, "Erase EOF");
        public static MyKey KEY_PLAY = new MyKey(0xFA, "Play");
        public static MyKey KEY_ZOOM = new MyKey(0xFB, "Zoom");
        public static MyKey KEY_NONAME = new MyKey(0xFC, "Reserved"); 
        public static MyKey KEY_PA1 = new MyKey(0xFD, "PA");
        public static MyKey KEY_OEM_CLEAR = new MyKey(0xFE, "Clear");
        #endregion KEY INICIALIZATION

        private static List<MyKey> m_ordinary = new List<MyKey>
            (new MyKey[] {
                KEY_A,
                KEY_B,
                KEY_C,            
                KEY_D,
                KEY_E,
                KEY_F,
                KEY_G,
                KEY_H,
                KEY_I,
                KEY_J,
                KEY_K,
                KEY_L,
                KEY_M,
                KEY_N,
                KEY_O,
                KEY_P,
                KEY_Q,
                KEY_R,
                KEY_S,
                KEY_T,
                KEY_U,
                KEY_V,
                KEY_W,
                KEY_X,
                KEY_Y,
                KEY_Z,
                KEY_0,
                KEY_1,
                KEY_2,
                KEY_3, 
                KEY_4,
                KEY_5,
                KEY_6,
                KEY_7,
                KEY_8,
                KEY_9,
                KEY_LEFT,
                KEY_UP,
                KEY_RIGHT,
                KEY_DOWN,
                KEY_TAB,
                KEY_CAPITAL,
                KEY_BACK,
                KEY_RETURN,            
                KEY_SPACE,
                KEY_APPS,
                KEY_ESCAPE,
                KEY_F1,
                KEY_F2,
                KEY_F3,
                KEY_F4,
                KEY_F5,
                KEY_F6,
                KEY_F7,
                KEY_F8,
                KEY_F9,
                KEY_F10,
                KEY_F11,
                KEY_F12,
                KEY_PRINT,
                KEY_SCROLL,
                KEY_PAUSE,
                KEY_INSERT,
                KEY_DELETE,
                KEY_HOME,
                KEY_END,
                KEY_PRIOR,
                KEY_NEXT,
                KEY_NUMPAD0,
                KEY_NUMPAD1,
                KEY_NUMPAD2,
                KEY_NUMPAD3,
                KEY_NUMPAD4,
                KEY_NUMPAD5,
                KEY_NUMPAD6,
                KEY_NUMPAD7,
                KEY_NUMPAD8,
                KEY_NUMPAD9,
                KEY_NUMLOCK,
                KEY_DIVIDE,
                KEY_MULTIPLY,
                KEY_SUBTRACT,
                KEY_ADD,
                KEY_DECIMAL,
                KEY_OEM_3,
                KEY_OEM_MINUS,
                KEY_OEM_PLUS,
                KEY_OEM_5,
                KEY_OEM_4,
                KEY_OEM_6,
                KEY_OEM_1,
                KEY_OEM_7,
                KEY_OEM_COMMA,
                KEY_OEM_PERIOD,
                KEY_OEM_2,
            });

        private const string DOWN = " (DOWN)";
        private const string UP = " (UP)";

        public const string STR_WIN_DOWN = "Windows Key" + DOWN;
        public const string STR_WIN_UP = "Windows Key" + UP;
        public const string STR_SHIFT_DOWN = "Shift" + DOWN;
        public const string STR_SHIFT_UP = "Shift" + UP;
        public const string STR_CONTROL_DOWN = "Ctrl" + DOWN;
        public const string STR_CONTROL_UP = "Ctrl" + UP;
        public const string STR_MENU_DOWN = "Alt" + DOWN;
        public const string STR_MENU_UP = "Alt" + UP;

        public static MyKey KEY_WIN_DOWN = new MyKey(0x5B, STR_WIN_DOWN, MyKey.Action.KeyDown);
        public static MyKey KEY_WIN_UP = new MyKey(0x5B, STR_WIN_UP, MyKey.Action.KeyUp);
        public static MyKey KEY_SHIFT_DOWN = new MyKey(0x10, STR_SHIFT_DOWN, MyKey.Action.KeyDown);
        public static MyKey KEY_SHIFT_UP = new MyKey(0x10, STR_SHIFT_UP, MyKey.Action.KeyUp);
        public static MyKey KEY_CONTROL_DOWN = new MyKey(0x11, STR_CONTROL_DOWN, MyKey.Action.KeyDown);
        public static MyKey KEY_CONTROL_UP = new MyKey(0x11, STR_CONTROL_UP, MyKey.Action.KeyUp);
        public static MyKey KEY_MENU_DOWN = new MyKey(0x12, STR_MENU_DOWN, MyKey.Action.KeyDown);
        public static MyKey KEY_MENU_UP = new MyKey(0x12, STR_MENU_UP, MyKey.Action.KeyUp);
        

        private static List<MyKey> m_modifiers = new List<MyKey>
            (new MyKey[] {
                KEY_WIN_DOWN,
                KEY_WIN_UP,
                KEY_SHIFT_DOWN,
                KEY_SHIFT_UP,
                KEY_CONTROL_DOWN,
                KEY_CONTROL_UP,
                KEY_MENU_DOWN,
                KEY_MENU_UP
             });

        public static MyKey MOUSE_LBUTTON_CLICK = new MyKey(Win32.MOUSEEVENTF_LEFTDOWN | Win32.MOUSEEVENTF_LEFTUP, "Left Button Click", MyKey.Action.MouseClick);
        public static MyKey MOUSE_RBUTTON_CLICK = new MyKey(Win32.MOUSEEVENTF_RIGHTDOWN | Win32.MOUSEEVENTF_RIGHTUP, "Right Button Click", MyKey.Action.MouseClick);
        public static MyKey MOUSE_MBUTTON_CLICK = new MyKey(Win32.MOUSEEVENTF_MIDDLEDOWN | Win32.MOUSEEVENTF_MIDDLEUP, "Middle Button Click", MyKey.Action.MouseClick);
        public static MyKey MOUSE_XBUTTON1_CLICK = new MyKey(Win32.MOUSEEVENTF_XDOWN | Win32.MOUSEEVENTF_XUP, "X1 Button Click", MyKey.Action.MouseX1Click);
        public static MyKey MOUSE_XBUTTON2_CLICK = new MyKey(Win32.MOUSEEVENTF_XDOWN | Win32.MOUSEEVENTF_XUP, "X2 Button Click", MyKey.Action.MouseX2Click);

        public static MyKey MOUSE_LBUTTON_DBLCLICK = new MyKey(Win32.MOUSEEVENTF_LEFTDOWN | Win32.MOUSEEVENTF_LEFTUP, "Left Button Double Click", MyKey.Action.MouseDblClick);
        public static MyKey MOUSE_RBUTTON_DBLCLICK = new MyKey(Win32.MOUSEEVENTF_RIGHTDOWN | Win32.MOUSEEVENTF_RIGHTUP, "Right Button Double Click", MyKey.Action.MouseDblClick);
        public static MyKey MOUSE_MBUTTON_DBLCLICK = new MyKey(Win32.MOUSEEVENTF_MIDDLEDOWN | Win32.MOUSEEVENTF_MIDDLEUP, "Middle Button Double Click", MyKey.Action.MouseDblClick);
        public static MyKey MOUSE_XBUTTON1_DBLCLICK = new MyKey(Win32.MOUSEEVENTF_XDOWN | Win32.MOUSEEVENTF_XUP, "X1 Button Double Click", MyKey.Action.MouseX1DblClick);
        public static MyKey MOUSE_XBUTTON2_DBLCLICK = new MyKey(Win32.MOUSEEVENTF_XDOWN | Win32.MOUSEEVENTF_XUP, "X2 Button Double Click", MyKey.Action.MouseX2DblClick);

        public static MyKey MOUSE_WHEEL_UP = new MyKey(Win32.MOUSEEVENTF_WHEEL, "Mouse Wheel Up", MyKey.Action.MouseWheelUp);
        public static MyKey MOUSE_WHEEL_DOWN = new MyKey(Win32.MOUSEEVENTF_WHEEL, "Mouse Wheel Down", MyKey.Action.MouseWheelDown);

        private static List<MyKey> m_mouseActions = new List<MyKey>
            (new MyKey[] {     
                MOUSE_LBUTTON_CLICK,
                MOUSE_LBUTTON_DBLCLICK,
                MOUSE_RBUTTON_CLICK,
                MOUSE_RBUTTON_DBLCLICK,
                MOUSE_MBUTTON_CLICK,
                MOUSE_MBUTTON_DBLCLICK,
                MOUSE_XBUTTON1_CLICK,
                MOUSE_XBUTTON1_DBLCLICK,
                MOUSE_XBUTTON2_CLICK,
                MOUSE_XBUTTON2_DBLCLICK,
                MOUSE_WHEEL_UP,
                MOUSE_WHEEL_DOWN
             });

        public static List<MyKey> Modifiers
        {
            get { return m_modifiers; }
        }

        public static List<MyKey> Ordinary
        {
            get { return m_ordinary; }
        }

        public static List<MyKey> MouseActions
        {
            get { return m_mouseActions; }
        }
    }
}
