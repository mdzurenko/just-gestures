using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using JustGestures.Features;
using MouseCore;

namespace JustGestures.ControlItems
{
    public class CrossCursorMode : PictureBox
    {
        [DllImport("user32.dll")]
        static extern bool SetSystemCursor(IntPtr hcur, uint id);

        [DllImport("user32.dll")]
        static extern IntPtr GetCursor();

        [DllImport("user32.dll")]
        static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        uint OCR_NORMAL = 32512;

        Image m_cursorImage;
        IntPtr m_hCurOld;
        static ExtraMouseHook m_mouse;
        Form m_parentForm;
        bool m_crossActive = false;

        public delegate void DlgApplicationChanged(IntPtr hWnd);
        public DlgApplicationChanged ApplicationChanged;

        private void OnApplicationChanged(IntPtr hWnd)
        {
            if (ApplicationChanged != null)
                ApplicationChanged(hWnd);
        }

        delegate void SetParentTopMostCallBack(bool topmost);

        public CrossCursorMode()
        {
            m_cursorImage = Properties.Resources.my_cursor_image.ToBitmap(); //LoadAniCursor.GetImageOfCursor("RotatingCross");
            this.Image = m_cursorImage;
            this.Width = m_cursorImage.Width;
            this.Height = m_cursorImage.Height;
        }



        public void SetCapture()
        {            
            m_mouse = MyEngine.MouseEngine;
            m_mouse.AppUnderCrossChanged += new ExtraMouseHook.DlgAppUnderCrossChanged(OnApplicationChanged);
            m_mouse.CrossLeftMouseUp += new ExtraMouseHook.DlgEmptyDelegate(LeftButtonUp);
            m_hCurOld = Cursor.CopyHandle();
            m_parentForm = this.FindForm();
        }

        //private void AppUnderCrossChanged(IntPtr hwnd)
        //{
        //    string path = TypeOfAction.AppGroupControl.GetPathFromHwnd(hwnd);
        //    OnApplicationChanged(path);
        //}

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_crossActive = true;
                SetParentTopMost(true);
                this.Image = null;
                Cursor crossCursor = LoadAniCursor.Load("RotatingCross");
                SetSystemCursor(crossCursor.Handle, OCR_NORMAL);
                m_mouse.StartCrossMode();                
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && m_crossActive)
            {
                LeftButtonUp();
            }
        }

        private void LeftButtonUp()
        {
            if (m_crossActive)
            {
                m_crossActive = false;
                SetParentTopMost(false);
                SetSystemCursor(m_hCurOld, OCR_NORMAL);
                this.Image = m_cursorImage;
                m_hCurOld = Cursor.CopyHandle();
                m_mouse.StopCrossMode();
            }
        }

        public void ReleaseCapture()
        {
            if (m_mouse != null)
            {
                SetParentTopMost(false);
                SetSystemCursor(m_hCurOld, OCR_NORMAL);
                this.Image = m_cursorImage;
                LoadAniCursor.ReleaseCursor("RotatingCross");
                m_mouse.StopCrossMode();
                m_mouse.AppUnderCrossChanged -= new ExtraMouseHook.DlgAppUnderCrossChanged(OnApplicationChanged);
                m_mouse.CrossLeftMouseUp -= new ExtraMouseHook.DlgEmptyDelegate(LeftButtonUp);
            }
        }

        private void SetParentTopMost(bool topmost)
        {
            if (m_parentForm == null) return;

            if (m_parentForm.InvokeRequired)
            {
                SetParentTopMostCallBack setTopmost = new SetParentTopMostCallBack(SetParentTopMost);
                m_parentForm.Invoke(setTopmost, new object[] { topmost });
            }
            else
            {
                m_parentForm.TopMost = topmost;
            }
        }
    }
}
