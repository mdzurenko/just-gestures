using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JustGestures.Languages;

namespace JustGestures.WizardItems
{
    public partial class UC_W_wheelBtn : UC_W_baseGesture
    {
        public UC_W_wheelBtn()
        {
            InitializeComponent();

            m_identifier = Page.WheelBtn;
            m_next = Page.Activation;
            m_previous = Page.DoubleBtn;

            m_gesture = new MyGesture("WheelButton");
            m_gesture.Activator = new GestureParts.WheelButton(MouseButtons.XButton1);
            
            cB_gesture.Checked = Config.User.UsingWheelBtn;
            cb_trigger.SelectedIndex = 0;

            m_caption = Translation.GetText("W_WB_caption");
            I_infoText = Translation.GetText("W_WB_info");
            cB_gesture.Text = Translation.Name_WheelGesture;
            rTB_instructions.AppendText(Translation.GetText("W_WB_instructions"));
            lbl_triggerBtn.Text = Translation.Name_WheelHoldDownBtn;
            lbl_and.Text = Translation.GetText("W_G_lbl_and");
            lbl_mouseWheel.Text = Translation.Name_MouseWheel;
            rTB_description.AppendText("\n" + Translation.GetText("W_WB_description") + "\n");
        }

        public override void SaveSettings()
        {
            Config.User.UsingWheelBtn = cB_gesture.Checked;
        }
    }
}
    