using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JustGestures.WizardItems;
using JustGestures.Languages;

namespace JustGestures.GUI
{
    public partial class Form_wizard : Form
    {
        BaseWizardControl m_currentControl;
        UC_W_welcome uc_welcome;
        UC_W_classicCurve uc_classicCurve;
        UC_W_doubleBtn uc_doubleBtn;
        UC_W_wheelBtn uc_wheelBtn;
        UC_W_activation uc_final;

        MyText W_page = new MyText("W_page");
        
        public Form_wizard()
        {   
            InitializeComponent();
            this.Text = Translation.GetText("W_caption");
            btn_back.Text = Translation.Btn_back;
            btn_next.Text = Translation.Btn_next;
            btn_cancel.Text = Translation.Btn_cancel;
            W_page.Translate();
            typeof(GroupBox).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, groupBox_MainPanel, new object[] { true });

            uc_welcome = new UC_W_welcome();
            uc_classicCurve = new UC_W_classicCurve();
            uc_doubleBtn = new UC_W_doubleBtn();
            uc_wheelBtn = new UC_W_wheelBtn();
            uc_final = new UC_W_activation();

            groupBox_MainPanel.Controls.Add(uc_welcome);
            groupBox_MainPanel.Controls.Add(uc_classicCurve);
            groupBox_MainPanel.Controls.Add(uc_doubleBtn);
            groupBox_MainPanel.Controls.Add(uc_wheelBtn);
            groupBox_MainPanel.Controls.Add(uc_final);

            foreach (BaseWizardControl control in groupBox_MainPanel.Controls)
            {
                control.Dock = DockStyle.Fill;
                control.Visible = false;
                control.ChangeCaption += new BaseWizardControl.DlgChangeCaption(ChangeCaption);
                control.ChangeInfoText += new BaseWizardControl.DlgChangeInfoText(uC_infoIcon1.ChangeInfoText);
                control.ShowBalloonTip += new BaseWizardControl.DlgShowBalloonTip(ShowBallonTip);
            }
            m_currentControl = uc_welcome;
            MoveToPage(m_currentControl.Identifier);           
        }        

        private void MoveToPage(BaseWizardControl.Page page)
        {
            m_currentControl.Hide();
            btn_back.Enabled = true;
            switch (page)
            {
                case BaseWizardControl.Page.Welcome:
                    m_currentControl = uc_welcome;
                    break;
                case BaseWizardControl.Page.ClassicCurve:
                    m_currentControl = uc_classicCurve;
                    break;
                case BaseWizardControl.Page.DoubleBtn:
                    m_currentControl = uc_doubleBtn;
                    break;
                case BaseWizardControl.Page.WheelBtn:
                    m_currentControl = uc_wheelBtn;
                    break;
                case BaseWizardControl.Page.Activation:
                    m_currentControl = uc_final;
                    break;               
            }
            m_currentControl.Show();


            if (m_currentControl.Next != BaseWizardControl.Page.None)
            {
                btn_next.DialogResult = DialogResult.None;
                btn_next.Text = Translation.Btn_next;
            }
            else
            {
                btn_next.DialogResult = DialogResult.OK;
                btn_next.Text = Translation.Btn_finish;
            }
            if (m_currentControl.Previous == BaseWizardControl.Page.None)
            {
                btn_back.Enabled = false;
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            uC_infoIcon1.HideBallonTip();
            this.Close();
        }

        private void button_next_Click(object sender, EventArgs e)
        {
            uC_infoIcon1.HideBallonTip();
            if (btn_next.DialogResult == DialogResult.OK)
            {
                foreach (BaseWizardControl control in groupBox_MainPanel.Controls)
                    control.SaveSettings();
                Config.User.Save();
            }
            else
                MoveToPage(m_currentControl.Next);
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            MoveToPage(m_currentControl.Previous);
        }

        private void ShowBallonTip(int milliseconds)
        {
            uC_infoIcon1.ShowBalloonTip(5000);
        }

        private void ChangeCaption(string title)
        {
            StringFormat strFormat = new StringFormat();
            strFormat.LineAlignment = StringAlignment.Center;
            //strFormat.Alignment = StringAlignment.Center;
            Font strFont = new Font(FontFamily.GenericSansSerif, 11, FontStyle.Bold);
            Rectangle r = new Rectangle(0, 0, pB_about.Width, pB_about.Height);

            Bitmap bmp = new Bitmap(pB_about.Width, pB_about.Height);
            Graphics gp = Graphics.FromImage(bmp);
            gp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            Pen pen = new Pen(Brushes.Gray, 1);
            gp.FillRectangle(new SolidBrush(pB_about.BackColor), r);

            //gp.DrawLine(pen, 0, 0, pB_about.Width, 0);
            gp.DrawLine(new Pen(Brushes.Gray, 1), 0, pB_about.Height - 2, pB_about.Width, pB_about.Height - 2);
            //gp.DrawLine(new Pen(Brushes.Black, 1), 0, pB_about.Height - 2, pB_about.Width, pB_about.Height - 2);
            gp.DrawLine(new Pen(Brushes.White, 1), 0, pB_about.Height - 1, pB_about.Width, pB_about.Height - 1);
            r.X += 10;
            //r.Width -= 6;
            r.Y += 2;
            r.Height -= 7;
            gp.DrawString(title, strFont, Brushes.Black, r, strFormat);

            int pos = (int)m_currentControl.Identifier;
            string text = string.Format(W_page + " {0}/{1}", pos, groupBox_MainPanel.Controls.Count);
            int width = (int)gp.MeasureString(text, strFont).Width + 20;
            r = new Rectangle(pB_about.Width - width, 0, width, pB_about.Height);
            gp.DrawString(text, strFont, Brushes.Black, r, strFormat);
            gp.Dispose();
            pB_about.Image = bmp;
        }

        private void panel_ForButtons_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panel_ForButtons.CreateGraphics();
            g.DrawLine(new Pen(Brushes.Gray, 1), 0, 2, panel_ForButtons.Width, 2);
            //g.DrawLine(new Pen(Brushes.Black, 1), 0, 2, panel_ForButtons.Width, 2);
            g.DrawLine(new Pen(Brushes.White, 1), 0, 1, panel_ForButtons.Width, 1);
            g.Dispose();
        }
    }
}
