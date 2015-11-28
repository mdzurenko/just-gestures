using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JustGestures.Languages;
using JustGestures.OptionItems;

namespace JustGestures.WizardItems
{
    public partial class UC_W_activation : BaseWizardControl
    {
        public UC_W_activation()
        {
            InitializeComponent();
            
            m_identifier = Page.Activation;
            m_next = Page.None;
            m_previous = Page.WheelBtn;

            m_caption = Translation.GetText("W_A_caption");
            I_infoText = Translation.GetText("W_A_info");
            gB_gestureActivation.Text = Translation.GetText("W_A_gB_gestureActivation");
            lbl_sensitiveZone.Text = Translation.GetText("O_Gestures_lbl_sensitiveZone");
            lbl_deactivationTimeout.Text = Translation.GetText("O_Gestures_lbl_deactivationTimeout");
            gB_description1.Text = gB_description2.Text = Translation.GetText("W_G_gB_description");
            lbl_typeOfWnd.Text = Translation.GetText("O_Gestures_lbl_typeOfWnd");
            rTB_zoneTimeout.AppendText("\n" + Translation.GetText("W_A_zoneTimeoutDescription") + "\n");
            rTB_controlPrg.AppendText("\n" + Translation.GetText("W_A_controlPrgDescription") + "\n");

            for (int i = 0; i < UC_gestureOptions.SENSITIVE_ZONE_RANGE + 1; i++)
            {
                if (i == 0)
                    cB_sensitiveZone.Items.Add("OFF");
                else
                    cB_sensitiveZone.Items.Add(i);
            }
            nUD_deactivateTimeout.Minimum = UC_gestureOptions.DEACTIVATION_TIME_MIN;
            nUD_deactivateTimeout.Maximum = UC_gestureOptions.DEACTIVATION_TIME_MAX;

            cB_wndHandle.Items.AddRange(new string[] { Translation.GetText("O_Gestures_cBI_underCursor"), Translation.GetText("O_Gestures_cBI_inForeground") });
            cB_wndHandle.SelectedIndex = Config.User.HandleWndUnderCursor ? 0 : 1;
            cB_sensitiveZone.SelectedIndex = Config.User.SensitiveZone;
            nUD_deactivateTimeout.Value = Math.Min(Math.Max(Config.User.DeactivationTimeout, nUD_deactivateTimeout.Minimum), nUD_deactivateTimeout.Maximum);            
        }

        public override void SaveSettings()
        {
            Config.User.HandleWndUnderCursor = cB_wndHandle.SelectedIndex == 0 ? true : false;
            Config.User.SensitiveZone = cB_sensitiveZone.SelectedIndex;
            Config.User.DeactivationTimeout = (int)nUD_deactivateTimeout.Value;
        }

        
    }
}
