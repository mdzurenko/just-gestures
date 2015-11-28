using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using JustGestures.Properties;
using JustGestures.GUI;
using JustGestures.Features;
using JustGestures.Languages;

namespace JustGestures
{
    public partial class Form_engine : Form, ITranslation
    {
        int m_processId = 0;
        /// <summary>
        /// Main JG's process id 
        /// </summary>
        public int ProcessId { get { return m_processId; } }
        /// <summary>
        /// Main GUI window
        /// </summary>
        Form_main form_mainWindow = null;
        /// <summary>
        /// Update window
        /// </summary>
        Form_update form_update = null;
        /// <summary>
        /// All user's gestures
        /// </summary>
        GesturesCollection m_gestures;
        /// <summary>
        /// JG's engine
        /// </summary>
        MyEngine m_engine;

        bool m_activeGestures;
        bool m_autoWasRunning;
        bool m_closing = false;
        /// <summary>
        /// Indicates that pc is about to suspend (hybernate/sleep)
        /// </summary>
        bool m_pcSuspended = false;
        
        static string[] m_privateTextClipboards;
        //static ReadOnlyCollection<ClipData>[] m_privateClipboards;        
        

        static List<NotifyIcon> m_trayIcons;
        /// <summary>
        /// All programs that were minimized to tray
        /// </summary>
        public static List<NotifyIcon> TrayIcons { get { return m_trayIcons; } set { m_trayIcons = value; } }
        //public static ReadOnlyCollection<ClipData>[] PrivateClipboards { get { return m_privateClipboards; } set { m_privateClipboards = value; } }
        /// <summary>
        /// All private clipboard texts 
        /// </summary>
        public static string[] PrivateTextClipboards { get { return m_privateTextClipboards; } set { m_privateTextClipboards = value; } }

        static Form_engine m_theForm;
        /// <summary>
        /// Singleton instance of JG
        /// </summary>
        public static Form_engine Instance
        {
            get
            {
                if (m_theForm == null)
                {
                    m_theForm = new Form_engine();
                }
                return m_theForm;
            }
        }

        #region ITranslation Members

        MyText ST_tT_learning = new MyText("ST_tT_learning");
        MyText ST_tT_autobehave = new MyText("ST_tT_autobehave");
        MyText ST_tT_disabled = new MyText("ST_tT_disabled");


        public void Translate()
        {
            tSMI_show.Text = Translation.GetText("ST_tSMI_show");
            tSMI_about.Text = Translation.GetText("MW_tSB_about"); // same name as in main window
            tSMI_autobehave_on.Text = Translation.GetText("ST_tSMI_autobehave_on");
            tSMI_autobehave_off.Text = Translation.GetText("ST_tSMI_autobehave_off");
            tSMI_enable.Text = Translation.GetText("ST_tSMI_enable");
            tSMI_disable.Text = Translation.GetText("ST_tSMI_disable");
            tSMI_exit.Text = Translation.GetText("MW_sMI_exit"); // same name as in main window
            ST_tT_autobehave.Translate();
            ST_tT_disabled.Translate();
            ST_tT_learning.Translate();
            m_engine.Translate();
            Form_about.Instance.Translate();
            if (form_update != null && !form_update.IsDisposed)
                form_update.Translate();
            if (form_mainWindow != null && !form_mainWindow.IsDisposed)
                form_mainWindow.Translate();
        }

        #endregion

        public Form_engine()
        {   
            InitializeComponent();
            m_theForm = this;
            m_trayIcons = new List<NotifyIcon>();
            //m_privateClipboards = new ReadOnlyCollection<ClipData>[ControlItems.UC_clipboard.CLIPS_COUNT];
            m_privateTextClipboards = new string[ControlItems.UC_clipboard.CLIPS_COUNT];

            // create directory for storing configuration and gestures if it doesn't exists
            // otherwise nothing will be saved 
            if (!System.IO.Directory.Exists(Config.Default.FilesLocation))
            {
                try { System.IO.Directory.CreateDirectory(Config.Default.FilesLocation); }
                catch { }
            }

            m_gestures = FileOptions.LoadGestures();
            List<PrgNamePath> whiteList, blackList, finalList;
            FileOptions.LoadLists(out whiteList, out blackList, out finalList);

            m_engine = new MyEngine();
            m_engine.LearntGestures = m_gestures;
            m_engine.Network.Curves = m_gestures.GetCurves();
            m_engine.Network.CheckParams();
            m_engine.AppStateChanged += new MyEngine.DlgAppStateChanged(AppStateChanged);
            m_engine.FinalList = finalList;
            m_engine.SetImageList();
            m_engine.ApplySettings();
            Microsoft.Win32.SystemEvents.DisplaySettingsChanged += new EventHandler(SystemEvents_DisplaySettingsChanged);
            Microsoft.Win32.SystemEvents.PowerModeChanged += new Microsoft.Win32.PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);            
            //RegistryEdit.SetWin7AntiBug();
            //RegistryEdit.RemWin7AntiBug();
            m_activeGestures = true; 
            Translate();
        }

