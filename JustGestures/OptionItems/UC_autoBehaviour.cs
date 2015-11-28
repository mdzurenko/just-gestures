using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JustGestures.Properties;
using JustGestures.Features;
using JustGestures.Languages;

namespace JustGestures.OptionItems
{
    public partial class UC_autoBehaviour : BaseOptionControl
    {
        public const string STATE_ENABLED = "enabled";
        public const string STATE_DISABLED = "disabled";
        public const int AUTO_CHECK_MIN = 100;
        public const int AUTO_CHECK_MAX = 2000;
        const string AUTO_1 = "auto1";
        const string AUTO_2 = "auto2";
        const string FULLSCREEN = "fullscreen";
        const string DEFAULT = "default";
        List<PrgNamePath> whiteList;
        List<PrgNamePath> blackList;
        List<PrgNamePath> finalList;
        bool onLoad;

        public override void Translate()
        {
            base.Translate();
            gB_conditions.Text = Translation.GetText("O_AB_gB_conditions");            
            lbl_autocheck.Text = Translation.GetText("O_AB_lbl_autocheck");
            lbl_defaultState.Text = Translation.GetText("O_AB_lbl_defaultState");
            lbl_fullscreenState.Text = Translation.GetText("O_AB_lbl_fullscreenState");
            lbl_1AutoState.Text = Translation.GetText("O_AB_lbl_1AutoState");
            lbl_2AutoState.Text = Translation.GetText("O_AB_lbl_2AutoState");
            gB_finalList.Text = Translation.GetText("O_AB_gB_finalList");
            cH_appState.Text = Translation.GetText("O_AB_cH_appState");
            cH_name.Text = Translation.GetText("O_AB_cH_name");
            cH_path.Text = Translation.GetText("O_AB_cH_path");
            cB_default.Items[0] = cB_fullscreen.Items[0] = Translation.GetText("O_AB_cBI_none");
            cB_default.Items[1] = cB_fullscreen.Items[1] = Translation.GetText("O_AB_cBI_autoEnable");
            cB_default.Items[2] = cB_fullscreen.Items[2] = Translation.GetText("O_AB_cBI_autoDisable");
            cB_auto1.Items[0] = cB_auto2.Items[0] = Translation.GetText("O_AB_cBI_none");
            cB_auto1.Items[1] = cB_auto2.Items[1] = Translation.GetText("O_AB_whiteList");
            cB_auto1.Items[2] = cB_auto2.Items[2] = Translation.GetText("O_AB_blackList");
        }

        public UC_autoBehaviour()
        {
            InitializeComponent();
            I_infoText = new MyText("O_AB_info");
            m_name = new MyText("O_AB_name");
            m_caption = new MyText("O_AB_caption");
            this.Dock = DockStyle.Fill;

            onLoad = true;
            cB_default.Tag = DEFAULT;
            cB_fullscreen.Tag = FULLSCREEN;
            cB_auto1.Tag = AUTO_1;
            cB_auto2.Tag = AUTO_2;
            cB_default.SelectedIndex = Config.User.StateDefault;
            cB_fullscreen.SelectedIndex = Config.User.StateFullScreen;
            cB_auto1.SelectedIndex = Config.User.StateAuto1;
            cB_auto2.SelectedIndex = Config.User.StateAuto2;
            nUD_autocheckTime.Value = Math.Min(Math.Max(Config.User.CheckWndLoop, nUD_autocheckTime.Minimum), nUD_autocheckTime.Maximum);
            onLoad = false;
            OnEnableApply(false);
        }

        public override void SaveSettings()
        {
            UpdateFinalList();
            Config.User.StateDefault = cB_default.SelectedIndex;
            Config.User.StateFullScreen = cB_fullscreen.SelectedIndex;
            Config.User.StateAuto1 = cB_auto1.SelectedIndex;
            Config.User.StateAuto2 = cB_auto2.SelectedIndex;
            Config.User.CheckWndLoop = (int)nUD_autocheckTime.Value;
        }
   
