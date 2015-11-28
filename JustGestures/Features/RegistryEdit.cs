using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace JustGestures.Features
{
    /// <summary>
    /// Saving program to the system registers so it will be started on Windows StartUp
    /// </summary>
    class RegistryEdit
    {
        private static RegistryKey key;

        public const string REG_PATH_AUTORUN = @"Software\Microsoft\Windows\CurrentVersion\Run";
        public const string REG_PATH_WIN7 = @"Control Panel\Desktop\";
        public const string JUST_GESTURES = "Just Gestures";
        public const string LOW_LVL_HOOK = "LowLevelHooksTimeout";

        private static RegistryKey getKey(int Section)
        {
            switch (Section)
            {
                case 1:
                    // HKEY_LOCALMACHINE
                    key = Registry.LocalMachine;
                    break;
                case 2:
                    // current user
                    key = Registry.CurrentUser;
                    break;
                case 3:
                    // classes root
                    key = Registry.ClassesRoot;
                    break;
                case 4:
                    // users
                    key = Registry.Users;
                    break;
                default:
                    key = Registry.LocalMachine;
                    break;
            }
            return key;
        }
        private static object getValue(int Section, string Location, string Name)
        {
            key = getKey(Section);
            object val;
            try
            {
                key = key.OpenSubKey(Location);
                val = key.GetValue(Name);
                return val;
            }
            catch
            {
                return null;
                //return "error " + e.Message;
                //throw new Exception("Key not Found");
            }
            finally
            {
                key.Close();
            }
        }
        private static bool setValue(int Section, string Location, string Name, object Value)
        {
            key = getKey(Section);
            try
            {
                key = key.OpenSubKey(Location, true);
                key.SetValue(Name, Value);
            }
            catch
            {
                return false;
            }
            finally
            {
                key.Close();
            }
            return true;
        }
        private static bool deleteValue(int Section, string Location, string Name)
        {
            key = getKey(Section);
            try
            {
                key = key.OpenSubKey(Location, true);
                key.DeleteValue(Name, false);
            }
            catch
            {
                return false;
            }
            finally
            {
                key.Close();
            }
            return true;
        }

        public static bool RegRun(int Section, string Name, string Path)
        {
            return setValue(Section, REG_PATH_AUTORUN, Name, Path);
        }

        public static bool RemoveRegRun(int Section, string Name)
        {
            return deleteValue(Section, REG_PATH_AUTORUN, Name);
        }
        
        /// <summary>
        /// Register JG to the registry
        /// </summary>
        /// <returns></returns>
        public static bool SetAutoStart()
        {
            string path = System.Windows.Forms.Application.ExecutablePath;
            return setValue(2, REG_PATH_AUTORUN, JUST_GESTURES, path);
        }

        /// <summary>
        /// Remove JG from the registry
        /// </summary>
        /// <returns></returns>
        public static bool RemoveAutoStart()
        {
            string path = System.Windows.Forms.Application.ExecutablePath;
            return deleteValue(2, REG_PATH_AUTORUN, JUST_GESTURES);
        }

        /// <summary>
        /// Check whether is JG in the registry
        /// </summary>
        /// <returns></returns>
        public static string GetAutoStartValue()
        {
            object path = getValue(2, REG_PATH_AUTORUN, JUST_GESTURES);
            return path == null ? null : path.ToString();
        }

        public static bool SetWin7AntiBug()
        {
            return setValue(2, REG_PATH_WIN7, LOW_LVL_HOOK, 1000);
        }

        /// <summary>
        /// Remove JG from the registry
        /// </summary>
        /// <returns></returns>
        public static bool RemWin7AntiBug()
        {
            return deleteValue(2, REG_PATH_WIN7, LOW_LVL_HOOK);
        }

        /// <summary>
        /// Check whether is JG in the registry
        /// </summary>
        /// <returns></returns>
        public static string GetWin7BugValue()
        {
            object path = getValue(2, REG_PATH_WIN7, LOW_LVL_HOOK);
            return path == null ? null : path.ToString();
        }
    }
}
