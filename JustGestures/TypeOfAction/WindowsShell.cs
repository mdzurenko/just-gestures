using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using JustGestures.Properties;
using System.Drawing;
using Microsoft.Win32;
using JustGestures.GestureParts;

namespace JustGestures.TypeOfAction
{
    [Serializable]
    class WindowsShell : BaseActionClass
    {

        #region Win32 Imports

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        [DllImport("user32.dll")]
        public static extern void LockWorkStation();

        [DllImport("kernel32.dll", ExactSpelling = true)]
        static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,
        ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        static extern bool ExitWindowsEx(int flg, int rea);

        const int SE_PRIVILEGE_ENABLED = 0x00000002;
        const int TOKEN_QUERY = 0x00000008;
        const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
        const int EWX_LOGOFF = 0x00000000;
        const int EWX_SHUTDOWN = 0x00000001;
        const int EWX_REBOOT = 0x00000002;
        const int EWX_FORCE = 0x00000004;
        const int EWX_POWEROFF = 0x00000008;
        const int EWX_FORCEIFHUNG = 0x00000010;

        #endregion Win32 Imports

        public const string NAME = "shell_name";

        public const string SHELL_SHUT_DOWN_DIALOG = "shell_dialog";
        public const string SHELL_SHUT_DOWN = "shell_shut_down";
        public const string SHELL_LOG_OFF = "shell_log_off";
        public const string SHELL_LOCK = "shell_lock";
        public const string SHELL_RESTART = "shell_restart";
        public const string SHELL_HIBERNATE = "shell_hibernate";
        public const string SHELL_SLEEP = "shell_sleep";
        public const string SHELL_SHOW_DESKTOP = "shell_show_desktop";
        public const string SHELL_START_PRG = "shell_start_prg";
        public const string SHELL_OPEN_FLDR = "shell_open_fldr";
        private const string SHELL_START_PROCESS = "shell_start_process";

        public const string WND_STYLE_NORMAL = "Normal";
        public const string WND_STYLE_MAXIMIZED = "Maximized";
        public const string WND_STYLE_MINIMIZED = "Minimized";

        public WindowsShell()
        {
            m_actions = new List<string>
                (new string[] { 
                    SHELL_START_PRG, 
                    SHELL_OPEN_FLDR, 
                    SHELL_SHUT_DOWN_DIALOG, 
                    SHELL_SHUT_DOWN,                      
                    SHELL_RESTART,
                    SHELL_LOG_OFF,
                    SHELL_LOCK,
                    SHELL_HIBERNATE,
                    SHELL_SLEEP,
                    SHELL_SHOW_DESKTOP
                });            
        }

        public WindowsShell(string action) : base(action) { }

        public WindowsShell(WindowsShell action) : base(action) { }

        public WindowsShell(SerializationInfo info, StreamingContext context)
            : base (info, context)
        {
        }

        public override object Clone()
        {
            return new WindowsShell(this);
        }

        public override bool IsSensitiveToMySystemWindows()
        {
            return false;
        }


        public override void ExecuteAction(IntPtr activeWnd, Point location)
        {
            Type typeShell = Type.GetTypeFromProgID("Shell.Application");
            object objShell = Activator.CreateInstance(typeShell);
            

            IntPtr lHwnd = Win32.FindWindow("Shell_TrayWnd", null);
            Process myProcess = new Process();
            string prefix = string.Empty;
            switch (this.Name)
            {
                case SHELL_OPEN_FLDR:
                    if (Directory.Exists(PathFromDetails()))
                        goto case SHELL_START_PROCESS;
                    break;
                case SHELL_START_PRG:
                    if (File.Exists(PathFromDetails()))
                        goto case SHELL_START_PROCESS;
                    break;
                case SHELL_START_PROCESS:
                    myProcess.StartInfo.FileName = prefix + PathFromDetails();
                    myProcess.StartInfo.WindowStyle = WndStyleFromDetails();
                    myProcess.StartInfo.Arguments = CmdArgumentsFromDetails();
                    myProcess.StartInfo.CreateNoWindow = false;
                    myProcess.Start();
                    break;
                case SHELL_SHUT_DOWN_DIALOG:
                    Win32.SendMessage(lHwnd, Win32.WM_COMMAND, Win32.TURN_OFF_DIALOG, 0);
                    break;
                case SHELL_SHUT_DOWN:
                    //System.Diagnostics.Process.Start("ShutDown", "/s");                    
                    DoExitWin(EWX_SHUTDOWN);
                    //Win32.ExitWindowsEx(Win32.EWX_SHUTDOWN, 0);
                    break;
                case SHELL_RESTART:
                    DoExitWin(EWX_REBOOT);
                    //Win32.ExitWindowsEx(Win32.EWX_REBOOT, 0);
                    break;
                case SHELL_LOG_OFF:
                    DoExitWin(EWX_LOGOFF);
                    //Win32.ExitWindowsEx(Win32.EWX_LOGOFF, 0);
                    break;
                case SHELL_LOCK:
                    LockWorkStation();
                    break;
                case SHELL_HIBERNATE:
                    Application.SetSuspendState(PowerState.Hibernate, true, true);
                    break;
                case SHELL_SLEEP:
                    Application.SetSuspendState(PowerState.Suspend, true, true);
                    break;
                case SHELL_SHOW_DESKTOP:
                    //// Create an instance of the shell class
                    //Shell32.ShellClass objShel = new Shell32.ShellClass();
                    //// Show the desktop
                    //((Shell32.IShellDispatch4)objShel).ToggleDesktop();//.ControlPanelItem("Main.cpl");
                    ////Win32.SendMessage(lHwnd, Win32.WM_COMMAND, Win32.SHOW_DESKTOP, 0);

                    //typeShell.InvokeMember("MinimizeAll", System.Reflection.BindingFlags.InvokeMethod, null, objShell, null);
                    typeShell.InvokeMember("ToggleDesktop", System.Reflection.BindingFlags.InvokeMethod, null, objShell, null);
                    break;
            }
        }

