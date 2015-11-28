using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JustGestures.TypeOfAction;
using JustGestures.Features;
using JustGestures.Languages;

namespace JustGestures.ControlItems
{
    public partial class UC_customKeystrokes : BaseActionControl
    {
        //Win32.KEYBOARD_INPUT[] m_keyInput;
        MyKey m_selectedKey;
        int m_errorCount;
        string m_strKeys = string.Empty;
        string m_errorMsg = string.Empty;
        ListView m_focusedListView;
        ListView m_prevFocusListView;
        string m_errMoreUpInline;
        string m_errMoreDownInline;
        string m_errDownWithoutUp;
        string m_errOneMouseActionAllowed;

        public UC_customKeystrokes()
        {
            InitializeComponent();
            m_focusedListView = lW_triggerDown;
            m_identifier = Page.Keystrokes;
            m_next = Page.Gesture;
            m_previous = Page.Action;
            foreach (MyKey k in AllKeys.Ordinary)
                lB_keys.Items.Add(k.KeyName);
            foreach (MyKey k in AllKeys.Modifiers)
                lB_modifiers.Items.Add(k.KeyName);
            foreach (MyKey k in AllKeys.MouseActions)
                lB_special.Items.Add(k.KeyName);
            m_selectedKey = null;
            gB_modifiers.Text = Translation.GetText("C_CK_gB_modifiers");
            gB_keys.Text = Translation.GetText("C_CK_gB_keys");
            gB_mouseActions.Text = Translation.GetText("C_CK_gB_mouseActions");
            btn_add.Text = Translation.GetText("C_CK_btn_add");
            btn_remove.Text = Translation.GetText("C_CK_btn_remove");
            gB_keyScript.Text = Translation.GetText("C_CK_gB_keyScript");
            cH_holdBtn_down.Text = Translation.GetText("C_WK_cH_holdBtn_down");
            cH_holdBtn_up.Text = Translation.GetText("C_WK_cH_holdBtn_up");
            cH_wheelMove_down.Text = Translation.GetText("C_WK_cH_wheelMove_down");
            cH_wheelMove_up.Text = Translation.GetText("C_WK_cH_wheelMove_up");
            m_errMoreDownInline = Translation.GetText("C_CK_errMoreDownInline");
            m_errMoreUpInline = Translation.GetText("C_CK_errMoreUpInline");
            m_errDownWithoutUp = Translation.GetText("C_CK_errDownWithoutUp");
            m_errOneMouseActionAllowed = Translation.GetText("C_CK_errOneMouseActionAllowed");
        }

