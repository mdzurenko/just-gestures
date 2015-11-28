namespace JustGestures.GUI
{
    partial class Form_main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_main));
            this.pB_close = new System.Windows.Forms.PictureBox();
            this.pB_minimize = new System.Windows.Forms.PictureBox();
            this.pB_displayGesture = new System.Windows.Forms.PictureBox();
            this.iL_gestures = new System.Windows.Forms.ImageList(this.components);
            this.iL_actions = new System.Windows.Forms.ImageList(this.components);
            this.cMS_gestureOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cMI_activate = new System.Windows.Forms.ToolStripMenuItem();
            this.cMI_deactivate = new System.Windows.Forms.ToolStripMenuItem();
            this.cMI_addNew = new System.Windows.Forms.ToolStripMenuItem();
            this.cMI_modify = new System.Windows.Forms.ToolStripMenuItem();
            this.cMI_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_gesturesCollection = new System.Windows.Forms.Panel();
            this.listView_GesturesActions = new JustGestures.GUI.GesturesListView(this.components);
            this.cH_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cH_description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel_matchedGestures = new System.Windows.Forms.Panel();
            this.cLV_matchedGestures = new JustGestures.GUI.ComboListView();
            this.cH_associatedActions = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cH_priority = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel_visualization = new System.Windows.Forms.Panel();
            this.cMS_startMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sMI_addGesture = new System.Windows.Forms.ToolStripMenuItem();
            this.sMI_addGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.sMI_viewGestures = new System.Windows.Forms.ToolStripMenuItem();
            this.sMI_viewActions = new System.Windows.Forms.ToolStripMenuItem();
            this.sMI_options = new System.Windows.Forms.ToolStripMenuItem();
            this.sMI_about = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.sMI_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tSB_addGesture = new System.Windows.Forms.ToolStripButton();
            this.tSB_addGroup = new System.Windows.Forms.ToolStripButton();
            this.tSB_modify = new System.Windows.Forms.ToolStripButton();
            this.tSB_delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tSB_viewGestures = new System.Windows.Forms.ToolStripButton();
            this.tSB_viewActions = new System.Windows.Forms.ToolStripButton();
            this.tSB_options = new System.Windows.Forms.ToolStripButton();
            this.tSB_about = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.startButton1 = new JustGestures.GUI.StartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pB_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB_minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB_displayGesture)).BeginInit();
            this.cMS_gestureOptions.SuspendLayout();
            this.panel_gesturesCollection.SuspendLayout();
            this.panel_matchedGestures.SuspendLayout();
            this.panel_visualization.SuspendLayout();
            this.cMS_startMenu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // pB_close
            // 
            this.pB_close.BackColor = System.Drawing.Color.Transparent;
            this.pB_close.BackgroundImage = global::JustGestures.Properties.Resources.close_normal;
            this.pB_close.Location = new System.Drawing.Point(675, 2);
            this.pB_close.Name = "pB_close";
            this.pB_close.Size = new System.Drawing.Size(40, 15);
            this.pB_close.TabIndex = 0;
            this.pB_close.TabStop = false;
            this.pB_close.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseClick);
            this.pB_close.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pB_close.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pB_close.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pB_close.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pB_minimize
            // 
            this.pB_minimize.BackColor = System.Drawing.Color.Transparent;
            this.pB_minimize.BackgroundImage = global::JustGestures.Properties.Resources.minimize_normal;
            this.pB_minimize.Location = new System.Drawing.Point(643, 2);
            this.pB_minimize.Name = "pB_minimize";
            this.pB_minimize.Size = new System.Drawing.Size(30, 15);
            this.pB_minimize.TabIndex = 1;
            this.pB_minimize.TabStop = false;
            this.pB_minimize.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseClick);
            this.pB_minimize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pB_minimize.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pB_minimize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pB_minimize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pB_displayGesture
            // 
            this.pB_displayGesture.BackColor = System.Drawing.Color.White;
            this.pB_displayGesture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pB_displayGesture.Location = new System.Drawing.Point(0, 0);
            this.pB_displayGesture.Name = "pB_displayGesture";
            this.pB_displayGesture.Size = new System.Drawing.Size(239, 238);
            this.pB_displayGesture.TabIndex = 11;
            this.pB_displayGesture.TabStop = false;
            this.pB_displayGesture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pB_showGesture_MouseClick);
            // 
            // iL_gestures
            // 
            this.iL_gestures.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.iL_gestures.ImageSize = new System.Drawing.Size(24, 24);
            this.iL_gestures.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // iL_actions
            // 
            this.iL_actions.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.iL_actions.ImageSize = new System.Drawing.Size(20, 20);
            this.iL_actions.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // cMS_gestureOptions
            // 
            this.cMS_gestureOptions.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cMS_gestureOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMI_activate,
            this.cMI_deactivate,
            this.cMI_addNew,
            this.cMI_modify,
            this.cMI_delete});
            this.cMS_gestureOptions.Name = "cMS_gestureOptions";
            this.cMS_gestureOptions.Size = new System.Drawing.Size(171, 134);
            // 
            // cMI_activate
            // 
            this.cMI_activate.Image = global::JustGestures.Properties.Resources.activate;
            this.cMI_activate.Name = "cMI_activate";
            this.cMI_activate.Size = new System.Drawing.Size(170, 26);
            this.cMI_activate.Tag = "activation";
            this.cMI_activate.Text = "Activate";
            this.cMI_activate.Click += new System.EventHandler(this.tSMI_activate_Click);
            // 
            // cMI_deactivate
            // 
            this.cMI_deactivate.Image = global::JustGestures.Properties.Resources.deactivate;
            this.cMI_deactivate.Name = "cMI_deactivate";
            this.cMI_deactivate.Size = new System.Drawing.Size(170, 26);
            this.cMI_deactivate.Text = "Deactivate";
            this.cMI_deactivate.Click += new System.EventHandler(this.tSMI_deactivate_Click);
            // 
            // cMI_addNew
            // 
            this.cMI_addNew.Image = global::JustGestures.Properties.Resources.add_gesture;
            this.cMI_addNew.Name = "cMI_addNew";
            this.cMI_addNew.Size = new System.Drawing.Size(170, 26);
            this.cMI_addNew.Text = "Add New Gesture";
            this.cMI_addNew.Visible = false;
            // 
            // cMI_modify
            // 
            this.cMI_modify.Image = global::JustGestures.Properties.Resources.modify;
            this.cMI_modify.Name = "cMI_modify";
            this.cMI_modify.Size = new System.Drawing.Size(170, 26);
            this.cMI_modify.Tag = "modify";
            this.cMI_modify.Text = "Modify";
            this.cMI_modify.Click += new System.EventHandler(this.tSB_modify_Click);
            // 
            // cMI_delete
            // 
            this.cMI_delete.Image = global::JustGestures.Properties.Resources.delete;
            this.cMI_delete.Name = "cMI_delete";
            this.cMI_delete.Size = new System.Drawing.Size(170, 26);
            this.cMI_delete.Text = "Delete";
            this.cMI_delete.Click += new System.EventHandler(this.tSB_delete_Click);
            // 
            // panel_gesturesCollection
            // 
            this.panel_gesturesCollection.Controls.Add(this.listView_GesturesActions);
            this.panel_gesturesCollection.Location = new System.Drawing.Point(12, 65);
            this.panel_gesturesCollection.Name = "panel_gesturesCollection";
            this.panel_gesturesCollection.Size = new System.Drawing.Size(447, 523);
            this.panel_gesturesCollection.TabIndex = 14;
            // 
            // listView_GesturesActions
            // 
            this.listView_GesturesActions.BackColor = System.Drawing.Color.White;
            this.listView_GesturesActions.CheckBoxes = true;
            this.listView_GesturesActions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cH_name,
            this.cH_description});
            this.listView_GesturesActions.Cursor = System.Windows.Forms.Cursors.Default;
            this.listView_GesturesActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_GesturesActions.FullRowSelect = true;
            this.listView_GesturesActions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_GesturesActions.Location = new System.Drawing.Point(0, 0);
            this.listView_GesturesActions.Name = "listView_GesturesActions";
            this.listView_GesturesActions.OwnerDraw = true;
            this.listView_GesturesActions.Size = new System.Drawing.Size(447, 523);
            this.listView_GesturesActions.TabIndex = 9;
            this.listView_GesturesActions.UseCompatibleStateImageBehavior = false;
            this.listView_GesturesActions.View = System.Windows.Forms.View.Details;
            this.listView_GesturesActions.ViewMode = JustGestures.GUI.GesturesListView.ViewModes.Action;
            this.listView_GesturesActions.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView_GesturesActions_ItemChecked);
            this.listView_GesturesActions.SelectedIndexChanged += new System.EventHandler(this.listView_GesturesActions_SelectedIndexChanged);
            this.listView_GesturesActions.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView_GesturesActions_MouseClick);            
            // 
            // cH_name
            // 
            this.cH_name.Text = "Gesture";
            this.cH_name.Width = 211;
            // 
            // cH_description
            // 
            this.cH_description.Text = "Action";
            this.cH_description.Width = 225;
            // 
            // panel_matchedGestures
            // 
            this.panel_matchedGestures.Controls.Add(this.cLV_matchedGestures);
            this.panel_matchedGestures.Location = new System.Drawing.Point(479, 65);
            this.panel_matchedGestures.Name = "panel_matchedGestures";
            this.panel_matchedGestures.Size = new System.Drawing.Size(239, 244);
            this.panel_matchedGestures.TabIndex = 15;
            // 
            // cLV_matchedGestures
            // 
            this.cLV_matchedGestures.AllowDrop = true;
            this.cLV_matchedGestures.AllowReorder = true;
            this.cLV_matchedGestures.BackColor = System.Drawing.Color.White;
            this.cLV_matchedGestures.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cH_associatedActions,
            this.cH_priority});
            this.cLV_matchedGestures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cLV_matchedGestures.FullRowSelect = true;
            this.cLV_matchedGestures.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.cLV_matchedGestures.LineColor = System.Drawing.Color.Blue;
            this.cLV_matchedGestures.Location = new System.Drawing.Point(0, 0);
            this.cLV_matchedGestures.Name = "cLV_matchedGestures";
            this.cLV_matchedGestures.OwnerDraw = true;
            this.cLV_matchedGestures.Size = new System.Drawing.Size(239, 244);
            this.cLV_matchedGestures.TabIndex = 10;
            this.cLV_matchedGestures.UseCompatibleStateImageBehavior = false;
            this.cLV_matchedGestures.View = System.Windows.Forms.View.Details;
            this.cLV_matchedGestures.DragDrop += new System.Windows.Forms.DragEventHandler(this.cLV_matchedGestures_DragDrop);
            // 
            // cH_associatedActions
            // 
            this.cH_associatedActions.Text = "Matched Gestures";
            this.cH_associatedActions.Width = 150;
            // 
            // cH_priority
            // 
            this.cH_priority.Text = "Priority";
            this.cH_priority.Width = 84;
            // 
            // panel_visualization
            // 
            this.panel_visualization.Controls.Add(this.pB_displayGesture);
            this.panel_visualization.Location = new System.Drawing.Point(479, 350);
            this.panel_visualization.Name = "panel_visualization";
            this.panel_visualization.Size = new System.Drawing.Size(239, 238);
            this.panel_visualization.TabIndex = 16;
            // 
            // cMS_startMenu
            // 
            this.cMS_startMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cMS_startMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sMI_addGesture,
            this.sMI_addGroup,
            this.toolStripSeparator2,
            this.sMI_viewGestures,
            this.sMI_viewActions,
            this.sMI_options,
            this.sMI_about,
            this.toolStripSeparator3,
            this.sMI_exit});
            this.cMS_startMenu.Name = "cMS_startMenu";
            this.cMS_startMenu.Size = new System.Drawing.Size(210, 220);
            this.cMS_startMenu.Text = "Start Menu";
            // 
            // sMI_addGesture
            // 
            this.sMI_addGesture.Image = global::JustGestures.Properties.Resources.add_gesture;
            this.sMI_addGesture.Name = "sMI_addGesture";
            this.sMI_addGesture.Size = new System.Drawing.Size(209, 26);
            this.sMI_addGesture.Text = "Add New Gesture...";
            this.sMI_addGesture.Click += new System.EventHandler(this.tSB_addGesture_Click);
            // 
            // sMI_addGroup
            // 
            this.sMI_addGroup.Image = global::JustGestures.Properties.Resources.add_application;
            this.sMI_addGroup.Name = "sMI_addGroup";
            this.sMI_addGroup.Size = new System.Drawing.Size(209, 26);
            this.sMI_addGroup.Text = "Add Application Group...";
            this.sMI_addGroup.Click += new System.EventHandler(this.tSB_addApp_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(206, 6);
            // 
            // sMI_viewGestures
            // 
            this.sMI_viewGestures.Image = global::JustGestures.Properties.Resources.view_gestures;
            this.sMI_viewGestures.Name = "sMI_viewGestures";
            this.sMI_viewGestures.Size = new System.Drawing.Size(209, 26);
            this.sMI_viewGestures.Text = "View Gestures";
            this.sMI_viewGestures.Click += new System.EventHandler(this.tSB_view_Click);
            // 
            // sMI_viewActions
            // 
            this.sMI_viewActions.Image = global::JustGestures.Properties.Resources.view_actions;
            this.sMI_viewActions.Name = "sMI_viewActions";
            this.sMI_viewActions.Size = new System.Drawing.Size(209, 26);
            this.sMI_viewActions.Text = "View Actions";
            this.sMI_viewActions.Click += new System.EventHandler(this.tSB_view_Click);
            // 
            // sMI_options
            // 
            this.sMI_options.Image = global::JustGestures.Properties.Resources.options;
            this.sMI_options.Name = "sMI_options";
            this.sMI_options.Size = new System.Drawing.Size(209, 26);
            this.sMI_options.Text = "Options...";
            this.sMI_options.Click += new System.EventHandler(this.tSB_options_Click);
            // 
            // sMI_about
            // 
            this.sMI_about.Image = global::JustGestures.Properties.Resources.about;
            this.sMI_about.Name = "sMI_about";
            this.sMI_about.Size = new System.Drawing.Size(209, 26);
            this.sMI_about.Text = "About";
            this.sMI_about.Click += new System.EventHandler(this.tSB_about_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(206, 6);
            // 
            // sMI_exit
            // 
            this.sMI_exit.Image = global::JustGestures.Properties.Resources.turnoff;
            this.sMI_exit.Name = "sMI_exit";
            this.sMI_exit.Size = new System.Drawing.Size(209, 26);
            this.sMI_exit.Text = "Exit";
            this.sMI_exit.Click += new System.EventHandler(this.tSMI_exit_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSB_addGesture,
            this.tSB_addGroup,
            this.tSB_modify,
            this.tSB_delete,
            this.toolStripSeparator1,
            this.tSB_viewGestures,
            this.tSB_viewActions,
            this.tSB_options,
            this.tSB_about});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(74, 26);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(215, 29);
            this.toolStrip1.TabIndex = 17;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tSB_addGesture
            // 
            this.tSB_addGesture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSB_addGesture.Image = global::JustGestures.Properties.Resources.add_gesture;
            this.tSB_addGesture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSB_addGesture.Name = "tSB_addGesture";
            this.tSB_addGesture.Size = new System.Drawing.Size(26, 26);
            this.tSB_addGesture.Text = "Add New Gesture";
            this.tSB_addGesture.Click += new System.EventHandler(this.tSB_addGesture_Click);
            // 
            // tSB_addGroup
            // 
            this.tSB_addGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSB_addGroup.Image = global::JustGestures.Properties.Resources.add_application;
            this.tSB_addGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSB_addGroup.Name = "tSB_addGroup";
            this.tSB_addGroup.Size = new System.Drawing.Size(26, 26);
            this.tSB_addGroup.Text = "Add Application Group";
            this.tSB_addGroup.Click += new System.EventHandler(this.tSB_addApp_Click);
            // 
            // tSB_modify
            // 
            this.tSB_modify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSB_modify.Image = global::JustGestures.Properties.Resources.modify;
            this.tSB_modify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSB_modify.Name = "tSB_modify";
            this.tSB_modify.Size = new System.Drawing.Size(26, 26);
            this.tSB_modify.Text = "Modify";
            this.tSB_modify.Click += new System.EventHandler(this.tSB_modify_Click);
            // 
            // tSB_delete
            // 
            this.tSB_delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSB_delete.Image = global::JustGestures.Properties.Resources.delete;
            this.tSB_delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSB_delete.Name = "tSB_delete";
            this.tSB_delete.Size = new System.Drawing.Size(26, 26);
            this.tSB_delete.Text = "Delete";
            this.tSB_delete.Click += new System.EventHandler(this.tSB_delete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // tSB_viewGestures
            // 
            this.tSB_viewGestures.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSB_viewGestures.Image = global::JustGestures.Properties.Resources.view_gestures;
            this.tSB_viewGestures.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSB_viewGestures.Name = "tSB_viewGestures";
            this.tSB_viewGestures.Size = new System.Drawing.Size(26, 26);
            this.tSB_viewGestures.Text = "View Gestures";
            this.tSB_viewGestures.Click += new System.EventHandler(this.tSB_view_Click);
            // 
            // tSB_viewActions
            // 
            this.tSB_viewActions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSB_viewActions.Image = global::JustGestures.Properties.Resources.view_actions;
            this.tSB_viewActions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSB_viewActions.Name = "tSB_viewActions";
            this.tSB_viewActions.Size = new System.Drawing.Size(26, 26);
            this.tSB_viewActions.Text = "View Actions";
            this.tSB_viewActions.Click += new System.EventHandler(this.tSB_view_Click);
            // 
            // tSB_options
            // 
            this.tSB_options.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSB_options.Image = global::JustGestures.Properties.Resources.options;
            this.tSB_options.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSB_options.Name = "tSB_options";
            this.tSB_options.Size = new System.Drawing.Size(26, 26);
            this.tSB_options.Text = "Options";
            this.tSB_options.Click += new System.EventHandler(this.tSB_options_Click);
            // 
            // tSB_about
            // 
            this.tSB_about.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSB_about.Image = global::JustGestures.Properties.Resources.about;
            this.tSB_about.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSB_about.Name = "tSB_about";
            this.tSB_about.Size = new System.Drawing.Size(26, 26);
            this.tSB_about.Text = "About";
            this.tSB_about.Click += new System.EventHandler(this.tSB_about_Click);
            // 
            // startButton1
            // 
            this.startButton1.BackColor = System.Drawing.Color.Transparent;
            this.startButton1.Image = ((System.Drawing.Image)(resources.GetObject("startButton1.Image")));
            this.startButton1.Location = new System.Drawing.Point(2, 2);
            this.startButton1.Name = "startButton1";
            this.startButton1.Size = new System.Drawing.Size(42, 39);
            this.startButton1.TabIndex = 6;
            this.startButton1.TabStop = false;
            // 
            // Form_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::JustGestures.Properties.Resources.designedBack2;
            this.ClientSize = new System.Drawing.Size(730, 600);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel_gesturesCollection);
            this.Controls.Add(this.panel_visualization);
            this.Controls.Add(this.panel_matchedGestures);
            this.Controls.Add(this.startButton1);
            this.Controls.Add(this.pB_minimize);
            this.Controls.Add(this.pB_close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Just Gestures";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_designed_FormClosing);
            this.Resize += new System.EventHandler(this.Form_designed_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pB_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB_minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB_displayGesture)).EndInit();
            this.cMS_gestureOptions.ResumeLayout(false);
            this.panel_gesturesCollection.ResumeLayout(false);
            this.panel_matchedGestures.ResumeLayout(false);
            this.panel_visualization.ResumeLayout(false);
            this.cMS_startMenu.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startButton1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pB_close;
        private System.Windows.Forms.PictureBox pB_minimize;
        private JustGestures.GUI.StartButton startButton1;
        private JustGestures.GUI.GesturesListView listView_GesturesActions;
        private System.Windows.Forms.ColumnHeader cH_name;
        private System.Windows.Forms.ColumnHeader cH_description;
        private ComboListView cLV_matchedGestures;
        private System.Windows.Forms.ColumnHeader cH_associatedActions;
        private System.Windows.Forms.ColumnHeader cH_priority;
        private System.Windows.Forms.PictureBox pB_displayGesture;
        private System.Windows.Forms.ImageList iL_gestures;
        private System.Windows.Forms.ImageList iL_actions;
        private System.Windows.Forms.ContextMenuStrip cMS_gestureOptions;
        private System.Windows.Forms.ToolStripMenuItem cMI_activate;
        private System.Windows.Forms.ToolStripMenuItem cMI_deactivate;
        private System.Windows.Forms.ToolStripMenuItem cMI_addNew;
        private System.Windows.Forms.ToolStripMenuItem cMI_modify;
        private System.Windows.Forms.ToolStripMenuItem cMI_delete;
        private System.Windows.Forms.Panel panel_gesturesCollection;
        private System.Windows.Forms.Panel panel_matchedGestures;
        private System.Windows.Forms.Panel panel_visualization;
        private System.Windows.Forms.ContextMenuStrip cMS_startMenu;
        private System.Windows.Forms.ToolStripMenuItem sMI_addGesture;
        private System.Windows.Forms.ToolStripMenuItem sMI_addGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem sMI_options;
        private System.Windows.Forms.ToolStripMenuItem sMI_about;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem sMI_exit;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tSB_addGesture;
        private System.Windows.Forms.ToolStripButton tSB_addGroup;
        private System.Windows.Forms.ToolStripButton tSB_modify;
        private System.Windows.Forms.ToolStripButton tSB_delete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tSB_viewGestures;
        private System.Windows.Forms.ToolStripButton tSB_options;
        private System.Windows.Forms.ToolStripButton tSB_about;
        private System.Windows.Forms.ToolStripMenuItem sMI_viewGestures;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton tSB_viewActions;
        private System.Windows.Forms.ToolStripMenuItem sMI_viewActions;
      
    }
}