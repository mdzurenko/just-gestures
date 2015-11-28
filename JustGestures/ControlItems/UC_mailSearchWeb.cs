using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using JustGestures.TypeOfAction;
using JustGestures.Features;
using JustGestures.Languages;

namespace JustGestures.ControlItems
{
    public partial class UC_mailSearchWeb : BaseActionControl
    {
        bool m_obtainingIcon = false;
        bool m_error = false;
        bool m_firstTime = true;
        Timer m_timer;
        Favicon m_webIconExtractor;
        Cursor m_cursor;
        delegate void EmptyDel();
        AutoCompleteStringCollection m_autoCompleteSearchItems;

        public UC_mailSearchWeb()
        {
            InitializeComponent();
            m_identifier = Page.MailSearchWeb;
            m_next = Page.Gesture;
            m_previous = Page.Action;
            lbl_icon.Text = Translation.GetText("C_SearchOpenWeb_lbl_icon");
            m_timer = new Timer();
            m_timer.Interval = 2000;
            m_timer.Tick += new EventHandler(m_timer_Tick);
            m_webIconExtractor = new Favicon();
            m_webIconExtractor.IconObatined += new Favicon.DlgIconObtained(IconObtained);
            m_autoCompleteSearchItems = new AutoCompleteStringCollection();
            m_autoCompleteSearchItems.AddRange(new string[] {
                "http://www.google.com/search?&q=(*)",
                "http://en.wikipedia.org/w/index.php?&search=(*)",
                "http://maps.google.com/maps?q=(*)",
                "http://translate.google.com/#auto|en|(*)",
                "http://www.last.fm/search?q=(*)",
                "http://www.imdb.com/find?s=all&q=(*)",                
                "http://search.yahoo.com/search?p=(*)",
                "http://www.bing.com/search?q=(*)",                
                "http://search.seznam.cz/?q=(*)",
                "http://www.slovnik.seznam.cz/?q=(*)&lang=en_cz",                
                "http://www.mapy.cz/?query=(*)",
                "http://www.csfd.cz/hledani-filmu-hercu-reziseru-ve-filmove-databazi/?search=(*)"
            });
        }

        public void OnClosingForm()
        {
            m_obtainingIcon = false;
            m_webIconExtractor.StopThread();
        }

        void m_timer_Tick(object sender, EventArgs e)
        {
            m_timer.Stop();
            SetInfoValues();
        }

        private void IconObtained(Bitmap img)
        {
            m_obtainingIcon = false;
            EmptyDel del = delegate()
            {
                pB_icon.Image = m_tempGesture.Action.GetIcon(pB_icon.Width);
                tB_address.Enabled = true;
                //btn_obtainIcon.Enabled = true;
                btn_obtainIcon.Text = Translation.GetText("C_SearchOpenWeb_btn_getIcon");//"Get Web Icon";
                this.Cursor = m_cursor;
            };
            if (tB_address.InvokeRequired || btn_obtainIcon.InvokeRequired || pB_icon.InvokeRequired)
                this.Invoke(del);
            else del();
        }

        private void btn_obtainIcon_Click(object sender, EventArgs e)
        {
            if (m_tempGesture.Action == null) return;
            switch (m_tempGesture.Action.Name)
            {
                case InternetOptions.INTERNET_OPEN_WEBSITE:
                case InternetOptions.INTERNET_SEARCH_WEB:
                    if (!m_obtainingIcon)
                    {
                        Bitmap favicon = Favicon.Load(tB_address.Text);
                        if (favicon != null)
                            pB_icon.Image = favicon;
                        else
                        {
                            tB_address.Enabled = false;
                            //btn_obtainIcon.Enabled = false;
                            btn_obtainIcon.Text = Translation.Btn_cancel;
                            m_cursor = this.Cursor;
                            this.Cursor = Cursors.WaitCursor;
                            m_webIconExtractor.Url = tB_address.Text;
                            m_webIconExtractor.ObtainIcon();
                            m_obtainingIcon = true;
                        }
                    }
                    else
                    {
                        m_webIconExtractor.StopThread();
                        m_obtainingIcon = false;
                        tB_address.Enabled = true;
                        btn_obtainIcon.Text = Translation.GetText("C_SearchOpenWeb_btn_getIcon"); //"Get Web Icon";
                        this.Cursor = m_cursor;
                    }
                    break;
            }
        }

        private void tB_address_TextChanged(object sender, EventArgs e)
        {
            if (m_tempGesture.Action == null) return;
            if (!m_firstTime)
            {
                m_timer.Stop();
                m_timer.Start();
            }
            m_error = true;
            switch (m_tempGesture.Action.Name)
            {
                case InternetOptions.INTERNET_SEARCH_WEB:
                case InternetOptions.INTERNET_OPEN_WEBSITE:
                    if (tB_address.Text.StartsWith("http://") || tB_address.Text.StartsWith("https://")
                        || tB_address.Text.StartsWith("ftp://"))
                    {
                        if (m_tempGesture.Action.Name == InternetOptions.INTERNET_SEARCH_WEB && !tB_address.Text.Contains("(*)"))
                        {
                            pB_icon.Image = null;
                            OnCanContinue(false);
                        }
                        else
                        {
                            SetValues();
                            pB_icon.Image = m_tempGesture.Action.GetIcon(pB_icon.Width);
                        }
                    }
                    else
                    {
                        pB_icon.Image = null;
                        OnCanContinue(false);
                    }
                    break;
                case InternetOptions.INTERNET_SEND_EMAIL:
                    if (Regex.IsMatch(tB_address.Text, @".+\@{1}.+[.]{1}.+"))
                    {
                        SetValues();
                    }
                    else OnCanContinue(false);
                    break;
            }
        }