        private void comboBox_selectedIndexChanged(object sender, EventArgs e)
        {
            if (onLoad) return;
            OnEnableApply(true);
            UpdateFinalList();            
        }

        private void UpdateFinalList()
        {
            lV_programs.Items.Clear();
            finalList.Clear();
            if (cB_auto1.SelectedIndex != 0 || cB_auto2.SelectedIndex != 0)
            {
                lV_programs.Enabled = true;
                switch (cB_auto1.SelectedIndex)
                {
                    case 0:
                        switch (cB_auto2.SelectedIndex)
                        {
                            case 0: lV_programs.Enabled = false; break;
                            case 1: AddToFinalList(whiteList, STATE_ENABLED); break;
                            case 2: AddToFinalList(blackList, STATE_DISABLED); break;
                        }
                        break;
                    case 1:
                        switch (cB_auto2.SelectedIndex)
                        {
                            case 0:
                            case 1: AddToFinalList(whiteList, STATE_ENABLED); break;
                            case 2: MergeWhiteBlackList(whiteList, blackList, false); break;
                        }
                        break;
                    case 2:
                        switch (cB_auto2.SelectedIndex)
                        {
                            case 0:
                            case 2: AddToFinalList(blackList, STATE_DISABLED); break;
                            case 1: MergeWhiteBlackList(whiteList, blackList, true); break;
                        }
                        break;
                }
                lV_programs.Items.AddRange(finalList.ToArray());
            }
            else
            {
                lV_programs.Enabled = false;
            }
        }

        private void AddToFinalList(List<PrgNamePath> list, string state)
        {            
            foreach (PrgNamePath prog in list)
            {
                if (prog.Active)
                    finalList.Add(new PrgNamePath(state, prog.PrgName, prog.Path));
            }
        }

        private bool ListContainItem(List<PrgNamePath> list, PrgNamePath item)
        {
            foreach (PrgNamePath prog in list)
                if (prog.Active && 
                    prog.PrgName == item.PrgName && 
                    prog.Path == item.Path)
                    return true;
            return false;

        }

        /// <summary>
        /// Merge like the first list is White and second is Black
        /// </summary>
        /// <param name="list1">list in auto1</param>
        /// <param name="list2">list in auto2</param>
        /// <param name="reversed">reverse order of lists</param>
        private void MergeWhiteBlackList(List<PrgNamePath> list1, List<PrgNamePath> list2, bool reversed)
        {
            foreach (PrgNamePath prog in whiteList)
            {
                if (prog.Active)
                {
                    if (ListContainItem(blackList, prog))
                    {
                        if (reversed) finalList.Add(new PrgNamePath(STATE_ENABLED, prog.PrgName, prog.Path));
                        else finalList.Add(new PrgNamePath(STATE_DISABLED, prog.PrgName, prog.Path));
                    }
                    else
                    {
                        finalList.Add(new PrgNamePath(STATE_ENABLED, prog.PrgName, prog.Path));
                    }
                }
            }
            
            foreach (PrgNamePath prog in blackList)
            {
                if (prog.Active)
                {
                    if (!ListContainItem(whiteList, prog))
                    {
                        finalList.Add(new PrgNamePath(STATE_DISABLED, prog.PrgName, prog.Path));
                    }
                }
            }            
        }
       
        public List<PrgNamePath> WhiteList
        {
            set { whiteList = value; }
        }

        public List<PrgNamePath> BlackList
        {
            set { blackList = value; }
        }

        public List<PrgNamePath> FinalList
        {
            get { return finalList; }
            set { finalList = value; }
        }

        private void UC_autoBehaviour_VisibleChanged(object sender, EventArgs e)
        {
            if (((UC_autoBehaviour)sender).Visible)
                 UpdateFinalList();
        }

        private void UC_autoBehaviour_Load(object sender, EventArgs e)
        {
            lV_programs.Items.Clear();
            if (finalList != null)
                foreach (PrgNamePath item in finalList)
                    lV_programs.Items.Add((ListViewItem)item.Clone());
        }

        private void nUD_autocheckTime_ValueChanged(object sender, EventArgs e)
        {
            OnEnableApply(true);
        }


    }
}
