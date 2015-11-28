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
    public partial class UC_TP_doubleBtn : UC_TP_baseActivator
    {
        DoubleButton m_doubleBtnAction = null;
        bool m_bOwnSelect = false;

        public UC_TP_doubleBtn()
        {
            InitializeComponent();

            for (int i = 0; i < ConvertValue.MouseButtonsCount; i++)
            {
                cB_holdBtn.Items.Add(Translation.GetMouseBtnText(ConvertValue.IndexToMouseBtn(i)));
                cB_executeBtn.Items.Add(Translation.GetMouseBtnText(ConvertValue.IndexToMouseBtn(i)));
            }

            m_doubleBtnAction = new DoubleButton(MouseButtons.None, MouseButtons.None);
            lbl_holdBtn.Text = Translation.Name_RockerHoldDownBtn;
            rTB_pushHoldBtn.Text = Translation.GetText("C_RockerG_pushHoldBtn");
            lbl_executeBtn.Text = Translation.Name_RockerExecuteBtn;
            rTB_clickExecuteBtn.Text = Translation.GetText("C_RockerG_clickExecuteBtn");
            rTB_notes.Text = Translation.GetText("C_RockerG_notes");
            cH_associatedActions.Text = Translation.GetText("C_Gestures_cH_associatedActions");
            cH_group.Text = Translation.GetText("C_Gestures_cH_group");
        }

        public override void Initialize()
        {
            base.Initialize();
            if (m_tempGesture.Activator != null && m_tempGesture.Activator.Type == BaseActivator.Types.DoubleButton)
            {
                m_doubleBtnAction = m_tempGesture.Activator;
                m_bOwnSelect = true;
                if (m_doubleBtnAction.Trigger != MouseButtons.None)
                {
                    //cB_executeBtn.Items.Remove(m_doubleBtnAction.Trigger.ToString());
                    cB_holdBtn.SelectedIndex = ConvertValue.MouseBtnToIndex(m_doubleBtnAction.Trigger);
                }
                if (m_doubleBtnAction.Modifier != MouseButtons.None)
                {
                    //cB_holdBtn.Items.Remove(m_doubleBtnAction.Modifier.ToString());
                    cB_executeBtn.SelectedIndex = ConvertValue.MouseBtnToIndex(m_doubleBtnAction.Modifier);
                }
                m_bOwnSelect = false;
                //tabControl_invokeAction.SelectedTab = tabPage_buttons;
                base.RedrawDisplay();
            }
        }

        public override void SetValues()
        {
            m_pbDisplay.Enabled = true;
            OnChangeAboutText(Translation.GetText("C_RockerG_about")); //"Chose Trigger and Moddifier buttons for invoking the action");
            m_tempGesture.Activator = m_doubleBtnAction;
            SetInfoValues();    
            base.RedrawDisplay();
        }

        private void SetInfoValues()
        {
            //if (m_doubleBtnAction.Trigger == MouseButtons.None || m_doubleBtnAction.Modifier == MouseButtons.None) OnCanContinue(false);
            if (m_doubleBtnAction.Trigger == m_doubleBtnAction.Modifier && m_doubleBtnAction.Trigger != MouseButtons.None)
            {
                OnChangeInfoText(ToolTipIcon.Error, Translation.Text_error, Translation.GetText("C_RockerG_errSameHoldAndExecuteBtn"));
                OnCanContinue(false);
            }
            else
            {
                if (Config.User.UsingDoubleBtn)
                    OnChangeInfoText(ToolTipIcon.Info, Translation.Text_info, Translation.GetText("C_RockerG_info"));// "To invoke this gesture push the Modifier button after the Trigger \nwithin the Deactivation Timeout and don't move mouse out of the Sensitive Zone. \nYou can change this parameters via: Options -> Gestures");
                else
                    OnChangeInfoText(ToolTipIcon.Warning, Translation.Text_warning, Translation.GetText("C_RockerG_warnRockerGDeactivated"));// "Gestures - Double Button Combo - are currently deactivated, you may still add it but will not be functional till it is activated. \nIt is possible to change this property via: Options -> Gestures");
                if (m_doubleBtnAction.Trigger == MouseButtons.None || m_doubleBtnAction.Modifier == MouseButtons.None) 
                    OnCanContinue(false);
                else
                    OnCanContinue(true);
            }
        }

 

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_bOwnSelect)
            {
                //m_bOwnSelect = true;
                //ComboBox cbIndexChanged = (ComboBox)sender;
                //ComboBox cbModifyCombo = cB_executeBtn;
                //if (cbIndexChanged == cB_executeBtn)
                //    cbModifyCombo = cB_holdBtn;
                //object selected = cbModifyCombo.SelectedItem;
                //cbModifyCombo.Items.Clear();
                //cbModifyCombo.Items.AddRange(Enum.GetNames(typeof(MouseButtons)));
                //cbModifyCombo.Items.Remove(MouseButtons.None.ToString());
                //cbModifyCombo.Items.Remove(cbIndexChanged.SelectedItem);
                //cbModifyCombo.SelectedItem = selected;
                //m_bOwnSelect = false;
                MouseButtons holdBtn = MouseButtons.None;
                MouseButtons executeBtn = MouseButtons.None;
                if (cB_holdBtn.SelectedIndex != -1)
                    holdBtn = ConvertValue.IndexToMouseBtn(cB_holdBtn.SelectedIndex); //(MouseButtons)Enum.Parse(typeof(MouseButtons), cB_holdBtn.SelectedItem.ToString());
                if (cB_executeBtn.SelectedIndex != -1)
                    executeBtn = ConvertValue.IndexToMouseBtn(cB_executeBtn.SelectedIndex);//(MouseButtons)Enum.Parse(typeof(MouseButtons), cB_executeBtn.SelectedItem.ToString());
                m_tempGesture.Activator = new DoubleButton(holdBtn, executeBtn);
                m_doubleBtnAction = m_tempGesture.Activator;
                base.RedrawDisplay();
                lV_buttonMatchedGestures.Items.Clear();
                List<MyGesture> matchedGest = m_gesturesCollection.MatchedGestures(m_tempGesture.Activator.ID);
                if (matchedGest != null)
                    foreach (MyGesture gest in matchedGest)
                    {
                        lV_buttonMatchedGestures.Items.Add(new ListViewItem(new string[] { gest.Caption, gest.AppGroup.Caption }));
                    }
                SetInfoValues();
            }
        }

    }
}
