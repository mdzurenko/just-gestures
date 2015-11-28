using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace JustGestures.ControlItems
{
    public partial class UC_TP_baseActivator : UserControl
    {
        /// <summary>
        /// If all conditions are met, it is possible to continue
        /// </summary>
        public BaseActionControl.DlgCanContinue CanContinue;
        /// <summary>
        /// Change of the text
        /// </summary>
        public BaseActionControl.DlgChangeAboutText ChangeAboutText;

        public BaseActionControl.DlgChangeInfoText ChangeInfoText;
        /// <summary>
        /// List of learnt gestures
        /// </summary>
        protected GesturesCollection m_gesturesCollection;
        /// <summary>
        /// Gesture which is being created
        /// </summary>
        protected MyGesture m_tempGesture;

        protected PictureBox m_pbDisplay;
        protected PictureBox m_pbInfo;
        protected Graphics m_gp;        

        protected void OnCanContinue(bool _continue)
        {
            if (CanContinue != null)
                CanContinue(_continue);
        }

        protected void OnChangeAboutText(string text)
        {
            if (ChangeAboutText != null)
                ChangeAboutText(text);
        }

        protected void OnChangeInfoText(ToolTipIcon icon, string title, string text)
        {
            if (ChangeInfoText != null)
                ChangeInfoText(icon, title, text);
        }

        public GesturesCollection Gestures { get { return m_gesturesCollection; } set { m_gesturesCollection = value; } }
        public MyGesture TempGesture { get { return m_tempGesture; } set { m_tempGesture = value; } }
        public PictureBox PB_Display { get { return m_pbDisplay; } set { m_pbDisplay = value; } }
        public PictureBox PB_Info { set { m_pbInfo = value; } }

        public UC_TP_baseActivator()
        {
            InitializeComponent();
        }

        public virtual void Initialize()
        {
            //Debug.WriteLine("BaseActivator - Initialize");
            //Debug.WriteLine(string.Format("Display Width: {0}, Height: {1}", m_pbDisplay.Width, m_pbDisplay.Height));
            //Bitmap bmp = new Bitmap(m_pbDisplay.Width, m_pbDisplay.Height);
            //m_gp = Graphics.FromImage(bmp);
            //m_gp.FillRectangle(Brushes.White, 0, 0, m_pbDisplay.Width, m_pbDisplay.Height);
            //m_gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //m_pbDisplay.Image = bmp;
            //m_gp.Dispose();
        }

        public void InitializePictureBox()
        {
            Debug.WriteLine("BaseActivator - Initialize");
            Debug.WriteLine(string.Format("Display Width: {0}, Height: {1}", m_pbDisplay.Width, m_pbDisplay.Height));
            Bitmap bmp = new Bitmap(m_pbDisplay.Width, m_pbDisplay.Height);
            m_gp = Graphics.FromImage(bmp);
            m_gp.FillRectangle(Brushes.White, 0, 0, m_pbDisplay.Width, m_pbDisplay.Height);
            m_gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            m_pbDisplay.Image = bmp;
            m_gp.Dispose();
        }

        public virtual void Display_MouseDown(object sender, MouseEventArgs e) { }

        public virtual void Display_MouseMove(object sender, MouseEventArgs e) { }

        public virtual void Display_MouseUp(object sender, MouseEventArgs e) { }

        public virtual void Display_MouseClick(object sender, MouseEventArgs e)
        {
            if (m_tempGesture.Activator != null)
            {
                if (m_gp != null) m_gp.Dispose();
                m_tempGesture.Activator.AnimateToPictureBox(m_pbDisplay);
            }
        }

        public void RedrawDisplay()
        {
            if (m_pbDisplay.Width <= 0 || m_pbDisplay.Height <= 0)
                return;
            if (m_tempGesture.Activator != null)
            {                
                m_tempGesture.Activator.DrawToPictureBox(m_pbDisplay);
            }
            else
            {
                if (m_pbDisplay.Image == null) return;
                m_gp = Graphics.FromImage(m_pbDisplay.Image);
                m_gp.FillRectangle(Brushes.White, 0, 0, m_pbDisplay.Width, m_pbDisplay.Height);
                m_pbDisplay.Invalidate();
                m_gp.Dispose();
            }
        }

        protected void SetInfoText(string text, bool usingGesture)
        {            
            if (!usingGesture)
                text = "This gesture type is currently deactivated.";

            StringFormat strFormat = new StringFormat();
            strFormat.LineAlignment = StringAlignment.Center;
            Font strFont = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular);            
            Rectangle r = new Rectangle(0, 0, m_pbInfo.Width, m_pbInfo.Height);
            if (r.Width <= 0 || r.Height <= 0) return;
            Bitmap bmp = new Bitmap(r.Width, r.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.FillRectangle(new SolidBrush(m_pbInfo.BackColor), r);//0, 0, bmp.Width, bmp.Height);\
            //g.DrawLine(new Pen(Color.Gray, 1), r.X, r.Height - 2, r.Width, r.Height - 2);
            //g.DrawLine(new Pen(Color.White, 1), r.X, r.Height - 1, r.Width, r.Height - 1);
            r.Height -= 2;
            int offset = 8;
            int height = r.Height - 2 * offset;
            if (!usingGesture)
                g.DrawImage(SystemIcons.Warning.ToBitmap(), offset, offset, height, height);
            else
                g.DrawImage(SystemIcons.Information.ToBitmap(), offset, offset, height, height);

            r.X += height + 2 * offset;
            r.Width -= height + 2 * offset;
            g.DrawString(text, strFont, Brushes.Black, r, strFormat);
            g.Dispose();
            m_pbInfo.Image = bmp;
        }

        public virtual void SetValues() { }
    }
}
