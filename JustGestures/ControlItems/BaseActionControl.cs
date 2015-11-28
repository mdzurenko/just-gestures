using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace JustGestures.ControlItems
{
    public class BaseActionControl : UserControl
    {
        public enum Page
        {
            None,
            Application,
            Action,
            PrgWwwFld,
            MailSearchWeb,
            Keystrokes,
            PlainText,
            Clipboard,
            Gesture,
            Name
        }

        protected Page m_next;
        protected Page m_previous;
        protected Page m_identifier;

        protected string m_about;
        protected string m_info; 

        public Page Next { get { return m_next; } }
        public Page Previous { get { return m_previous; } set { m_previous = value; } }
        public Page Identifier { get { return m_identifier; } }

        protected BaseActionControl()
        {
            //this.Margin = new System.Windows.Forms.Padding(40, 40, 40, 40);
        }

        public delegate void DlgCanContinue(bool _continue);
        public delegate void DlgChangeAboutText(string text);
        public delegate void DlgChangeInfoText(ToolTipIcon icon, string title, string text);
        /// <summary>
        /// If all conditions are met, it is possible to continue
        /// </summary>
        public DlgCanContinue CanContinue;
        /// <summary>
        /// Change of the text
        /// </summary>
        public DlgChangeAboutText ChangeAboutText;

        public DlgChangeInfoText ChangeInfoText;
        /// <summary>
        /// List of learnt gestures
        /// </summary>
        protected GesturesCollection m_gesturesCollection;
        /// <summary>
        /// Gesture which is being created
        /// </summary>
        protected MyGesture m_tempGesture;

        protected void OnCanContinue(bool _continue)
        {
            if (CanContinue != null)
                CanContinue(_continue);
        }

        protected void OnChangeAboutText(string text)
        {
            if (ChangeAboutText != null)
                ChangeAboutText(text);
        }

        protected void OnChangeInfoText(ToolTipIcon icon, string title, string text)
        {
            if (ChangeInfoText != null)
                ChangeInfoText(icon, title, text);
        }

        public GesturesCollection Gestures
        {     
            get { return m_gesturesCollection; }
            set { m_gesturesCollection = value; }
        }

        public MyGesture TempGesture
        {
            get { return m_tempGesture; }
            set { m_tempGesture = value; }
        }
    }
}
