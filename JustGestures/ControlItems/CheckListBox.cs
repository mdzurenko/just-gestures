using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices;

namespace JustGestures.ControlItems
{
    class CheckListBox : ListView
    {
        private const int ILD_TRANSPARENT = 0x00000001;
        private const int ILD_BLEND25 = 0x00000002;
        private const int ILD_BLEND50 = 0x00000004;
        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        private static extern bool ImageList_Draw(IntPtr himl, int i, IntPtr hdcDst, int x, int y, int fStyle);

        ListViewItem m_hoverItem = null;
        private int m_maxItems = 6;
       
        public CheckListBox()
        {
            this.DoubleBuffered = true;
            this.BorderStyle = BorderStyle.None;
            this.View = View.Details;
            this.HeaderStyle = ColumnHeaderStyle.None;
            this.Columns.Add("column");
            this.CheckBoxes = true;
            this.FullRowSelect = true;
            this.OwnerDraw = true;
            this.Height = 16;
        }

        public void AddItem(string id, string caption)
        {
            ListViewItem item = new ListViewItem(caption, id);
            item.Name = id;
            this.Items.Add(item);
            if (this.Items.Count <= m_maxItems)
            {
                this.Height = this.Items.Count * (this.SmallImageList.ImageSize.Height + 1);
            }
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            Rectangle r = e.Bounds;
            if (r.Width > this.ClientSize.Width) r.Width = this.ClientSize.Width;
            Graphics g = e.Graphics;
            r.X += 1; //3
            r.Width -= 2;
            int offset = 0;
            bool isSelected = e.Item == m_hoverItem;
            Brush backBrush = isSelected ? SystemBrushes.Highlight : new SolidBrush(this.BackColor);
            g.FillRectangle(backBrush, r);            
            if (this.CheckBoxes)
            {
                CheckBoxState boxState = e.Item.Checked ? CheckBoxState.CheckedNormal : CheckBoxState.UncheckedNormal;
                CheckBoxRenderer.DrawCheckBox(g, new Point(r.X + 3, r.Y + (r.Height / 2) - 6), boxState);
                offset = CheckBoxRenderer.GetGlyphSize(g, boxState).Width + 6;
                r.X += offset;
                r.Width -= offset;
            }
            int imgFlags = ILD_TRANSPARENT;
            if (isSelected)
                imgFlags |= ILD_BLEND25;
            int imgIndex = this.SmallImageList.Images.IndexOfKey(e.Item.ImageKey);
            bool result = ImageList_Draw(this.SmallImageList.Handle, imgIndex, g.GetHdc(), r.X, r.Y, imgFlags);
            g.ReleaseHdc();
            offset = this.SmallImageList.ImageSize.Width;
            r.X += offset;
            r.Width -= offset;
            string txt = e.Item.SubItems[0].Text;
            Color backColor = isSelected ? SystemColors.Highlight : Color.Transparent;
            Color foreColor = isSelected ? SystemColors.HighlightText : this.ForeColor;
            TextFormatFlags textFlags = TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix |
                TextFormatFlags.VerticalCenter | TextFormatFlags.PreserveGraphicsTranslateTransform |
                TextFormatFlags.SingleLine;

            TextRenderer.DrawText(g, txt, this.Font, r, foreColor, backColor, textFlags);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            ListViewItem item = this.GetItemAt(e.X, e.Y);
            if (m_hoverItem != item)
            {
                if (m_hoverItem != null)
                    this.Invalidate(m_hoverItem.Bounds);
                if (item != null)
                    this.Invalidate(item.Bounds);
                m_hoverItem = item;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (m_hoverItem != null)
            {
                Rectangle r = m_hoverItem.Bounds;
                m_hoverItem = null;
                this.Invalidate(r);
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0201: // WM_LBUTTONDOWN
                case 0x0204: // WM_RBUTTONDOWN
                case 0x0203: // WM_LBUTTONDBLCLK
                case 0x0206: // WM_RBUTTONDBLCLK
                    int x = m.LParam.ToInt32() & 0xFFFF;
                    int y = (m.LParam.ToInt32() >> 16) & 0xFFFF;
                    ListViewHitTestInfo hti = this.HitTest(x, y);
                    if (hti.Item != null)
                    {
                        hti.Item.Checked = !hti.Item.Checked;
                        return;
                    }                   
                    break;           
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {

            base.OnClientSizeChanged(e);
            if (this.Visible)
            {
                this.Columns[0].Width = this.ClientSize.Width;
            }
        }
        
    }
}
