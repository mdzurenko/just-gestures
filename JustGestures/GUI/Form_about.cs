using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JustGestures.Languages;

namespace JustGestures.GUI
{
    public partial class Form_about : Form, ITranslation
    {
        static Form_about m_theForm;
        Form_update form_update;
        const string DONATION = "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=support%40justgestures%2ecom&lc=CZ&item_name=Just%20Gestures&currency_code=EUR&bn=PP%2dDonationsBF%3abtn_donate_LG%2egif%3aNonHosted";
        const string FLATTR = "http://flattr.com/thing/107788/Just-Gestures";
        const string HOMEPAGE = "http://www.justgestures.com";
        const string FORUM = "http://forum.justgestures.com";
        const string MAIL = "support@justgestures.com";
        Cursor m_cursor;

        public static Form_about Instance
        {
            get
            {
                if (m_theForm == null || m_theForm.IsDisposed)
                {
                    m_theForm = new Form_about();
                }
                return m_theForm;
            }
        }

        delegate void Emptydel();


        #region ITranslation Members

        MyText A_msg_uptodate = new MyText("A_msgText_uptodate");
        MyText A_msg_caption = new MyText("A_msgCaption_uptodate");

        public void Translate()
        {
            this.Text = Translation.GetText("A_caption");
            lbl_createdBy.Text = Translation.GetText("A_lbl_createdBy");
            rTB_submitBugs.Text = Translation.GetText("A_rTB_submitBugs");
            btn_checkUpdate.Text = Translation.GetText("A_btn_checkUpdate");
            rTB_submitBugs.SelectionAlignment = HorizontalAlignment.Center;
            A_msg_uptodate.Translate();
            A_msg_caption.Translate();
        }

        #endregion


        public Form_about()
        {
            InitializeComponent();
            Translate();
            this.Icon = Properties.Resources.about1;
            toolTip1.SetToolTip(pB_paypal, DONATION);
            toolTip1.SetToolTip(pB_flattr, FLATTR);
            toolTip1.SetToolTip(pB_logo, HOMEPAGE);
            toolTip1.SetToolTip(linkLabel_author, "mailto: " + MAIL);
            toolTip1.SetToolTip(linkLabel_forum, FORUM);
            form_update = new Form_update();
            form_update.NewUpdateAvaliable += new Form_update.DlgNewUpdateAvaliable(NewUpdateAvaliable);
        }

        private void Form_about_Load(object sender, EventArgs e)
        {
            pB_logo.Image = Properties.Resources.logo48x48.ToBitmap();
            Version curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            label_version.Text = string.Format("Just Gestures v {0}.{1}.{2}", curVersion.Major, curVersion.Minor, curVersion.Build);
        }

        private void linkLabel_forum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process openWeb = new System.Diagnostics.Process();
            openWeb.StartInfo.FileName = FORUM;
            openWeb.Start();
        }

        private void linkLabel_author_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process sendMail = new System.Diagnostics.Process();
            sendMail.StartInfo.FileName = "mailto:" + MAIL;
            sendMail.Start();
        }

        private void pB_paypal_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process openWeb = new System.Diagnostics.Process();
            openWeb.StartInfo.FileName = DONATION;
            openWeb.Start();
        }

        private void pB_flattr_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process openWeb = new System.Diagnostics.Process();
            openWeb.StartInfo.FileName = FLATTR;
            openWeb.Start();
        }

        private void pB_logo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process openWeb = new System.Diagnostics.Process();
            openWeb.StartInfo.FileName = HOMEPAGE;
            openWeb.Start();
        }

        private void btn_checkUpdate_Click(object sender, EventArgs e)
        {
            if (!form_update.Visible)
            {
                m_cursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;
                btn_checkUpdate.Enabled = false;
                form_update.CheckForUpdate();
            }
            else
            {
                Emptydel del = delegate()
                {                    
                    form_update.Activate();
                };
                if (form_update.InvokeRequired || btn_checkUpdate.InvokeRequired)
                    this.Invoke(del);
                else
                    del();
            }
        }

        private void NewUpdateAvaliable(bool avaliable)
        {
            Emptydel del = delegate()
            {
                this.Cursor = m_cursor;
                btn_checkUpdate.Enabled = true;
            };
            if (btn_checkUpdate.InvokeRequired)
                this.Invoke(del);
            else
                del();    
  
            if (avaliable)
                form_update.ShowDialog();
            else
                MessageBox.Show(A_msg_uptodate, A_msg_caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}