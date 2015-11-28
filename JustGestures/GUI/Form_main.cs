using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JustGestures.Properties;
using JustGestures.GestureParts;
using JustGestures.Languages;

namespace JustGestures.GUI
{
    public partial class Form_main : Form, ITranslation
    {
        Form_options formOptions;
        //Form_about formAbout;
        GesturesCollection m_gestures;
        MyGesture m_prevSelectedGesture = null;
        MyEngine m_engine;
        bool m_animateGesture = false;
        bool m_wndClosed = false;
        bool m_copyGestures = false;

        bool m_pbMinMouseLeave = true;
        bool m_pbCloseMouseLeave = true;

        public GesturesCollection Gestures { get { return m_gestures; } set { m_gestures = value; } }
        public MyEngine Engine { get { return m_engine; } set { m_engine = value; } }
        //public ImageList ImagesActions { get { return iL_actions; } set { iL_actions = value; } }
        //public ImageList ImagesGestures { get { return iL_gestures; } set { iL_gestures = value; } }
        public bool WndClosed { get { return m_wndClosed; } }

        bool m_viewGestures = false;

        delegate void EmptyHandler();


        #region ITranslation Members

        MyText MW_cMI_addGesture = new MyText("MW_cMI_addGesture"); //  "Add New Gesture to "
        MyText MW_cMI_copyGesture = new MyText("MW_cMI_copyGesture"); // "Copy {0} to"
        MyText MW_cMI_copyMultiple = new MyText("MW_cMI_copyMultiple"); // "Copy Selected Gestures to"

        MyText MW_msgCaption_error = new MyText("I_errorCaption");
        MyText MW_msgCaption_warning = new MyText("I_warningCaption");
        MyText MW_msgText_deleteGesture = new MyText("MW_msgText_deleteGesture"); //  "Do you really want to delete {0} ?"
        MyText MW_msgText_deleteMultiple = new MyText("MW_msgText_deleteMultiple"); //  "Do you really want to delete selected gestures?"
        MyText MW_msgText_errNotEmptyGroup = new MyText("MW_msgText_errNotEmptyGroup"); //  "You can delete only empty Group or with all it's items."
        MyText MW_msgText_errCantDelGlobal = new MyText("MW_msgText_errCantDelGlobal"); //  "You cannot delete the Global Group."
        

        public void Translate()
        {
            m_gestures.Translate();
            // Tool tips
            toolTip1.SetToolTip(cLV_matchedGestures, Translation.GetText("MW_tT_macthedGestures"));// "Drag items to re-order"
            toolTip1.SetToolTip(pB_displayGesture, Translation.GetText("MW_tT_displayGesture"));// "Click to animate the gesture"
            // Column headers
            cH_name.Text = Translation.GetText("MW_cH_name");
            cH_description.Text = Translation.GetText("MW_cH_description");
            cH_associatedActions.Text = Translation.GetText("MW_cH_associatedActions");
            cH_priority.Text = Translation.GetText("MW_cH_priority");
            // Tool strip buttons
            tSB_addGesture.Text = Translation.GetText("MW_tSB_addGesture");
            tSB_addGroup.Text = Translation.GetText("MW_tSB_addGroup");
            tSB_modify.Text = Translation.GetText("MW_tSB_modify");
            tSB_delete.Text = Translation.GetText("MW_tSB_delete"); 
            tSB_viewGestures.Text = Translation.GetText("MW_tSB_viewGestures");            
            tSB_viewActions.Text = Translation.GetText("MW_tSB_viewActions");
            tSB_options.Text = Translation.GetText("MW_tSB_options");
            tSB_about.Text = Translation.GetText("MW_tSB_about");
            // Context menu items
            cMI_activate.Text = Translation.GetText("MW_cMI_activate");
            cMI_deactivate.Text = Translation.GetText("MW_cMI_deactivate");
            MW_cMI_addGesture.Translate();
            MW_cMI_copyGesture.Translate();
            MW_cMI_copyMultiple.Translate();
            cMI_modify.Text = tSB_modify.Text;
            cMI_delete.Text = tSB_delete.Text;
            // Start menu items
            sMI_addGesture.Text = tSB_addGesture.Text + "...";
            sMI_addGroup.Text = tSB_addGroup.Text + "...";
            sMI_viewActions.Text = tSB_viewActions.Text;
            sMI_viewGestures.Text = tSB_viewGestures.Text;
            sMI_options.Text = tSB_options.Text + "...";
            sMI_about.Text = tSB_about.Text;
            sMI_exit.Text = Translation.GetText("MW_sMI_exit");
            // Messages
            MW_msgCaption_error.Translate();
            MW_msgCaption_warning.Translate();
            MW_msgText_deleteGesture.Translate();
            MW_msgText_deleteMultiple.Translate();
            MW_msgText_errNotEmptyGroup.Translate();
            MW_msgText_errCantDelGlobal.Translate();
            cLV_matchedGestures.Translate();
        }

