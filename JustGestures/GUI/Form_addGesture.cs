using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Imaging;
using JustGestures.GestureParts;
using JustGestures.Languages;

namespace JustGestures.GUI
{
    using ControlItems;
    public partial class Form_addGesture : Form
    {
        BaseActionControl m_currentControl;
        UC_actions uc_actions;
        UC_name uc_name;
        UC_gesture uc_gesture;
        UC_openPrgFld uc_openPrgFld;
        UC_mailSearchWeb uc_mailSearchWeb;
        UC_customKeystrokes uc_customKeystrokes;
        UC_selectProgram uc_selectProgram;
        UC_plainText uc_plainText;
        UC_clipboard uc_clipboard;
        List<MyGesture> m_newGestures;
        MyGesture m_tempGesture;
        GesturesCollection m_gestures;
        MyNeuralNetwork m_network;
        bool m_appMode = false;
        List<MyGesture> m_selectedGroups;
        

        public Form_addGesture()
        {
            InitializeComponent();            
            MyInitializing();
        }

        public bool AppMode { set { m_appMode = value; } }

        public GesturesCollection Gestures
        {
            //get { return gestures; }
            set { m_gestures = value; }
        }

        public List<MyGesture> NewGestures
        {
            get { return m_newGestures; }
        }

        public MyNeuralNetwork MyNNetwork
        {
            get { return m_network; }
            set { m_network = value; }
        }

        public List<MyGesture> SelectedGroups { set { m_selectedGroups = value; } }

        private void MyInitializing()
        {
            typeof(GroupBox).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, groupBox_MainPanel, new object[] { true });
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            m_tempGesture = new MyGesture(string.Empty);
            m_newGestures = new List<MyGesture>();
            m_selectedGroups = new List<MyGesture>();

            #region User Control Declaration

            uc_openPrgFld = new UC_openPrgFld();
            uc_actions = new UC_actions();
            uc_name = new UC_name();
            uc_gesture = new UC_gesture();
            uc_customKeystrokes = new UC_customKeystrokes();
            uc_selectProgram = new UC_selectProgram();
            uc_plainText = new UC_plainText();
            uc_clipboard = new UC_clipboard();
            uc_mailSearchWeb = new UC_mailSearchWeb();

            groupBox_MainPanel.Controls.Add(uc_actions);
            groupBox_MainPanel.Controls.Add(uc_name);
            groupBox_MainPanel.Controls.Add(uc_gesture);
            groupBox_MainPanel.Controls.Add(uc_openPrgFld);
            groupBox_MainPanel.Controls.Add(uc_customKeystrokes);
            groupBox_MainPanel.Controls.Add(uc_selectProgram);
            groupBox_MainPanel.Controls.Add(uc_plainText);
            groupBox_MainPanel.Controls.Add(uc_clipboard);
            groupBox_MainPanel.Controls.Add(uc_mailSearchWeb);

            foreach (BaseActionControl myControl in groupBox_MainPanel.Controls)
            {                
                myControl.Dock = DockStyle.Fill;
                myControl.TempGesture = m_tempGesture;
                myControl.CanContinue += new BaseActionControl.DlgCanContinue(Continue);
                myControl.ChangeAboutText += new BaseActionControl.DlgChangeAboutText(ChangeAboutText);
                myControl.ChangeInfoText += new BaseActionControl.DlgChangeInfoText(uC_infoIcon1.ChangeInfoText);
                myControl.Visible = false;
            }

            #endregion User Control Declaration

            btn_back.Text = Translation.Btn_back;
            btn_cancel.Text = Translation.Btn_cancel;
            btn_next.Text = Translation.Btn_next;
        }

        private void Form_addGesture_Load(object sender, EventArgs e)
        {           
            if (!m_appMode)
            {
                this.Text = Translation.GetText("AG_caption");
                this.Icon = Properties.Resources.add_gesture16;
                uc_gesture.MyNNetwork = m_network;
                uc_gesture.Gestures = m_gestures;
                uc_name.Gestures = m_gestures;                
                uc_actions.Gestures = m_gestures;
                uc_actions.SelectedGroups = m_selectedGroups;
                m_currentControl = uc_actions;
                uc_clipboard.Gestures = m_gestures;
                this.Size = new Size(680, 580);
            }
            else
            {
                this.Text = Translation.GetText("AA_caption");
                this.Icon = Properties.Resources.add_app16;
                btn_back.Visible = false;
                m_currentControl = uc_selectProgram;
                this.Size = new Size(530, 430);
            }
            this.MinimumSize = this.MaximumSize = this.Size;
            m_currentControl.Show();
            MoveToPage(m_currentControl.Identifier);
        }

