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
    public class WheelButton : BaseActivator, ISerializable
    {
        const int DELAY = 2500;
        MouseButtons m_trigger = MouseButtons.None;

        public MouseButtons Trigger { get { return m_trigger; } set { m_trigger = value; } }

        public WheelButton(MouseButtons trigger)
        {
            m_id = GetName(trigger);
            m_trigger = trigger;
            m_type = Types.WheelButton;
        }

        public static implicit operator MouseActivator(WheelButton wheelButton)
        {
            if (wheelButton == null)
                return null;
            else
                return new MouseActivator((object)wheelButton);
        }

        public static string GetName(MouseButtons trigger)
        {
            return string.Format("WheelBtn_{0}", trigger);
        }

        public override Bitmap ExtractIcon(Size size)
        {
            Bitmap original = DrawWheelBtnMouse(Resources.base_mouse.Size, true);
            Bitmap imgNew = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(imgNew);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.DrawImage(original, 0, 0, size.Width, size.Height);
            g.Dispose();
            return imgNew;
        }

        public override void DrawToPictureBox(PictureBox pictureBox)
        {
            pictureBox.Image = DrawWheelBtnMouse(pictureBox.Size, false);
        }

        private Bitmap DrawWheelBtnMouse(Size pictSize, bool icon)
        {
            double ration = icon ? 1.05 : 0.8;
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
            gp.DrawImage(Resources.btn_middle_modifier, r.X, r.Y, r.Width, r.Height);
            DrawTrigger(m_trigger, gp, r);
            gp.DrawImage(Resources.btn_wheel_down, r.X, r.Y, r.Width, r.Height);
            gp.DrawImage(Resources.btn_wheel_up, r.X, r.Y, r.Width, r.Height);            
            gp.Dispose();

            // If buttons are swaped then flip the image
            bool swapedButtons = Win32.GetSystemMetrics(Win32.SM_SWAPBUTTON) == 0 ? false : true;
            if (swapedButtons) bmp.RotateFlip(RotateFlipType.Rotate180FlipY);         
   
            return bmp;
        }

        public override void AnimateToPictureBox(PictureBox pictureBox)
        {
            if (m_trigger == MouseButtons.None) return;

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
            string txtPushHoldBtn = string.Format(Translation.GetText("Animate_WheelG_pushHoldBtn"), Translation.GetMouseBtnText(m_trigger));
            string txtScrollWheelUp = Translation.GetText("Animate_WheelG_scrollWheelUp");
            string txtScrollWheelDown = Translation.GetText("Animate_WheelG_scrollWheelDown");
            string txtReleaseHoldBtn = string.Format(Translation.GetText("Animate_WheelG_releaseHoldBtn"), Translation.GetMouseBtnText(m_trigger));
            StringFormat strFormat = new StringFormat();
            strFormat.LineAlignment = StringAlignment.Center;
            strFormat.Alignment = StringAlignment.Center;            
            SolidBrush strTriggerBrush = new SolidBrush(Color.FromArgb(250, 10, 10)); //red 
            SolidBrush strModifierBrush = new SolidBrush(Color.FromArgb(220, 215, 16)); //yellow
                        
            gp.DrawImage(Resources.base_mouse, r.X, r.Y, r.Width, r.Height);
            pictureBox.Image = bmp;
            if (swapedButtons) bmp.RotateFlip(RotateFlipType.Rotate180FlipY); 
            System.Threading.Thread.Sleep(DELAY / 4);

            // Push Hold-down button
            if (swapedButtons) bmp.RotateFlip(RotateFlipType.Rotate180FlipY); 
            DrawTrigger(m_trigger, gp, r);
            if (swapedButtons) bmp.RotateFlip(RotateFlipType.Rotate180FlipY); 
            gp.FillRectangle(new SolidBrush(pictureBox.BackColor), rString);
            gp.DrawString(txtPushHoldBtn, strFont, strTriggerBrush, rString, strFormat);
            pictureBox.Invalidate();
            System.Threading.Thread.Sleep(DELAY);

            // Scroll with wheel up
            if (swapedButtons) bmp.RotateFlip(RotateFlipType.Rotate180FlipY); 
            gp.DrawImage(Resources.btn_wheel_up, r.X, r.Y, r.Width, r.Height);
            gp.DrawImage(Resources.btn_middle_modifier, r.X, r.Y, r.Width, r.Height);
            if (swapedButtons) bmp.RotateFlip(RotateFlipType.Rotate180FlipY); 
            gp.FillRectangle(new SolidBrush(pictureBox.BackColor), rString);
            gp.DrawString(txtScrollWheelUp, strFont, strModifierBrush, rString, strFormat);
            pictureBox.Invalidate();
            System.Threading.Thread.Sleep(DELAY);

            // Scroll with wheel down
            if (swapedButtons) bmp.RotateFlip(RotateFlipType.Rotate180FlipY); 
            gp.FillRectangle(new SolidBrush(pictureBox.BackColor), 0, 0, pictureBox.Width, pictureBox.Height);
            gp.DrawImage(Resources.base_mouse, r.X, r.Y, r.Width, r.Height);
            DrawTrigger(m_trigger, gp, r);            
            gp.DrawImage(Resources.btn_wheel_down, r.X, r.Y, r.Width, r.Height);
            gp.DrawImage(Resources.btn_middle_modifier, r.X, r.Y, r.Width, r.Height);
            if (swapedButtons) bmp.RotateFlip(RotateFlipType.Rotate180FlipY); 
            gp.FillRectangle(new SolidBrush(pictureBox.BackColor), rString);
            gp.DrawString(txtScrollWheelDown, strFont, strModifierBrush, rString, strFormat);
            pictureBox.Invalidate();
            System.Threading.Thread.Sleep(DELAY);

            // Release Hold-down button
            if (swapedButtons) bmp.RotateFlip(RotateFlipType.Rotate180FlipY); 
            gp.FillRectangle(new SolidBrush(pictureBox.BackColor), 0, 0, pictureBox.Width, pictureBox.Height);
            gp.DrawImage(Resources.base_mouse, r.X, r.Y, r.Width, r.Height);
            if (swapedButtons) bmp.RotateFlip(RotateFlipType.Rotate180FlipY); 
            gp.FillRectangle(new SolidBrush(pictureBox.BackColor), rString);
            gp.DrawString(txtReleaseHoldBtn, strFont, strTriggerBrush, rString, strFormat);
            pictureBox.Invalidate();
            System.Threading.Thread.Sleep(DELAY);


            gp.FillRectangle(new SolidBrush(pictureBox.BackColor), rString);
            gp.Dispose();
            pictureBox.Image = DrawWheelBtnMouse(pictureBox.Size, false);            
            pictureBox.Invalidate();
            
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

        private void DrawWheelAction(Graphics gp, Rectangle r)
        {
            gp.DrawImage(Resources.btn_middle_modifier, r.X, r.Y, r.Width, r.Height);
            gp.DrawImage(Resources.btn_wheel_down, r.X, r.Y, r.Width, r.Height);
            gp.DrawImage(Resources.btn_wheel_up, r.X, r.Y, r.Width, r.Height);
        }

        public WheelButton(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            try { m_trigger = (MouseButtons)info.GetValue("Trigger", typeof(MouseButtons)); }
            catch { m_trigger = MouseButtons.None; }

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Trigger", m_trigger, typeof(MouseButtons));
        }
    }
}
