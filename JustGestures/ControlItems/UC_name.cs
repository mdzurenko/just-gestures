using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using JustGestures.Languages;

namespace JustGestures.ControlItems
{
    public partial class UC_name : BaseActionControl
    {
        public UC_name()
        {
            InitializeComponent();            
            m_identifier = Page.Name;
            m_next = Page.None;
            m_previous = Page.Gesture;
            lbl_name.Text = Translation.GetText("C_Name_lbl_name");
            lbl_priority.Text = Translation.GetText("C_Name_lbl_priority");
            cB_active.Text = Translation.GetText("C_Name_cB_active");
            lbl_uncheckToDeactivate.Text = Translation.GetText("C_Name_lbl_uncheckToDeactivate");

        }
      
        private void UserControl_name_Load(object sender, EventArgs e)
        {
            tB_name.Text = m_tempGesture.Caption;
            cB_active.Checked = m_tempGesture.Active;
            
        }

        private void tB_name_TextChanged(object sender, EventArgs e)
        { 
            m_tempGesture.Caption = tB_name.Text;
            if (tB_name.Text.Length > 0)
                OnCanContinue(true);
            else
                OnCanContinue(false);
        }

        private void checkBox_active_CheckedChanged(object sender, EventArgs e) 
        {
            m_tempGesture.Active = cB_active.Checked;
        }

        private void SetInfoValues()
        {
            string text = string.Empty;
            if (m_tempGesture.IsImplicitOnly)
                text += Translation.GetText("C_Name_info_highPriorityOnly") + "\n\n";
            text += Translation.GetText("C_Name_info");
            OnChangeInfoText(ToolTipIcon.Info, Translation.Text_info, text);

            //if (!m_tempGesture.IsImplicitOnly)
            //{
            //    //text += "Execution type determines whether the context menu of matched gestures is shown. \n\n";
            //    //text += "Context Menu is shown: \n";
            //    //text += "Implict if Unique - ONLY IF contains more matching gestures \n";
            //    //text += "Implicit - NEVER, runs first active and implicit one \n";
            //    //text += "Explicit - ALWAYS, to confirm action";
            //    OnChangeInfoText(ToolTipIcon.Info, Translation.Text_info, Translation.GetText("C_Name_info"));
            //}
            //else
            //{
            //    //text += "Script containing Mouse Actions and gesture type Wheel Button is ALWAYS IMPLICIT.";
            //    OnChangeInfoText(ToolTipIcon.Info, Translation.Text_info, text);
            //}
        }

        private void UserControl_name_VisibleChanged(object sender, EventArgs e)
        {
            if (((UC_name)sender).Visible)
            {
                OnChangeAboutText(Translation.GetText("C_Name_about"));//"Chose the name for your gesture");
                 
                SetInfoValues();
                tB_name.Focus();
                tB_name.Text = m_tempGesture.Caption;
                cB_active.Checked = m_tempGesture.Active;
                SetValues();
                if (tB_name.Text.Length > 0)
                    OnCanContinue(true);
                else
                    OnCanContinue(false);
            }
        }

        private void SetValues()
        {
            cB_priority.Items.Clear();
            if (!m_tempGesture.IsImplicitOnly)
            {
                for (int i = 0; i < ConvertValue.PrioritiesCount; i++)
                    cB_priority.Items.Add(Translation.GetPriorityText(ConvertValue.IndexToPriority(i)));
            }
            else
            {
                cB_priority.Items.Add(Translation.GetPriorityText(ConvertValue.IndexToPriority(0)));
                m_tempGesture.ExecutionType = ExecuteType.Implicit;
            }
            cB_priority.SelectedIndex = ConvertValue.PriorityToIndex(m_tempGesture.ExecutionType);
        }
        
        private void cB_priority_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_tempGesture.ExecutionType = ConvertValue.IndexToPriority(cB_priority.SelectedIndex);
        }
       
    }
}
