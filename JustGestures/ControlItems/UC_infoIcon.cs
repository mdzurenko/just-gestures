using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace JustGestures.ControlItems
{
    public partial class UC_infoIcon : UserControl
    {
        Timer m_flickerTimer;
        Timer m_mouseLeaveTimer;
        Timer m_showBallonTimer;
        int m_timerTickCount = 0;
        Bitmap m_timerBmp = null;
        bool m_mouseOver = false;
        bool m_vissibleBalloon = false;
        BalloonTip m_balloonTip;

        public UC_infoIcon()
        {
            InitializeComponent();
            m_flickerTimer = new Timer();
            m_flickerTimer.Interval = 400;
            m_flickerTimer.Tick += new EventHandler(m_timer_Tick);

            m_mouseLeaveTimer = new Timer();
            m_mouseLeaveTimer.Interval = 200;
            m_mouseLeaveTimer.Tick += new EventHandler(m_mouseLeaveTimer_Tick);

            m_showBallonTimer = new Timer();
            m_showBallonTimer.Tick += new EventHandler(m_showBallonTimer_Tick);
            
        }

        private void UC_infoIcon_Load(object sender, EventArgs e)
        {
            m_balloonTip = new BalloonTip(pB_info);
            m_balloonTip.Align = BalloonTip.BalloonAlignment.MiddleMiddle;
            m_balloonTip.MouseLeave += new MessageTool.DlgMouseLeave(BalloonTip_MouseLeave);
            m_balloonTip.DeActivate += new MessageTool.DlgDeActivate(MouseLeft);
            ChangeInfoText(ToolTipIcon.Info, "Info", "Info Text");
        }

        public void ChangeInfoText(ToolTipIcon icon, string title, string text)
        {
            m_flickerTimer.Stop();            
            m_balloonTip.TitleIcon = icon;
            m_balloonTip.Title = title;
            m_balloonTip.Text = text;
            int height = pB_info.Height * 4 / 5;
            int offset = (pB_info.Height - height) / 2;
            m_timerBmp = new Bitmap(height, height);
            string infoText = string.Empty;
            switch (icon)
            {
                case ToolTipIcon.Error:
                    //m_timerBmp = Properties.Resources.error;
                    m_timerBmp = SystemIcons.Error.ToBitmap(); 
                    infoText = "Error"; 
                    break;
                case ToolTipIcon.Info:
                    //m_timerBmp = Properties.Resources.info;
                    m_timerBmp = SystemIcons.Information.ToBitmap();                     
                    infoText = "Info"; break;
                case ToolTipIcon.Warning: 
                    //m_timerBmp = Properties.Resources.warning;
                    m_timerBmp = SystemIcons.Warning.ToBitmap(); 
                    infoText = "Warning"; 
                    break;
            }
            pB_info.Image = new Bitmap(pB_info.Width, pB_info.Height);
            Graphics g = Graphics.FromImage(pB_info.Image);
            g.FillRectangle(new SolidBrush(pB_info.BackColor), 0, 0, pB_info.Width, pB_info.Height);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(m_timerBmp, offset, (pB_info.Height - height) / 2, height, height);

            m_timerTickCount = 0;
            if (icon == ToolTipIcon.Error || icon == ToolTipIcon.Warning)
            {                
                m_flickerTimer.Start();                
            }

            //StringFormat strFormat = new StringFormat();
            //strFormat.LineAlignment = StringAlignment.Center;
            //Font strFont = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
            //int pos_x = offset + offset / 2 + height;
            //Rectangle r = new Rectangle(pos_x, 0, pB_info.Width - pos_x, pB_info.Height);
            //g.DrawString(infoText, strFont, Brushes.Black, r, strFormat);
            g.Dispose();
        }

        private void HighlightIcon()
        {
            int height = pB_info.Height;
            int offset = 0;
            Graphics g = Graphics.FromImage(pB_info.Image);
            g.FillRectangle(new SolidBrush(pB_info.BackColor), 0, 0, pB_info.Width, pB_info.Height);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            float brightness = 1.2f; // no change in brightness
            float constrast = 1.5f; // twice the contrast
            float gamma = 2.0f; // no change in gamma

            float adjustedBrightness = brightness - 1.0f;
            // create matrix that will brighten and contrast the image
            ColorMatrix colorMatrix = new ColorMatrix(
                new float[][]
                {
                    new float[] {constrast, 0, 0, 0, 0}, // scale red
                    new float[] {0, constrast, 0, 0, 0}, // scale green
                    new float[] {0, 0, constrast, 0, 0}, // scale blue
                    new float[] {0, 0, 0, 1.0f, 0}, // don't scale alpha
                    new float[] {adjustedBrightness, adjustedBrightness, adjustedBrightness, 0, 1}
                });


            ImageAttributes imageAttributes = new ImageAttributes();
            //imageAttributes.ClearColorMatrix();
            //imageAttributes.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            imageAttributes.SetGamma(gamma, ColorAdjustType.Bitmap);

            g.DrawImage(m_timerBmp, new Rectangle(offset, (pB_info.Height - height) / 2, height, height)
            , 0, 0, m_timerBmp.Width, m_timerBmp.Height,
            GraphicsUnit.Pixel, imageAttributes);
            g.Dispose();
            pB_info.Invalidate();
        }

        private void NormalIcon()
        {
            int height = pB_info.Height;
            int offset = 0;
            Graphics g = Graphics.FromImage(pB_info.Image);
            g.FillRectangle(new SolidBrush(pB_info.BackColor), 0, 0, pB_info.Width, pB_info.Height);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            
            height = height * 4 / 5;
            offset = (pB_info.Height - height) / 2;
            g.DrawImage(m_timerBmp, offset, (pB_info.Height - height) / 2, height, height);
            g.Dispose();
            pB_info.Invalidate();
        }

        void m_timer_Tick(object sender, EventArgs e)
        {
            m_timerTickCount++;

            if (m_timerTickCount % 2 == 0 && !m_mouseOver && !m_vissibleBalloon)
                NormalIcon();
            else
                HighlightIcon();

            if (m_timerTickCount / 2 == 3) m_flickerTimer.Stop();
        }

        void m_mouseLeaveTimer_Tick(object sender, EventArgs e)
        {
            m_mouseOver = false;
            m_mouseLeaveTimer.Stop();
            MouseLeft();
        }

        private void BalloonTip_MouseLeave()
        {
            MouseLeft();
        }

        private void MouseLeft()
        {
            if (!m_balloonTip.IsMouseHover && !m_mouseOver)
            {
                m_vissibleBalloon = false;
                NormalIcon();
                m_balloonTip.Hide();
            }
        }

        private void pB_info_MouseEnter(object sender, EventArgs e)
        {
            m_mouseOver = true;
            m_mouseLeaveTimer.Stop();
            m_showBallonTimer.Stop();
            if (!m_vissibleBalloon)
            {
                HighlightIcon();
            }
        }

        private void pB_info_MouseLeave(object sender, EventArgs e)
        {
            m_mouseLeaveTimer.Start();
        }

        private void pB_info_MouseHover(object sender, EventArgs e)
        {
            //m_mouseOver = false;
            if (!m_vissibleBalloon)
            {
                m_vissibleBalloon = true;
                m_balloonTip.Show();                
            }
        }

        public void ShowBalloonTip(int milliseconds)
        {
            m_showBallonTimer.Stop();
            m_showBallonTimer.Interval = milliseconds;
            if (!m_vissibleBalloon)
            {                
                m_balloonTip.Show();
                m_vissibleBalloon = true;
                HighlightIcon();
                m_showBallonTimer.Start();
            }
        }

        public void HideBallonTip()
        {
            if (m_vissibleBalloon)
            {
                m_showBallonTimer.Stop();
                m_vissibleBalloon = false;
                NormalIcon();
                m_balloonTip.Hide();
            }
        }

        void m_showBallonTimer_Tick(object sender, EventArgs e)
        {
            HideBallonTip();
        }
    }
}
