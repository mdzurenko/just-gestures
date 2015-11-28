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
    public partial class BaseWizardControl : UserControl, ITranslation
    {
        public enum Page
        {
            None = 0,
            Welcome = 1,
            ClassicCurve = 2,
            DoubleBtn = 3,
            WheelBtn = 4,
            Activation = 5
        }

        #region ITranslation Members

        public void Translate()
        {
            throw new NotImplementedException();
        }

        #endregion

        protected Page m_next;
        protected Page m_previous;
        protected Page m_identifier;

        public Page Next { get { return m_next; } }
        public Page Previous { get { return m_previous; } }
        public Page Identifier { get { return m_identifier; } }

        protected string I_infoCaption = Translation.GetText("I_infoCaption");
        /// <summary>
        /// Info for each page
        /// </summary>
        protected string I_infoText = string.Empty;
        /// <summary>
        /// Caption of the control
        /// </summary>
        protected string m_caption = string.Empty;
        
        public delegate void DlgChangeCaption(string text);
        public DlgChangeCaption ChangeCaption;

        public delegate void DlgChangeInfoText(ToolTipIcon icon, string title, string text);
        public DlgChangeInfoText ChangeInfoText;

        public delegate void DlgShowBalloonTip(int milliseconds);
        public DlgShowBalloonTip ShowBalloonTip;

        public virtual void SaveSettings() { } 

        public BaseWizardControl()
        {
            InitializeComponent();
            this.VisibleChanged +=new EventHandler(BaseWizardControl_VisibleChanged);
        }

        private void OnChangeCaption(string text)
        {
            if (ChangeCaption != null)
                ChangeCaption(text);
        }

        private void OnChangeInfoText(ToolTipIcon icon, string title, string text)
        {
            if (ChangeInfoText != null)
                ChangeInfoText(icon, title, text);
        }

        private void BaseWizardControl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                OnChangeCaption(m_caption);
                OnChangeInfoText(ToolTipIcon.Info, I_infoCaption, I_infoText);
            }
        }

     
    }
}
