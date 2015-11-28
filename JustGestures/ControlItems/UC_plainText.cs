using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JustGestures.Languages;

namespace JustGestures.ControlItems
{
    public partial class UC_plainText : BaseActionControl
    {
        public UC_plainText()
        {
            InitializeComponent();
            m_identifier = Page.PlainText;
            m_next = Page.Gesture;
            m_previous = Page.Action;
            lbl_text.Text = Translation.GetText("C_PlainTxt_lbl_text");
        }

        private void UC_plainText_Load(object sender, EventArgs e)
        {
            tB_text.Text = m_tempGesture.Action.Details;
        }


        private void tB_text_TextChanged(object sender, EventArgs e)
        {
            m_tempGesture.Action.Details = tB_text.Text;
            if (tB_text.Text.Length > 0)
                OnCanContinue(true);
            else
                OnCanContinue(false);
        }
        
        private void UC_plainText_VisibleChanged(object sender, EventArgs e)
        {
            if (((UC_plainText)sender).Visible)
            {
                OnChangeAboutText(Translation.GetText("C_PlainTxt_about")); //"Set text to insert");
                string text = string.Empty;
                //text += "Inserts whole text at once. \n";
                //text += "Inserting text does not modify system clipboard.";
                text = Translation.GetText("C_PlainTxt_info");
                OnChangeInfoText(ToolTipIcon.Info, Translation.Text_info, text);
                if (tB_text.Text.Length > 0)
                    OnCanContinue(true);
                else
                    OnCanContinue(false);
            }
        }

     

       
    }
}
