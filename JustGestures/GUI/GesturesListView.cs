using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Globalization;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices;

namespace JustGestures.GUI
{
    public partial class GesturesListView : ListView
    {

        #region Win32

        public const int WS_VSCROLL = 0x00200000;
        public const int WS_HSCROLL = 0x00100000;
        public const int GWL_STYLE = -16;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
 
        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        private static extern bool ImageList_Draw(IntPtr himl, int i, IntPtr hdcDst, int x, int y, int fStyle);

        [DllImport("user32.dll")]
        public static extern int SetScrollPos(IntPtr hWnd, System.Windows.Forms.Orientation nBar, int nPos, bool bRedraw);

        private const int ILD_NORMAL = 0x00000000;
        private const int ILD_TRANSPARENT = 0x00000001;
        private const int ILD_MASK = 0x00000010;
        private const int ILD_IMAGE = 0x00000020;
        private const int ILD_BLEND25 = 0x00000002;
        private const int ILD_BLEND50 = 0x00000004;

        #endregion #Win32

        public enum ViewModes
        {
            Action = 0,
            Gesture = 1
        }

        GesturesCollection m_gestures;
        /// <summary>
        /// Width of the rectangle that used for glyph
        /// </summary>
        private const int GLYPH_OFFSET = 18;
        /// <summary>
        /// Width of the glyph (is 10 when is possible to render glyph 8 otherwise)
        /// </summary>
        private int GLYPH_WIDTH = 10;
        /// <summary>
        /// Width of the check box
        /// </summary>
        private int CHECK_BOX_WIDTH = 0;
        /// <summary>
        /// Total offset for actions in categories
        /// </summary>
        private int OFFSET;
        /// <summary>
        /// Pen used for drawing the lines
        /// </summary>
        private Pen m_linePen;

        private bool m_resizeClientArea = true;
        private string m_selectedItemCurve = string.Empty;
        private string m_prevItemCurve= string.Empty;
        private ListViewItem m_listItem = null;
        private int m_columnIndex = 0;
        private ListViewItem m_hoverItem = null;
        private ViewModes m_viewMode = ViewModes.Action;
        private ImageList m_il_actions;
        private ImageList m_il_gestures;
        private int m_rowHeight = 0;
        private int m_realClientWidth = 0;
        private bool m_resizeColumn = false;
        private bool m_columnResizing = false;
        private Color m_colorGroupHover = Color.LightBlue;
        private Color m_colorGroupSelected = Color.FromArgb(135, 198, 218);

        public bool ResizeColumn { set { m_resizeColumn = value; } }
        public GesturesCollection Gestures { get { return m_gestures; } }
        public List<MyGesture> MatchedGestures { get { return m_gestures.MatchedGestures(m_selectedItemCurve); } }
        public string SelectedItemCurve { get { return m_selectedItemCurve; } }
        public ImageList ILActions { set { m_il_actions = value; } }
        public ImageList ILGestures { set { m_il_gestures = value; } }

        public ViewModes ViewMode 
        { 
            set 
            {
                this.BeginUpdate();
                //if (this.Items != null && this.Items.Count > 0)
                //    this.EnsureVisible(0);
                m_viewMode = value;
                ImageList imgList;                
                if (m_viewMode == ViewModes.Action)
                    imgList = m_il_actions;
                else
                    imgList = m_il_gestures;
                if (imgList != null)
                    m_rowHeight = imgList.ImageSize.Height;
                if (m_gestures != null)
                {
                    m_resizeColumn = true;
                    this.Invalidate();
                }
                this.SmallImageList = imgList;
                this.EndUpdate();
            } 
            get { return m_viewMode; } }
        
        public GesturesListView()
        {
            Initialization();
        }