        #endregion

        public Form_main()
        {
            InitializeComponent();            
            #region Compenents Initialization

            this.FormBorderStyle = FormBorderStyle.None;
            this.Width = this.BackgroundImage.Width;
            this.Height = this.BackgroundImage.Height;
            this.MaximumSize = this.MinimumSize = new Size(this.Width, this.Height);
            this.Region = Region.FromHrgn(Win32.CreateRoundRectRgn(0, 0, Width + 1, Height + 1, 23, 23));
            this.DoubleBuffered = true;

            toolStrip1.Renderer = new MyToolStripRenderer();
            startButton1.LoadContextMenu(cMS_startMenu);

            foreach (ToolStripItem item in toolStrip1.Items)
                item.Margin = new Padding(0, 1, 2, 2);
            tSB_modify.Enabled = false;
            tSB_delete.Enabled = false;
            pB_displayGesture.Image = new Bitmap(pB_displayGesture.Width, pB_displayGesture.Height);
            cLV_matchedGestures.ComboIndexChanged = ComboIndexChanged;//+= new GUI.ComboListView.DlgComboIndexChanged(ComboIndexChanged);            

            #endregion Compenents Initialization
            m_viewGestures = Config.User.LW_viewMode == 1;
            SetViewModeButton();
        }


        #region Window Actions

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0084 /*WM_NCHITTEST*/)
            {
                Rectangle r = new Rectangle(this.Location.X, this.Location.Y, this.Width, 24);
                if (r.Contains(Cursor.Position))
                {
                    m.Result = (IntPtr)2;	// HTCLIENT
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            if (pb == pB_minimize)
            {
                pB_minimize.Image = Resources.minimize_normal;
                m_pbMinMouseLeave = true;
            }
            else if (pb == pB_close)
            {
                pB_close.Image = Resources.close_normal;
                m_pbCloseMouseLeave = true;
            }
            
        }

        //private void pictureBox_MouseEnter(object sender, EventArgs e)
        //{
        //    PictureBox pb = (PictureBox)sender;
        //    if (pb == pB_minimize)
        //        pB_minimize.Image = Resources.minimize_hover;
        //    else if (pb == pB_close)
        //        pB_close.Image = Resources.close_hover;            
        //}

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {            
            PictureBox pb = (PictureBox)sender;
           if (pb == pB_minimize)
                pB_minimize.Image = Resources.minimize_pushed;
            else if (pb == pB_close)
                pB_close.Image = Resources.close_pushed;
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {            
            PictureBox pb = (PictureBox)sender;
            if (pb == pB_minimize)
                pB_minimize.Image = Resources.minimize_normal;
            else if (pb == pB_close)
                pB_close.Image = Resources.close_normal;
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {            
            PictureBox pb = (PictureBox)sender;
            if (pb == pB_minimize)
            {                
                this.WindowState = FormWindowState.Minimized;
                m_pbMinMouseLeave = true;
            }
            else if (pb == pB_close)
            {
                if (!Config.User.CloseToTray)
                    m_wndClosed = true;
                this.Close();
                m_pbCloseMouseLeave = true;
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            if (pb == pB_minimize)
            {
                if (m_pbMinMouseLeave)
                {
                    m_pbMinMouseLeave = false;                    
                    pB_minimize.Image = Resources.minimize_hover;
                }
            }
            else if (pb == pB_close)
            {
                if (m_pbCloseMouseLeave)
                {
                    m_pbCloseMouseLeave = false;                    
                    pB_close.Image = Resources.close_hover;
                }
            }
        }

        #endregion Window Actions

        public void ShowMainForm()
        {
            m_wndClosed = false;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.BringToFront();
            Win32.SetForegroundWindow(this.Handle);
            CheckButtonsState();
        }

        public void SetListView()
        {
            iL_actions.Images.Clear();
            iL_gestures.Images.Clear();
            iL_actions.Images.Add("alert", Properties.Resources.alert);
            iL_gestures.Images.Add("alert", Properties.Resources.alert);
            foreach (MyGesture gesture in m_gestures.GetAll())
            {
                gesture.SetActionIcon(iL_actions);
                gesture.SetGestureIcon(iL_gestures);
            }
            listView_GesturesActions.ILActions = iL_actions;
            listView_GesturesActions.ILGestures = iL_gestures;
            listView_GesturesActions.AddGesturesCollection(m_gestures);
            cLV_matchedGestures.SmallImageList = iL_actions;
        }

        private void Form_designed_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                if (Config.User.MinToTray)
                {
                    this.ShowInTaskbar = false;
                    this.Hide();
                    SaveSettings();
                    //notifyIcon1.Visible = true;
                }
            }
        }

        private void Form_designed_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_prevSelectedGesture != null)
                m_prevSelectedGesture.Activator.AbortAnimating();
            SaveSettings();
        }