        private void MoveToPage(BaseActionControl.Page page)
        {            
            m_currentControl.Hide();
            btn_next.Enabled = false;
            btn_back.Enabled = true;
            switch (page)
            {                           
                case BaseActionControl.Page.Action:
                    m_currentControl = uc_actions; 
                    break;
                case BaseActionControl.Page.PrgWwwFld:
                    m_currentControl = uc_openPrgFld;
                    break;
                case BaseActionControl.Page.MailSearchWeb:
                    m_currentControl = uc_mailSearchWeb;
                    break;
                case BaseActionControl.Page.Keystrokes:
                    m_currentControl = uc_customKeystrokes;
                    break;  
                case BaseActionControl.Page.Gesture:
                    m_currentControl = uc_gesture;
                    uc_gesture.Previous = uc_actions.Next == uc_gesture.Identifier ? uc_actions.Identifier : uc_actions.Next;
                    break;
                case BaseActionControl.Page.Name:
                    m_currentControl = uc_name;                    
                    break;
                case BaseActionControl.Page.Application:
                    m_currentControl = uc_selectProgram;
                    break;
                case BaseActionControl.Page.PlainText:
                    m_currentControl = uc_plainText;
                    break;
                case BaseActionControl.Page.Clipboard:
                    m_currentControl = uc_clipboard;
                    break;
            }
            m_currentControl.Show();


            if (m_currentControl.Next != BaseActionControl.Page.None)
            {
                btn_next.DialogResult = DialogResult.None;
                btn_next.Text = Translation.Btn_next;
            }
            else
            {
                btn_next.DialogResult = DialogResult.OK;
                if (!m_appMode)
                    btn_next.Text = Translation.Btn_finish;
                else
                    btn_next.Text = Translation.Btn_add;
            }
            if (m_currentControl.Previous == BaseActionControl.Page.None)
            {
                btn_back.Enabled = false;
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        private void ChangeAboutText(string title) 
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
            
            int height = pB_about.Height - 12;
            int offset = pB_about.Height;
            gp.DrawImage(Properties.Resources.logo32, pB_about.Width - offset, 6, height, height);
                        
            //gp.DrawLine(pen, 0, 0, pB_about.Width, 0);
            gp.DrawLine(new Pen(Brushes.Gray, 1), 0, pB_about.Height - 2, pB_about.Width, pB_about.Height - 2);
            //gp.DrawLine(new Pen(Brushes.Black, 1), 0, pB_about.Height - 2, pB_about.Width, pB_about.Height - 2);
            gp.DrawLine(new Pen(Brushes.White, 1), 0, pB_about.Height - 1, pB_about.Width, pB_about.Height - 1);
            r.X += 10;
            //r.Width -= 6;
            r.Y += 2;
            r.Height -= 7;
            gp.DrawString(title, strFont, Brushes.Black, r, strFormat);
            gp.Dispose();
            pB_about.Image = bmp;
        }        

        private void Continue(bool _continue) { btn_next.Enabled = _continue; }


        private void button_next_Click(object sender, EventArgs e)
        {
            if (btn_next.DialogResult == DialogResult.OK)
            {
                if (!m_appMode)
                {
                    m_network = uc_gesture.MyNNetwork;
                    foreach (MyGesture group in uc_actions.NewGroups)
                        m_newGestures.Add(group);
                    foreach (MyGesture group in uc_actions.SelectedGroups)
                    {
                        string id = MyGesture.CreateUniqueId(m_tempGesture, m_gestures);
                        MyGesture gest = new MyGesture(id);
                        gest.SetItem(m_tempGesture);
                        gest.Activator = m_tempGesture.Activator;
                        gest.Action = m_tempGesture.Action;
                        gest.AppGroup = group;
                        m_newGestures.Add(gest);
                        m_gestures.Add(gest);
                    }
                }
                else
                {
                    string id = MyGesture.CreateUniqueId(m_tempGesture, m_gestures);
                    MyGesture gest = new MyGesture(id);
                    gest.SetItem(m_tempGesture);
                    gest.Action = m_tempGesture.Action;
                    gest.Activator = new MouseActivator(string.Empty, MouseActivator.Types.Undefined);
                    m_newGestures.Add(gest);
                }
            }
            MoveToPage(m_currentControl.Next);
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            MoveToPage(m_currentControl.Previous);
        }

        private void Form_addGesture_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!m_appMode)
            {
                uc_gesture.OnClosingForm(this.DialogResult);
                uc_openPrgFld.OnClosingForm();
                uc_mailSearchWeb.OnClosingForm();
            }
            else
                uc_selectProgram.OnClosingForm();
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