        public GesturesListView(IContainer container)
        {
            container.Add(this);
            Initialization();
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        private void Initialization()
        {
            m_linePen = new Pen(Color.Blue, 1.0f);
            m_linePen.DashStyle = DashStyle.Dot;

            CHECK_BOX_WIDTH = CheckBoxRenderer.GetGlyphSize(this.CreateGraphics(), CheckBoxState.UncheckedNormal).Width;
            int width = CHECK_BOX_WIDTH + 6;
            width += GLYPH_OFFSET;
            OFFSET = width;// +(16 - width) / 2;
            this.DoubleBuffered = true;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        public void AddGesturesCollection(GesturesCollection gestures)
        {            
            m_gestures = gestures;
            this.ViewMode = (ViewModes)Config.User.LW_viewMode;            
            this.Items.AddRange(gestures.GetAll().ToArray());
            //m_gestures.UpdateCollapsedGroups();    

            //this.Columns[this.Columns.Count - 1].Width = -2; 
            foreach (MyGesture group in m_gestures.Groups)
                CollapseExpandGroup(this.Items[group.Index]);
            SetLastColumnWidthToEnd();
        }

        private void SetLastColumnWidthToEnd()
        {
            int width = 0;
            foreach (ColumnHeader column in this.Columns)
                width += column.Width;
            ColumnHeader lastColumn = this.Columns[this.Columns.Count - 1];
            lastColumn.Width = this.ClientSize.Width - (width - lastColumn.Width);
            //lastColumn.Width = m_realClientWidth - (width - lastColumn.Width);
        }

        private void ResetItems()
        {
            m_listItem = null;
            m_columnIndex = 0;
        }

        private bool IsGroup(ListViewItem item)
        {
            if (item == null || m_gestures == null || 
                item.Index >= m_gestures.Count || item.Index == -1) return false;
            else
                return m_gestures[item.Index].IsGroup; 
        }

        public bool IsExpanded(ListViewItem item)
        {
            return m_gestures[item.Index].IsExpanded;
        }

        public bool IsParentGroupExpanded(ListViewItem item)
        {
            if (item == null || m_gestures == null ||
                item.Index >= m_gestures.Count || item.Index == -1) return true;
            else
            {
                if (m_gestures[item.Index].IsGroup)
                    return true;
                else
                    return m_gestures[item.Index].AppGroup.IsExpanded;
            }
        }

        private bool IsLastItemInGroup(ListViewItem item)
        {
            if (item == null || m_gestures == null ||
                item.Index >= m_gestures.Count || item.Index == -1) return false;
            else
            {
                if (item.Index == m_gestures.Count - 1 || 
                    m_gestures[item.Index + 1].IsGroup)
                    return true;
                else 
                    return false;
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            if (this.SelectedIndices.Count == 0)
                m_selectedItemCurve = string.Empty;
            else if (this.SelectedIndices.Count == 1)
                m_selectedItemCurve = m_gestures[this.SelectedItems[0].Name].Activator.ID;
            else
            {
                string selectedItemCurve = m_gestures[this.SelectedItems[0].Name].Activator.ID;
                bool allSame = true;
                foreach (ListViewItem item in this.SelectedItems)
                    if (m_gestures[item.Name].Activator.ID != selectedItemCurve) allSame = false;
                if (allSame)
                    m_selectedItemCurve = selectedItemCurve;
                else
                    m_selectedItemCurve = string.Empty;
            }
            //if (m_seledtedGroup != null)
            //    GroupItemHit(m_seledtedGroup);
            foreach (MyGesture gest in m_gestures.MatchedGestures(m_selectedItemCurve))
            {
                try { this.Invalidate(gest.Bounds); }
                catch { }
            }
            foreach (MyGesture gest in m_gestures.MatchedGestures(m_prevItemCurve))
            {
                try { this.Invalidate(gest.Bounds); }
                catch { }
            }
            m_prevItemCurve = m_selectedItemCurve;
            base.OnSelectedIndexChanged(e);
        }

        protected override void OnItemChecked(ItemCheckedEventArgs e)
        {
            if (IsGroup(e.Item))
            {                
                int start, end;
                GetGroupItemsIndexes(e.Item, out start, out end);
                for (int i = start; i < end; i++)
                {
                    try { this.Invalidate(m_gestures[i].Bounds); }
                    catch { }
                }
            }
            base.OnItemChecked(e);
        }        

        protected override void OnClientSizeChanged(EventArgs e)
        {            
            //InvalidateGroupsRectangle();
            this.Invalidate();
        }

        //private void InvalidateGroupsRectangle()
        //{
        //    if (m_gestures != null)
        //        foreach (ListViewItem item in m_gestures.Groups)
        //            this.Invalidate(item.Bounds);
        //}

       
        #region Drawing

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            ResetItems();
            if (!IsGroup(e.Item))
            {                
                e.DrawDefault = false;                
            }
            else
            {
                m_columnIndex = 0;
                m_listItem = e.Item;
                Graphics g = e.Graphics;
                Rectangle r = e.Bounds;
                int x_location = r.Location.X;
                if (m_hoverItem != null && e.Item == m_hoverItem)
                {

                    Rectangle rect = r;
                    rect.Height--;
                    rect.Width--;
                    Pen outline = new Pen(Color.FromArgb(128, 128, 255), 1);
                    LinearGradientBrush b = new LinearGradientBrush(rect, Color.Transparent, m_colorGroupHover, 90);
                    if (m_hoverItem.Selected)
                    {
                        DrawRoundedRectangle(g, rect, 7, outline, Brushes.LightBlue);
                        //b = new LinearGradientBrush(rect, Color.Transparent, m_colorGroupSelected, 90);
                    }
                    else
                    {
                        DrawRoundedRectangle(g, rect, 7, outline, b);
                    }

                }
                else
                    this.DrawBackground(g, r);                
                r.X += 1; //3
                r.Width -= 1;


                int offset = this.DrawImageAndText(g, r);
                r.X += offset + 1 - x_location;
                r.Width -= offset + 1 + 10 - x_location;
                if (r.Width < 1) return;

                LinearGradientBrush brush = new LinearGradientBrush(r, Color.LightSteelBlue, Color.Transparent, LinearGradientMode.Horizontal);
                Pen pen = new Pen(brush, 1);
                g.DrawLine(pen, new Point(r.X, r.Y + r.Height / 2), new Point(r.Right - 5, r.Y + r.Height / 2));                
            }
        }


        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            ResetItems();
            if (!IsGroup(e.Item))                    
            {
                m_columnIndex = e.ColumnIndex;
                m_listItem = e.Item;
                Rectangle r = e.Bounds;
                Graphics g = e.Graphics;
                
                this.DrawBackground(g, r);
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
                this.DrawImageAndText(g, r);
            }
        }


        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            // to many complications while trying to automatically resize the the columns
            #region Not in use
            //if (m_resizeColumn)
            //{
            //    int width = this.Width - 4;
            //    int columnsWidth = 0;
            //    for (int i = 0; i < this.Columns.Count; i++)
            //        columnsWidth += this.Columns[i].Width;

            //    if (!m_columnResizing)
            //    {
            //        if (columnsWidth > width)
            //        {
            //            columnsWidth = 0;
            //            m_columnResizing = true;
            //            int newColumnWidth = width / this.Columns.Count;
            //            for (int i = 0; i < this.Columns.Count; i++)
            //            {
            //                this.Columns[i].Width = newColumnWidth;
            //                columnsWidth += newColumnWidth;
            //            }
            //            m_columnResizing = false;
            //        }
            //    }

            //    if (e.ColumnIndex == this.Columns.Count - 1)
            //    {
            //        columnsWidth -= this.Columns[this.Columns.Count - 1].Width;
            //        int height = m_gestures.Count * (m_rowHeight + 1) + e.Bounds.Height;
            //        if (!m_columnResizing)
            //        {
            //            bool hsvisible = (GetWindowLong(this.Handle, GWL_STYLE) & WS_HSCROLL) != 0;
            //            bool vsvisible = (GetWindowLong(this.Handle, GWL_STYLE) & WS_VSCROLL) != 0;
            //            //Debug.WriteLine(string.Format("Real height: {0} Height: {1} H_ScrollBar: {2} V_ScrollBar: {3}", height, this.Height, hsvisible, vsvisible));
            //            m_columnResizing = true;
            //            if ((height < this.Height - 4) || (!vsvisible && height == this.Height - 4)
            //                || (vsvisible && height == this.Height - 4 && m_viewMode == ViewModes.Action))
            //            {
            //                height = this.Height;
            //                e.Header.Width = width - columnsWidth;
            //            }
            //            else
            //            {
            //                e.Header.Width = (width - SystemInformation.VerticalScrollBarWidth) - columnsWidth;
            //            }
            //            m_realClientWidth = columnsWidth + e.Header.Width;

            //            if (m_resizeClientArea)
            //                this.ClientSize = new Size(width, height);
            //            m_resizeClientArea = true;

            //            m_columnResizing = false;
            //            m_resizeColumn = false;
            //        }
            //    }
            //}
            #endregion Not in use
            e.DrawDefault = true;
        }
        

        
        private void DrawBackground(Graphics g, Rectangle r)
        {
            Brush brush = new SolidBrush(this.BackColor);
            if (this.FullRowSelect)
            {
                if (!IsGroup(m_listItem))
                {
                    if (m_listItem.Selected)
                        brush = new SolidBrush(SystemColors.Highlight);
                    else if (m_gestures[m_listItem.Index].Activator.ID == m_selectedItemCurve)
                        brush = new SolidBrush(Color.Lavender);
                }
                else
                {
                    if (m_listItem.Selected)
                    {
                        Rectangle rect = r;
                        rect.Height--;
                        rect.Width--;
                        //LinearGradientBrush b = new LinearGradientBrush(rect, Color.Transparent, m_colorGroupSelected, 90);
                        Pen outline = new Pen(Color.FromArgb(128, 128, 255), 1);
                        //DrawRoundedRectangle(g, rect, 7, outline, b);
                        DrawRoundedRectangle(g, rect, 7, outline, Brushes.LightBlue);
                        return;
                    }
                }
            }
            g.FillRectangle(brush, r);
        }