        #region Toolstrip Button Clicks

        private void tSB_addGesture_Click(object sender, EventArgs e)
        {
            Form_addGesture addGesture = new Form_addGesture();
            addGesture.Gestures = new GesturesCollection(m_gestures.GetAll().ToArray());
            addGesture.ShowInTaskbar = false;
            addGesture.MyNNetwork = new MyNeuralNetwork(m_engine.Network);
            addGesture.MyNNetwork.LoadCurves();
            if (addGesture.ShowDialog() == DialogResult.OK)
            {
                listView_GesturesActions.BeginUpdate();
                foreach (MyGesture gesture in addGesture.NewGestures)
                {
                    gesture.SetActionIcon(iL_actions);                    
                    gesture.SetGestureIcon(iL_gestures);
                    gesture.SetActionIcon(m_engine.ImgListActions);
                    int index = m_gestures.AutoInsert(new MyGesture(gesture));
                    listView_GesturesActions.ResizeColumn = true; //will resize columns at the end
                    // do not insert it because it should be added into collapsed group
                    if (index != -1)
                        listView_GesturesActions.Items.Insert(index, m_gestures[index]);
                }
                listView_GesturesActions.EndUpdate();
                listView_GesturesActions.Refresh(); //will resize columns at the end               
                m_engine.Network = addGesture.MyNNetwork;
                CheckButtonsState();
                SaveSettings();
                RedrawGesture();
            }
        }

        private void tSB_addApp_Click(object sender, EventArgs e)
        {
            Form_addGesture addGesture = new Form_addGesture();
            addGesture.Gestures = new GesturesCollection(m_gestures.GetAll().ToArray());
            addGesture.ShowInTaskbar = false;
            addGesture.AppMode = true;
            if (addGesture.ShowDialog() == DialogResult.OK)
            {
                listView_GesturesActions.BeginUpdate();
                foreach (MyGesture gesture in addGesture.NewGestures)
                {
                    gesture.SetActionIcon(iL_actions);                    
                    gesture.SetGestureIcon(iL_gestures);
                    gesture.SetActionIcon(m_engine.ImgListActions);
                    int index = m_gestures.AutoInsert(new MyGesture(gesture));
                    listView_GesturesActions.ResizeColumn = true; //will resize columns at the end
                    listView_GesturesActions.Items.Insert(index, m_gestures[index]);
                }
                listView_GesturesActions.EndUpdate();
                listView_GesturesActions.Refresh(); //will resize columns at the end                
                SaveSettings();
                RedrawGesture();
            }
        }

