using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using JustGestures.Properties;
using JustGestures.Features;
using JustGestures.Languages;

namespace JustGestures.OptionItems
{
    public partial class UC_generalOptions : BaseOptionControl
    {
        public DlgTranslateTexts TranslateTexts;
        public delegate void DlgTranslateTexts();
        private Dictionary<string, string> m_languages;

        protected void OnTranslateTexts()
        {
            if (TranslateTexts != null)
                TranslateTexts();
        }

        public override void Translate()
        {
            base.Translate();
            gB_startUpSettings.Text = Translation.GetText("O_General_gB_startUpSettings");
            cB_startup.Text = Translation.GetText("O_General_cB_startUp");
            cB_startMinimized.Text = Translation.GetText("O_General_cB_startMinimized");
            cB_autobehave.Text = Translation.GetText("O_General_cB_autobehave");
            cB_checkUpdate.Text = Translation.GetText("O_General_cB_checkUpdate");
            gB_systemTraySettings.Text = Translation.GetText("O_General_gB_systemTraySettings");
            cB_minToTray.Text = Translation.GetText("O_General_cB_minToTray");
            cB_closeToTray.Text = Translation.GetText("O_General_cB_closeToTray");
            gB_localization.Text = Translation.GetText("O_General_gB_localization");
            lbl_changeLanguage.Text = Translation.GetText("O_General_lbl_changeLanguage");
        }

        public UC_generalOptions()
        {    
            InitializeComponent();
            m_name = new MyText("O_General_name");
            m_caption = new MyText("O_General_caption");
            I_infoText = new MyText("O_General_info");       

            m_languages = Translation.GetAllLanguages();
            foreach (string lng in m_languages.Keys)
                cB_languages.Items.Add(lng);

            cB_startup.Checked = Config.User.AutoStart;
            cB_startMinimized.Checked = Config.User.StartMinimized;
            cB_autobehave.Checked = Config.User.AutoBehaviour;
            cB_checkUpdate.Checked = Config.User.CheckForUpdate;
            cB_minToTray.Checked = Config.User.MinToTray;
            cB_closeToTray.Checked = Config.User.CloseToTray;
            cB_languages.SelectedItem = Config.User.Language;
            OnEnableApply(false);
        }        

        public override void SaveSettings()
        {            
            Config.User.AutoStart = cB_startup.Checked;
            Config.User.StartMinimized = cB_startMinimized.Checked;
            Config.User.AutoBehaviour = cB_autobehave.Checked;
            Config.User.CheckForUpdate = cB_checkUpdate.Checked;
            Config.User.MinToTray = cB_minToTray.Checked;
            Config.User.CloseToTray = cB_closeToTray.Checked;
            
            if (Config.User.AutoStart)
            {
                string path = RegistryEdit.GetAutoStartValue();
                if (path == null)
                {
                    RegistryEdit.SetAutoStart();
                }
                else
                {
                    string EXEC_PATH = System.Windows.Forms.Application.ExecutablePath;
                    if (!path.ToUpper().Equals(EXEC_PATH.ToUpper()))
                    {
                        RegistryEdit.RemoveAutoStart();
                        RegistryEdit.SetAutoStart();
                    }
                }
            }
            else
            {
                RegistryEdit.RemoveAutoStart();
            }

            string language = cB_languages.SelectedItem.ToString();
            if (language != Config.User.Language)
            {
                Config.User.Language = language;
                Translation.Current.LoadLanguage(m_languages[language]);
                OnTranslateTexts();             
            }
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            OnEnableApply(true);
        }
    }
}