        private int DrawCheckBox(Graphics g, Rectangle r)
        {
            int imageIndex = this.m_listItem.StateImageIndex;

            CheckBoxState boxState = CheckBoxState.UncheckedNormal;
            int switchValue = (imageIndex << 4);
            switch (switchValue)
            {
                case 0x00:
                    boxState = CheckBoxState.UncheckedNormal;
                    break;
                case 0x01:
                    boxState = CheckBoxState.UncheckedHot;
                    break;
                case 0x10:
                    boxState = CheckBoxState.CheckedNormal;
                    break;
                case 0x11:
                    boxState = CheckBoxState.CheckedHot;
                    break;
                case 0x20:
                    boxState = CheckBoxState.MixedNormal;
                    break;
                case 0x21:
                    boxState = CheckBoxState.MixedHot;
                    break;
            }

            CheckBoxRenderer.DrawCheckBox(g, new Point(r.X + 2, r.Y + (r.Height / 2) - 6), boxState);
            return CheckBoxRenderer.GetGlyphSize(g, boxState).Width + 6;
        }

        private int DrawImage(Graphics g, Rectangle r, Object imageSelector)
        {
            if (imageSelector == null || imageSelector == System.DBNull.Value)
                return 0;
            ImageList il = this.SmallImageList;
            //if (m_viewMode == ViewModes.Action || IsGroup(m_listItem))
            //    il = this.SmallImageList;
            //else
            //    il = this.LargeImageList;
            if (il != null)
            {
                int imgIndex = -1;

                if (imageSelector is Int32)
                    imgIndex = (Int32)imageSelector;
                else
                {
                    String selectorAsString = imageSelector as String;
                    if (selectorAsString != null)
                        imgIndex = il.Images.IndexOfKey(selectorAsString);
                }
                if (imgIndex >= 0)
                {
                    //Rectangle r2 = new Rectangle(r.X - this.Bounds.X, r.Y - this.Bounds.Y, r.Width, r.Height);
                    Rectangle r2 = new Rectangle(r.X, r.Y, r.Width, r.Height);
                    int flags = ILD_TRANSPARENT;
                    if (m_listItem.Selected)
                        flags |= ILD_BLEND25;
                    
                    MyGesture item = m_gestures[m_listItem.Index];
                    if ((item != null && item.AppGroup != null && !item.AppGroup.Checked) || !m_listItem.Checked)
                    {
                        ControlPaint.DrawImageDisabled(g, il.Images[imgIndex], r2.X, r2.Y, Color.Transparent);
                        //Bitmap bmp = new Bitmap(il.ImageSize.Width, il.ImageSize.Height);
                        //Graphics g2 = Graphics.FromImage(bmp);
                        //ControlPaint.DrawImageDisabled(g2, il.Images[imgIndex], 0, 0, Color.Transparent);
                        //g2.Dispose();
                        //ImageList il2 = new ImageList();
                        //il2.ImageSize = il.ImageSize;
                        //il2.ColorDepth = il.ColorDepth;
                        //il2.Images.Add(bmp);

                        //ImageList_Draw(il2.Handle, 0, g.GetHdc(), r2.X, r2.Y, flags);
                        //g.ReleaseHdc();
                    }
                    else
                    {
                        ImageList_Draw(il.Handle, imgIndex, g.GetHdc(), r2.X, r2.Y, flags);
                        g.ReleaseHdc();
                    }
                    
                    if (!m_gestures[m_listItem.Index].IsWorking)
                    {
                        ImageList_Draw(il.Handle, 0, g.GetHdc(), r2.X, r2.Y, flags);
                        g.ReleaseHdc();
                    }                    
                    return il.ImageSize.Width;
                }
            }
            return 0;
        }

