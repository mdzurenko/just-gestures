using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace JustGestures.GUI
{
    class Form_invisible : Form
    {

        public Form_invisible()
        {            
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.MaximumSize = new System.Drawing.Size(3, 3);
            this.Size = new System.Drawing.Size(3, 3);
        }

        public void ShowWindow(int x, int y)
        {
            Win32.SetWindowPos(this.Handle, Win32.HWND_TOPMOST, x - 1, y - 1, 3, 3, Win32.SWP_SHOWWINDOW);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= Win32.WS_EX_TRANSPARENT;
                cp.ExStyle |= Win32.WS_EX_TOOLWINDOW;
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }
    }
}
