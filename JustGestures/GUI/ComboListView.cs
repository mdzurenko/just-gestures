using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using JustGestures.Languages;

namespace JustGestures.GUI
{
    public class ComboListView : DragAndDropListView, ITranslation
    {
        #region Win32


        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        private static extern bool ImageList_Draw(IntPtr himl, int i, IntPtr hdcDst, int x, int y, int fStyle);

        private const int ILD_NORMAL = 0x00000000;
        private const int ILD_TRANSPARENT = 0x00000001;
        private const int ILD_MASK = 0x00000010;
        private const int ILD_IMAGE = 0x00000020;
        private const int ILD_BLEND25 = 0x00000002;
        private const int ILD_BLEND50 = 0x00000004;

        #endregion #Win32

        ContextMenuStrip cms_options;
        ListViewItem m_listItem = null;
        int m_columnIndex = 0;
        int OFFSET = 5;
        ListViewItem m_comboItem;
        int m_comboColumnIndex = 1;
        ComboBox cB_execution = new ComboBox();
        List<string> m_cbItems = null;
        List<MyGesture> m_gestures = null;
        List<string> m_selectedGestures = null;


        public delegate void DlgComboIndexChanged(ListViewItem item, ExecuteType comboValue);
        public DlgComboIndexChanged ComboIndexChanged;

        private void OnComboIndexChanged(ListViewItem item, ExecuteType comboValue)
        {
            if (ComboIndexChanged != null)
                ComboIndexChanged(item, comboValue);
        }

        #region ITranslation Members

        public void Translate()
        {
            for (int i = 0; i < m_gestures.Count; i++)
            {
                this.Items[i].SubItems[1].Text = m_gestures[i].PriorityName;
            }
            for (int i = 0; i < ConvertValue.PrioritiesCount; i++)
                m_cbItems[i] = Translation.GetPriorityText(ConvertValue.IndexToPriority(i));
            cms_options.Items[0].Text = Translation.GetText("MW_cMI_changePriority");
        }

        #endregion

