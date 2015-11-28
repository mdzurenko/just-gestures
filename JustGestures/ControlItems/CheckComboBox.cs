using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Diagnostics;

namespace JustGestures.ControlItems
{
    class CheckComboBox : ComboBox
    {
        ToolStripDropDown m_dropDown;
        bool m_droppedDown = false;
        CheckListBox m_checkListBox;
        DateTime m_lastHideTime = DateTime.Now;
        string m_text = string.Empty;

        public ImageList ImageList { set { m_checkListBox.SmallImageList = value; } }

        public event ItemCheckedEventHandler ItemChecked;
        private void OnItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (ItemChecked != null)
                ItemChecked(sender, e);
        }

        public ListView.ListViewItemCollection ListItems { get { return m_checkListBox.Items; } }
        public ListView.CheckedListViewItemCollection CheckedListItems { get { return m_checkListBox.CheckedItems; } }


        public CheckComboBox()
            : base()
        {
            m_checkListBox = new CheckListBox();
            m_checkListBox.Dock = DockStyle.Fill;            
            m_checkListBox.ItemChecked += new ItemCheckedEventHandler(m_checkListBox_ItemChecked);
            m_dropDown = new ToolStripDropDown();
            m_dropDown.Margin = new Padding(0);
            m_dropDown.Padding = new Padding(1);
            m_dropDown.Items.Add(new ToolStripControlHost(m_checkListBox));
            m_dropDown.Closing += new ToolStripDropDownClosingEventHandler(m_dropDown_Closing);            
        }



        void m_checkListBox_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            CheckListItems();
            OnItemChecked(sender, e);
            //this.Text = m_text;
        }

        public void CheckListItems()
        {
            m_text = string.Empty;
            this.Items.Clear();
            foreach (ListViewItem item in m_checkListBox.CheckedItems)
                m_text += item.Text + ", ";
            if (m_text != string.Empty)
            {
                m_text = m_text.Substring(0, m_text.Length - 2);
                TextFormatFlags textFlags = TextFormatFlags.EndEllipsis | TextFormatFlags.ModifyString;
                TextRenderer.MeasureText(m_text, this.Font, new Size(this.ClientSize.Width - 15, 0), textFlags);
                this.Items.Add(m_text);
                this.SelectedIndex = 0;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            m_checkListBox.Width = this.ClientSize.Width - 2;
        }


        void m_dropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            m_droppedDown = false;
            m_lastHideTime = DateTime.Now;
        }

        public void AddItem(string id, string caption)
        {
            m_checkListBox.AddItem(id, caption);
        }

        public virtual void ShowDropDown()
        {
            if (!m_droppedDown)
            {
                m_dropDown.Show(this, 0, this.Height);
                m_droppedDown = true;
                m_checkListBox.Focus();
                m_sShowTime = DateTime.Now;
            }
        }

        public virtual void HideDropDown()
        {
            if (m_droppedDown)
            {
                m_dropDown.Hide();
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
            if (m_dropDown.Visible)
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