        private void tSB_modify_Click(object sender, EventArgs e)
        {
            if (listView_GesturesActions.SelectedItems.Count == 0
                || !PossibleToModify()) return;
            GesturesCollection modify = new GesturesCollection();
            GesturesCollection gestures = new GesturesCollection(m_gestures.GetAll().ToArray());
            List<string> curvesToDelete = new List<string>();
            for (int i = listView_GesturesActions.SelectedItems.Count; i > 0; i--)
            {
                string itemId = listView_GesturesActions.SelectedItems[i - 1].Name;
                modify.Add(new MyGesture(gestures[itemId]));

                bool classicCurve = gestures[itemId].Activator.Type == BaseActivator.Types.ClassicCurve;
                string curve = gestures.Remove(gestures[itemId]);
                if (curve != string.Empty && classicCurve)
                    curvesToDelete.Add(curve);
            }
            modify.Reverse();
            //GesturesCollection gestures = new GesturesCollection(m_gestures.ToArray());
            //gestures.RemoveAt(index);
            Form_modifyGesture modifyGesture = new Form_modifyGesture();
            modifyGesture.ModifiedGestures = modify.ToArray();
            modifyGesture.Gestures = gestures;
            modifyGesture.ShowInTaskbar = false;
            modifyGesture.MyNNetwork = new MyNeuralNetwork(m_engine.Network);
            modifyGesture.MyNNetworkOriginal = new MyNeuralNetwork(m_engine.Network);
            modifyGesture.MyNNetwork.LoadCurves();
            modifyGesture.MyNNetwork.UnlearnCurves(curvesToDelete.ToArray(), true);
            if (modify.Count == 1 && modify[0].IsGroup)
            {                
                modifyGesture.AppMode = true;
            }

            if (modifyGesture.ShowDialog() == DialogResult.OK)
            {
                m_engine.Network = modifyGesture.MyNNetwork;
                CheckButtonsState();
                listView_GesturesActions.BeginUpdate();
                foreach (MyGesture gesture in modifyGesture.ModifiedGestures)
                {
                    int index = m_gestures[gesture.ID].Index;
                    m_gestures[index] = new MyGesture(gesture);
                    m_gestures[index].SetActionIcon(iL_actions);                    
                    m_gestures[index].SetGestureIcon(iL_gestures);
                    m_gestures[index].SetActionIcon(m_engine.ImgListActions);
                    if (m_gestures.MatchedGestures(m_gestures[index].Activator.ID).Count == 1)
                        m_gestures[index].SetGestureIcon(iL_gestures);

                    listView_GesturesActions.Items[index] = m_gestures[index];
                }
                listView_GesturesActions.EndUpdate();
                listView_GesturesActions.Refresh(); //will resize column
                listView_GesturesActions.Update();
                SaveSettings();
                RedrawGesture();
            }
            else { } // DialogResult.Cancel
        }

        private void tSB_delete_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items;
            items = listView_GesturesActions.SelectedItems;
            string msg;
            if (items.Count == 0) return;
            else if (items.Count == 1)
                msg = String.Format(MW_msgText_deleteGesture, m_gestures[items[0].Name].Caption);
            else
                msg = MW_msgText_deleteMultiple;


