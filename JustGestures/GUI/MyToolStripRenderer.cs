using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms.VisualStyles;

namespace JustGestures.GUI
{
    public class MyToolStripRenderer : ToolStripRenderer
    {

        protected override void InitializeItem(ToolStripItem item)
        {
            base.InitializeItem(item);
            item.Height = Math.Max(23, item.Height);
        }

        private int OpacityCalc(int num)
        {
            float perc = ((255 - num) / 255f);
            int ret = (int)(255 - (255 * perc));

            if (ret < 0) ret = 0;
            return ret;
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            Rectangle destRect = e.ImageRectangle;
            destRect.X++;
            destRect.Width -= 2;
            destRect.Y++;
            destRect.Height -= 2;
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

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            Rectangle r = e.Item.ContentRectangle;
            Color color = Color.MidnightBlue;//Color.FromArgb(255, Color.MidnightBlue);
            int x = r.X;
            int y1 = r.Y + 2;
            int y2 = r.Bottom;
            e.Graphics.DrawLine(new Pen(Color.MidnightBlue, 1), x, y1, x, y2);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(150, Color.Gray), 1), x + 1, y1 + 1, x + 1, y2 + 1);

        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            GraphicsPath gp;
            Rectangle b = e.Item.Bounds;

            b.X = 0;

            b.Width--;
            b.Height -= (e.Item.Margin.Top * 2) + 1;

            if (e.Item.Pressed)
            {
                gp = CreateRoundedRectanglePath(b, 3);
                e.Graphics.DrawPath(new Pen(Color.FromArgb(200, Color.Black), 1), gp);

                Color c1 = Color.FromArgb(OpacityCalc(100), Color.Black);
                Color c2 = Color.FromArgb(OpacityCalc(30), Color.Black);

                ColorBlend cb = new ColorBlend();

                cb.Colors = new Color[] { c1, c2, Color.FromArgb(0, Color.Black) };
                cb.Positions = new Single[] { 0.0f, 0.1f, 1.0f };

                LinearGradientBrush lgb;

                lgb = new LinearGradientBrush(b, c1, c2, LinearGradientMode.Vertical);
                lgb.InterpolationColors = cb;

                e.Graphics.FillPath(lgb, gp);
                lgb.Dispose();

                lgb = new LinearGradientBrush(b, c1, c2, LinearGradientMode.Horizontal);
                lgb.InterpolationColors = cb;

                e.Graphics.FillPath(lgb, gp);
                lgb.Dispose();
            }
            else if (e.Item.Selected)
            {
                Pen pen = new Pen(Color.FromArgb(200, Color.Black), 1f);

                gp = CreateRoundedRectanglePath(b, 3);
                e.Graphics.DrawPath(pen, gp);

                gp.Dispose();

                b.X++; b.Y++;

                b.Width -= 2;
                b.Height -= 2;

                pen.Color = Color.FromArgb(200, Color.DarkBlue);

                gp = CreateRoundedRectanglePath(b, 3);
                e.Graphics.DrawPath(pen, gp);

                pen.Dispose(); gp.Dispose();
            }
        }

        protected GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();

            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);

            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);

            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();

            return roundedRect;
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            //base.OnRenderToolStripBorder(e);
        }



    }
}
