using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NeuralNetwork;
using JustGestures.TypeOfAction;
using JustGestures.Languages;

namespace JustGestures.GUI
{
    using ControlItems;
    public partial class Form_modifyGesture : Form
    {
        GesturesCollection gestures;
        MyGesture[] modifiedGestures;
        MyGesture tempGesture;
        UC_openPrgFld uc_openPrgFld;        
        UC_name uc_name;
        UC_gesture uc_gesture;
        UC_customKeystrokes uc_customKeystrokes;
        UC_selectProgram uc_selectProgram;
        UC_plainText uc_plainText;
        UC_clipboard uc_clipboard;
        UC_mailSearchWeb uc_mailSearchWeb;
        bool canContinue;
        MyNeuralNetwork m_network;
        MyNeuralNetwork m_networkOriginal;
        bool m_appMode = false;


        public Form_modifyGesture()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.modify16;
            canContinue = true;

            #region User Control Declaration
            uc_name = new UC_name();
            uc_gesture = new UC_gesture();
            uc_openPrgFld = new UC_openPrgFld();
            uc_mailSearchWeb = new UC_mailSearchWeb();
            uc_customKeystrokes = new UC_customKeystrokes();
            uc_selectProgram = new UC_selectProgram();
            uc_plainText = new UC_plainText();
            uc_clipboard = new UC_clipboard();
            
            List<BaseActionControl> uc_controls = new List<BaseActionControl>();
            uc_controls.Add(uc_name);
            uc_controls.Add(uc_gesture);
            uc_controls.Add(uc_openPrgFld);
            uc_controls.Add(uc_customKeystrokes);
            uc_controls.Add(uc_selectProgram);
            uc_controls.Add(uc_plainText);
            uc_controls.Add(uc_clipboard);
            uc_controls.Add(uc_mailSearchWeb);

            foreach (BaseActionControl uc_control in uc_controls)
            {
                uc_control.CanContinue += new BaseActionControl.DlgCanContinue(Continue);
                uc_control.ChangeInfoText += new BaseActionControl.DlgChangeInfoText(uC_infoIcon1.ChangeInfoText);
                uc_control.Dock = DockStyle.Fill;
            }

            #endregion User Control Declaration

            btn_cancel.Text = Translation.Btn_cancel;
            btn_ok.Text = Translation.Btn_ok;
        }

        public bool AppMode { set { m_appMode = value; } }

        public GesturesCollection Gestures
        {
            get { return gestures; }
            set { gestures = value; }
        }

        public MyGesture[] ModifiedGestures
        {
            get { return modifiedGestures; }
            set { modifiedGestures = value; }
        }

        public MyNeuralNetwork MyNNetwork
        {
            get { return m_network; }
            set { m_network = value; }
        }

        public MyNeuralNetwork MyNNetworkOriginal
        {
            set { m_networkOriginal = value; }
        }

        public void Continue(bool _continue)
        {
            canContinue = _continue;
            if (canContinue)
            {
                btn_ok.Enabled = true;
            }
            else
            {
                btn_ok.Enabled = false;
            }
        }

