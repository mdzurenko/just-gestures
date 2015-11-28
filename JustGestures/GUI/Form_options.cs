using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JustGestures.Properties;
using JustGestures.OptionItems;
using JustGestures.Features;
using JustGestures.Languages;

namespace JustGestures.GUI
{
    public partial class Form_options : Form, ITranslation
    {        
        private enum Options
        {
            General,
            AutoBehaviour,
            BlackList,
            WhiteList,
            Gesture,
            Visual,
        }
        
        UC_generalOptions ucGeneralOptions;
        UC_autoBehaviour ucAutoBehaviour;
        UC_blackList ucBlackList;
        UC_whiteList ucWhiteList;
        UC_visualisation ucVisualisation;
        UC_gestureOptions ucGestureOptions;

        List<PrgNamePath> blackList;
        List<PrgNamePath> whiteList;
        List<PrgNamePath> finalList;
        List<PrgNamePath> backUpBlackList;
        List<PrgNamePath> backUpWhiteList;
        List<PrgNamePath> backUpFinalList;

        bool m_okClick = false;
        
        public delegate void DlgApplySettings();

        public DlgApplySettings ApplySettings;
        protected void OnApplySettings()
        {
            if (ApplySettings != null)
                ApplySettings();
        }

        #region ITranslation Members

        public void Translate()
        {
            Form_engine.Instance.Translate();

            // do not translate options when user clicked on OK (it will be closed)
            if (m_okClick) return;

            this.Text = Translation.GetText("O_caption");
            btn_ok.Text = Translation.Btn_ok;
            btn_cancel.Text = Translation.Btn_cancel;
            btn_apply.Text = Translation.Btn_apply;
            
            foreach (BaseOptionControl control in panel_fill.Controls)
                control.Translate();
            foreach (TreeNode node in tW_options.Nodes)
                TranslateTreeNode(node);
        }

        public void TranslateTreeNode(TreeNode root)
        {
            foreach (TreeNode node in root.Nodes)
                TranslateTreeNode(node);

            Options selectedOption = (Options)Enum.Parse(typeof(Options), root.Name);
            switch (selectedOption)
            {
                case Options.General: root.Text = ucGeneralOptions.ControlName; break;
                case Options.AutoBehaviour: root.Text = ucAutoBehaviour.ControlName; break;
                case Options.BlackList: root.Text = ucBlackList.ControlName; break;
                case Options.WhiteList: root.Text = ucWhiteList.ControlName; break;
                case Options.Visual: root.Text = ucVisualisation.ControlName; break;
                case Options.Gesture: root.Text = ucGestureOptions.ControlName; break;
            }
        }

        #endregion

        public Form_options()
        {
            InitializeComponent();
            MyInitializing();
        }

        private void MyInitializing()
        {
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, panel_fill, new object[] { true });
            this.Icon = Properties.Resources.options16;
            ucGeneralOptions = new UC_generalOptions();
            ucAutoBehaviour = new UC_autoBehaviour();
            ucWhiteList = new UC_whiteList();
            ucBlackList = new UC_blackList();
            ucVisualisation = new UC_visualisation();
            ucGestureOptions = new UC_gestureOptions();
            ucGeneralOptions.TranslateTexts = Translate;

            panel_fill.Controls.Add(ucGeneralOptions);
            panel_fill.Controls.Add(ucAutoBehaviour);
            panel_fill.Controls.Add(ucWhiteList);
            panel_fill.Controls.Add(ucBlackList);
            panel_fill.Controls.Add(ucGestureOptions);
            panel_fill.Controls.Add(ucVisualisation);

            foreach (BaseOptionControl control in panel_fill.Controls)
            {
                control.Dock = DockStyle.Fill;
                control.ChangeInfoText += new BaseOptionControl.DlgChangeInfoText(uC_infoIcon1.ChangeInfoText);
            }
            
            tW_options.Nodes.Add(Options.General.ToString(), ucGeneralOptions.ControlName);
            TreeNode nodeGestureOptions = new TreeNode(ucGestureOptions.ControlName);
            nodeGestureOptions.Name = Options.Gesture.ToString();
            nodeGestureOptions.Nodes.Add(Options.Visual.ToString(), ucVisualisation.ControlName);
            tW_options.Nodes.Add(nodeGestureOptions);
            TreeNode nodeAutoBehave = new TreeNode(ucAutoBehaviour.ControlName);
            nodeAutoBehave.Name = Options.AutoBehaviour.ToString();            
            nodeAutoBehave.Nodes.Add(Options.WhiteList.ToString(), ucWhiteList.ControlName);
            nodeAutoBehave.Nodes.Add(Options.BlackList.ToString(), ucBlackList.ControlName);
            tW_options.Nodes.Add(nodeAutoBehave);            
            tW_options.ExpandAll();
            //tW_options.Nodes.Add(Options.Visual.ToString(), ucVisualisation.About);