        void SystemEvents_PowerModeChanged(object sender, Microsoft.Win32.PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case Microsoft.Win32.PowerModes.Resume:
                    if (m_activeGestures)
                        m_engine.ManualInstall();
                    m_pcSuspended = false;
                    break;
                case Microsoft.Win32.PowerModes.Suspend:
                    m_pcSuspended = true;
                    m_engine.ManualUninstall();
                    break;
            }
        }

        void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            m_engine.ResizeTopWindow();
        }

        protected override void OnLoad(EventArgs e)
        {            
            this.Visible = false;
            this.ShowInTaskbar = false;
            base.OnLoad(e);
            m_processId = Process.GetCurrentProcess().Id;

            if (Config.User.FirstTimeRun)
            {
                string path = TypeOfAction.InternetOptions.GetDefaultBrowser();
                if (path != string.Empty && m_gestures.Groups.Count > 1)
                {                    
                    string name = System.IO.Path.GetFileNameWithoutExtension(path);
                    m_gestures.Groups[1].Caption = name;
                    m_gestures.Groups[1].Text = name;
                    m_gestures.Groups[1].Action.Details = path;
                }
                cMS_SystemTray.Enabled = false;
                Form_wizard form_wizard = new Form_wizard();
                form_wizard.ShowDialog();
                Config.User.FirstTimeRun = false;
                Config.User.Save();
                cMS_SystemTray.Enabled = true;
            }
            if (!Config.User.StartMinimized)
                ShowMainWindow();

            m_engine.Load();
            m_engine.ManualInstall();
            if (Config.User.AutoBehaviour)
            {
                tSMI_autobehave_off.Visible = true;
                tSMI_autobehave_on.Visible = false;
                m_engine.StartAutoBehave();
            }
            if (Config.User.CheckForUpdate)
            {
                form_update = new Form_update();
                form_update.NewUpdateAvaliable += new Form_update.DlgNewUpdateAvaliable(NewUpdateAvaliable);
                form_update.CheckForUpdate();
            }
        }

        private void NewUpdateAvaliable(bool avaliable)
        {
            if (avaliable) 
                form_update.ShowDialog();
        }
 
        private void Form_engine_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (form_mainWindow != null) form_mainWindow.Dispose();
            m_closing = true;
            // abort update in case of long connecting problems 
            if (form_update != null) form_update.AbortUpdate();
            // do not save any data here - it might not be correctly written into file!
        
            m_engine.Network.StopLearning();
            m_engine.StopAutoBehave();
            m_engine.ManualUninstall();
            Form_about.Instance.Close();            
            TypeOfAction.WindowOptions.ShowAllTrayWindows();

        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ShowMainWindow();
        }

        public void ShowMainWindow()
        {            
            if (form_mainWindow == null || form_mainWindow.IsDisposed)
            {
                form_mainWindow = new GUI.Form_main();
                form_mainWindow.Gestures = m_gestures;
                form_mainWindow.Engine = m_engine;
                form_mainWindow.SetListView();
                form_mainWindow.FormClosed += this.form_mainWindow_FormClosed;
                form_mainWindow.Translate();
            }
            form_mainWindow.Show();
            form_mainWindow.ShowMainForm();
        }

        void form_mainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (form_mainWindow.WndClosed)
                this.Close();
            else
            {
                form_mainWindow.FormClosed -= form_mainWindow_FormClosed;
                form_mainWindow = null;
            }
        }

        private void AppStateChanged(bool active, MyEngine.AppState state, MyEngine.AppState realState)
        {

            if (m_closing // setting icon for closing form caused exception
                || m_pcSuspended) // if pc is about to suspend do not change the icon 
            {
                return;
            }

            Debug.WriteLine(string.Format("AppStateChanged - Active: {0}, State: {1}, RealState: {2} ", active, state, realState));
            Bitmap icon = Resources.logo16x16.ToBitmap();            
            Graphics g = Graphics.FromImage(icon);
            if (!active)
            {
                icon = MakeGrayscale(icon);
                g = Graphics.FromImage(icon);
                //Bitmap disabledIcon = new Bitmap(icon.Width, icon.Height);
                //Graphics gp = Graphics.FromImage(disabledIcon);
                //ControlPaint.DrawImageDisabled(gp, icon, 0, 0, Color.Transparent);
                //gp.Dispose();
                //icon = disabledIcon;
            }
            if (state == MyEngine.AppState.Autobehave)
                g.DrawImage(Resources.autobehave.ToBitmap(), new Point(0, 0));
            else if (state == MyEngine.AppState.Learning)
                g.DrawImage(Resources.clock.ToBitmap(), new Point(0, 0));
            g.Dispose();
            
            string text = "Just Gestures";
            
            if (realState == MyEngine.AppState.Learning)
                text += "\n(" + ST_tT_learning + ")";
            else if (realState == MyEngine.AppState.Autobehave)
                text += "\n(" + ST_tT_autobehave +")";
            else if (!active)
                text += "\n(" + ST_tT_disabled +")";
            //Debug.WriteLine("NotifyIcont Text: " + text);
            notifyIcon1.Text = text;
            notifyIcon1.Icon = Icon.FromHandle(icon.GetHicon());             
            
        }

        public static Bitmap MakeGrayscale(Bitmap bmp)
        {
            Bitmap newBitmap = new Bitmap(bmp.Width, bmp.Height);
            Graphics g = Graphics.FromImage(newBitmap);

            ColorMatrix colorMatrix = new ColorMatrix(
                new float[][]
                {
                    new float[] {.3f, .3f, .3f, 0, 0},
                    new float[] {.59f, .59f, .59f, 0, 0},
                    new float[] {.11f, .11f, .11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0.15f, 0.15f, 0.15f, 0, 1}
                });
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);
            g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attributes);
            g.Dispose();
            return newBitmap;
        }

        #region Context menu from system tray

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMainWindow();
        }


        private void tSMI_disable_Click(object sender, EventArgs e)
        {
            m_activeGestures = !m_activeGestures;
            tSMI_disable.Visible = m_activeGestures;
            tSMI_enable.Visible = !m_activeGestures;
            if (m_activeGestures)
            {
                m_engine.ManualInstall();
                if (m_autoWasRunning) m_engine.StartAutoBehave();
            }
            else
            {
                if (m_engine.IsRunningAutoBehave)
                {
                    m_autoWasRunning = true;
                    m_engine.StopAutoBehave();
                }
                else
                    m_autoWasRunning = false;
                m_engine.ManualUninstall();
            }
            tSMI_autobehave_on.Enabled = m_activeGestures;
            tSMI_autobehave_off.Enabled = m_activeGestures;
        }

        private void tSMI_exit_Click(object sender, EventArgs e)
        {
            FileOptions.SaveGestures(m_gestures);
            m_engine.Network.SaveNeuralNetwork();
            this.Close();
        }

        private void tSMI_cM_autobehave_Click(object sender, EventArgs e)
        {
            tSMI_autobehave_on.Visible = m_engine.IsRunningAutoBehave;
            tSMI_autobehave_off.Visible = !m_engine.IsRunningAutoBehave;
            if (!m_engine.IsRunningAutoBehave)
                m_engine.StartAutoBehave();
            else
                m_engine.StopAutoBehave();
        }

        private void tSMI_about_Click(object sender, EventArgs e)
        {
            if (!Form_about.Instance.Visible)
                Form_about.Instance.Show();
            else
            {
                Form_about.Instance.Invalidate();
                Form_about.Instance.Activate();
            }
        }

        #endregion Context menu from system tray

       
    }
}