        private int DrawGlyph(Graphics g, Rectangle r)
        {
            if (!IsGroup(m_listItem))
                return 0;

            Rectangle r2 = r;
            r2.X = r2.X + 4;            
            r2.Y = r.Y + (r.Height / 2) - 4;
            r2.Width = GLYPH_WIDTH;
            r2.Height = GLYPH_WIDTH;

            if (Application.RenderWithVisualStyles)
            {                
                VisualStyleElement element = VisualStyleElement.TreeView.Glyph.Opened;
                if (!IsExpanded(m_listItem))
                    element = VisualStyleElement.TreeView.Glyph.Closed;
                VisualStyleRenderer renderer = new VisualStyleRenderer(element);
                renderer.DrawBackground(g, r2);
            }
            else
            {
                // glyph width is by default 10, but to draw it manually is necessary to decrease the size
                r2.Width -= 2;
                r2.Height -= 2;

                g.DrawRectangle(new Pen(SystemBrushes.ControlDark), r2);
                g.FillRectangle(Brushes.White, r2.X + 1, r2.Y + 1, r2.Width - 1, r2.Height - 1);
                g.DrawLine(Pens.Black, r2.X + 2, r2.Y + 4, r2.X + r2.Width - 2, r2.Y + 4);
                if (!IsExpanded(m_listItem))
                    g.DrawLine(Pens.Black, r2.X + 4, r2.Y + 2, r2.X + 4, r2.Y + r2.Height - 2);

            }
            return GLYPH_OFFSET;
        }