            foreach (BaseOptionControl control in panel_fill.Controls)
            {
                control.Visible = false;
                control.ChangeCaption += new BaseOptionControl.DlgChangeCaption(ChangeCaption);
                control.EnableApply += new BaseOptionControl.DlgEnableApply(EnableBtnApply);
            }
            ucGeneralOptions.Visible = true;
            btn_apply.Enabled = false;
            Translate();
        }

        private void ChangeCaption(string text) 
        {
            StringFormat strFormat = new StringFormat();
            strFormat.LineAlignment = StringAlignment.Center;
            //strFormat.Alignment = StringAlignment.Center;
            Font strFont = new Font(FontFamily.GenericSansSerif, 11, FontStyle.Bold);
            Rectangle r = new Rectangle(0, 0, pB_caption.Width, pB_caption.Height);

            Bitmap bmp = new Bitmap(pB_caption.Width, pB_caption.Height);
            Graphics gp = Graphics.FromImage(bmp); 
            Pen pen = new Pen(Brushes.Gray, 1);
            gp.FillRectangle(new SolidBrush(pB_caption.BackColor), r);
            gp.DrawLine(pen, 0, 0, pB_caption.Width, 0);
            gp.DrawLine(new Pen(Brushes.Gray, 1), 0, pB_caption.Height - 2, pB_caption.Width, pB_caption.Height - 2);
            //gp.DrawLine(new Pen(Brushes.Black, 1), 0, pB_caption.Height - 2, pB_caption.Width, pB_caption.Height - 2);
            gp.DrawLine(new Pen(Brushes.White, 1), 0, pB_caption.Height - 1, pB_caption.Width, pB_caption.Height - 1);            
            r.X += 10;
            //r.Width -= 6;
            r.Y += 2;
            r.Height -= 7;
            gp.DrawString(text, strFont, Brushes.Black, r, strFormat);
            gp.Dispose();
            pB_caption.Image = bmp;
        }

        private void EnableBtnApply(bool enable) { btn_apply.Enabled = enable; }

        private void SaveSettings()
        {
            foreach (BaseOptionControl control in panel_fill.Controls)
                control.SaveSettings();         
            blackList = ucBlackList.Programs;
            whiteList = ucWhiteList.Programs;
            finalList = ucAutoBehaviour.FinalList;
            backUpBlackList = blackList;
            backUpWhiteList = whiteList;
            backUpFinalList = finalList;            
            FileOptions.SaveLists(whiteList, blackList, finalList);
            Config.User.Save();
            OnApplySettings();
        }

        private void ReturnSettings()
        {
            blackList = backUpBlackList;
            whiteList = backUpWhiteList;
            finalList = backUpFinalList;            
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            m_okClick = true;
            SaveSettings();
        }

        private void tW_options_AfterSelect(object sender, TreeViewEventArgs e)
        {
            foreach (Control control in panel_fill.Controls)
                control.Visible = false;
            Options selectedOption = (Options)Enum.Parse(typeof(Options), e.Node.Name);

            switch (selectedOption)
            {
                case Options.General:
                    ucGeneralOptions.Visible = true;
                    break;
                case Options.AutoBehaviour:
                    //autoBehaviour.BlackList = blackList;
                    //autoBehaviour.WhiteList = whiteList;
                    //autoBehaviour.FinalList = finalList;
                    ucAutoBehaviour.Visible = true;
                    break;
                case Options.BlackList:
                    //ucBlackList.Programs = blackList;
                    ucBlackList.Visible = true;
                    break;
                case Options.WhiteList:
                    //ucWhiteList.Programs = whiteList;
                    ucWhiteList.Visible = true;
                    break;
                case Options.Gesture:
                    ucGestureOptions.Visible = true;
                    break;
                case Options.Visual:
                    ucVisualisation.Visible = true;
                    break;
            }
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            SaveSettings();
            btn_apply.Enabled = false;
        }

        //public List<PrgNamePath> BlackList { get { return blackList; } set { blackList = value; } }
        //public List<PrgNamePath> WhiteList { get { return whiteList; } set { whiteList = value; } }
        public List<PrgNamePath> FinalList { get { return finalList; } set { finalList = value; } }

        private void Form_options_Load(object sender, EventArgs e)
        {
            FileOptions.LoadLists(out whiteList, out blackList, out finalList);
            ucBlackList.Programs = blackList;
            ucWhiteList.Programs = whiteList;
            ucAutoBehaviour.BlackList = blackList;
            ucAutoBehaviour.WhiteList = whiteList;
            ucAutoBehaviour.FinalList = finalList;
            backUpWhiteList = new List<PrgNamePath>(whiteList);
            backUpBlackList = new List<PrgNamePath>(blackList);
            backUpFinalList = new List<PrgNamePath>(finalList);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            ReturnSettings();
        }

        private void Form_options_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReturnSettings();            
        }

        private void panel_down_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panel_down.CreateGraphics();
            g.DrawLine(new Pen(Brushes.Gray, 1), 0, 2, panel_down.Width, 2);
            g.DrawLine(new Pen(Brushes.White, 1), 0, 1, panel_down.Width, 1);
            g.Dispose();
        }

        private void tW_options_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }


    }
}