        private void SetValues()
        {
            m_error = false;
            m_tempGesture.Action.Details = tB_address.Text;
            OnCanContinue(true);
        }

        private void SetInfoValues()
        {
            bool error = m_error;
            if (m_firstTime)
                m_error = false;
            string text = string.Empty;
            switch (m_tempGesture.Action.Name)
            {
                case InternetOptions.INTERNET_SEARCH_WEB:
                    if (!m_error)
                    {
                        //text = "Type website url in the specific format directly to text box. \n";
                        //text += "Url has to start with 'http://', 'https://' or 'ftp://'. and contian special string (*).\n";
                        //text += "When gesture with this action is invoked, the special string (*) is replaced with any selected text and this new url is executed.\n";
                        //text += "You may use the suggestions from textbox by typing http://...\n";
                        //text += "To retrieve website icon connection to internet is required.";
                        text = Translation.GetText("C_SearchWeb_info");
                        OnChangeInfoText(ToolTipIcon.Info, Translation.Text_info, text);
                    }
                    else
                    {
                        text = Translation.GetText("C_SearchWeb_errInvalidFormat");
                        OnChangeInfoText(ToolTipIcon.Error, Translation.Text_error, text);//"Website url is in incorrect format. \nIt has to start with 'http://', 'https://' or 'ftp://' and contain special string (*).");
                    }

                    break;
                case InternetOptions.INTERNET_OPEN_WEBSITE:
                    if (!m_error)
                    {
                        //text = "Type website url directly to text box. \n";
                        //text += "Url has to start with 'http://', 'https://' or 'ftp://'. \n";
                        //text += "To retrieve website icon connection to internet is required.";
                        text = Translation.GetText("C_OpenWeb_info");
                        OnChangeInfoText(ToolTipIcon.Info, Translation.Text_info, text);
                    }
                    else
                    {
                        text = Translation.GetText("C_OpenWeb_errInvalidFormat");
                        OnChangeInfoText(ToolTipIcon.Error, Translation.Text_error, text);//"Website url is in incorrect format. \nIt has to start with 'http://', 'https://' or 'ftp://'");
                    }
                    break;
                case InternetOptions.INTERNET_SEND_EMAIL:
                    if (!m_error)
                    {
                        text = Translation.GetText("C_Email_info");
                        OnChangeInfoText(ToolTipIcon.Info, Translation.Text_info, text);//"Type e-mail address directly to text box.");
                    }
                    else
                    {
                        text = Translation.GetText("C_Email_errInvalidFormat");
                        OnChangeInfoText(ToolTipIcon.Error, Translation.Text_error, text);//"E-mail address is in incorrect format.");
                    }
                    break;
            }
            m_error = error;
        }

        private void SetComponents()
        {
            tB_address.Text = " ";
            tB_address.Text = m_tempGesture.Action.Details;
            panel_icon.Visible = true;
            btn_obtainIcon.Visible = true;
            tB_address.AutoCompleteMode = AutoCompleteMode.Suggest;            
            switch (m_tempGesture.Action.Name)
            {

                case InternetOptions.INTERNET_SEARCH_WEB:
                    OnChangeAboutText(Translation.GetText("C_SearchWeb_about")); //"Chose the website url");
                    lbl_address.Text = Translation.GetText("C_SearchWeb_lbl_searchUrl");// "Website url";
                    tB_address.AutoCompleteCustomSource = m_autoCompleteSearchItems;
                    tB_address.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    break;
                case InternetOptions.INTERNET_OPEN_WEBSITE:
                    OnChangeAboutText(Translation.GetText("C_OpenWeb_about"));//"Chose the website url");
                    lbl_address.Text = Translation.GetText("C_OpenWeb_lbl_webUrl");//"Website url";
                    tB_address.AutoCompleteSource = AutoCompleteSource.AllUrl;
                    break;
                case InternetOptions.INTERNET_SEND_EMAIL:
                    OnChangeAboutText(Translation.GetText("C_Email_about"));//"Chose the e-mail address");
                    lbl_address.Text = Translation.GetText("C_Email_lbl_mail"); //"E-mail address";
                    tB_address.AutoCompleteMode = AutoCompleteMode.None;
                    panel_icon.Visible = false;
                    btn_obtainIcon.Visible = false;                    
                    break;
            }
        }

        private void UC_mailSearchWeb_VisibleChanged(object sender, EventArgs e)
        {
            if (m_tempGesture == null) return;
            if (((UC_mailSearchWeb)sender).Visible)
            {
                if (m_tempGesture.Action == null) return;
                if (m_tempGesture.Action.Details == string.Empty)
                {
                    m_firstTime = true;
                    m_webIconExtractor.StopThread();
                    IconObtained(null);
                    pB_icon.Image = null;
                    m_obtainingIcon = false;
                }
                SetComponents();
                SetInfoValues();
                m_firstTime = false;
            }
            else
            {
                if (m_tempGesture.Action != null && m_tempGesture.Action.Details != string.Empty)
                {
                    string fileName = string.Empty;
                    if (m_tempGesture.Action.Name != InternetOptions.INTERNET_SEND_EMAIL)
                    {
                        try
                        {
                            Uri uri = new Uri(tB_address.Text);
                            fileName = uri.Host;
                        }
                        catch (Exception ex)
                        {
                            fileName = string.Empty;
                        }
                    }
                    else
                        fileName = tB_address.Text;
                    m_tempGesture.Caption = Languages.Translation.GetText(m_tempGesture.Action.Name) + " " + fileName;
                }
            }
        }

        private void tB_address_Leave(object sender, EventArgs e)
        {
            m_timer.Stop();
            SetInfoValues();
        }
    }
}