        private void DrawLines(Graphics g, Rectangle r)
        {
            int width = OFFSET - GLYPH_OFFSET;
            int x = GLYPH_OFFSET + (r.Location.X - OFFSET);
            int top = r.Top;            
            //if ((top & 1) == 1) top++;
            if (m_listItem.Index % 2 == 1) top++;
            int midX = x + width / 2;
            int bottom = r.Bottom;

            // set vertical line positions
            if (IsGroup(m_listItem))
            {
                int start;
                int end;
                GetGroupItemsIndexes(m_listItem, out start, out end);
                if (IsExpanded(m_listItem) && start != end)
                {
                    top = top + (bottom - top) / 2;
                    midX = GLYPH_OFFSET + r.Location.X + width / 2;
                }
                else
                {
                    top = 0;
                    bottom = 0;
                }
            }
            else if (IsLastItemInGroup(m_listItem))
                bottom = bottom - (bottom - top) / 2;

            // draw vertical lines
            g.DrawLine(m_linePen, midX, top, midX, bottom);
                        
            // draw horizontal lines
            int midY = r.Top + r.Height / 2;
            if (!IsGroup(m_listItem))
            {
                g.DrawLine(m_linePen, midX + 1, midY, midX + width / 2, midY);
            }
            else
            {
                midX = (GLYPH_OFFSET / 2) + r.Location.X;
                int midX2 = GLYPH_OFFSET + r.Location.X;
                g.DrawLine(m_linePen, midX, midY, midX2, midY);
            }
        }

