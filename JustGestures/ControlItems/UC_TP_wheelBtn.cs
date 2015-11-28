using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JustGestures.GestureParts;
using JustGestures.Languages;

namespace JustGestures.ControlItems
{
    public partial class UC_TP_wheelBtn : UC_TP_baseActivator
    {
        WheelButton m_wheelBtnAction = null;

        public UC_TP_wheelBtn()
        {
            InitializeComponent();
            for (int i = 0; i < ConvertValue.MouseButtonsCount; i++)
                cB_holdBtn.Items.Add(Translation.GetMouseBtnText(ConvertValue.IndexToMouseBtn(i)));
            m_wheelBtnAction = new WheelButton(MouseButtons.None);
            lbl_holdBtn.Text = Translation.Name_WheelHoldDownBtn;
            rTB_pushHoldBtn.Text = Translation.GetText("C_WheelG_pushHoldBtn");
            rTB_scrollWheel.Text = Translation.GetText("C_WheelG_scrollWithWheel");
            rTB_releaseHoldBtn.Text = Translation.GetText("C_WheelG_releaseHoldBtn");
            cH_associatedActions.Text = Translation.GetText("C_Gestures_cH_associatedActions");
            cH_group.Text = Translation.GetText("C_Gestures_cH_group");
        }

        public override void Initialize()
        {
            base.Initialize();
            if (m_tempGesture.Activator != null && m_tempGesture.Activator.Type == BaseActivator.Types.WheelButton)
            {
                m_wheelBtnAction = m_tempGesture.Activator;
                if (m_wheelBtnAction.Trigger != MouseButtons.None)
                    cB_holdBtn.SelectedIndex = ConvertValue.MouseBtnToIndex(m_wheelBtnAction.Trigger);
                base.RedrawDisplay();
            }
        }

        public override void SetValues()
        {
            m_pbDisplay.Enabled = true;
            OnChangeAboutText(Translation.GetText("C_WheelG_about")); //"Chose Trigger button for invoking the action");
            if (Config.User.UsingWheelBtn)
                OnChangeInfoText(ToolTipIcon.Info, Translation.Text_info, Translation.GetText("C_WheelG_info")); //"To invoke this gesture move with the Wheel after the Trigger is pushed \nwithin the Deactivation Timeout and don't move mouse out of the Sensitive Zone. \nYou can change this parameters via: Options -> Gestures");
            else
                OnChangeInfoText(ToolTipIcon.Warning, Translation.Text_warning, Translation.GetText("C_WheelG_warnWheelGDeactivated"));//"Gestures - Wheel Button Combo - are currently deactivated, you may still add it but will not be functional till it is activated. \nIt is possible to change this property via: Options -> Gestures");
            m_tempGesture.Activator = m_wheelBtnAction;
            if (m_wheelBtnAction.Trigger == MouseButtons.None) OnCanContinue(false);
            else OnCanContinue(true);
            base.RedrawDisplay();
        }

     

        private void cB_trigger_SelectedIndexChanged(object sender, EventArgs e)
        {
            MouseButtons trigger = MouseButtons.None;
            if (cB_holdBtn.SelectedItem != null)
                trigger = ConvertValue.IndexToMouseBtn(cB_holdBtn.SelectedIndex);

            m_tempGesture.Activator = new WheelButton(trigger);
            m_wheelBtnAction = m_tempGesture.Activator;
            base.RedrawDisplay();
            lV_buttonMatchedGestures.Items.Clear();
            List<MyGesture> matchedGest = m_gesturesCollection.MatchedGestures(m_tempGesture.Activator.ID);
            if (matchedGest != null)
                foreach (MyGesture gest in matchedGest)
                {
                    lV_buttonMatchedGestures.Items.Add(new ListViewItem(new string[] { gest.Caption, gest.AppGroup.Caption }));
                }

            if (trigger == MouseButtons.None)
                OnCanContinue(false);
            else
                OnCanContinue(true);
        }

    }
}
