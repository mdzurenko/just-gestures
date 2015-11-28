using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms.VisualStyles;
using System.Diagnostics;

namespace JustGestures.GUI
{
    class MyContextMenuRenderer : ToolStripRenderer
    {
        int m_sideBorder = 5;

        protected override void Initialize(ToolStrip toolStrip)
        {
            base.Initialize(toolStrip);            
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            Rectangle destRect = e.ImageRectangle;
            destRect.X += m_sideBorder + m_sideBorder / 2;
            Image normalImage = e.Image;
            if ((destRect != Rectangle.Empty) && (normalImage != null))
            {
                if (e.Item.Enabled)
                {
                    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    e.Graphics.DrawImage(normalImage, destRect);
                }
                else
                {
                    Image disabledImage = new Bitmap(destRect.Width, destRect.Height);
                    Graphics g = Graphics.FromImage(disabledImage);
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(e.Image, 0, 0, destRect.Width, destRect.Height);
                    g.Dispose();
                    ControlPaint.DrawImageDisabled(e.Graphics, disabledImage, destRect.X, destRect.Y, Color.Transparent);
                }
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            //e.TextFormat = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
            if (e.Item.Enabled)
                e.TextColor = Color.Black;
            else
                e.TextColor = Color.Gray;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            Rectangle r = new Rectangle(1, 0, e.Item.Bounds.Width - 3, e.Item.Bounds.Height);
            r.X += m_sideBorder + m_sideBorder / 2;
            r.Width -= 2 * m_sideBorder;
            Color color = Color.MidnightBlue;//Color.FromArgb(255, Color.MidnightBlue);            
            LinearGradientBrush brush1 = new LinearGradientBrush(r, Color.Transparent, color , LinearGradientMode.Horizontal);
            LinearGradientBrush brush2 = new LinearGradientBrush(r, color, Color.Transparent, LinearGradientMode.Horizontal);
            //Pen pen = new Pen(brush, 1);
            int x1 = r.X + 1;
            int y = r.Y + r.Height / 2;
            int x2 = r.X + r.Width / 2;
            int x3 = r.Right - m_sideBorder;
            e.Graphics.DrawLine(new Pen(brush1, 1), x1, y, x2, y);
            e.Graphics.DrawLine(new Pen(brush2, 1), x2 + 1, y, x3, y);

            //e.Graphics.DrawLine(pen, new Point(r.X, r.Y + r.Height / 2), new Point(r.Right - 5, r.Y + r.Height / 2));            
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            Rectangle r = e.AffectedBounds;
            LinearGradientBrush b = new LinearGradientBrush(r, Color.DarkBlue, Color.MidnightBlue, 90);
            e.Graphics.DrawRectangle(new Pen(b, m_sideBorder * 2 + 1), r);
            r.X += m_sideBorder;
            r.Y += m_sideBorder;
            r.Width -= m_sideBorder * 2;
            r.Height -= m_sideBorder * 2;
            Color inside = Color.FromArgb(150, Color.Gray);
            e.Graphics.DrawRectangle(new Pen(inside, 1), r);
            int d = 10;
            r = e.AffectedBounds;
            r.Width--;
            r.Height--;
            Color borderIn = Color.FromArgb(100, Color.Gray);
            DrawOutLine(e.Graphics, r, d, new Pen(borderIn, 3));
            r = e.AffectedBounds;
            Color borderOut = Color.FromArgb(150, Color.Black);
            DrawOutLine(e.Graphics, r, d, new Pen(borderOut, 2));
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            Debug.WriteLine("OnRenderMenuItemBackground " + e.Item.Text);
            Rectangle rect = new Rectangle(2, 0, e.Item.Bounds.Width - 3, e.Item.Bounds.Height);
            rect.X += m_sideBorder;
            //rect.X--;
            //rect.Y += m_sideBorder;
            rect.Height--;
            rect.Width -= m_sideBorder * 2;
            //rect.Width += 2;
            Pen outline = new Pen(Color.FromArgb(128, 128, 255), 1);
            LinearGradientBrush b = new LinearGradientBrush(rect, Color.Transparent, Color.LightBlue, 90);
            if (e.Item.Selected)
            {
                if (e.Item.Enabled)
                    DrawRoundedRectangle(e.Graphics, rect, 5, outline, b);
            }
            else
                e.Graphics.FillRectangle(new SolidBrush(e.ToolStrip.BackColor), e.Item.Bounds);
        }

        private static void DrawOutLine(Graphics g, Rectangle r, int d, Pen p)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            gp.AddLine(r.X, r.Y + r.Height - d, r.X, r.Y + d / 2);
            g.DrawPath(p, gp);
        }

        private static void DrawRoundedRectangle(Graphics g, Rectangle r, int d, Pen p, Brush brush)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            gp.AddLine(r.X, r.Y + r.Height - d, r.X, r.Y + d / 2);
            g.FillRegion(brush, new Region(gp));
            g.DrawPath(p, gp);
        }
    }
}