        public ComboListView()
        {
            // set comboBoxLanguages (comboBox1)
            m_gestures = new List<MyGesture>();
            m_selectedGestures = new List<string>();
            this.DoubleBuffered = true;
            OwnerDraw = true;

            cms_options = new ContextMenuStrip();
            cms_options.ShowImageMargin = false;
            cms_options.Items.Add("ChangePriority");
            cms_options.ItemClicked += new ToolStripItemClickedEventHandler(cms_options_ItemClicked);                 

            m_cbItems = new List<string>();
            for (int i = 0; i < ConvertValue.PrioritiesCount; i++)
                m_cbItems.Add(ConvertValue.IndexToPriority(i).ToString());

            Translate();
           
            cB_execution.Items.AddRange(m_cbItems.ToArray());
            cB_execution.Size = new System.Drawing.Size(0, 0);
            cB_execution.Location = new System.Drawing.Point(0, 0);
            cB_execution.FlatStyle = FlatStyle.Flat;
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.cB_execution });
            cB_execution.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSelectedIndexChanged);
            cB_execution.LostFocus += new System.EventHandler(this.ComboBoxFocusExit);
            cB_execution.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBoxKeyPress);
            cB_execution.DropDownStyle = ComboBoxStyle.DropDownList;        
            cB_execution.Hide();            
            
            
        }
       
        public void SetMatchedItems(List<MyGesture> gestures)
        {
            if (gestures == null) return;
            bool same = true;
            if (gestures.Count == m_gestures.Count)
            {
                for (int i = 0; i < gestures.Count; i++)
                {
                    if (gestures[i].ID != m_gestures[i].ID)
                    {
                        same = false;
                        break;
                    }
                }
            }
            else 
                same = false;
            
            if (!same)
            {
                m_gestures = new List<MyGesture>(gestures.ToArray());
                this.Items.Clear();
                //m_gestures.Clear();
                //m_gestures.AddRange(gestures.ToArray());
                for (int i = 0; i < m_gestures.Count; i++)
                    this.Items.Add(m_gestures[i].ToSameListItem());
            }
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);

            // reorder also collection of gestures according to items in list
            List<MyGesture> gestures = new List<MyGesture>();
            foreach (ListViewItem item in this.Items)
            {
                MyGesture gest = null;
                for (int i = 0; i < m_gestures.Count; i++)
                {
                    if (m_gestures[i].ID == item.Tag.ToString())
                    {
                        gest = m_gestures[i];
                        break;
                    }
                }
                gestures.Add(gest);
            }
            m_gestures = gestures;
        }

        public void DrawFocusRectangleAround(List<MyGesture> gestures)
        {
            List<string> temp = new List<string>(m_selectedGestures.ToArray());            
            m_selectedGestures = new List<string>();

            foreach (string key in temp)
                if (this.Items.ContainsKey(key))
                    this.Invalidate(this.Items[key].Bounds);

            foreach (MyGesture gest in gestures)
            {
                m_selectedGestures.Add(gest.ID);
                if (this.Items.ContainsKey(gest.ID))
                    this.Invalidate(this.Items[gest.ID].Bounds);
            }
        }
        
        private void ComboBoxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 27)
            {
                cB_execution.Hide();
            }
        }

        private void ComboBoxSelectedIndexChanged(object sender, System.EventArgs e)
        {
            int index = cB_execution.SelectedIndex;
            if (index >= 0)
            {
                string str = cB_execution.Items[index].ToString();
                m_comboItem.SubItems[m_comboColumnIndex].Text = str;
                OnComboIndexChanged(m_comboItem, ConvertValue.IndexToPriority(index));
            }
        }
            

        private void ComboBoxFocusExit(object sender, System.EventArgs e)
        {
            cB_execution.Hide();
        }


        private void SetComboParams()
        {
            int x = m_comboItem.Position.X;
            for (int i = 0; i < m_comboColumnIndex; i++)
                x += this.Columns[i].Width;

            Rectangle r = new Rectangle(x, m_comboItem.Bounds.Top, this.Columns[m_comboColumnIndex].Width, m_comboItem.Bounds.Height);
            cB_execution.Size = new Size(r.Width, r.Height - 1);
            cB_execution.Location = new Point(r.X - 2, r.Top);
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            if (cB_execution != null && cB_execution.Visible)
            {            
                SetComboParams();             
            }
            e.DrawDefault = false;
            //e.DrawFocusRectangle();
            //if (e.Item.SubItems[m_comboColumnIndex].Text == ExecuteType.Implicit.ToString())
            //    e.Item.BackColor = Color.Red;
            //else if (e.Item.SubItems[m_comboColumnIndex].Text == ExecuteType.ImplicitIfUnique.ToString())
            //    e.Item.BackColor = Color.Orange;
            //else
            //    e.Item.BackColor = Color.Green;
            //if (!e.Item.Checked) e.Item.ForeColor = SystemColors.GrayText;
            //e.Graphics.FillRectangle(new SolidBrush(e.Item.BackColor), e.Bounds);
            //e.DrawDefault = true;
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            m_columnIndex = e.ColumnIndex;
            m_listItem = e.Item;
            Rectangle r = e.Bounds;
            Graphics g = e.Graphics;

            if (m_listItem == m_comboItem && m_comboColumnIndex == m_columnIndex &&
                cB_execution != null && cB_execution.Visible)
            {
                e.DrawDefault = false;
                return;
            }

            Color backColor = SystemColors.Highlight;
            if (!m_listItem.Selected)
            {
                //if (m_listItem.SubItems[m_comboColumnIndex].Text == ExecuteType.Implicit.ToString())
                ExecuteType priority = m_gestures[m_listItem.Index].ExecutionType;
                if (priority == ExecuteType.Implicit)
                {
                    if (m_listItem.Checked) //IsActive(m_gestures[m_listItem.Index]))
                        backColor = Color.FromArgb(228, 148, 148);
                    else
                        backColor = Color.FromArgb(238, 187, 187);
                }
                else if (priority == ExecuteType.ImplicitIfUnique)
                {
                    if (m_listItem.Checked)//IsActive(m_gestures[m_listItem.Index]))
                        backColor = Color.FromArgb(228, 228, 148);
                    else
                        backColor = Color.FromArgb(238, 238, 187);
                }
                else
                {
                    if (m_listItem.Checked)//IsActive(m_gestures[m_listItem.Index]))
                        backColor = Color.FromArgb(148, 228, 148);
                    else
                        backColor = Color.FromArgb(187, 238, 187);
                }
            }

            g.FillRectangle(new SolidBrush(backColor), r);

            if (m_columnIndex == 0)
            {
                r.X += OFFSET;
                r.Width -= OFFSET;
            }

            if (m_columnIndex == 0)
            {
                r.X += 1; //3
                r.Width -= 1;
            }
            DrawImageAndText(g, r);

            if (m_selectedGestures.Contains(e.Item.Name))
                ControlPaint.DrawFocusRectangle(e.Graphics, e.Item.Bounds);
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        protected void DrawImageAndText(Graphics g, Rectangle r)
        {
            int offset = 0;            
            if (m_columnIndex == 0)
            {
                int imgIndex = SmallImageList.Images.IndexOfKey(m_listItem.ImageKey);
                Rectangle r2 = new Rectangle(r.X, r.Y, r.Width, r.Height);
                int flags = ILD_TRANSPARENT;
                if (m_listItem.Selected)
                    flags |= ILD_BLEND25;
                if (m_listItem.Checked)
                {
                    bool result = ImageList_Draw(SmallImageList.Handle, imgIndex, g.GetHdc(), r2.X, r2.Y, flags);
                    g.ReleaseHdc();
                }
                else
                    ControlPaint.DrawImageDisabled(g, SmallImageList.Images[imgIndex], r2.X, r2.Y, Color.Transparent);
                offset = SmallImageList.ImageSize.Width;
            }
            r.X += offset;
            r.Width -= offset;
            offset = this.DrawText(g, r, m_listItem.SubItems[m_columnIndex].Text);
        }


        private int DrawText(Graphics g, Rectangle r, String txt)
        {
            Color backColor = Color.Transparent;
            Color foreColor = this.ForeColor;
            if (m_listItem.Selected)
            {
                backColor = SystemColors.Highlight;//this.BackColor;
                foreColor = SystemColors.HighlightText;
            }
            if (m_listItem != null && !m_listItem.Checked)//!IsActive(m_gestures[m_listItem.Index])) //!m_listItem.Checked)
            {
                foreColor = SystemColors.GrayText;
            }

            TextFormatFlags flags = TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix |
                TextFormatFlags.VerticalCenter | TextFormatFlags.PreserveGraphicsTranslateTransform |
                TextFormatFlags.SingleLine;

            TextRenderer.DrawText(g, txt, this.Font, r, foreColor, backColor, flags);
            Size size = TextRenderer.MeasureText(txt, this.Font);
            //if (m_columnIndex == 0)
            //{
            //    size = TextRenderer.MeasureText(txt, this.Font);
            //    if (r.Width > size.Width)
            //        r.Width = size.Width;
            //    .DrawFocusRectangle(r);
            //}
            return size.Width;
        }

        private bool IsActive(MyGesture gesture)
        {
            return (gesture.Active && gesture.AppGroup.Active);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {            
            m_comboItem = this.GetItemAt(e.X, e.Y);
            if (m_comboItem == null) return;
            if (e.Button == MouseButtons.Left)
            {
                SetComboParams();
                if (!cB_execution.Bounds.Contains(new Point(e.X, e.Y))) return;
                ShowComboBox();
            }
            else if (e.Button == MouseButtons.Right && this.SelectedItems.Count == 1)
            {
                cms_options.Show(this, e.Location);
            }
        }

        void cms_options_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            SetComboParams();
            ShowComboBox();
            cB_execution.DroppedDown = true;
        }

        private void ShowComboBox()
        {        
            cB_execution.Items.Clear();
            if (!m_gestures[m_comboItem.Index].IsImplicitOnly)
                cB_execution.Items.AddRange(m_cbItems.ToArray());
            else
                cB_execution.Items.Add(m_cbItems[0]);
            cB_execution.Show();
            //cB_execution.Text = m_comboItem.SubItems[m_comboColumnIndex].Text;
            cB_execution.SelectedIndex = ConvertValue.PriorityToIndex(m_gestures[m_comboItem.Index].ExecutionType);
            cB_execution.SelectAll();
            cB_execution.Focus();
        }

     
    }
}
