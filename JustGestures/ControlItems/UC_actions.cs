using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using JustGestures.GestureParts;
using JustGestures.TypeOfAction;
using JustGestures.Languages;

namespace JustGestures.ControlItems
{
    public partial class UC_actions : BaseActionControl
    {
        MouseCore.ExtraMouseHook m_mouse;
        Dictionary<string, TreeNode> m_actions;
        List<MyGesture> m_selectedGroups;
        List<MyGesture> m_newGroups;
        Dictionary<string, TreeView> m_actionTrees;
        string m_prevSelected = string.Empty;
        TreeNode m_selectedNode = null;


        public List<MyGesture> SelectedGroups { get { return m_selectedGroups; } set { m_selectedGroups = value; } }
        public List<MyGesture> NewGroups { get { return m_newGroups; } }
        public MouseCore.ExtraMouseHook MouseEngine { set { m_mouse = value; } }
        

        public UC_actions()
        {             
            InitializeComponent();
            m_identifier = Page.Action;
            m_next = Page.Gesture; //default
            m_previous = Page.None;
            m_actions = new Dictionary<string, TreeNode>();
            typeof(UserControl).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, this, new object[] { true });
            m_newGroups = new List<MyGesture>();
            m_selectedGroups = new List<MyGesture>();
            m_actionTrees = new Dictionary<string, TreeView>();
            cCB_groups.ItemChecked += new ItemCheckedEventHandler(cCB_groups_ItemChecked);
            cCB_groups.ImageList = iL_applications;
            //tV_actions.Visible = false;

            m_about = Translation.GetText("C_Act_about");
            m_info = Translation.GetText("C_Act_info");
            gB_category.Text = Translation.GetText("C_Act_gB_category");
            gB_action.Text = Translation.GetText("C_Act_gB_action");
            gB_useOfAction.Text = Translation.GetText("C_Act_gB_useOfAction");
            rB_global.Text = Translation.GetText("C_Act_rB_globally");
            rB_local.Text = Translation.GetText("C_Act_rB_locally");
            btn_addApp.Text = Translation.GetText("Btn_addNew");

        }

        private void UserControl_actions_Load(object sender, EventArgs e)
        {
            tV_category.Nodes.Add(ActionCategory.GetCategories(iL_actions));
            foreach (TreeNode node in tV_category.Nodes[0].Nodes)
            {
                m_actions.Add(node.Name, ActionCategory.GetActions(node.Name, iL_actions));
                TreeView actionTree = new TreeView();                
                //actionTree.HideSelection = false;
                actionTree.Nodes.Add(m_actions[node.Name]);                
                actionTree.Visible = false;
                actionTree.ShowPlusMinus = false;
                actionTree.ShowRootLines = false;
                actionTree.ImageList = iL_actions;
                actionTree.AfterSelect += new TreeViewEventHandler(tV_actions_AfterSelect);                
                actionTree.BeforeCollapse += new TreeViewCancelEventHandler(tV_actions_BeforeCollapse);
                gB_action.Controls.Add(actionTree);
                actionTree.Dock = DockStyle.Fill;
                m_actionTrees.Add(node.Name, actionTree);
            }
            foreach (MyGesture gest in m_gesturesCollection.Groups)
            {
                if (gest.ID != AppGroupOptions.APP_GROUP_GLOBAL)
                {
                    gest.SetActionIcon(iL_applications);
                    cCB_groups.AddItem(gest.ID, gest.Caption);
                }
            }
            if (m_selectedGroups.Count == 0 || m_selectedGroups[0].ID == AppGroupOptions.APP_GROUP_GLOBAL)
                rB_global.Checked = true;
            else
            {
                foreach (MyGesture group in m_selectedGroups)
                    cCB_groups.ListItems[group.ID].Checked = true;
                cCB_groups.CheckListItems();
                rB_local.Checked = true;
            }
            if (cCB_groups.ListItems.Count > 0) cCB_groups.ListItems[0].Selected = true;
        }