            if (MessageBox.Show(this, msg, MW_msgCaption_warning, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                List<string> curvesToDelete = new List<string>();
                listView_GesturesActions.BeginUpdate();

                for (int i = listView_GesturesActions.SelectedItems.Count; i > 0; i--)
                {
                    string itemId = listView_GesturesActions.SelectedItems[i - 1].Name;
                    if (m_gestures[itemId].ID != TypeOfAction.AppGroupOptions.APP_GROUP_GLOBAL)
                    {
                        if (m_gestures[itemId].IsGroup && !m_gestures.IsEmptyGroup(m_gestures[itemId]))
                            MessageBox.Show(this, MW_msgText_errNotEmptyGroup, MW_msgCaption_error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {

                            iL_actions.Images.RemoveByKey(m_gestures[itemId].ID);
                            iL_gestures.Images.RemoveByKey(m_gestures[itemId].ID);
                            m_engine.ImgListActions.Images.RemoveByKey(m_gestures[itemId].ID);

                            listView_GesturesActions.ResizeColumn = true; //will resize column
                            listView_GesturesActions.Items.RemoveAt(m_gestures[itemId].Index);
                            bool classicCurve = m_gestures[itemId].Activator.Type == BaseActivator.Types.ClassicCurve;
                            string curve = m_gestures.Remove(m_gestures[itemId]);
                            if (curve != string.Empty && classicCurve)
                                curvesToDelete.Add(curve);
                           
                        }
                    }
                    else
                        MessageBox.Show(this, MW_msgText_errCantDelGlobal, MW_msgCaption_error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                listView_GesturesActions.EndUpdate();
                listView_GesturesActions.Refresh(); //will resize column
                m_engine.Network.LoadCurves();
                m_engine.Network.UnlearnCurves(curvesToDelete.ToArray(), false);
                CheckButtonsState();
                listView_GesturesActions.Update();
                SaveSettings();
                RedrawGesture();
            }
        }

        private void SetViewModeButton()
        {
            tSB_viewActions.Visible = sMI_viewActions.Visible = m_viewGestures;
            tSB_viewGestures.Visible = sMI_viewGestures.Visible = !m_viewGestures;
        }

        private void tSB_view_Click(object sender, EventArgs e)
        {            
            m_viewGestures = !m_viewGestures;
            SetViewModeButton();
            int mode = (int)listView_GesturesActions.ViewMode;
            mode = (mode + 1) % 2;
            listView_GesturesActions.ViewMode = (GesturesListView.ViewModes)mode;
            Config.User.LW_viewMode = mode;
            Config.User.Save();
        }

        private void tSB_options_Click(object sender, EventArgs e)
        {
            formOptions = new Form_options();
            formOptions.ShowInTaskbar = false;
            formOptions.ApplySettings += new Form_options.DlgApplySettings(ApplySettings);
            formOptions.ShowDialog();
        }

        private void tSB_about_Click(object sender, EventArgs e)
        {
            if (!Form_about.Instance.Visible)
                Form_about.Instance.Show();
            else
            {
                Form_about.Instance.Invalidate();
                Form_about.Instance.Activate();
            }
        }

        private void tSMI_exit_Click(object sender, EventArgs e)
        {
            m_wndClosed = true;
            this.Close();
        }

        #endregion Toolstrip Button Clicks



        private void NetworkLearnt(double error, int epochs)
        {
            SetButtonsState();
        }
        
        private void CheckButtonsState()
        {
            SetButtonsState();
            //m_engine.Network.NetworkLearnt = NetworkLearnt;
            //m_engine.Network.NetworkStartTraining = SetButtonsState;
        }

        private void SetButtonsState()
        {
            EmptyHandler del = delegate()
            {
                lock (this)
                {
                    bool selectedItem = listView_GesturesActions.SelectedItems.Count != 0;
                    bool enabled = true;//!m_engine.Network.IsTraining;

                    cMI_addNew.Enabled = enabled; //context menu
                    cMI_modify.Enabled = enabled && selectedItem; //context menu                
                    sMI_addGesture.Enabled = enabled; //start menu                
                    sMI_addGroup.Enabled = enabled; //start menu
                    tSB_addGesture.Enabled = enabled; //toolstrip
                    tSB_addGroup.Enabled = enabled; //toolstrip
                    tSB_modify.Enabled = enabled && selectedItem; //toolstrip
                }
            };
            if (this.InvokeRequired)
                this.Invoke(del);
            else
                del();
        }

        private void ApplySettings()
        {
            listView_GesturesActions.Refresh();
            m_engine.FinalList = formOptions.FinalList;
            m_engine.ApplySettings();
        }

        private void SaveSettings()
        {
            FileOptions.SaveGestures(m_gestures);
            m_engine.Network.SaveNeuralNetwork();
        }


        private void cLV_matchedGestures_DragDrop(object sender, DragEventArgs e)
        {
            foreach (MyGesture gesture in listView_GesturesActions.MatchedGestures)
            {
                int index = 0;
                foreach (ListViewItem item in cLV_matchedGestures.Items)
                    if (item.Name == gesture.ID)
                    {
                        index = item.Index;
                        break;
                    }
                gesture.ItemPos = index;
            }
            m_gestures.SortMatchedGestures(listView_GesturesActions.SelectedItemCurve);
            SaveSettings();            
        }


        void ComboIndexChanged(ListViewItem item, ExecuteType comboValue)
        {
            m_gestures[item.Name].ExecutionType = comboValue;
            SaveSettings();
        }


        private void RedrawGesture()
        {
            List<MyGesture> gests = m_gestures.MatchedGestures(listView_GesturesActions.SelectedItemCurve);
            if (gests.Count == 0)
            {
                if (m_prevSelectedGesture != null)
                    m_prevSelectedGesture.Activator.AbortAnimating();
                Graphics gp = Graphics.FromImage(pB_displayGesture.Image);
                gp.FillRectangle(new SolidBrush(pB_displayGesture.BackColor), 0, 0, pB_displayGesture.Width, pB_displayGesture.Height);
                pB_displayGesture.Invalidate();
                gp.Dispose();
                m_prevSelectedGesture = null;
            }
            else
            {
                if (m_prevSelectedGesture != gests[0])
                {
                    if (m_prevSelectedGesture != null)
                        m_prevSelectedGesture.Activator.AbortAnimating();
                    m_prevSelectedGesture = gests[0];
                    m_prevSelectedGesture.Activator.DrawToPictureBox(pB_displayGesture);
                }
                else
                {
                    if (m_animateGesture)
                    {
                        m_prevSelectedGesture = gests[0];
                        m_prevSelectedGesture.Activator.AnimateToPictureBox(pB_displayGesture);
                    }
                }
            }            
            cLV_matchedGestures.SetMatchedItems(listView_GesturesActions.MatchedGestures);
            
            List<MyGesture> selectedGestures = new List<MyGesture>();
            foreach (ListViewItem item in listView_GesturesActions.SelectedItems)
                selectedGestures.Add(m_gestures[item.Name]);
            cLV_matchedGestures.DrawFocusRectangleAround(selectedGestures);
        }

        private void listView_GesturesActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            tSB_modify.Enabled = PossibleToModify() && !m_engine.Network.IsTraining;
            tSB_delete.Enabled = PossibleToDelete();
            RedrawGesture();
        }     

        private void pB_showGesture_MouseClick(object sender, MouseEventArgs e)
        {
            m_animateGesture = true;
            RedrawGesture();
            m_animateGesture = false;
        }

        private void listView_GesturesActions_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            m_gestures[e.Item.Name].Active = e.Item.Checked;
            foreach (ListViewItem item in cLV_matchedGestures.Items)
            {
                item.Checked = m_gestures[item.Name].Active && m_gestures[item.Name].AppGroup.Active;
            }
        }

        private bool PossibleToDelete()
        {
            ListView.SelectedListViewItemCollection items = listView_GesturesActions.SelectedItems;
            if (items.Count == 0)
                return false;
            else if (items.Count == 1 && m_gestures[items[0].Name].ID == MyGesture.GlobalGroup.ID)
                return false;
            else
                return true;
        }

        private bool PossibleToModify()
        {
            ListView.SelectedListViewItemCollection items = listView_GesturesActions.SelectedItems;
            if (items.Count == 0)
                return false;
            else if (items.Count == 1 && m_gestures[items[0].Name].ID == MyGesture.GlobalGroup.ID)
                return false;
            else if (items.Count > 1)
            {
                MouseActivator.Types firstType, secondType;
                firstType = m_gestures[items[0].Name].Activator.Type;
                if (firstType == BaseActivator.Types.ClassicCurve)
                    secondType = BaseActivator.Types.DoubleButton;
                else if (firstType == BaseActivator.Types.DoubleButton)
                    secondType = BaseActivator.Types.ClassicCurve;
                else
                    secondType = firstType;
                foreach (ListViewItem item in items)
                {
                    if (m_gestures[item.Name].IsGroup)
                        return false;
                    else if (firstType != m_gestures[item.Name].Activator.Type && secondType != m_gestures[item.Name].Activator.Type)
                        return false;
                }
            }
            return true;
        }

        private void listView_GesturesActions_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            System.Diagnostics.Debug.WriteLine("OPENING cMS_gestureOptions");
            ListView.SelectedListViewItemCollection items = listView_GesturesActions.SelectedItems;
            cMI_addNew.Visible = false;
            cMI_activate.Visible = true;
            cMI_deactivate.Visible = true;
            cMI_modify.Visible = true;
            cMI_delete.Visible = true;
            m_copyGestures = false;
            switch (items.Count)
            {
                case 0:
                    cMI_activate.Visible = false;
                    cMI_deactivate.Visible = false;
                    return;
                    //e.Cancel = true;
                    break;
                case 1:
                    string id = items[0].Name;
                    if (m_gestures[id].Checked)
                        cMI_activate.Visible = false;
                    else
                        cMI_deactivate.Visible = false;

                    cMI_addNew.Visible = true;
                    if (m_gestures[id].IsGroup)
                    {
                        cMI_addNew.Text = MW_cMI_addGesture; // + m_gestures[index].Caption;
                        cMI_addNew.DropDown = GetGroupsDropDown();
                        //m_selectedGroup = new List<MyGesture>() { m_gestures[index] };
                    }
                    else
                    {
                        m_copyGestures = true;
                        cMI_addNew.Text = string.Format(MW_cMI_copyGesture, m_gestures[id].Caption);
                        cMI_addNew.DropDown = GetGroupsDropDown();
                    }
                    break;
                default:
                    bool containGroup = false;
                    foreach (ListViewItem item in items)
                        if (m_gestures[item.Name].IsGroup) containGroup = true;
                    if (!containGroup)
                    {
                        cMI_addNew.Visible = true;
                        m_copyGestures = true;
                        cMI_addNew.Text = MW_cMI_copyMultiple;
                        cMI_addNew.DropDown = GetGroupsDropDown();
                    }
                    break;
            }
            cMI_modify.Visible = PossibleToModify();
            cMI_modify.Enabled = !m_engine.Network.IsTraining;
            cMI_delete.Visible = PossibleToDelete();
            cMS_gestureOptions.Show(Cursor.Position);
        }

