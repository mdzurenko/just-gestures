using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JustGestures.GestureParts;
using JustGestures.Languages;

namespace JustGestures.WizardItems
{
    public partial class UC_W_baseGesture : BaseWizardControl
    {
        protected MyGesture m_gesture;
        protected GesturesCollection m_gesturesCollection;

        public UC_W_baseGesture()
        {
            InitializeComponent();
            m_gesturesCollection = new GesturesCollection();
            gB_gesture.Text = Translation.GetText("W_G_gB_gesture");
            lbl_instructions.Text = Translation.GetText("W_G_lbl_instructions");
            gB_preview.Text = Translation.GetText("W_G_gB_preview");
            gB_activation.Text = Translation.GetText("W_G_gB_activation");
            gB_description.Text = Translation.GetText("W_G_gB_description");
            toolTip1.SetToolTip(pB_animation, Translation.GetText("W_G_tT_animation")); // Click to animate the gesture.

   
        }

        private void pB_animation_Click(object sender, EventArgs e)
        {
            m_gesture.Activator.AnimateToPictureBox(pB_animation);
        }

        private void UC_W_baseGesture_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                if (m_gesture != null && m_gesture.Activator != null)
                {
                    m_gesture.Activator.AbortAnimating();
                    m_gesture.Activator.DrawToPictureBox(pB_animation);
                }
            }
        }

        private void UC_W_baseGesture_Load(object sender, EventArgs e)
        {
            if (m_gesture != null && m_gesture.Activator != null)
            {
                m_gesture.Activator.DrawToPictureBox(pB_animation);
            }
        }


    }
}
