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
    public partial class UC_W_welcome : BaseWizardControl
    {
        bool m_firstRun = true;

        public UC_W_welcome()
        {
            InitializeComponent();
            m_identifier = Page.Welcome;
            m_next = Page.ClassicCurve;
            m_previous = Page.None;

            m_caption = Translation.GetText("W_W_caption"); 
            I_infoText = Translation.GetText("W_W_info");
            lbl_introCaption.Text = Translation.GetText("W_W_introCaption");
            rTB_introDescription.AppendText(Translation.GetText("W_W_introDescription"));
        }

        private void UC_W_welcome_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && m_firstRun)
            {
                ShowBalloonTip(5000);
                m_firstRun = false;
            }

        }
    }
}
