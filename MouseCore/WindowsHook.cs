using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text;

namespace MouseCore
{
    #region Class HookEventArgs
    public class HookEventArgs : EventArgs
    {
        public int HookCode;	// Hook code
        public IntPtr wParam;	// WPARAM argument
        public IntPtr lParam;	// LPARAM argument
    }
    #endregion

    #region Enum HookType
    // Hook Types
    public enum HookType : int
    {
        WH_JOURNALRECORD = 0,
        WH_JOURNALPLAYBACK = 1,
        WH_KEYBOARD = 2,
        WH_GETMESSAGE = 3,
        WH_CALLWNDPROC = 4,
        WH_CBT = 5,
        WH_SYSMSGFILTER = 6,
        WH_MOUSE = 7,
        WH_HARDWARE = 8,
        WH_DEBUG = 9,
        WH_SHELL = 10,
        WH_FOREGROUNDIDLE = 11,
        WH_CALLWNDPROCRET = 12,
        WH_KEYBOARD_LL = 13,
        WH_MOUSE_LL = 14
    }
    #endregion

    #region Class WindowsHook
    public class WindowsHook
    {

        // ************************************************************************
        // Filter function delegate
        public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);
        // ************************************************************************

        // ************************************************************************
        // Internal properties  
        
        protected IntPtr m_hhook = IntPtr.Zero;
        protected HookProc m_filterFunc = null;
        protected HookType m_hookType;
        // ************************************************************************
 
        // ************************************************************************
        // Event delegate
        public delegate void HookEventHandler(object sender, HookEventArgs e);
        // ************************************************************************

        // ************************************************************************
        // Event: HookInvoked 
        public event HookEventHandler HookInvoked;
        protected void OnHookInvoked(HookEventArgs e)
        {
            if (HookInvoked != null)
                HookInvoked(this, e);
        }
        // ************************************************************************

        // ************************************************************************
        // Class constructor(s)
        public WindowsHook(HookType hook)
        {
            m_hookType = hook;
            m_filterFunc = new HookProc(this.CoreHookProc);
        }
        public WindowsHook(HookType hook, HookProc func)
        {
            m_hookType = hook;
            m_filterFunc = func;
        }

        // ************************************************************************

        // ************************************************************************
        // Default filter function
        protected int CoreHookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
                return CallNextHookEx(m_hhook, code, wParam, lParam);

            // Let clients determine what to do
            HookEventArgs e = new HookEventArgs();
            e.HookCode = code;
            e.wParam = wParam;
            e.lParam = lParam;
            OnHookInvoked(e);
            
            // Yield to the next hook in the chain
            return CallNextHookEx(m_hhook, code, wParam, lParam);            
        }
        // ************************************************************************

        // ************************************************************************
                    
        // Install the hook
        public void Install()
        {
            //StringBuilder name = new StringBuilder(512);
            //GetModuleFileName(IntPtr.Zero, name, 512);
            //IntPtr result = GetModuleHandle(name.ToString());

            int hInstance = LoadLibrary("User32");
            m_hhook = SetWindowsHookEx(m_hookType,
                m_filterFunc,
                (IntPtr)hInstance,//IntPtr.Zero, //GetModuleHandle(null),
                0//(int)AppDomain.GetCurrentThreadId()  
                );
        }
        // ************************************************************************

        // ************************************************************************
    
        // Uninstall the hook
        public void Uninstall()
        {            
            UnhookWindowsHookEx(m_hhook);
            m_hhook = IntPtr.Zero;            
        }
        // ************************************************************************

        public bool IsInstalled
        {            
            get 
            {
                return m_hhook != IntPtr.Zero; 
            }
        }

        #region Win32 Imports

        // ************************************************************************
        // Win32: SetWindowsHookEx()
        [DllImport("user32.dll")]
        protected static extern IntPtr SetWindowsHookEx(HookType code,
            HookProc func,
            IntPtr hInstance,
            int threadID);
        // ************************************************************************

        // ************************************************************************
        // Win32: UnhookWindowsHookEx()
        [DllImport("user32.dll")]
        protected static extern int UnhookWindowsHookEx(IntPtr hhook);
        // ************************************************************************

        // ************************************************************************
        // Win32: CallNextHookEx()
        [DllImport("user32.dll")]
        protected static extern int CallNextHookEx(IntPtr hhook,
            int code, IntPtr wParam, IntPtr lParam);
        // ************************************************************************

        //Used to find HWND to user32.dll
        [DllImport("kernel32")]
        public extern static int LoadLibrary(string lpLibFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [PreserveSig]
        public static extern uint GetModuleFileName
        ([In] IntPtr hModule, [Out] StringBuilder lpFilename, 
            [In] [MarshalAs(UnmanagedType.U4)] int nSize);

       

        #endregion
    }
    #endregion
}
