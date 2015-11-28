using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using MouseCore;
using JustGestures.TypeOfAction;
using JustGestures.GestureParts;
using JustGestures.Languages;

namespace JustGestures.ControlItems
{
    public partial class UC_selectProgram : BaseActionControl
    {
        bool m_initialize;

        public UC_selectProgram()
        {
            InitializeComponent();
            m_identifier = Page.Application;
            m_previous = Page.None; //default
            m_next = Page.None;
            lbl_path.Text = Translation.GetText("C_SelectApp_lbl_path");
            lbl_name.Text = Translation.GetText("C_SelectApp_lbl_name");
            lbl_icon.Text = Translation.GetText("C_SelectApp_lbl_icon");
            cB_active.Text = Translation.GetText("C_SelectApp_cB_active");
            lbl_uncheckToDeactivate.Text = Translation.GetText("C_SelectApp_lbl_uncheckToDeactivate");
            btn_browse.Text = Translation.GetText("C_SelectApp_btn_browse");

        }

        private void UC_selectProgram_Load(object sender, EventArgs e)
        {
            m_initialize = true;
            crossCursorMode1.SetCapture();
            crossCursorMode1.ApplicationChanged += new CrossCursorMode.DlgApplicationChanged(ApplicationPathChanged);
            tB_name.Text = m_tempGesture.Action != null ? ((AppGroupOptions)m_tempGesture.Action).ExtractFileName() : string.Empty;// AppGroupControl.ExtractFileName(m_tempGesture.Action);
            tB_path.Text = m_tempGesture.Action != null ? ((AppGroupOptions)m_tempGesture.Action).ExtractPath() : string.Empty;//AppGroupControl.ExtractPath(m_tempGesture.Action);
            pB_icon.Image = m_tempGesture.Action != null ? m_tempGesture.Action.GetIcon(pB_icon.Width) : null; // tB_path.Text != string.Empty ? Icon.ExtractAssociatedIcon(tB_path.Text).ToBitmap() : null;
            cB_active.Checked = m_tempGesture.Active;
            if (m_tempGesture.ID == string.Empty)
            {
                m_tempGesture.AppGroup = null;
                m_tempGesture.Action = new AppGroupOptions(AppGroupOptions.APP_GROUP_USERS);// new Action(AppGroupControl.APP_GROUP_USERS, AppGroupControl.NAME);
                m_tempGesture.Activator = new MouseActivator(string.Empty, MouseActivator.Types.Undefined);
                OnCanContinue(false);
            }
            else
            {
                tB_path.Text = m_tempGesture.Action.Details;
                tB_name.Text = m_tempGesture.Caption;
                OnCanContinue(true);
            }
            m_initialize = false;
        }

        private void ApplicationPathChanged(IntPtr hWnd)
        {
            StringBuilder buff = new StringBuilder(256);
            Win32.GetClassName(hWnd, buff, 256);
            string wndName = buff.ToString();
            Win32.GetWindowText(hWnd, buff, 256);
            string wndText = buff.ToString();

            Debug.WriteLine(string.Format("AppPathChanged hWnd: {0} ClassName: {1} WindowText: {2}", hWnd, wndName, wndText));

            if (AppGroupOptions.IsTaskbar(wndName))
            {
                m_tempGesture.Action = new AppGroupOptions(AppGroupOptions.APP_GROUP_TASKBAR);
                tB_name.Text = tB_path.Text = Languages.Translation.GetText(m_tempGesture.Action.Name);
                pB_icon.Image = m_tempGesture.Action.GetIcon(pB_icon.Width);
            }
            else if (AppGroupOptions.IsDesktop(wndName))
            {
                m_tempGesture.Action = new AppGroupOptions(AppGroupOptions.APP_GROUP_DESKTOP);
                tB_name.Text = tB_path.Text = Languages.Translation.GetText(m_tempGesture.Action.Name);
                pB_icon.Image = m_tempGesture.Action.GetIcon(pB_icon.Width);
            }
            else
            {
                m_tempGesture.Action = new AppGroupOptions(AppGroupOptions.APP_GROUP_USERS);
                string path = AppGroupOptions.GetPathFromHwnd(hWnd);
                tB_path.Text = path;
                tB_name.Text = Path.GetFileNameWithoutExtension(path); //ExtractName(path);
                if (path != string.Empty)
                    pB_icon.Image = Icon.ExtractAssociatedIcon(path).ToBitmap();
                else
                    pB_icon.Image = null;
            }
            SetValues();
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Programs (*.exe)|*.exe";
            file.Multiselect = false;
            //!!! bez tohto prikazu neuklada naucene gesta do xml suborov, jednoducho to nejde !!!
            file.RestoreDirectory = true;
            if (file.ShowDialog() == DialogResult.OK)
            {
                m_tempGesture.Action = new AppGroupOptions(AppGroupOptions.APP_GROUP_USERS);// new Action(AppGroupControl.APP_GROUP_USERS, AppGroupControl.NAME);
                string path = file.FileName;
                tB_path.Text = path;
                tB_name.Text = Path.GetFileNameWithoutExtension(path);
                pB_icon.Image = Icon.ExtractAssociatedIcon(path).ToBitmap();
                SetValues();
            }
        }

        public void OnClosingForm()
        {
            crossCursorMode1.ReleaseCapture();
        }

        private void SetValues()
        {
            if (m_initialize) return;
            m_tempGesture.Caption = tB_name.Text;
            m_tempGesture.Action.Details = tB_path.Text;
            if (tB_path.Text.Length != 0 && (File.Exists(tB_path.Text) 
                || m_tempGesture.Action.Name == AppGroupOptions.APP_GROUP_TASKBAR
                || m_tempGesture.Action.Name == AppGroupOptions.APP_GROUP_DESKTOP))
                OnCanContinue(true);
            else
                OnCanContinue(false);
        }

        private void UC_selectProgram_VisibleChanged(object sender, EventArgs e)
        {
            if (((UC_selectProgram)sender).Visible)
            {
                string text = string.Empty;
                //text += "You can drag Cross cursor over the application that you want to add. \n";
                //text += "You may create two special groups: Taskbar & Desktop (selecting is possible only via Cross cursor).\n";
                //text += "It is possible to: \n";
                //text += " - Combine draging and Alt + Tab (Task Switcher). \n";
                //text += " - Add same application more times so you can better categorize actions. \n";
                text = Translation.GetText("C_SelectApp_info");

                OnChangeInfoText(ToolTipIcon.Info, Translation.Text_info, text);
                OnChangeAboutText(Translation.GetText("C_SelectApp_about")); //"Create Application Group for gestures");
                SetValues();
            }
        }

        private void tB_name_TextChanged(object sender, EventArgs e)
        {
            SetValues();
        }

        private void cB_active_CheckedChanged(object sender, EventArgs e)
        {
            m_tempGesture.Active = cB_active.Checked;
        }
    }
}
