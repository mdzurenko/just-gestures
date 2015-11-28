using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Drawing;
using System.Windows.Forms;
using JustGestures.Properties;
using JustGestures.GestureParts;
using System.Runtime.InteropServices;

namespace JustGestures.TypeOfAction
{
    [Serializable]
    class AppGroupOptions : BaseActionClass
    {
        public const string NAME = "app_group_name";

        public const string APP_GROUP_USERS = "app_group_user";
        public const string APP_GROUP_GLOBAL = "app_group_global";
        public const string APP_GROUP_DESKTOP = "app_group_desktop";
        public const string APP_GROUP_TASKBAR = "app_group_taskbar";

        public const string SYSTEM_DESKTOP_WHOLE = "WorkerW"; //in win 7 when destkop is shown via features in taskbar
        public const string SYSTEM_DESKTOP = "Progman";
        public const string SYSTEM_TASKBAR = "Shell_TrayWnd";
        public const string SYSTEM_TASKBAR_TOOLBAR = "ToolbarWindow32"; // is part of taskbar without window's buttons (necessary for windows XP)
        public const string SYSTEM_TRAY_NOTIFY_WINDOW = "TrayNotifyWnd";
        public const string SYSTEM_TRAY_CLOCKS = "TrayClockWClass";
        public const string SYSTEM_TRAY_SHOW_DESKTOP = "TrayShowDesktopButtonWClass";
        public const string SYSTEM_START_MENU = "DV2ControlHost";
        public const string SYSTEM_TASKLIST_THUMBS = "TaskListThumbnailWnd";
        public const string SYSTEM_START_BUTTON = "Button";
        public const string SYSTEM_TASKBAR_PROGRAMS = "MSTaskListWClass"; // list of programs on taskbar
        public const string SYSTEM_TASKBAR_REBAR = "ReBarWindow32"; //keyboard/languages & widgets

        public AppGroupOptions()
        {
            m_actions = new List<string>
                (new string[] { 
                    APP_GROUP_USERS
                });            
        }

        public AppGroupOptions(string action) : base(action) { }

        public AppGroupOptions(AppGroupOptions action) : base(action) { }

        public AppGroupOptions(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override object Clone() 
        { 
            return new AppGroupOptions(this); 
        }
        public override Bitmap GetIcon(int size)
        {
            switch (this.Name)
            {
                case APP_GROUP_USERS:
                    string path = this.Details;
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
                    //break;
                case APP_GROUP_GLOBAL:
                    if (size == 20)
                        return Resources.system_global20;
                    else if (size == 24)
                        return Resources.system_global24;
                    else
                        return Resources.system_global32;
                    //break;
                case APP_GROUP_TASKBAR:
                    if (size == 20)
                        return Resources.system_taskbar20;
                    else if (size == 24)
                        return Resources.system_taskbar24;
                    else
                        return Resources.system_taskbar32;                   
                    //break;
                case APP_GROUP_DESKTOP:
                    if (size == 20)
                        return Resources.system_desktop20;
                    else if (size == 24)
                        return Resources.system_desktop24;
                    else
                        return Resources.system_desktop32; 
                    //break;
                default:
                    return base.GetIcon(size);
                    //break;
            }
        }

        public bool IsAppGroupActive(IntPtr hWnd)
        {
            StringBuilder buff = new StringBuilder(256);
            Win32.GetClassName(hWnd, buff, 256);
            string wndName = buff.ToString();            

            bool active = false;
            switch (this.Name)
            {
                case APP_GROUP_GLOBAL:
                    active = true;
                    break;
                case APP_GROUP_USERS:
                    string wndPath = GetPathFromHwnd(hWnd).ToLower();
                    string appPath = this.Details.ToLower();
                    active = (wndPath == appPath);
                    break;
                case APP_GROUP_DESKTOP:
                    active = IsDesktop(wndName);
                    break;
                case APP_GROUP_TASKBAR:
                    active = IsTaskbar(wndName);
                    break;
            }
            return active;
        }

        public static string GetPathFromHwnd(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) return string.Empty;
            int procId;
            Win32.GetWindowThreadProcessId(hWnd, out procId);

            // this method is much faster then the below one
            const int nChars = 1024;
            StringBuilder filenameBuffer = new StringBuilder(nChars);
            IntPtr hProcess = Win32.OpenProcess(1040, false, procId);
            Win32.GetModuleFileNameEx(hProcess, IntPtr.Zero, filenameBuffer, nChars);            
            Win32.CloseHandle(hProcess);
            string fileName = filenameBuffer.ToString();
            Debug.WriteLine(fileName);
            
            // if file doesn't exist use previous and slower method
            if (!File.Exists(fileName))
            {
                Process foregroundProc = Process.GetProcessById(procId);
                filenameBuffer = new StringBuilder(500);
                try
                {
                    Win32.GetLongPathName(foregroundProc.MainModule.FileName, filenameBuffer, 500);
                }
                catch (Exception ex) //this exception might occure on 64bit windows because of MainModule!            
                {
                    fileName = string.Empty;
                }
                if (fileName != string.Empty)
                    fileName = filenameBuffer.ToString();
                Debug.WriteLine(fileName);

            }
            return fileName;

            // this check has to be commented because the BAT! email client has this attribute Zero, but mainModule is OK
            //if (foregroundProc.MainWindowHandle == IntPtr.Zero) return string.Empty;
        }

        public static bool IsDesktop(string className)
        {
            return (className == SYSTEM_DESKTOP || className == SYSTEM_DESKTOP_WHOLE);
        }

        public static bool IsTaskbar(string className)
        {
            return (className == SYSTEM_TASKBAR || className == SYSTEM_TASKLIST_THUMBS 
                || className == SYSTEM_START_MENU || className == SYSTEM_START_BUTTON
                || className == SYSTEM_TASKBAR_PROGRAMS || className == SYSTEM_TASKBAR_REBAR
                || className == SYSTEM_TASKBAR_TOOLBAR || className == SYSTEM_TRAY_CLOCKS
                || className == SYSTEM_TRAY_SHOW_DESKTOP || className == SYSTEM_TRAY_NOTIFY_WINDOW
                );
        }

        public string ExtractPath()
        {
            return this.Details;
        }

        public string ExtractFileName()
        {
            return Path.GetFileNameWithoutExtension(ExtractPath());
        }
    }
}
