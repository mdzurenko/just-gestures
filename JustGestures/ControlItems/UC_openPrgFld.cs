using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using JustGestures.TypeOfAction;
using JustGestures.Features;
using MouseCore;
using JustGestures.Languages;

namespace JustGestures.ControlItems
{
    public partial class UC_openPrgFld : BaseActionControl
    {
        bool m_error = false;
        bool m_firstTime = true;
        Timer m_timer;        

        delegate void EmptyDel();

        public UC_openPrgFld()
        {
            InitializeComponent();
            //!!! without this command the learnt gestures are not saved to the xml files !!!
            openFileDialog1.RestoreDirectory = true;
            m_identifier = Page.PrgWwwFld;
            m_next = Page.Gesture;
            m_previous = Page.Action;
            m_timer = new Timer();
            m_timer.Interval = 2000;
            m_timer.Tick += new EventHandler(m_timer_Tick);
            for (int i = 0; i < 3; i++)
                cB_wndStyle.Items.Add(GetWndStyleText(IndexToWndStyle(i)));
            btn_Browse.Text = Translation.GetText("C_OpenFldrPrg_btn_browse");
            lbl_arguments.Text = Translation.GetText("C_StartPrg_lbl_cmdArguments");
            lbl_wndStyle.Text = Translation.GetText("C_OpenFldrPrg_lbl_windowStyle");
            lbl_icon.Text = Translation.GetText("C_StartPrg_lbl_icon");
        }

        private void UC_openPrgWwwFld_Load(object sender, EventArgs e)
        {
            crossCursorMode1.SetCapture();
            crossCursorMode1.ApplicationChanged += new CrossCursorMode.DlgApplicationChanged(ApplicationPathChanged);
        }

        public void OnClosingForm()
        {
            crossCursorMode1.ReleaseCapture();
        }


        private static string GetWndStyleText(string wndStyle)
        {
            switch (wndStyle)
            {
                case WindowsShell.WND_STYLE_NORMAL: return Translation.GetText("C_OpenFldrPrg_cBI_wndStyleNormal");
                case WindowsShell.WND_STYLE_MINIMIZED: return Translation.GetText("C_OpenFldrPrg_cBI_wndStyleMinimized");
                case WindowsShell.WND_STYLE_MAXIMIZED: return Translation.GetText("C_OpenFldrPrg_cBI_wndStyleMaximized");
            }
            return string.Empty;
        }

        private static int WndStyleToIndex(string wndStyle)
        {
            switch (wndStyle)
            {
                case WindowsShell.WND_STYLE_NORMAL: return 0;
                case WindowsShell.WND_STYLE_MINIMIZED: return 1;
                case WindowsShell.WND_STYLE_MAXIMIZED: return 2;
                default: return 0;
            }
        }

        private static string IndexToWndStyle(int index)
        {
            switch (index)
            {
                case 0: return WindowsShell.WND_STYLE_NORMAL;
                case 1: return WindowsShell.WND_STYLE_MINIMIZED;
                case 2: return WindowsShell.WND_STYLE_MAXIMIZED;
                default: return WindowsShell.WND_STYLE_NORMAL;
            }
        }

        private void ApplicationPathChanged(IntPtr hWnd)
        {
            string path = TypeOfAction.AppGroupOptions.GetPathFromHwnd(hWnd);
            tB_path.Text = path;
        }

