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
    public partial class UC_W_doubleBtn : UC_W_baseGesture
    {
        public UC_W_doubleBtn()
        {
            InitializeComponent();

            m_identifier = Page.DoubleBtn;
            m_next = Page.WheelBtn;
            m_previous = Page.ClassicCurve;

            m_gesture = new MyGesture("DoubleButton");
            m_gesture.Activator = new GestureParts.DoubleButton(MouseButtons.Left, MouseButtons.Right);

            cB_gesture.Checked = Config.User.UsingDoubleBtn;

            cB_triggerBtn.Items.Add(Translation.Name_LeftButton);
            cB_modifierBtn.Items.Add(Translation.Name_RightButton);
            cB_modifierBtn.SelectedIndex = 0;
            cB_triggerBtn.SelectedIndex = 0;

            m_caption = Translation.GetText("W_DB_caption");
            I_infoText = Translation.GetText("W_DB_info");
            cB_gesture.Text = Translation.Name_RockerGesture;
            rTB_instructions.AppendText(Translation.GetText("W_DB_instructions"));
            lbl_triggerBtn.Text = Translation.Name_RockerHoldDownBtn;
            lbl_and.Text = Translation.GetText("W_G_lbl_and");
            lbl_modifierBtn.Text = Translation.Name_RockerExecuteBtn;
            rTB_description.AppendText("\n" + Translation.GetText("W_DB_description") + "\n");
        }

        public override void SaveSettings()
        {
            Config.User.UsingDoubleBtn = cB_gesture.Checked;
        }
    }
}