        private ToolStripDropDown GetGroupsDropDown()
        {
            ToolStripMenuItem[] groups = new ToolStripMenuItem[m_gestures.Groups.Count];
            for (int i = 0; i < m_gestures.Groups.Count; i++)
            {
                groups[i] = new ToolStripMenuItem(m_gestures.Groups[i].Caption);
                groups[i].Image = iL_actions.Images[m_gestures.Groups[i].ID];
                groups[i].Name = m_gestures.Groups[i].ID;
                groups[i].Click += new EventHandler(tSMI_groupItem_Click);
            }

            ToolStripDropDown dropDown = new ContextMenuStrip();
            ((ContextMenuStrip)dropDown).ImageScalingSize = new Size(20, 20);
            ((ContextMenuStrip)dropDown).ShowImageMargin = true;
            dropDown.Items.AddRange(groups);
            return dropDown;
        }

        void tSMI_groupItem_Click(object sender, EventArgs e)
        {
            string groupKey = ((ToolStripMenuItem)sender).Name;
            if (m_copyGestures)
            {
                ListView.SelectedListViewItemCollection items = listView_GesturesActions.SelectedItems;
                for (int i = 0; i < items.Count; i++)
                {
                    string itemId = items[i].Name;
                    string id = MyGesture.CreateUniqueId(m_gestures[itemId], m_gestures);
                    //MyGesture gest = new MyGesture(id);
                    //gest.SetItem(m_gestures[pos]);
                    //gest.Activator = m_gestures[pos].Activator;
                    //gest.Action = m_gestures[pos].Action;
                    //gest.AppGroup = m_gestures[groupKey];
                    MyGesture gest = new MyGesture(id, m_gestures[itemId], m_gestures[groupKey]);
                    gest.SetActionIcon(iL_actions);
                    gest.SetActionIcon(m_engine.ImgListActions);
                    gest.SetGestureIcon(iL_gestures);

                    int index = m_gestures.AutoInsert(new MyGesture(gest));
                    listView_GesturesActions.ResizeColumn = true; //will resize columns at the end
                    listView_GesturesActions.Items.Insert(index, m_gestures[index]);
                }
                listView_GesturesActions.Refresh(); //will resize columns at the end    
                listView_GesturesActions.Update();
                //listView_GesturesActions.ReselectItems();
                SaveSettings();
                RedrawGesture();
            }
            else //add new gestures
            {
                Form_addGesture addGesture = new Form_addGesture();
                addGesture.SelectedGroups = new List<MyGesture>() { m_gestures[groupKey] };
                addGesture.Gestures = new GesturesCollection(m_gestures.GetAll().ToArray());
                addGesture.ShowInTaskbar = false;
                addGesture.MyNNetwork = new MyNeuralNetwork(m_engine.Network);
                addGesture.MyNNetwork.LoadCurves();
                if (addGesture.ShowDialog() == DialogResult.OK)
                {
                    listView_GesturesActions.BeginUpdate();
                    foreach (MyGesture gesture in addGesture.NewGestures)
                    {
                        gesture.SetActionIcon(iL_actions);
                        gesture.SetActionIcon(m_engine.ImgListActions);
                        gesture.SetGestureIcon(iL_gestures);
                        int index = m_gestures.AutoInsert(new MyGesture(gesture));
                        listView_GesturesActions.ResizeColumn = true; //will resize columns at the end
                        // do not insert it because it should be added into collapsed group
                        if (index != -1)
                            listView_GesturesActions.Items.Insert(index, m_gestures[index]);
                    }
                    listView_GesturesActions.EndUpdate();
                    listView_GesturesActions.Refresh(); //will resize columns at the end               
                    m_engine.Network = addGesture.MyNNetwork;
                    CheckButtonsState();
                    SaveSettings();
                    RedrawGesture();
                }
            }

        }

        private void tSMI_activate_Click(object sender, EventArgs e)
        {
            ChangeItemsState(true);
        }

        private void tSMI_deactivate_Click(object sender, EventArgs e)
        {
            ChangeItemsState(false);
        }

        private void ChangeItemsState(bool active)
        {
            ListView.SelectedListViewItemCollection items;
            items = listView_GesturesActions.SelectedItems;
            for (int i = 0; i < items.Count; i++)
            {
                string itemId = items[i].Name;
                MyGesture activeGesture = m_gestures[itemId];
                activeGesture.Active = active;
                activeGesture.Checked = active;
                listView_GesturesActions.Items[itemId].Checked = active;
            }
        }
    }
}