        void m_timer_Tick(object sender, EventArgs e)
        {
            m_timer.Stop();
            SetInfoValues();
        }

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            if (m_tempGesture.Action == null) return;
            switch (m_tempGesture.Action.Name)
            {
                case WindowsShell.SHELL_START_PRG:
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        tB_path.Text = openFileDialog1.FileName;
                    }
                    break;
                case WindowsShell.SHELL_OPEN_FLDR:
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        tB_path.Text = folderBrowserDialog1.SelectedPath;
                    }
                    break;          
            }            
        }
        
        private void tB_path_TextChanged(object sender, EventArgs e)
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
                case WindowsShell.SHELL_START_PRG:
                    if (File.Exists(tB_path.Text))
                    {
                        pB_icon.Image = Icon.ExtractAssociatedIcon(tB_path.Text).ToBitmap();
                        SetValues();
                    }
                    else
                    {
                        pB_icon.Image = null;
                        OnCanContinue(false);
                    }
                    break;
                case WindowsShell.SHELL_OPEN_FLDR:
                    if (Directory.Exists(tB_path.Text)) SetValues();
                    else OnCanContinue(false);
                    break;             
            }
        }     

        private void tB_cmdArguments_TextChanged(object sender, EventArgs e)
        {
            if (!tB_cmdArguments.Visible) return;
            m_tempGesture.Action.Details = string.Format("{0}|{1}|{2}", tB_path.Text, ((WindowsShell)m_tempGesture.Action).WndStyleFromDetails(), tB_cmdArguments.Text);
        }

        private void SetValues()
        {
            m_error = false;
            m_tempGesture.Action.Details = string.Format("{0}|{1}|{2}", tB_path.Text, ((WindowsShell)m_tempGesture.Action).WndStyleFromDetails(),
                ((WindowsShell)m_tempGesture.Action).CmdArgumentsFromDetails());
            //string fileName = "";
            //fileName = Path.GetFileName(tB_path.Text);
            //m_tempGesture.Caption = Languages.Translation.GetText(m_tempGesture.Action.Name) + " " + fileName;
            OnCanContinue(true);
        }


        private void cB_wndStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cB_wndStyle.SelectedIndex;
            //gesture.Details = cB_wndStyle.Text;
            m_tempGesture.Action.Details = string.Format("{0}|{1}|{2}", tB_path.Text, IndexToWndStyle(index),
                 ((WindowsShell)m_tempGesture.Action).CmdArgumentsFromDetails());
        }

        private void SetInfoValues()
        {
            bool error = m_error;
            if (m_firstTime)
                m_error = false;
            string text = string.Empty;
            switch (m_tempGesture.Action.Name)
            {
                case WindowsShell.SHELL_START_PRG:
                    if (!m_error)
                    {
                        //text = "You may chose path to the application via Browse button, typing it directly to text box\n"
                        //text += "or by draging Cross cursor over the application (it is possible combine it with ALT + TAB)."
                        text = Translation.GetText("C_StartPrg_info");
                        OnChangeInfoText(ToolTipIcon.Info, Translation.Text_info, text);
                    }
                    else
                    {
                        text = Translation.GetText("C_StartPrg_errProgramNotExists");
                        OnChangeInfoText(ToolTipIcon.Error, Translation.Text_error, text);//"Path to the application does not exist."
                    }
                    break;
                case WindowsShell.SHELL_OPEN_FLDR:
                    if (!m_error)
                    {
                        text = Translation.GetText("C_OpenFldr_info");
                        OnChangeInfoText(ToolTipIcon.Info, Translation.Text_info, text);//"Chose path to the folder via Browse button or type it directly to text box."
                    }
                    else
                    {
                        text = Translation.GetText("C_OpenFldr_errFolderNotExists");
                        OnChangeInfoText(ToolTipIcon.Error, Translation.Text_error, text);// "Path to the folder does not exist."
                    }
                    break;              
            }
            m_error = error;
        }

        private void SetComponents()
        {
            tB_path.Text = " ";
            tB_path.Text = ((WindowsShell)m_tempGesture.Action).PathFromDetails();
            cB_wndStyle.SelectedIndex = WndStyleToIndex(((WindowsShell)m_tempGesture.Action).StrWndStyleFromDetails());
            switch (m_tempGesture.Action.Name)
            {
                case WindowsShell.SHELL_START_PRG:
                    OnChangeAboutText(Translation.GetText("C_StartPrg_about"));//"Chose the destination path to the application"                  
                    lbl_path.Text = Translation.GetText("C_StartPrg_lbl_path");// "Path"
                    tB_cmdArguments.Text = ((WindowsShell)m_tempGesture.Action).CmdArgumentsFromDetails();
                    panel_cmdArguments.Visible = true;
                    panel_icon.Visible = true;
                    crossCursorMode1.Visible = true;                    
                    tB_path.AutoCompleteSource = AutoCompleteSource.FileSystem;
                    break;
                case WindowsShell.SHELL_OPEN_FLDR:
                    OnChangeAboutText(Translation.GetText("C_OpenFldr_about"));// "Chose the destination path to the folder"
                    lbl_path.Text = Translation.GetText("C_OpenFldr_lbl_folder");// "Folder"
                    panel_cmdArguments.Visible = false;
                    panel_icon.Visible = false;
                    crossCursorMode1.Visible = false;
                    tB_path.AutoCompleteSource = AutoCompleteSource.FileSystemDirectories;
                    break;
            }
        }


        private void UC_openPrgWwwFld_VisibleChanged(object sender, EventArgs e)
        {
            if (m_tempGesture == null) return;
            if (((UC_openPrgFld)sender).Visible)
            {
                if (m_tempGesture.Action == null) return;
                if (m_tempGesture.Action.Details == string.Empty)
                {
                    m_firstTime = true;
                    pB_icon.Image = null;
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
                    try { fileName = Path.GetFileName(tB_path.Text); }
                    catch (Exception ex) { fileName = string.Empty; }
                    m_tempGesture.Caption = Translation.GetText(m_tempGesture.Action.Name) + " " + fileName;
                }
            }
        }

        private void tB_path_Leave(object sender, EventArgs e)
        {
            m_timer.Stop();
            SetInfoValues();
        }
    }
}
