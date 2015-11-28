using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JustGestures.TypeOfAction;
using JustGestures.Languages;

namespace JustGestures.ControlItems
{
    public partial class UC_clipboard : BaseActionControl
    {
        public const int CLIPS_COUNT = 5;

        public UC_clipboard()
        {
            InitializeComponent();
            m_identifier = Page.Clipboard;
            m_next = Page.Gesture;
            m_previous = Page.Action;
        }

        private void cB_clipboards_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_tempGesture.Action.Details = cB_clipboards.SelectedIndex.ToString();
            //string name = string.Empty;
            // if (m_tempGesture.Action.Name == KeystrokesOptions.KEYSTROKES_PRIVATE_COPY_TEXT)
            //        name =  "Copy Text to";
            //    else //KeystrokesControl.KEYSTROKES_PASTE
            //        name = "Paste Text from";
            //m_tempGesture.Caption = string.Format("{0} Clipboard {1}", name, cB_clipboards.SelectedIndex + 1);
            m_tempGesture.Caption = m_tempGesture.Action.GetDescription();
            OnCanContinue(true);
        }

        private void UC_clipboard_VisibleChanged(object sender, EventArgs e)
        {
            if (((UC_clipboard)sender).Visible)
            {
                string text = string.Empty;
                if (m_tempGesture.Action.Name == KeystrokesOptions.KEYSTROKES_PRIVATE_COPY_TEXT)
                {
                    OnChangeAboutText(Translation.GetText("C_PC_about"));// //"Select clipboard");
                    lbl_clipboard.Text = Translation.GetText("C_PC_lbl_copyTo");//"Copy Text to ";
                    text += Translation.GetText("C_PC_info"); //"Copies selected text to system clipboard and then stores it in private one.\n";
                }
                else //KeystrokesControl.KEYSTROKES_PASTE
                {
                    OnChangeAboutText(Translation.GetText("C_PP_about")); //"Associate pasting with clipboard");
                    lbl_clipboard.Text = Translation.GetText("C_PP_lbl_pasteFrom");//"Paste Text from ";
                    text += Translation.GetText("C_PP_info");// "Copies stored text from private clipboard to system one and paste it.\n";
                }
                text += "\n" + Translation.GetText("C_PC_PP_info");
                //text += "FREE - free position to use.\n";
                //text += "COPY - clipboard is already associated with Copy gesture.\n";
                //text += "PASTE - clipboard is already associated with Paste gesture.";
                OnChangeInfoText(ToolTipIcon.Info, Translation.Text_info, text);
                SetComboxList();
                if (m_tempGesture.Action.Details != string.Empty)
                {
                    cB_clipboards.SelectedIndex = int.Parse(m_tempGesture.Action.Details);
                    OnCanContinue(true);
                }
                else
                {
                    cB_clipboards.SelectedIndex = -1;
                    OnCanContinue(false);
                }
            }
        }

        private void SetComboxList()
        {
            cB_clipboards.Items.Clear();
            for (int i = 0; i < CLIPS_COUNT; i++)
            {                
                string postfixCopy = string.Empty;
                string postfixPaste = string.Empty;
                string postfix = string.Empty;

                foreach (MyGesture gesture in m_gesturesCollection)
                {
                    switch (gesture.Action.Name)
                    {
                        case KeystrokesOptions.KEYSTROKES_PRIVATE_COPY_TEXT:
                            if (gesture.Action.Details == i.ToString())
                                postfixCopy = Translation.GetText("C_PC_PP_postfixCopy"); // "COPY";
                            break;
                        case KeystrokesOptions.KEYSTROKES_PRIVATE_PASTE_TEXT:
                            if (gesture.Action.Details == i.ToString())
                                postfixPaste = Translation.GetText("C_PC_PP_postfixPaste"); // "PASTE";
                            break;
                    }
                }
                if (postfixCopy != string.Empty && postfixPaste != string.Empty)
                    postfix = Translation.GetText("C_PC_PP_postfixCopyPaste"); // postfixCopy + " & " + postfixPaste;
                else
                    postfix = postfixCopy + postfixPaste;
                if (postfix == string.Empty)
                    postfix = Translation.GetText("C_PC_PP_postfixFree");//"FREE";
                string item = string.Format(Translation.GetText("C_PC_PP_cBI_privateClipboard"), i + 1, postfix);//"Private Clipboard {0} ({1})"
                cB_clipboards.Items.Add(item);
            }
        }

    }
}