        protected int DrawImageAndText(Graphics g, Rectangle r)
        {
            int offset = 0;           

            if (this.CheckBoxes && m_columnIndex == 0)
            {
                DrawLines(g, r);

                offset = this.DrawGlyph(g, r);
                r.X += offset;
                r.Width -= offset;

                offset = this.DrawCheckBox(g, r);
                r.X += offset;
                r.Width -= offset;
            }

            if (m_columnIndex == 0)
            {
                string imgKey;
                if (m_viewMode == ViewModes.Action || IsGroup(m_listItem))
                    imgKey = m_listItem.ImageKey;
                else
                    imgKey = m_gestures[m_listItem.Index].Activator.ID;
                offset = this.DrawImage(g, r, imgKey);
            }

            r.X += offset;
            r.Width -= offset;
            offset = this.DrawText(g, r, m_listItem.SubItems[m_columnIndex].Text);
            r.X += offset;

            return r.X;
        }

        private int DrawText(Graphics g, Rectangle r, String txt)
        {
            Color backColor = Color.Transparent;
            Color foreColor = this.ForeColor;
            if (m_listItem.Selected && !IsGroup(m_listItem) && (m_columnIndex == 0 || this.FullRowSelect))
            {
                backColor = SystemColors.Highlight;//this.BackColor;
                foreColor = SystemColors.HighlightText;
            }
            MyGesture item = m_gestures[m_listItem.Index];
            if ((item != null && item.AppGroup != null && !item.AppGroup.Checked) || !m_listItem.Checked)
                foreColor = SystemColors.GrayText;

            TextFormatFlags flags = TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix |
                TextFormatFlags.VerticalCenter | TextFormatFlags.PreserveGraphicsTranslateTransform |
                TextFormatFlags.SingleLine;

            Font font = IsGroup(m_listItem) ? new Font(this.Font, FontStyle.Bold) : this.Font;
            
            TextRenderer.DrawText(g, txt, font, r, foreColor, backColor, flags);
            Size size = TextRenderer.MeasureText(txt, font);
            //if (m_columnIndex == 0)
            //{
            //    size = TextRenderer.MeasureText(txt, this.Font);                
            //    if (r.Width > size.Width)
            //        r.Width = size.Width;
            //    m_event.DrawFocusRectangle(r);
            //}
            return size.Width;
        }

        #endregion Drawing

        #region Not in use

        //protected override void OnColumnWidthChanging(ColumnWidthChangingEventArgs e)
        //{
        //    if (m_resizeColumn || m_columnResizing)
        //        base.OnColumnWidthChanging(e);
        //    else
        //    {
        //        ColumnHeader lastColumn = this.Columns[this.Columns.Count - 1];
        //        int width = 0;
        //        foreach (ColumnHeader column in this.Columns)
        //            width += column.Width;

        //        if (e.ColumnIndex == lastColumn.Index)
        //        {
        //            if (e.NewWidth < lastColumn.Width)
        //                if (width < this.ClientSize.Width)
        //                    e.Cancel = true;
        //        }
        //        else
        //        {
        //            if (width < m_realClientWidth)
        //            {
        //                lastColumn.Width = m_realClientWidth - (width - lastColumn.Width);
        //            }
        //        }
        //    }
        //}

