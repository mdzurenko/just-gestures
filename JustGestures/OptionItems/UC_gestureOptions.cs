using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JustGestures.Properties;
using JustGestures.Languages;

namespace JustGestures.OptionItems
{
    public partial class UC_gestureOptions : BaseOptionControl
    {
        public const int DEACTIVATION_TIME_MIN = 100;
        public const int DEACTIVATION_TIME_MAX = 1000;
        public const int SENSITIVE_ZONE_RANGE = 5;
        const string m_enabled = "Enabled";
        const string m_disabled = "Disabled";
        public const string m_underCursor = "under the cursor";
        public const string m_foreground = "at the foreground";

        public override void Translate()
        {
            base.Translate();
            gB_gestureTypes.Text = Translation.GetText("O_Gestures_gB_gestureTypes");
            lbl_classicCurve.Text = Translation.Name_CurveGesture;
            lbl_doubleBtn.Text = Translation.Name_RockerGesture;
            lbl_wheelBtn.Text = Translation.Name_WheelGesture;
            gB_activation.Text = Translation.GetText("O_Gestures_gB_activation");
            lbl_typeOfWnd.Text = Translation.GetText("O_Gestures_lbl_typeOfWnd");
            lbl_toggleButton.Text = Translation.Name_CurveHoldDownBtn;
            lbl_sensitiveZone.Text = Translation.GetText("O_Gestures_lbl_sensitiveZone");
            lbl_deactivationTimeout.Text = Translation.GetText("O_Gestures_lbl_deactivationTimeout");
            btn_wizard.Text = Translation.GetText("O_Gestures_btn_wizard");

            cB_classicCurve.Items[0] = cB_doubleBtn.Items[0] = cB_wheelBtn.Items[0] = Translation.GetText("O_Gestures_cBI_enabled");
            cB_classicCurve.Items[1] = cB_doubleBtn.Items[1] = cB_wheelBtn.Items[1] = Translation.GetText("O_Gestures_cBI_disabled");
            cB_wndHandle.Items[0] = Translation.GetText("O_Gestures_cBI_underCursor");
            cB_wndHandle.Items[1] = Translation.GetText("O_Gestures_cBI_inForeground");
            for (int i = 0; i < ConvertValue.MouseButtonsCount; i++)
                cB_toggleBtn.Items[i] = Translation.GetMouseBtnText(ConvertValue.IndexToMouseBtn(i));
            cB_sensitiveZone.Items[0] = Translation.GetText("O_Gestures_cBI_off");
        }

        public UC_gestureOptions()
        {
            InitializeComponent();
            m_name = new MyText("O_Gestures_name");
            m_caption = new MyText("O_Gestures_caption");
            I_infoText = new MyText("O_Gestures_info");
            cB_classicCurve.Items.AddRange(new object[] { m_enabled, m_disabled });
            cB_doubleBtn.Items.AddRange(new object[] { m_enabled, m_disabled }); 
            cB_wheelBtn.Items.AddRange(new object[] { m_enabled, m_disabled });
            cB_wndHandle.Items.AddRange(new object[] { m_underCursor, m_foreground }); 
            for (int i = 0; i < ConvertValue.MouseButtonsCount; i++)
                cB_toggleBtn.Items.Add(ConvertValue.IndexToMouseBtn(i));
            for (int i = 0; i < SENSITIVE_ZONE_RANGE + 1; i++)
            {
                if (i == 0)
                    cB_sensitiveZone.Items.Add("OFF");
                else
                    cB_sensitiveZone.Items.Add(i);
            } 
            nUD_deactivateTimeout.Minimum = DEACTIVATION_TIME_MIN;
            nUD_deactivateTimeout.Maximum = DEACTIVATION_TIME_MAX;
            SetValues();
            OnEnableApply(false);
        }

        private void SetValues()
        {
            cB_classicCurve.SelectedIndex = Config.User.UsingClassicCurve ? 0 : 1;
            cB_doubleBtn.SelectedIndex = Config.User.UsingDoubleBtn ? 0 : 1;
            cB_wheelBtn.SelectedIndex = Config.User.UsingWheelBtn ? 0 : 1;
            cB_wndHandle.SelectedIndex = Config.User.HandleWndUnderCursor ? 0 : 1;
            cB_toggleBtn.SelectedIndex = ConvertValue.MouseBtnToIndex(Config.User.BtnToggle);
            cB_sensitiveZone.SelectedIndex = Config.User.SensitiveZone;
            nUD_deactivateTimeout.Value = Math.Min(Math.Max(Config.User.DeactivationTimeout, nUD_deactivateTimeout.Minimum), nUD_deactivateTimeout.Maximum);
        }

        public override void SaveSettings()
        {
            Config.User.UsingClassicCurve = cB_classicCurve.SelectedIndex == 0 ? true : false;
            Config.User.UsingDoubleBtn = cB_doubleBtn.SelectedIndex == 0 ? true : false;
            Config.User.UsingWheelBtn = cB_wheelBtn.SelectedIndex == 0 ? true : false;
            Config.User.HandleWndUnderCursor = cB_wndHandle.SelectedIndex == 0 ? true : false;
            Config.User.BtnToggle = ConvertValue.IndexToMouseBtn(cB_toggleBtn.SelectedIndex);
            Config.User.SensitiveZone = cB_sensitiveZone.SelectedIndex;
            Config.User.DeactivationTimeout = (int)nUD_deactivateTimeout.Value;
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnEnableApply(true);
        }

        private void nUD_deactivateTimeout_ValueChanged(object sender, EventArgs e)
        {
            OnEnableApply(true);
        }

        private void btn_wizard_Click(object sender, EventArgs e)
        {
            GUI.Form_wizard form_wizard = new JustGestures.GUI.Form_wizard();
            if (form_wizard.ShowDialog() == DialogResult.OK)
            {
                SetValues();
            }
        }

    }
}
