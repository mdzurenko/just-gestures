using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JustGestures.Properties;

namespace JustGestures.GUI
{
    public partial class StartButton : PictureBox
    {
        ContextMenuStrip m_contextMenu;
        bool m_droppedDown = false;
        bool m_mouseHover = false;
        DateTime m_lastHideTime = DateTime.Now;

        public StartButton()
        {
            PictureBox pb = new PictureBox();

            //m_contextMenu = new ContextMenuStrip();
            //m_contextMenu.ImageScalingSize = new Size(16, 16);
            //m_contextMenu.BackColor = Color.White;

            //m_contextMenu.Renderer = new MyContextMenuRenderer();            
            //m_contextMenu.LayoutCompleted += new EventHandler(m_contextMenu_LayoutCompleted);
            //m_contextMenu.Closing += new ToolStripDropDownClosingEventHandler(m_contextMenu_Closing);
            //m_contextMenu.Items.Add("Add Gesture", Resources.add_gesture.ToBitmap());
            //m_contextMenu.Items.Add("Add Application Group", Resources.app_add.ToBitmap());            
            //m_contextMenu.Items.Add("Options", Resources.folder);
            //m_contextMenu.Items.Add(new ToolStripSeparator());
            //m_contextMenu.Items.Add("Exit", Resources.logo16x16.ToBitmap());

            //for (int i = 0; i < m_contextMenu.Items.Count; i++)
            //{
            //    if (m_contextMenu.Items[i] is ToolStripMenuItem)
            //    {
            //        ToolStripMenuItem item = (ToolStripMenuItem)m_contextMenu.Items[i];
            //        item.TextAlign = ContentAlignment.MiddleLeft;
            //        item.ImageAlign = ContentAlignment.MiddleRight;

            //        if (i == 0)
            //            item.Margin = new Padding(0, 5, 0, 0);
            //        else if (i == m_contextMenu.Items.Count - 1)
            //        {
            //            item.Margin = new Padding(0, 0, 0, 4);
            //            item.Enabled = false;
            //        }
            //    }

            //}
            
            this.Image = Resources.start_normal;
            this.Width = this.Image.Width;
            this.Height = this.Image.Height;
            this.BackColor = Color.Transparent;
        }

        public void LoadContextMenu(ContextMenuStrip startMenu)
        {
            m_contextMenu = startMenu;
            m_contextMenu.BackColor = Color.White;
            m_contextMenu.Renderer = new MyContextMenuRenderer();
            m_contextMenu.LayoutCompleted += new EventHandler(m_contextMenu_LayoutCompleted);
            m_contextMenu.Closing += new ToolStripDropDownClosingEventHandler(m_contextMenu_Closing);
            for (int i = 0; i < m_contextMenu.Items.Count; i++)
            {
                if (m_contextMenu.Items[i] is ToolStripMenuItem)
                {
                    ToolStripMenuItem item = (ToolStripMenuItem)m_contextMenu.Items[i];
                    item.TextAlign = ContentAlignment.MiddleLeft;
                    item.ImageAlign = ContentAlignment.MiddleRight;

                    if (i == 0)
                        item.Margin = new Padding(0, 5, 0, 0);
                    else if (i == m_contextMenu.Items.Count - 1)
                    {
                        item.Margin = new Padding(0, 0, 0, 4);
                    }
                }
            }
        }
       
        void m_contextMenu_LayoutCompleted(object sender, EventArgs e)
        {
            Rectangle r = m_contextMenu.ClientRectangle;
            int d = 10;
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            gp.CloseFigure();
            m_contextMenu.Region = new Region(gp);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            m_mouseHover = true;
            if (!m_droppedDown)
                this.Image = Resources.start_hover;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            m_mouseHover = false;
            if (!m_droppedDown)
                this.Image = Resources.start_normal;
            base.OnMouseLeave(e);
        }

        void m_contextMenu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (m_mouseHover)
                this.Image = Resources.start_hover;
            else
                this.Image = Resources.start_normal;
            m_droppedDown = false;
            m_lastHideTime = DateTime.Now;
        }


        void m_dropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (m_mouseHover)
                this.Image = Resources.start_hover;
            else
                this.Image = Resources.start_normal;
            m_droppedDown = false;
            m_lastHideTime = DateTime.Now;
        }

        public virtual void ShowDropDown()
        {
            if (!m_droppedDown)
            {
                this.Image = Resources.start_pushed;
                m_contextMenu.Show(this, 0, this.Height);
                m_droppedDown = true;
                m_sShowTime = DateTime.Now;
            }
        }

        public virtual void HideDropDown()
        {
            if (m_droppedDown)
            {
                m_contextMenu.Hide();
                m_droppedDown = false;
            }
        }

        public const uint WM_COMMAND = 0x0111;
        public const uint WM_USER = 0x0400;
        public const uint WM_REFLECT = WM_USER + 0x1C00;
        public const uint WM_LBUTTONDOWN = 0x0201;
        public const uint WM_LBUTTONDBLCLK = 0x0203;

        public const uint CBN_DROPDOWN = 7;
        public const uint CBN_CLOSEUP = 8;

        public static uint HIWORD(int n)
        {
            return (uint)(n >> 16) & 0xffff;
        }

        private static DateTime m_sShowTime = DateTime.Now;

        private void AutoDropDown()
        {
            if (m_contextMenu.Visible)
                HideDropDown();
            else if ((DateTime.Now - m_lastHideTime).Milliseconds > 50)
                ShowDropDown();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDOWN || m.Msg == WM_LBUTTONDBLCLK)
            {
                AutoDropDown();
                return;
            }

            if (m.Msg == (WM_REFLECT + WM_COMMAND))
            {
                switch (HIWORD((int)m.WParam))
                {
                    case CBN_DROPDOWN:
                        AutoDropDown();
                        return;

                    case CBN_CLOSEUP:
                        HideDropDown();
                        return;
                }
            }
            base.WndProc(ref m);
        }


    }
}
