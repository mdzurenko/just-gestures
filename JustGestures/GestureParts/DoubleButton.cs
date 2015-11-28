using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.Serialization;
using JustGestures.Properties;
using JustGestures.Languages;

namespace JustGestures.GestureParts
{
    [Serializable]
    public class DoubleButton : BaseActivator, ISerializable
    {
        const int DELAY = 2500;
        MouseButtons m_trigger = MouseButtons.None;
        MouseButtons m_modifier = MouseButtons.None;

        public MouseButtons Trigger { get { return m_trigger; } set { m_trigger = value; } }
        public MouseButtons Modifier { get { return m_modifier; } set { m_modifier = value; } }

        public DoubleButton(MouseButtons trigger, MouseButtons modifier)
        {
            m_id = GetName(trigger, modifier);
            m_trigger = trigger;
            m_modifier = modifier;
            m_type = Types.DoubleButton;
        }

        public static implicit operator MouseActivator(DoubleButton doubleButton)
        {
            if (doubleButton == null)
                return null;
            else
                return new MouseActivator((object)doubleButton);
        }

        public static string GetName(MouseButtons trigger, MouseButtons modifier)
        {
            return string.Format("DblBtn_{0}_{1}", trigger, modifier);
        }

        public override Bitmap ExtractIcon(Size size)
        {
            Bitmap original = DrawDoubleBtnMouse(Resources.base_mouse.Size, true);
            Bitmap imgNew = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(imgNew);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.DrawImage(original, 0, 0, size.Width, size.Height);
            g.Dispose();
            return imgNew;
        }

        public override void DrawToPictureBox(PictureBox pictureBox)
        {
            pictureBox.Image = DrawDoubleBtnMouse(pictureBox.Size, false);
        }

        private Bitmap DrawDoubleBtnMouse(Size pictSize, bool icon)
        {
            double ration = icon ? 1.05 : 0.8;
            if (pictSize.Width <= 0 || pictSize.Height <= 0) return null;
            Bitmap bmp = new Bitmap(pictSize.Width, pictSize.Height);
            Graphics gp = Graphics.FromImage(bmp);
            gp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            int width = (int)Math.Min(bmp.Width * ration, bmp.Height * ration);
            if (width > Resources.base_mouse.Width)
                width = Resources.base_mouse.Width;
            int height = width;
            Point pos;
            if (icon)
                pos = new Point((bmp.Width - width) / 2, (bmp.Height - height) / 2);
            else
                pos = new Point((bmp.Width - width) / 2, (bmp.Height - height) / 4);
            Rectangle r = new Rectangle(pos, new Size(width, height));
            gp.DrawImage(Resources.base_mouse, r.X, r.Y, r.Width, r.Height);
            DrawTrigger(m_trigger, gp, r);
            DrawModifier(m_modifier, gp, r);
            gp.Dispose();
            
            // If buttons are swaped then flip the image
            bool swapedButtons = Win32.GetSystemMetrics(Win32.SM_SWAPBUTTON) == 0 ? false : true;
            if (swapedButtons) bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
            return bmp;
        }

