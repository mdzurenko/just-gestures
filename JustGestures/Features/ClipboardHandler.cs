using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Runtime.InteropServices;

namespace JustGestures.Features
{
    public class ClipboardHandler
    {

        #region Win32 Imports

        [DllImport("user32.dll")]
        private static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        public static extern bool EmptyClipboard();

        [DllImport("user32.dll")]
        private static extern IntPtr GetClipboardData(uint uFormat);

        [DllImport("user32.dll")]
        private static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

        [DllImport("user32.dll")]
        private static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        private static extern uint EnumClipboardFormats(uint format);

        [DllImport("user32.dll")]
        private static extern int GetClipboardFormatName(uint format, [Out] StringBuilder lpszFormatName, int cchMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint RegisterClipboardFormat(string lpszFormat);


        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = false)]
        private static extern void CopyMemory(IntPtr dest, IntPtr src, int size);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GlobalUnlock(IntPtr hMem);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GlobalFree(IntPtr hMem);

        [DllImport("kernel32.dll")]
        private static extern UIntPtr GlobalSize(IntPtr hMem);

        private const uint GMEM_DDESHARE = 0x2000;
        private const uint GMEM_MOVEABLE = 0x2;

        private static uint CF_TEXT = 1;
        private static uint CF_UNICODETEXT = 13;

        #endregion Win32 Imports

        /// <summary>
        /// Empty the Clipboard and Restore to system clipboard data contained in a collection of ClipData objects
        /// </summary>
        /// <param name="clipData">The collection of ClipData containing data stored from clipboard</param>
        /// <returns></returns>    
        public static bool SetClipboard(ReadOnlyCollection<ClipData> clipData)
        {
            //Open clipboard to allow its manipultaion
            if (!OpenClipboard(IntPtr.Zero))
                return false;

            //Clear the clipboard
            EmptyClipboard();
            if (clipData == null || clipData.Count == 0) return false;

            //Get an Enumerator to iterate into each ClipData contained into the collection
            IEnumerator<ClipData> cData = clipData.GetEnumerator();
            while (cData.MoveNext())
            {
                ClipData cd = cData.Current;

                //Get the pointer for inserting the buffer data into the clipboard
                IntPtr alloc = GlobalAlloc(GMEM_MOVEABLE | GMEM_DDESHARE, cd.Size);
                IntPtr gLock = GlobalLock(alloc);

                //Clopy the buffer of the ClipData into the clipboard
                if ((int)cd.Size > 0)
                {
                    Marshal.Copy(cd.Buffer, 0, gLock, cd.Buffer.GetLength(0));
                }
                else
                {
                }
                //Release pointers 
                GlobalUnlock(alloc);
                SetClipboardData(cd.Format, alloc);
            };
            //Close the clipboard to realese unused resources
            CloseClipboard();
            return true;
        }


        /// <summary>
        /// Convert to a ClipData collection all data present in the clipboard
        /// </summary>
        /// <returns></returns>
        public static ReadOnlyCollection<ClipData> GetClipboard()
        {
            //Init a list of ClipData, which will contain each Clipboard Data
            List<ClipData> clipData = new List<ClipData>();

            //Open Clipboard to allow us to read from it
            if (!OpenClipboard(IntPtr.Zero))
                return new ReadOnlyCollection<ClipData>(clipData);

            //Loop for each clipboard data type
            uint format = 0;
            while ((format = EnumClipboardFormats(format)) != 0)
            {
                //Check if clipboard data type is recognized, and get its name
                string formatName = "0";
                ClipData cd;
                if (format > 14)
                {
                    StringBuilder res = new StringBuilder();
                    if (GetClipboardFormatName(format, res, 200) > 0)
                    {
                        formatName = res.ToString();
                    }

                }
                //Get the pointer for the current Clipboard Data 
                IntPtr pos = GetClipboardData(format);
                //Goto next if it's unreachable
                if (pos == IntPtr.Zero)
                    continue;
                //Get the clipboard buffer data properties
                UIntPtr lenght = GlobalSize(pos);
                IntPtr gLock = GlobalLock(pos);
                byte[] buffer;
                if ((int)lenght > 0)
                {
                    //Init a buffer which will contain the clipboard data
                    buffer = new byte[(int)lenght];
                    int l = Convert.ToInt32(lenght.ToString());
                    //Copy data from clipboard to our byte[] buffer
                    Marshal.Copy(gLock, buffer, 0, l);
                }
                else
                {
                    buffer = new byte[0];
                }
                //Create a ClipData object that represtens current clipboard data
                cd = new ClipData(format, formatName, buffer);
                cd.FormatName = formatName;
                //Add current Clipboard Data to the list


                clipData.Add(cd);
            }
            //Close the clipboard and realese unused resources
            CloseClipboard();
            //Returns the list of Clipboard Datas as a ReadOnlyCollection of ClipData
            return new ReadOnlyCollection<ClipData>(clipData);
        }

        public static string GetClipboardText()
        {
            if (!OpenClipboard(IntPtr.Zero))
                return string.Empty;

            IntPtr pos = GetClipboardData(CF_UNICODETEXT);

            if (pos != IntPtr.Zero)
            {
                CloseClipboard();
                return string.Empty;
            }                            

            UIntPtr lenght = GlobalSize(pos);
            IntPtr gLock = GlobalLock(pos);
            byte[] buffer = null;
            if ((int)lenght > 0)
            {
                //Init a buffer which will contain the clipboard data
                buffer = new byte[(int)lenght];
                int l = Convert.ToInt32(lenght.ToString());
                //Copy data from clipboard to our byte[] buffer
                Marshal.Copy(gLock, buffer, 0, l);
            }
            string text = string.Empty;
            if (buffer != null)
                 //text = System.Text.Encoding.ASCII.GetString(buffer);
                text = System.Text.Encoding.Unicode.GetString(buffer);
            CloseClipboard();

            return text;
        }


    }
}