        private void tV_category_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tV_category.SelectedNode.Nodes.Count != 0) return;
            if (m_selectedNode != null)
                m_selectedNode.TreeView.HideSelection = true;            
            m_selectedNode = null;
            if (m_prevSelected != string.Empty)
            {
                m_actionTrees[m_prevSelected].Visible = false;
                m_actionTrees[m_prevSelected].SelectedNode = null;
            }
            else
                tV_actions.Visible = false;
            
            m_actionTrees[tV_category.SelectedNode.Name].Visible = true;
            m_prevSelected = tV_category.SelectedNode.Name;
            
            //tV_actions.Nodes.Clear();
            //tV_actions.Nodes.Add(m_actions[tV_category.SelectedNode.Name]);
            OnCanContinue(false);
        }

        void cCB_groups_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            m_selectedGroups = new List<MyGesture>();
            foreach (ListViewItem item in cCB_groups.CheckedListItems)
                m_selectedGroups.Add(m_gesturesCollection.Groups[item.Index + 1]);
            if (m_selectedNode != null && m_selectedGroups.Count != 0)
                OnCanContinue(true);
            else
                OnCanContinue(false);
        }

        private void tV_actions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeView actionTreeView = (TreeView)sender;
            if (actionTreeView.SelectedNode.Parent == null)
            {
                m_selectedNode = null;
                OnCanContinue(false);
            }
            else
            {
                m_selectedNode = actionTreeView.SelectedNode;
                m_selectedNode.TreeView.HideSelection = false;
                m_tempGesture.Action = (BaseActionClass)((BaseActionClass)m_selectedNode.Tag).Clone();
                m_tempGesture.Caption = m_selectedNode.Text;
                //m_tempGesture.Action.Details = string.Empty;
                switch (m_tempGesture.Action.Name)
                {
                    case WindowsShell.SHELL_OPEN_FLDR:                   
                    case WindowsShell.SHELL_START_PRG:
                        m_next = Page.PrgWwwFld;
                        break;
                    case KeystrokesOptions.KEYSTROKES_PRIVATE_COPY_TEXT:
                    case KeystrokesOptions.KEYSTROKES_PRIVATE_PASTE_TEXT:
                        m_next = Page.Clipboard;
                        break;
                    case InternetOptions.INTERNET_OPEN_WEBSITE:
                    case InternetOptions.INTERNET_SEND_EMAIL:
                    case InternetOptions.INTERNET_SEARCH_WEB:
                        m_next = Page.MailSearchWeb;
                        break;
                    case InternetOptions.INTERNET_TAB_NEW:
                    case InternetOptions.INTERNET_TAB_CLOSE:
                    case InternetOptions.INTERNET_TAB_REOPEN:
                    case KeystrokesOptions.KEYSTROKES_ZOOM_IN:
                    case KeystrokesOptions.KEYSTROKES_ZOOM_OUT:
                    case KeystrokesOptions.KEYSTROKES_SYSTEM_COPY:
                    case KeystrokesOptions.KEYSTROKES_SYSTEM_PASTE:
                    case KeystrokesOptions.KEYSTROKES_SYSTEM_CUT:
                    case KeystrokesOptions.KEYSTROKES_CUSTOM:
                    case ExtrasOptions.EXTRAS_TAB_SWITCHER:
                    case ExtrasOptions.EXTRAS_TASK_SWITCHER:
                    case ExtrasOptions.EXTRAS_ZOOM:
                    case ExtrasOptions.EXTRAS_CUSTOM_WHEEL_BTN:
                        m_next = Page.Keystrokes;
                        break;
                    case KeystrokesOptions.KEYSTROKES_PLAIN_TEXT:
                        m_next = Page.PlainText;
                        break;
                    default:
                        m_next = Page.Gesture;
                        break;

                }                
                if (m_selectedGroups.Count > 0)
                    OnCanContinue(true);
                else
                    OnCanContinue(false);
            }
        }
       
        private void UserControl_actions_VisibleChanged(object sender, EventArgs e)
        {
            if (((UC_actions)sender).Visible)
            {
                OnChangeAboutText(m_about);              
                OnChangeInfoText(ToolTipIcon.Info, Translation.Text_info, m_info);
                if (m_selectedNode != null)
                    m_selectedNode.TreeView.Focus();
                OnCanContinue(false);
                if (m_tempGesture.Action != null) OnCanContinue(true);
            }
        }

        private void rB_GlobalLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                CheckCheckedState();           
        }

        private void CheckCheckedState()
        {
            if (rB_global.Checked)
            {                
                cCB_groups.Enabled = false;
                btn_addApp.Enabled = false;
                m_selectedGroups = new List<MyGesture>();
                m_selectedGroups.Add(m_gesturesCollection.Groups[0]);
            }
            else
            {
                m_selectedGroups = new List<MyGesture>();
                foreach (ListViewItem item in cCB_groups.CheckedListItems)
                    m_selectedGroups.Add(m_gesturesCollection.Groups[item.Index + 1]);
                                
                cCB_groups.Enabled = true;
                btn_addApp.Enabled = true;                
            }
            //if (m_selectedNode != null)
            if (m_selectedGroups.Count != 0 && m_selectedNode != null)
                OnCanContinue(true);
            else
                OnCanContinue(false);
            //if (m_selectedNode != null)
            //    m_selectedNode.TreeView.Select();
        }
              
        private void btn_addApp_Click(object sender, EventArgs e)
        {
            GUI.Form_addGesture addGesture = new GUI.Form_addGesture();
            addGesture.Gestures = m_gesturesCollection;
            addGesture.AppMode = true;
            if (addGesture.ShowDialog() == DialogResult.OK)
            {                
                foreach (MyGesture gesture in addGesture.NewGestures)
                {
                    MyGesture gestureToAdd = new MyGesture(gesture);
                    gesture.SetActionIcon(iL_applications);
                    m_gesturesCollection.Add(gestureToAdd);
                    m_newGroups.Add(gestureToAdd);
                    cCB_groups.AddItem(gestureToAdd.ID, gestureToAdd.Caption);
                }
            }
        }

        private void tV_actions_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void tV_category_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }
    

        
    }
}
