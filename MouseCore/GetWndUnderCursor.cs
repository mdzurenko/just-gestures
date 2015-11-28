using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MouseCore
{
    class GetWndUnderCursor
    {
        private const int COUNT_OF_TRY = 10; //amount of tries till the correct window handle is received
        public List<List<IntPtr>> allWndHandle;
        public List<string> allScndWndHandle;
        IntPtr m_mainWindow;
        IntPtr m_window;

        #region Zlozity sposob ako ziskat, konkretne okno pod kurzorom (pomale)

        public GetWndUnderCursor()
        {
            allWndHandle = new List<List<IntPtr>>();
            allScndWndHandle = new List<string>();
            m_mainWindow = IntPtr.Zero;
        }        

        /// <summary>
        /// Returns a list of child windows
        /// </summary>
        /// <param name="parent">Parent of the windows to return</param>
        /// <returns>List of child windows</returns>
        private static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                Win32.EnumWindowProc childProc = new Win32.EnumWindowProc(EnumWindow);
                Win32.EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }

        /// <summary>
        /// Callback method to be used when enumerating windows.
        /// </summary>
        /// <param name="handle">Handle of the next window</param>
        /// <param name="pointer">Pointer to a GCHandle that holds a reference to the list to fill</param>
        /// <returns>True to continue the enumeration, false to bail</returns>
        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
            List<IntPtr> list = gch.Target as List<IntPtr>;
            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<Int>");
            }
            list.Add(handle);
            //  You can modify this to check to see if you want to cancel the operation, then return a null here
            return true;
        }

        private bool Result(IntPtr hwnd, IntPtr lParam)
        {
            List<IntPtr> results = GetChildWindows(hwnd);

            allWndHandle.Add(results);
            StringBuilder strbuild = new StringBuilder(256);
            Win32.GetWindowText(hwnd, strbuild, 256);
            allScndWndHandle.Add(hwnd.ToString() + " " + strbuild.ToString());
                        
            if (results.Find(
                delegate(IntPtr wnd)
                {
                    if (wnd == m_window)
                        return true;
                    else
                        return false;
                }) != IntPtr.Zero || hwnd == m_window)
            {
                StringBuilder mainWin = new StringBuilder(256);
                StringBuilder scndWin = new StringBuilder(256);
                Win32.GetWindowText(hwnd, mainWin, 256);
                Win32.GetWindowText(m_window, scndWin, 256);
                m_mainWindow = hwnd;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// Getting the main window under the cursor, if Zero original handle is returned
        /// </summary>
        /// <param name="point">Cursor location</param>
        /// <returns>Window Hwnd</returns>
        public static IntPtr FromPoint(Point point)
        {
            StringBuilder wndText = new StringBuilder(256);
            Random rnd = new Random();
            IntPtr originalHandle = IntPtr.Zero;
            IntPtr mainWindow = IntPtr.Zero;            
            IntPtr window = IntPtr.Zero;

            originalHandle = Win32.WindowFromPoint(point);
            
            // sometimes it happens that window is not possible to get from the mouse location, near cursor location must be tried as well
            for (int i = 0; i < COUNT_OF_TRY; i++)
            {
                
                window = Win32.WindowFromPoint(point);
                //mainWindow = Win32.GetAncestor(window, Win32.GA_ROOTOWNER);
                //Win32.GetWindowText((int)mainWindow, wndText, 256);
                //Debug.WriteLine(string.Format("ROOTOWNER: {0}, Caption: '{1}', X: {2}, Y: {3}", mainWindow, wndText, point.X, point.Y));  
                //mainWindow = Win32.GetAncestor(window, Win32.GA_PARENT);
                //Win32.GetWindowText((int)mainWindow, wndText, 256);
                //Debug.WriteLine(string.Format("PARENT: {0}, Caption: '{1}', X: {2}, Y: {3}", mainWindow, wndText, point.X, point.Y));

                mainWindow = Win32.GetAncestor(window, Win32.GA_ROOT);
                Win32.GetWindowText(mainWindow, wndText, 256);
                
                //Debug.WriteLine(string.Format("ROOT: {0}, Caption: '{1}', X: {2}, Y: {3}", mainWindow, wndText, point.X, point.Y));
                //Debug.WriteLine(string.Format("Hwnd from Cursor: {0} class name: {1}", window, nameClass));
                //Win32.EnumWindowProc enumProc = new Win32.EnumWindowProc(Result);
                //Win32.EnumWindows(enumProc, IntPtr.Zero);
                //Win32.GetWindowText((int)mainWindow, wndText, 256);
                //Debug.WriteLine(string.Format("Used ENUMwnds: {0}, Caption: '{1}', X: {2}, Y: {3}", mainWindow, wndText, point.X, point.Y));                
                if (wndText.Length != 0) break;
                //Debug.WriteLine(string.Format("Another try to get window, number: {0}", i + 1));
                int xDeviation = rnd.Next(2, 5);
                int yDeviation = rnd.Next(2, 5);
                
                point = new Point(point.X + xDeviation, point.Y + yDeviation);
            }
            if (mainWindow != IntPtr.Zero)
                return mainWindow;
            else
                return originalHandle;            
        }
    }
}