        //protected override void OnColumnWidthChanged(ColumnWidthChangedEventArgs e)
        //{
        //    if (m_resizeColumn || m_columnResizing)
        //        base.OnColumnWidthChanged(e);
        //    else
        //    {
        //        int width = 0;
        //        foreach (ColumnHeader column in this.Columns)
        //            width += column.Width;
        //        if (width < m_realClientWidth)
        //        {
        //            ColumnHeader lastColumn = this.Columns[this.Columns.Count - 1];
        //            lastColumn.Width = m_realClientWidth - (width - lastColumn.Width);
        //        }
        //    }
        //}

        #endregion Not in use

        protected override void OnMouseLeave(EventArgs e)
        {
            if (m_hoverItem != null)
            {
                Rectangle r = m_hoverItem.Bounds;
                m_hoverItem = null;
                this.Invalidate(r);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            ListViewItem item = this.GetItemAt(e.X, e.Y);
            if (item != null && IsGroup(item))
            {
                if (m_hoverItem != null)
                    this.Invalidate(m_hoverItem.Bounds);
                m_hoverItem = item;                    
                this.Invalidate(item.Bounds);                
            }
            else
            {
                if (m_hoverItem != null)
                {
                    Rectangle r = m_hoverItem.Bounds;
                    m_hoverItem = null;
                    this.Invalidate(r);
                }
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            //ListViewItem item = this.GetItemAt(e.X, e.Y);
            //if (IsGroup(item)) GroupItemHit(item);
            base.OnMouseClick(e);
        }

        private void GroupItemHit(ListViewItem group)
        {
            int start, end;
            GetGroupItemsIndexes(group, out start, out end);
            for (int i = start; i < end; i++)
            {
                m_gestures[i].Selected = true;
            } 
        }

        private void GroupGlyphHit(ListViewItem group)
        {
            bool isExpanded = !m_gestures[group.Index].IsExpanded;
            m_gestures[group.Index].IsExpanded = isExpanded;
            this.Invalidate(group.Bounds);
            CollapseExpandGroup(group);
        }

        private void CollapseExpandGroup(ListViewItem group)
        {            
            int start, end;            
            this.BeginUpdate();
            if (!m_gestures[group.Index].IsExpanded)
            {
                GetGroupItemsIndexes(group, out start, out end);
                m_gestures.CollapseGroup(m_gestures[group.Index]);
                for (int i = end - 1; i >= start; i--)
                {                
                    this.Items.RemoveAt(i);                
                }
            }
            else
            {
                List<MyGesture> gestures = m_gestures.ExpandGroup(m_gestures[group.Index]);                
                int groupIndex = group.Index;
                for (int i = 0; i < gestures.Count; i++)
                {
                    groupIndex++;
                    this.Items.Insert(groupIndex, gestures[i]);                    
                }
            }
            //m_resizeClientArea = false;
            //m_resizeColumn = true;
            //this.Invalidate();
            this.Invalidate();
            this.EndUpdate();
            //SetScrollPos(this.Handle, Orientation.Vertical, 0, true);
        }

        private void GetGroupItemsIndexes(ListViewItem group, out int start, out int end)
        {
            // get items count in list view because might be different from gestures (~collapsed & expanded)
            end = this.Items.Count;
            start = group.Index;
            MyGesture groupGest = m_gestures[group.Name];
            int next = m_gestures.Groups.IndexOf(groupGest);
            next++;
            if (next < m_gestures.Groups.Count)
                //end = m_gestures.Groups.IndexOf(m_gestures.Groups[next][next].Index;
                end = m_gestures.Groups[next].Index;
            start++;
        }

        private static void DrawRoundedRectangle(Graphics g, Rectangle r, int d, Pen p, Brush brush)
        {
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            gp.AddLine(r.X, r.Y + r.Height - d, r.X, r.Y + d / 2);
            g.FillRegion(brush, new Region(gp));
            g.DrawPath(p, gp);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0201: // WM_LBUTTONDOWN
                case 0x0204: // WM_RBUTTONDOWN
                    if (HitCheckBox(ref m))
                    {
                        if (m_listItem != null)
                            m_listItem.Checked = !m_listItem.Checked;
                    }
                    if (HitGlyphkBox(ref m))
                    {
                        if (m_listItem != null)
                            GroupGlyphHit(m_listItem);
                    }
                    else
                        base.WndProc(ref m);
                    break;
                case 0x0203: // WM_LBUTTONDBLCLK
                case 0x0206: // WM_RBUTTONDBLCLK
                    if (!HitCheckBox(ref m) && !HitGlyphkBox(ref m))
                    {
                        int x = m.LParam.ToInt32() & 0xFFFF;
                        int y = (m.LParam.ToInt32() >> 16) & 0xFFFF;
                        ListViewHitTestInfo hti = this.HitTest(x, y);
                        if (hti.Item != null)
                        {
                            m_listItem = hti.Item;
                            if (!IsGroup(m_listItem))
                                m_listItem.Checked = !m_listItem.Checked;
                            else
                                GroupGlyphHit(m_listItem);
                        }
                        else
                            base.WndProc(ref m);
                    }
                    else
                        base.WndProc(ref m);
                    break;
                case 0x114: // WM_HSCROLL:
                case 0x115: // WM_VSCROLL:                    
                    base.WndProc(ref m);
                    this.Invalidate();
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private bool HitGlyphkBox(ref Message m)
        {
            ResetItems();
            int x = m.LParam.ToInt32() & 0xFFFF;
            int y = (m.LParam.ToInt32() >> 16) & 0xFFFF;
            ListViewHitTestInfo hti = this.HitTest(x, y);
            if (hti.Item != null)
            {
                m_listItem = hti.Item;
                if (IsGroup(m_listItem))
                {
                    Rectangle r = m_listItem.Bounds;
                    r.X += 5;
                    r.Width = GLYPH_WIDTH;
                    if (r.Contains(x, y))
                    {
                        m_listItem = hti.Item;
                        return true;
                    }
                    else if (hti.Location == ListViewHitTestLocations.StateImage)
                    {
                        m_listItem = null;
                        return true;
                    }   
                }
                else
                {
                    if (hti.Location == ListViewHitTestLocations.StateImage)
                    {
                        m_listItem = null;
                        return true;
                    }
                }
            }
            return false;
        }

        private bool HitCheckBox(ref Message m)
        {
            ResetItems();
            int x = m.LParam.ToInt32() & 0xFFFF;
            int y = (m.LParam.ToInt32() >> 16) & 0xFFFF;
            ListViewHitTestInfo hti = this.HitTest(x, y);
            if (hti.Item != null)
            {
                m_listItem = hti.Item;
                if (IsGroup(m_listItem))
                {
                    Rectangle r = m_listItem.Bounds;
                    r.X += GLYPH_OFFSET + 3;
                    r.Width = CHECK_BOX_WIDTH;
                    if (r.Contains(x, y))
                    {
                        m_listItem = hti.Item;
                        return true;
                    }
                    else if (hti.Location == ListViewHitTestLocations.StateImage)
                    {
                        m_listItem = null;
                        return true;
                    }   
                }
                else
                {
                    Graphics g = m_listItem.ListView.CreateGraphics();
                    int width = CHECK_BOX_WIDTH;
                    Rectangle r = m_listItem.Bounds;
                    r.X += OFFSET + 3;
                    r.Width = width;
                    if (r.Contains(x, y))
                    {
                        m_listItem = hti.Item;
                        return true;
                    }
                    else if (hti.Location == ListViewHitTestLocations.StateImage)
                    {   
                        m_listItem = null;
                        return true;
                    }
                }
            }
            return false;
        }


    }
}
