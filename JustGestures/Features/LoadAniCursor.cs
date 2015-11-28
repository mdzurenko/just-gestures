using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Resources;
using System.Drawing;



namespace JustGestures.Features
{
    class LoadAniCursor
    {
        static int RT_ANICURSOR = 21;
        //static int RT_ICON = 3;

        [DllImport("kernel32.dll")]
        static extern IntPtr FindResource(IntPtr hModule, string lpName, int lpType);
        //static extern IntPtr FindResource(IntPtr hModule, string lpName, string lpType);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr LoadResource(IntPtr hModule, IntPtr hResInfo);

        [DllImport("kernel32.dll")]
        static extern IntPtr LockResource(IntPtr hResData);
        
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern uint SizeofResource(IntPtr hModule, IntPtr hResInfo);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        static extern IntPtr LoadCursorFromFile(string lpFileName);

        [DllImport("user32.dll")]
        static extern IntPtr CreateIconFromResource(byte[] presbits, uint dwResSize, bool fIcon, uint dwVer);

        //Not working
        private static byte[] LoadFromResources(string name, int type)
        {
            //IntPtr resH1 = FindResource(IntPtr.Zero, name, type);
            //if (resH1 == IntPtr.Zero) return null;

            //IntPtr resH2 = LoadResource(IntPtr.Zero, resH1);
            //if (resH2 == IntPtr.Zero) return null;

            //IntPtr resH3 = LockResource(resH2);
            //if (resH3 == IntPtr.Zero) return null;

            //uint resSize = SizeofResource(IntPtr.Zero, resH1);

            //byte[] y = new byte[resSize];

            //byte[] y = null;
            byte[] y = Properties.Resources.RotatingCross;

            //Marshal.Copy(resH3, y, 0, (int)resSize);
            
            return y;
        }


        //Not working
        public static byte[] LoadFromResources2()
        {
            IntPtr resH1 = FindResource(IntPtr.Zero, "my_cursor.ani", RT_ANICURSOR);
            //if (resH1 == IntPtr.Zero) return null;

            //IntPtr resH2 = LoadResource(IntPtr.Zero, resH1);
            //if (resH2 == IntPtr.Zero) return null;

            //IntPtr resH3 = LockResource(resH2);
            //if (resH3 == IntPtr.Zero) return null;

            //uint resSize = SizeofResource(IntPtr.Zero, resH1);

            //byte[] y = new byte[resSize];

            //byte[] y = null;
            byte[] y = Properties.Resources.RotatingCross;

            //Marshal.Copy(resH3, y, 0, (int)resSize);

            return y;
        }
        private static byte[] LoadFromResources(string name)
        {
            ResourceManager rm = Properties.Resources.ResourceManager;
            Object cursor = rm.GetObject(name);
            byte[] buffer = (byte[])cursor;
            return buffer;
        }

        private static bool WriteToDisk(byte[] buffer, string filePath)
        {            
            try
            {
                Stream stream = File.Open(filePath, FileMode.Create);
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Load animated cursor from resources
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Cursor Load(string name)
        {
            IntPtr hCursor = IntPtr.Zero;
            byte[] buffer = LoadFromResources(name);
            if (buffer != null)
            {
                bool succeed = WriteToDisk(buffer, Config.Default.FilesLocation + name);
                if (succeed)
                {
                    hCursor = LoadCursorFromFile(Config.Default.FilesLocation + name);
                }
            }
            if (hCursor != IntPtr.Zero)
                return new Cursor(hCursor);
            else
                return null;
        }

        public static void ReleaseCursor(string name)
        {
            try
            {
                File.Delete(Config.Default.FilesLocation + name);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// This method is not working properly. Cause invalid pointer exception in some systems.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Bitmap GetImageOfCursor(string name)
        {            
            ResourceManager rm = Properties.Resources.ResourceManager;
            Object cursor = rm.GetObject(name);            
            byte[] buffer = (byte[])cursor;
            byte[] buffer2 = new byte[buffer.Length];
            buffer.CopyTo(buffer2, 0);            
            IntPtr hIcon = CreateIconFromResource(buffer2, (uint)buffer2.Length, false, 0x00030000);
            IntPtr hIconLocked = LockResource(hIcon);
            Icon icon = Icon.FromHandle(hIconLocked);
            ////Icon icon = Icon.FromHandle(hIcon);
            //Icon icon = Icon.FromHandle(hCuros);
            return icon.ToBitmap();    
        }
        

    }
}
