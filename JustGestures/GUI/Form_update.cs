using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Xml;
using JustGestures.Languages;

namespace JustGestures.GUI
{
    public partial class Form_update : Form, ITranslation
    {
        private const string URL_VERSION = "http://justgestures.com/version.xml";
        private const string URL_DOWNLOAD = "http://justgestures.com/download";

        Version m_newVersion = null;
        List<string> m_features = null;
        Thread m_threadCheckUpdate = null;

        delegate void Emptydel();
        
        public delegate void DlgNewUpdateAvaliable(bool avaliable);
        public DlgNewUpdateAvaliable NewUpdateAvaliable;

        private void OnNewUpdateAvaliable(bool avaliable)
        {
            if (NewUpdateAvaliable != null)
                NewUpdateAvaliable(avaliable);
        }

        #region ITranslation Members

        public void Translate()
        {
            this.Text = Translation.GetText("U_caption");
            lbl_newUpdate.Text = Translation.GetText("U_lbl_newUpdate");
            lbl_version.Text = Translation.GetText("U_lbl_version");
            lbl_features.Text = Translation.GetText("U_lbl_features");
            btn_download.Text = Translation.GetText("U_btn_download");
            
        }

        #endregion

        public Form_update()
        {
            InitializeComponent();
            Translate();
            this.Icon = Properties.Resources.download_ico;
            m_features = new List<string>();
            toolTip1.SetToolTip(btn_download, URL_DOWNLOAD);
        }

        private void Form_update_Load(object sender, EventArgs e)
        {
            lbl_versionNumber.Text = string.Format("{0}.{1}.{2}", m_newVersion.Major, m_newVersion.Minor, m_newVersion.Build);
            string text = string.Empty;
            foreach (string feature in m_features)
                text += "- " + feature + "\n";
            rTB_features.Text = text;
        }

        public void CheckForUpdate()
        {
            AbortUpdate();
            m_threadCheckUpdate = new Thread(new ThreadStart(CheckNewVersion));
            m_threadCheckUpdate.Start();
        }

        public void AbortUpdate()
        {
            if (m_threadCheckUpdate != null && m_threadCheckUpdate.IsAlive)
                m_threadCheckUpdate.Abort();
        }

        private void CheckNewVersion()
        {
            Version curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            XmlTextReader reader = null;
            m_features.Clear();
            try
            {
                reader = new XmlTextReader(URL_VERSION);
                reader.MoveToContent();
                string value = string.Empty;
                while (reader.Read())
                {
                    if (reader.Name == "version")
                    {
                        value = reader.ReadString();
                        m_newVersion = new Version(value);
                    }
                    else if (reader.Name == "feature")
                    {
                        value = reader.ReadString();
                        m_features.Add(value);
                    }
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
            }


            if (m_newVersion != null && curVersion.CompareTo(m_newVersion) < 0)
                OnNewUpdateAvaliable(true);
            else
                OnNewUpdateAvaliable(false);

        }

        private void btn_download_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(URL_DOWNLOAD);
        }

     
    }
}
