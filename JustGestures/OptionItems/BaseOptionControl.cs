using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JustGestures.Languages;

namespace JustGestures.OptionItems
{
    public partial class BaseOptionControl : UserControl, ITranslation
    {
        #region ITranslation Members

        public virtual void Translate()
        {
            I_infoCaption.Translate();
            I_infoText.Translate();
            m_name.Translate();
            m_caption.Translate();
            if (this.Visible)
                SetCaptionAndInfo();
        }

        #endregion

        public BaseOptionControl()
        {            
            //this.Dock = DockStyle.Fill;
            this.VisibleChanged += new EventHandler(BaseOptionControl_VisibleChanged);
        }

        protected MyText I_infoCaption = new MyText("I_infoCaption");
        /// <summary>
        /// Info for each page
        /// </summary>
        protected MyText I_infoText = new MyText("");
        /// <summary>
        /// Name of the control - visible in treeview located on left side
        /// </summary>
        protected MyText m_name = new MyText("");
        /// <summary>
        /// Caption of the control - visible on the top part of window in options
        /// </summary>
        protected MyText m_caption = new MyText("");
        public delegate void DlgChangeCaption(string text);
        /// <summary>
        /// Change of the text
        /// </summary>
        public DlgChangeCaption ChangeCaption;
        public delegate void DlgEnableApply(bool enable);
        /// <summary>
        /// Whether the apply button should be enabled
        /// </summary>
        public DlgEnableApply EnableApply;

        public delegate void DlgChangeInfoText(ToolTipIcon icon, string title, string text);
        public DlgChangeInfoText ChangeInfoText;


        public virtual void SaveSettings() { } 

        protected void OnChangeCaption(string text)
        {            
            if (ChangeCaption != null)
                ChangeCaption(text);
        }

        protected void OnEnableApply(bool enable)
        {
            if (EnableApply != null)
                EnableApply(enable);
        }

        protected void OnChangeInfoText(ToolTipIcon icon, string title, string text)
        {
            if (ChangeInfoText != null)
                ChangeInfoText(icon, title, text);
        }

        protected void SetCaptionAndInfo()
        {
            OnChangeCaption(m_caption);
            OnChangeInfoText(ToolTipIcon.Info, I_infoCaption, I_infoText);
        }

        void BaseOptionControl_VisibleChanged(object sender, EventArgs e)
        {
            if (((UserControl)sender).Visible)
                SetCaptionAndInfo();
        }


        public string ControlName
        {
            get { return m_name; }
        }

        public string ControlCaption
        {
            get { return m_caption; }
        }


      
    }
}