        public override bool IsSameType(JustGestures.TypeOfAction.BaseActionClass action)
        {
            if (!base.IsSameType(action))
                return false;
            else
            {
                switch (this.Name)
                {
                    case SHELL_START_PRG:
                    case SHELL_OPEN_FLDR:
                        return (this.Name == action.Name);
                }
                switch (action.Name)
                {
                    case SHELL_START_PRG:
                    case SHELL_OPEN_FLDR:
                        return (this.Name == action.Name);
                }
                return true;
            }
        }

        public override string GetDescription()
        {
            string str = Languages.Translation.GetText(this.Name);            
            string[] parts;

            switch (this.Name)
            {
                case SHELL_OPEN_FLDR:
                case SHELL_START_PRG:
                    parts = this.Details.Split('|');
                    str += " " + parts[0];
                    break;
            }

            return str;
        }

        public override Bitmap GetIcon(int size)
        {            
            switch (this.Name)
            {
                case NAME:
                    return Resources.shell;
                    //break;
                case SHELL_START_PRG:
                    if (this.Details != "")
                    {
                        string path = PathFromDetails();
                        if (File.Exists(path))
                            return System.Drawing.Icon.ExtractAssociatedIcon(path).ToBitmap();
                        else
                        {
                            if (size == 20)
                                return Resources.unreadable20;
                            else if (size == 24)
                                return Resources.unreadable24;
                            else
                                return Resources.unreadable32;                         
                        }
                    }
                    else
                    {
                        return Resources.application;
                        //break;
                    }
                case SHELL_OPEN_FLDR:
                    return Resources.folder_open;
                    //break;
                case SHELL_LOCK:
                    return Resources.lock2;
                case SHELL_LOG_OFF:
                    return Resources.logoff;
                    //break;
                case SHELL_SHUT_DOWN:
                    return Resources.shutdown;
                    //break;
                case SHELL_RESTART:
                    return Resources.restart;
                    //break;
                case SHELL_HIBERNATE:
                case SHELL_SLEEP:
                    return Resources.sleep;
                    //break;
                case SHELL_SHOW_DESKTOP:
                    return Resources.desktop;
                    //break;
                default:
                    return Resources.shell;
                    //break;
            }
        }

        private void DoExitWin(int flg)
        {
            bool ok;
            TokPriv1Luid tp;
            IntPtr hproc = GetCurrentProcess();
            IntPtr htok = IntPtr.Zero;
            ok = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = SE_PRIVILEGE_ENABLED;
            ok = LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);
            ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            ok = ExitWindowsEx(flg, 0);
        }

        public ProcessWindowStyle WndStyleFromDetails()
        {
            ProcessWindowStyle style = ProcessWindowStyle.Normal;
            //if (details == null) return ProcessWindowStyle.Normal;
            string[] commands = this.Details.Split('|');
            string str;
            if (commands.Length > 1)
                str = commands[1];
            else
                str = commands[0];

            switch (str)
            {
                case WND_STYLE_NORMAL: style = ProcessWindowStyle.Normal; break;
                case WND_STYLE_MINIMIZED: style = ProcessWindowStyle.Minimized; break;
                case WND_STYLE_MAXIMIZED: style = ProcessWindowStyle.Maximized; break;
            }
            return style;
        }

        public string StrWndStyleFromDetails()
        {
            ProcessWindowStyle style = this.WndStyleFromDetails();
            switch (style)
            {
                case ProcessWindowStyle.Normal: return WND_STYLE_NORMAL;
                case ProcessWindowStyle.Minimized: return WND_STYLE_MINIMIZED;
                case ProcessWindowStyle.Maximized: return WND_STYLE_MAXIMIZED;
                default: return WND_STYLE_NORMAL;
            }
        }

        public string CmdArgumentsFromDetails()
        {
            string[] commands = this.Details.Split('|');
            string str;
            if (commands.Length > 2)
                str = commands[2];
            else
                str = string.Empty;
            return str;
        }

        public string PathFromDetails()
        {
            string[] commands;
            //if (details == null) return null;
            commands = this.Details.Split('|');
            return commands[0];
        }

    }
}