        private void Form_modifyGesture_Load(object sender, EventArgs e)
        {
            if (!m_appMode)
            {
                this.Size = new Size(680, 580);
                this.Text = modifiedGestures[0].Caption;
                bool singleGesture = true;
                bool singleCurve = true;
                tempGesture = new MyGesture(modifiedGestures[0]);
                if (modifiedGestures.Length > 1)
                {
                    this.Text = Translation.GetText("MG_multiple"); //Modify Multiple Gestures
                    singleGesture = false;
                    string curve = modifiedGestures[0].Activator.ID;
                    foreach (MyGesture gest in modifiedGestures)
                        if (curve != gest.Activator.ID)
                            singleCurve = false;
                    if (!singleCurve)
                    {
                        tempGesture = new MyGesture(string.Empty);
                        tempGesture.Action = modifiedGestures[0].Action;
                    }
                }
                uc_openPrgFld.TempGesture = tempGesture;
                uc_mailSearchWeb.TempGesture = tempGesture;
                uc_gesture.MyNNetwork = m_network;
                uc_gesture.MyNNetworkOriginal = m_networkOriginal;
                uc_gesture.TempGesture = tempGesture;
                uc_name.TempGesture = tempGesture;
                uc_gesture.Gestures = gestures;
                uc_name.Gestures = gestures;
                uc_customKeystrokes.TempGesture = tempGesture;
                uc_plainText.TempGesture = tempGesture;
                uc_clipboard.TempGesture = tempGesture;
                uc_clipboard.Gestures = gestures;

                if (singleGesture)
                {
                    TabPage tP_gesture = new TabPage();
                    tP_gesture.Text = Translation.GetText("MG_tP_gesture"); //  "Gesture";
                    tP_gesture.Controls.Add(uc_gesture);                    
                    TabPage tP_name = new TabPage();
                    tP_name.Text = Translation.GetText("MG_tP_name"); // "Name"
                    tP_name.Controls.Add(uc_name);

                    TabPage tp_mailSearchWeb = new TabPage();
                    TabPage tP_openPrgFld = new TabPage();
                    TabPage tP_customKeystrokes = new TabPage();
                    switch (tempGesture.Action.Name)
                    {
                        case WindowsShell.SHELL_START_PRG:
                            tP_openPrgFld.Text = Translation.GetText("MG_tP_program"); // "Program";
                            tP_openPrgFld.Controls.Add(uc_openPrgFld);
                            tabControl1.TabPages.Add(tP_openPrgFld);
                            break;                      
                        case WindowsShell.SHELL_OPEN_FLDR:
                            tP_openPrgFld.Text = Translation.GetText("MG_tP_folder"); // "Folder";
                            tP_openPrgFld.Controls.Add(uc_openPrgFld);
                            tabControl1.TabPages.Add(tP_openPrgFld);
                            break;
                        case InternetOptions.INTERNET_SEARCH_WEB:
                        case InternetOptions.INTERNET_OPEN_WEBSITE:
                            tp_mailSearchWeb.Text = Translation.GetText("MG_tP_url"); // "Website Url";
                            tp_mailSearchWeb.Controls.Add(uc_mailSearchWeb);
                            tabControl1.TabPages.Add(tp_mailSearchWeb);
                            break;
                        case InternetOptions.INTERNET_SEND_EMAIL:
                            tp_mailSearchWeb.Text = Translation.GetText("MG_tP_mail"); // "E-mail";
                            tp_mailSearchWeb.Controls.Add(uc_mailSearchWeb);
                            tabControl1.TabPages.Add(tp_mailSearchWeb);
                            break;
                        case InternetOptions.INTERNET_TAB_NEW:
                        case InternetOptions.INTERNET_TAB_CLOSE:
                        case InternetOptions.INTERNET_TAB_REOPEN:
                        case KeystrokesOptions.KEYSTROKES_ZOOM_IN:
                        case KeystrokesOptions.KEYSTROKES_ZOOM_OUT:
                        case KeystrokesOptions.KEYSTROKES_SYSTEM_COPY:
                        case KeystrokesOptions.KEYSTROKES_SYSTEM_CUT:
                        case KeystrokesOptions.KEYSTROKES_SYSTEM_PASTE:
                        case KeystrokesOptions.KEYSTROKES_CUSTOM:
                        case ExtrasOptions.EXTRAS_CUSTOM_WHEEL_BTN:
                        case ExtrasOptions.EXTRAS_TAB_SWITCHER:
                        case ExtrasOptions.EXTRAS_TASK_SWITCHER:
                        case ExtrasOptions.EXTRAS_ZOOM:
                            tP_customKeystrokes.Text = Translation.GetText("MG_tP_customKeystrokes"); // "Custom Keystrokes";
                            tP_customKeystrokes.Controls.Add(uc_customKeystrokes);
                            tabControl1.TabPages.Add(tP_customKeystrokes);
                            break;
                        case KeystrokesOptions.KEYSTROKES_PRIVATE_COPY_TEXT:
                        case KeystrokesOptions.KEYSTROKES_PRIVATE_PASTE_TEXT:
                            TabPage tP_clipboard = new TabPage();
                            tP_clipboard.Text = Translation.GetText("MG_tP_privateClipboard"); // "Private Clipboard";
                            tP_clipboard.Controls.Add(uc_clipboard);
                            tabControl1.TabPages.Add(tP_clipboard);
                            break;
                        case KeystrokesOptions.KEYSTROKES_PLAIN_TEXT:
                            TabPage tP_plainText = new TabPage();
                            tP_plainText.Text = Translation.GetText("MG_tP_plainText"); // "Plain Text";
                            tP_plainText.Controls.Add(uc_plainText);
                            tabControl1.TabPages.Add(tP_plainText);
                            break;
                    }
                    tabControl1.TabPages.Add(tP_gesture);
                    tabControl1.TabPages.Add(tP_name);
                }
                else
                {
                    TabPage tP_gesture = new TabPage();
                    tP_gesture.Text = Translation.GetText("MG_tP_gesture"); //"Gesture"
                    tP_gesture.Controls.Add(uc_gesture);
                    tabControl1.TabPages.Add(tP_gesture);
                }
            }
            else
            {
                this.Text = modifiedGestures[0].Caption;
                this.Size = new Size(530, 430);
                tempGesture = new MyGesture(modifiedGestures[0]);
                uc_selectProgram.TempGesture = tempGesture;
                TabPage tP_application = new TabPage();
                tP_application.Text = Translation.GetText("MG_tP_application"); // "Application"
                tP_application.Controls.Add(uc_selectProgram);
                tabControl1.TabPages.Add(tP_application);
            }
            foreach (TabPage page in tabControl1.TabPages)
                page.Padding = new Padding(15, 10, 15, 10);

            
        }

        private void tabControl1_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (!canContinue)
            {
                e.Cancel = true;
                MessageBox.Show(this, Translation.GetText("MG_msgText_validProps"), Translation.Text_warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }	
        } 

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (!m_appMode)
                m_network = uc_gesture.MyNNetwork;
            if (modifiedGestures.Length == 1)
            {
                modifiedGestures[0].SetItem(tempGesture);
                modifiedGestures[0].Activator = tempGesture.Activator;
                modifiedGestures[0].Action = tempGesture.Action;
                if (modifiedGestures[0].IsImplicitOnly)
                    modifiedGestures[0].ExecutionType = ExecuteType.Implicit;
                if (!m_appMode)
                    modifiedGestures[0].ItemPos = -1;
            }
            else
            {
                foreach (MyGesture gest in modifiedGestures)
                {
                    gest.Activator = tempGesture.Activator;
                    gest.ItemPos = -1;
                }
            }
        }

        private void Form_modifyGesture_FormClosing(object sender, FormClosingEventArgs e)
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

        private void panel_down_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panel_down.CreateGraphics();
            g.DrawLine(new Pen(Brushes.Gray, 1), 0, 2, panel_down.Width, 2);
            g.DrawLine(new Pen(Brushes.White, 1), 0, 1, panel_down.Width, 1);
            g.Dispose();
            
        }

      

    }
}