        public override void AnimateToPictureBox(PictureBox pictureBox)
        {
            if (m_trigger == MouseButtons.None || m_modifier == MouseButtons.None) return;

            bool swapedButtons = Win32.GetSystemMetrics(Win32.SM_SWAPBUTTON) == 0 ? false : true;
            
            Bitmap bmp = new Bitmap(pictureBox.Width, pictureBox.Height);            
            Graphics gp = Graphics.FromImage(bmp);
            gp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;            
            int width = (int)Math.Min(bmp.Width * 0.8, bmp.Height * 0.8);
            if (width > Resources.base_mouse.Width)
                width = Resources.base_mouse.Width;
            int height = width;
            Point pos = new Point((bmp.Width - width) / 2, (bmp.Height - height) / 4);
            Rectangle r = new Rectangle(pos, new Size(width, height));
            Point strPos = new Point(0, pos.Y + (int)Math.Floor(r.Height * 0.95));
            Rectangle rString = new Rectangle(strPos, new Size(bmp.Width, (int)Math.Floor((bmp.Height - height) * 0.75)));
            float fontHeight = (float)r.Height / 18;
            Font strFont = new Font(FontFamily.GenericSansSerif, fontHeight, FontStyle.Bold); //SystemFonts.DefaultFont;
            
            string txtPushHoldBtn = string.Format(Translation.GetText("Animate_RockerG_pushHoldBtn"), Translation.GetMouseBtnText(m_trigger));
            string txtClickExecuteBtn = string.Format(Translation.GetText("Animate_RockerG_clickExecuteBtn"), Translation.GetMouseBtnText(m_modifier));

            //float strWidth = Math.Max(gp.MeasureString(txtPushHoldBtn, strFont).Width, gp.MeasureString(txtClickExecuteBtn, strFont).Width);
            //if (strWidth > r.Width * 2)
            //{
            //    fontHeight = (float)r.Height / 22;
            //    strFont = new Font(strFont.FontFamily, fontHeight, strFont.Style);
            //}

            StringFormat strFormat = new StringFormat();
            strFormat.LineAlignment = StringAlignment.Center;
            strFormat.Alignment = StringAlignment.Center;            
            SolidBrush strTriggerBrush = new SolidBrush(Color.FromArgb(250, 10, 10)); //red 
            SolidBrush strModifierBrush = new SolidBrush(Color.FromArgb(220, 215, 16)); //yellow

            gp.DrawImage(Resources.base_mouse, r.X, r.Y, r.Width, r.Height);
            if (swapedButtons) bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
            pictureBox.Image = bmp;            
            System.Threading.Thread.Sleep(DELAY / 4);
            
            if (swapedButtons) bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
            DrawTrigger(m_trigger, gp, r);
            if (swapedButtons) bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
            //text = string.Format("Push and hold the {0} button.", button.ToUpper());
            gp.FillRectangle(new SolidBrush(pictureBox.BackColor), rString);
            gp.DrawString(txtPushHoldBtn, strFont, strTriggerBrush, rString, strFormat);            
            pictureBox.Invalidate();
            System.Threading.Thread.Sleep(DELAY);

            if (swapedButtons) bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
            DrawModifier(m_modifier, gp, r);
            if (swapedButtons) bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
            //text = string.Format("Click on the {0} button to invoke the action.", button.ToUpper());
            gp.FillRectangle(new SolidBrush(pictureBox.BackColor), rString);            
            gp.DrawString(txtClickExecuteBtn, strFont, strModifierBrush, rString, strFormat);            
            pictureBox.Invalidate();
            System.Threading.Thread.Sleep(DELAY);
            
            gp.FillRectangle(new SolidBrush(pictureBox.BackColor), rString);
            pictureBox.Invalidate();
            gp.Dispose();
        }

        private void DrawTrigger(MouseButtons button, Graphics gp, Rectangle r)
        {  
            switch (button)
            {
                case MouseButtons.Left: gp.DrawImage(Resources.btn_left_trigger, r.X, r.Y, r.Width, r.Height); break;
                case MouseButtons.Right: gp.DrawImage(Resources.btn_right_trigger, r.X, r.Y, r.Width, r.Height); break;
                case MouseButtons.Middle: gp.DrawImage(Resources.btn_middle_trigger, r.X, r.Y, r.Width, r.Height); break;
                case MouseButtons.XButton1: gp.DrawImage(Resources.btn_x1_trigger, r.X, r.Y, r.Width, r.Height); break;
                case MouseButtons.XButton2: gp.DrawImage(Resources.btn_x2_trigger, r.X, r.Y, r.Width, r.Height); break;
            }
        }

        private void DrawModifier(MouseButtons button, Graphics gp, Rectangle r)
        {
            switch (button)
            {
                case MouseButtons.Left: gp.DrawImage(Resources.btn_left_modifier, r.X, r.Y, r.Width, r.Height); break;
                case MouseButtons.Right: gp.DrawImage(Resources.btn_right_modifier, r.X, r.Y, r.Width, r.Height); break;
                case MouseButtons.Middle: gp.DrawImage(Resources.btn_middle_modifier, r.X, r.Y, r.Width, r.Height); break;
                case MouseButtons.XButton1: gp.DrawImage(Resources.btn_x1_modifier, r.X, r.Y, r.Width, r.Height); break;
                case MouseButtons.XButton2: gp.DrawImage(Resources.btn_x2_modifier, r.X, r.Y, r.Width, r.Height); break;
            }
        }

        public DoubleButton(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            try { m_trigger = (MouseButtons)info.GetValue("Trigger", typeof(MouseButtons)); }
            catch { m_trigger = MouseButtons.None; }
            try { m_modifier = (MouseButtons)info.GetValue("Modifier", typeof(MouseButtons)); }
            catch { m_modifier = MouseButtons.None; }

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Trigger", m_trigger, typeof(MouseButtons));
            info.AddValue("Modifier", m_modifier, typeof(MouseButtons));
        }

    }
}