        private void SetComponents()
        {            
            m_prevFocusListView = m_focusedListView;
            m_focusedListView.BackColor = SystemColors.Window;
            if (!m_tempGesture.Action.IsExtras())
            {
                panel_triggerDown.Visible = true;
                cH_holdBtn_down.Text = Translation.GetText("C_CK_cH_executeBtn_click");
                //lW_triggerDown.Columns[0].Text = "On Modifier Click Script";
                panel_wheelDown.Visible = false;
                panel_wheelUp.Visible = false;
                panel_triggerUp.Visible = false;
            }
            else
            {
                panel_triggerDown.Visible = true;
                cH_holdBtn_down.Text = Translation.GetText("C_WK_cH_holdBtn_down");
                //lW_triggerDown.Columns[0].Text = "On Trigger Down Script";
                panel_wheelDown.Visible = true;
                panel_wheelUp.Visible = true;
                panel_triggerUp.Visible = true;
            }
            lW_triggerDown.Items.Clear();
            lW_wheelDown.Items.Clear();
            lW_wheelUp.Items.Clear();
            lW_triggerUp.Items.Clear();
            if (m_tempGesture.Action.KeyScript != null)
            {
                m_strKeys = string.Empty;
                foreach (MyKey k in m_tempGesture.Action.ExtractKeyList(BaseActionClass.MouseAction.TriggerDown))
                    lW_triggerDown.Items.Add((ListViewItem)k.Clone());
                foreach (MyKey k in m_tempGesture.Action.ExtractKeyList(BaseActionClass.MouseAction.TriggerUp))
                    lW_triggerUp.Items.Add((ListViewItem)k.Clone());
                foreach (MyKey k in m_tempGesture.Action.ExtractKeyList(BaseActionClass.MouseAction.WheelDown))
                    lW_wheelDown.Items.Add((ListViewItem)k.Clone());
                foreach (MyKey k in m_tempGesture.Action.ExtractKeyList(BaseActionClass.MouseAction.WheelUp))
                    lW_wheelUp.Items.Add((ListViewItem)k.Clone());
            }
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ListBox)sender).SelectedIndex == -1) return;

            if (((ListBox)sender) == lB_modifiers)
            {
                lB_keys.SelectedIndex = -1;
                lB_special.SelectedIndex = -1;
                m_selectedKey = AllKeys.Modifiers[lB_modifiers.SelectedIndex];
            }
            else if (((ListBox)sender) == lB_keys)
            {
                lB_modifiers.SelectedIndex = -1;
                lB_special.SelectedIndex = -1;
                m_selectedKey = AllKeys.Ordinary[lB_keys.SelectedIndex];
            }
            else if (((ListBox)sender) == lB_special)
            {
                lB_keys.SelectedIndex = -1;
                lB_modifiers.SelectedIndex = -1;
                m_selectedKey = AllKeys.MouseActions[lB_special.SelectedIndex];
            }
        }

        private void listBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AddKeyToScript(m_focusedListView);
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            AddKeyToScript(m_focusedListView);
        }

        private void listBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Return)
                AddKeyToScript(m_focusedListView);
        }

        private void AddKeyToScript(ListView listview)
        {
            if (m_selectedKey == null) return;
            listview.Items.Add((ListViewItem)m_selectedKey.Clone());
            listview.EnsureVisible(listview.Items.Count - 1);
            AutoCheckValidity();
        }

        private void lW_script_SelectedIndexChanged(object sender, EventArgs e)
        {            
            btn_remove.Enabled = ((ListView)sender).SelectedItems.Count > 0 ? true : false;
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            RemoveFromScript(m_focusedListView);
        }

        private void lW_script_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                RemoveFromScript((ListView)sender);
        }

        private void lW_script_DragDrop(object sender, DragEventArgs e)
        {
            AutoCheckValidity();
        }

        private void lW_script_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RemoveFromScript((ListView)sender);
        }

        private void RemoveFromScript(ListView listview)
        {
            ListView.SelectedIndexCollection items = listview.SelectedIndices;
            while (items.Count > 0)
            {
                listview.Items.RemoveAt(items[items.Count - 1]);
            }
            AutoCheckValidity();
        }

        private void AutoCheckValidity()
        {            
            List<List<MyKey>> finalScript = new List<List<MyKey>>();
            List<MyKey> scriptTrigger = new List<MyKey>();
            List<MyKey> scriptTriggerDown = new List<MyKey>();
            List<MyKey> scriptTriggerUp = new List<MyKey>();
            List<MyKey> scriptWheelDown = new List<MyKey>();
            List<MyKey> scriptWheelUp = new List<MyKey>();

            m_strKeys = string.Empty;
            m_errorCount = 0;
            m_errorMsg = string.Empty;

            if (!m_tempGesture.Action.IsExtras())
            {
                scriptTriggerDown = GetScript(lW_triggerDown);
                CheckScript(scriptTriggerDown);
            }
            else
            {
                string strKeysBegin = string.Empty;
                string strKeysEnd = string.Empty;
                
                scriptTriggerDown = GetScript(lW_triggerDown);
                strKeysBegin = m_strKeys;
                m_strKeys = string.Empty;
                
                scriptTriggerUp = GetScript(lW_triggerUp);
                strKeysEnd = m_strKeys;
                m_strKeys = string.Empty;

                scriptTrigger.AddRange(scriptTriggerDown.ToArray());
                scriptTrigger.AddRange(scriptTriggerUp.ToArray());
                CheckScript(scriptTrigger);
                scriptWheelDown = GetScript(lW_wheelDown);
                CheckScript(scriptWheelDown);
                scriptWheelUp = GetScript(lW_wheelUp);
                CheckScript(scriptWheelUp);
                m_strKeys = strKeysBegin + m_strKeys + strKeysEnd;
            }

                //has to be in this specific order because in KeystrokesControl is used ExtractingMetod
            finalScript.Add(scriptTriggerDown);
            finalScript.Add(scriptWheelDown);
            finalScript.Add(scriptWheelUp);
            finalScript.Add(scriptTriggerUp);

            if (m_errorCount == 0)
            {
                m_tempGesture.Action.KeyScript = finalScript;
                // always call check sript for mouse to properly set the script
                m_tempGesture.Action.CheckScriptForMouse();
                m_tempGesture.Action.Details = m_strKeys;
                bool wndUnderCursor = Config.User.HandleWndUnderCursor;
                string text = string.Empty;
                if (!m_tempGesture.Action.IsExtras())
                {
                    //text += "Script may contain only one mouse action.\n";
                    //text += "Script is send to window according to option: 'Control Window which is...'";
                    OnChangeInfoText(ToolTipIcon.Info, Translation.Text_info, Translation.GetText("C_CK_info"));
                }
                else
                {
                    //text += "On Trigger Down & On Trigger Up is considered as one script.\n";
                    //text += "Each script may contain only one mouse action.\n";
                    //text += "On Trigger Down script is invoked by first wheel move! (Not only by pushing it.) \n";
                    //text += "Script is send to window according to option: 'Control Window which is...'";
                    OnChangeInfoText(ToolTipIcon.Info, Translation.Text_info, Translation.GetText("C_WK_info"));
                }
                if (scriptTriggerDown.Count > 0 || scriptTriggerUp.Count > 0 || scriptWheelDown.Count > 0 || scriptWheelUp.Count > 0)
                    OnCanContinue(true);
                else
                    OnCanContinue(false);
            }
            else
            {
                OnChangeInfoText(ToolTipIcon.Error, Translation.Text_error, m_errorMsg);
                OnCanContinue(false);
            }            
        }


        private List<MyKey> GetScript(ListView listViewScript)
        {
            List<MyKey> script = new List<MyKey>();
            string columnName = listViewScript.Columns[0].Text;
            if (listViewScript.Items.Count > 0)
                m_strKeys += columnName.Replace(" ", "") + ":";
            for (int i = 0; i < listViewScript.Items.Count; i++)
            {
                script.Add(MyKey.ListViewItemToKey(listViewScript.Items[i]));
                m_strKeys += script[script.Count - 1].KeyName;
                if (i < listViewScript.Items.Count - 1)
                    m_strKeys += "+";
            }
            if (listViewScript.Items.Count > 0)
                m_strKeys += " ";
            return script;
        }


        private void CheckScript(List<MyKey> script)
        {
            const string STR_WIN = "Windows Key";
            const string STR_SHIFT = "SHIFT";
            const string STR_CTRL = "CTRL";
            const string STR_ALT = "ALT";

            int c_win = 0;
            int c_shift = 0;
            int c_ctrl = 0;
            int c_alt = 0;
            int c_mouse = 0;

            for (int i = 0; i < script.Count; i++)
            {
                switch (script[i].KeyName)
                {
                    case AllKeys.STR_WIN_DOWN: c_win++; goto case STR_WIN;
                    case AllKeys.STR_WIN_UP: c_win--; goto case STR_WIN;
                    case STR_WIN: CheckScriptValidity(false, c_win, STR_WIN); break;

                    case AllKeys.STR_SHIFT_DOWN: c_shift++; goto case STR_SHIFT;
                    case AllKeys.STR_SHIFT_UP: c_shift--; goto case STR_SHIFT;
                    case STR_SHIFT: CheckScriptValidity(false, c_shift, STR_SHIFT); break;

                    case AllKeys.STR_CONTROL_DOWN: c_ctrl++; goto case STR_CTRL;
                    case AllKeys.STR_CONTROL_UP: c_ctrl--; goto case STR_CTRL;
                    case STR_CTRL: CheckScriptValidity(false, c_ctrl, STR_CTRL); break;

                    case AllKeys.STR_MENU_DOWN: c_alt++; goto case STR_ALT;
                    case AllKeys.STR_MENU_UP: c_alt--; goto case STR_ALT;
                    case STR_ALT: CheckScriptValidity(false, c_alt, STR_ALT); break;                   
                }

                switch (script[i].KeyAction)
                {
                    case MyKey.Action.MouseClick:
                    case MyKey.Action.MouseDblClick:
                    case MyKey.Action.MouseWheelDown:
                    case MyKey.Action.MouseWheelUp:
                    case MyKey.Action.MouseX1Click:
                    case MyKey.Action.MouseX1DblClick:
                    case MyKey.Action.MouseX2Click:
                    case MyKey.Action.MouseX2DblClick:
                        c_mouse++;
                        break;
                }
            }
            CheckScriptValidity(true, c_win, STR_WIN);
            CheckScriptValidity(true, c_shift, STR_SHIFT);
            CheckScriptValidity(true, c_ctrl, STR_CTRL);
            CheckScriptValidity(true, c_alt, STR_ALT);

            if (c_mouse > 1)
            {
                //m_errorMsg += string.Format("Error {0}: You can use only one mouse action in script. \n", ++m_errorCount);
                m_errorMsg += string.Format(m_errOneMouseActionAllowed + "\n", ++m_errorCount);
            }
        }

        private void CheckScriptValidity(bool end, int count, string str_key)
        {
            if (!end)
            {
                if (count < 0)
                {
                    //m_errorMsg += string.Format("Error {0}: You have used more {1} (UP) inline. \n", ++m_errorCount, str_key);
                    m_errorMsg += string.Format(m_errMoreUpInline + "\n", ++m_errorCount, str_key);
                }
                else if (count > 1)
                {
                    //m_errorMsg += string.Format("Error {0}: You have used more {1} (DOWN) inline. \n", ++m_errorCount, str_key);
                    m_errorMsg += string.Format(m_errMoreDownInline + "\n", ++m_errorCount, str_key);
                }
            }
            else
            {
                if (count == 1 & end)
                {
                    //m_errorMsg += string.Format("Error {0}: You have used {1} (DOWN) without {1} (UP). \n", ++m_errorCount, str_key);
                    m_errorMsg += string.Format(m_errDownWithoutUp + "\n", ++m_errorCount, str_key);
                }
            }
        }

        private void lW_script_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                e.Cancel = true;
                e.NewWidth = 0;
                this.Cursor = Cursors.Default;
            }
        }        

        private void UC_customKeystrokes_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                SetComponents();
                if (!m_tempGesture.Action.IsExtras())
                    OnChangeAboutText(Translation.GetText("C_CK_about")); //"Create your action key script");
                else
                    OnChangeAboutText(Translation.GetText("C_WK_about")); 
                AutoCheckValidity();
            }
        }

        private void lW_script_Enter(object sender, EventArgs e)
        {
            m_focusedListView = (ListView)sender;
            if (m_focusedListView != m_prevFocusListView)
                m_prevFocusListView.BackColor = SystemColors.Control;
            m_focusedListView.BackColor = SystemColors.Window;
            m_prevFocusListView = m_focusedListView;
        }

        private void lW_script_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void lW_script_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.Item.BackColor = ((ListView)sender).BackColor;
            e.DrawDefault = true;
        }

        private void lW_script_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.Item.BackColor = ((ListView)sender).BackColor;
            e.DrawDefault = true;
        }



    }
}