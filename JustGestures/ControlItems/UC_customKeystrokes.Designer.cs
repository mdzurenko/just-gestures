namespace JustGestures.ControlItems
{
    partial class UC_customKeystrokes
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lB_modifiers = new System.Windows.Forms.ListBox();
            this.lB_keys = new System.Windows.Forms.ListBox();
            this.lB_special = new System.Windows.Forms.ListBox();
            this.lW_wheelDown = new JustGestures.GUI.DragAndDropListView();
            this.cH_wheelMove_down = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.lW_triggerUp = new JustGestures.GUI.DragAndDropListView();
            this.cH_holdBtn_up = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.lW_wheelUp = new JustGestures.GUI.DragAndDropListView();
            this.cH_wheelMove_up = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.lW_triggerDown = new JustGestures.GUI.DragAndDropListView();
            this.cH_holdBtn_down = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btn_remove = new System.Windows.Forms.Button();
            this.panel_left = new System.Windows.Forms.Panel();
            this.gB_mouseActions = new System.Windows.Forms.GroupBox();
            this.gB_keys = new System.Windows.Forms.GroupBox();
            this.gB_modifiers = new System.Windows.Forms.GroupBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel_scripts = new System.Windows.Forms.Panel();
            this.gB_keyScript = new System.Windows.Forms.GroupBox();
            this.panel_triggerDown = new System.Windows.Forms.Panel();
            this.panel_wheelDown = new System.Windows.Forms.Panel();
            this.panel_wheelUp = new System.Windows.Forms.Panel();
            this.panel_triggerUp = new System.Windows.Forms.Panel();
            this.panel4.SuspendLayout();
            this.panel_left.SuspendLayout();
            this.gB_mouseActions.SuspendLayout();
            this.gB_keys.SuspendLayout();
            this.gB_modifiers.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel_scripts.SuspendLayout();
            this.gB_keyScript.SuspendLayout();
            this.panel_triggerDown.SuspendLayout();
            this.panel_wheelDown.SuspendLayout();
            this.panel_wheelUp.SuspendLayout();
            this.panel_triggerUp.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 1000;
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 1000;
            this.toolTip1.ReshowDelay = 1000;
            // 
            // lB_modifiers
            // 
            this.lB_modifiers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lB_modifiers.FormattingEnabled = true;
            this.lB_modifiers.Location = new System.Drawing.Point(3, 16);
            this.lB_modifiers.Name = "lB_modifiers";
            this.lB_modifiers.Size = new System.Drawing.Size(173, 95);
            this.lB_modifiers.TabIndex = 0;
            this.toolTip1.SetToolTip(this.lB_modifiers, "You can use ENTER or DoubleClick for adding \r\nthe keys to the srcipt.");
            this.lB_modifiers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox_MouseDoubleClick);
            this.lB_modifiers.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            this.lB_modifiers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listBox_KeyPress);
            // 
            // lB_keys
            // 
            this.lB_keys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lB_keys.FormattingEnabled = true;
            this.lB_keys.Location = new System.Drawing.Point(3, 16);
            this.lB_keys.Name = "lB_keys";
            this.lB_keys.Size = new System.Drawing.Size(173, 147);
            this.lB_keys.TabIndex = 1;
            this.toolTip1.SetToolTip(this.lB_keys, "You can use ENTER or DoubleClick for adding \r\nthe keys to the srcipt.");
            this.lB_keys.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox_MouseDoubleClick);
            this.lB_keys.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            this.lB_keys.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listBox_KeyPress);
            // 
            // lB_special
            // 
            this.lB_special.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lB_special.FormattingEnabled = true;
            this.lB_special.Location = new System.Drawing.Point(3, 16);
            this.lB_special.Name = "lB_special";
            this.lB_special.Size = new System.Drawing.Size(173, 147);
            this.lB_special.TabIndex = 0;
            this.toolTip1.SetToolTip(this.lB_special, "You can use ENTER or DoubleClick for adding \r\nthe keys to the srcipt.");
            this.lB_special.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox_MouseDoubleClick);
            this.lB_special.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            this.lB_special.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listBox_KeyPress);
            // 
            // lW_wheelDown
            // 
            this.lW_wheelDown.AllowDrop = true;
            this.lW_wheelDown.AllowReorder = true;
            this.lW_wheelDown.BackColor = System.Drawing.SystemColors.Control;
            this.lW_wheelDown.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cH_wheelMove_down,
            this.columnHeader1,
            this.columnHeader3});
            this.lW_wheelDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lW_wheelDown.FullRowSelect = true;
            this.lW_wheelDown.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lW_wheelDown.HideSelection = false;
            this.lW_wheelDown.LineColor = System.Drawing.Color.Blue;
            this.lW_wheelDown.Location = new System.Drawing.Point(0, 0);
            this.lW_wheelDown.Name = "lW_wheelDown";
            this.lW_wheelDown.OwnerDraw = true;
            this.lW_wheelDown.Size = new System.Drawing.Size(178, 97);
            this.lW_wheelDown.TabIndex = 0;
            this.toolTip1.SetToolTip(this.lW_wheelDown, "You can use Drag & Drop to \r\nreorganize keys sequences\r\nand DoubleClick to remove" +
                    " \r\nitem. ");
            this.lW_wheelDown.UseCompatibleStateImageBehavior = false;
            this.lW_wheelDown.View = System.Windows.Forms.View.Details;
            this.lW_wheelDown.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lW_script_MouseDoubleClick);
            this.lW_wheelDown.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lW_script_DrawColumnHeader);
            this.lW_wheelDown.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lW_script_DrawItem);
            this.lW_wheelDown.SelectedIndexChanged += new System.EventHandler(this.lW_script_SelectedIndexChanged);
            this.lW_wheelDown.Enter += new System.EventHandler(this.lW_script_Enter);
            this.lW_wheelDown.DragDrop += new System.Windows.Forms.DragEventHandler(this.lW_script_DragDrop);
            this.lW_wheelDown.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lW_script_ColumnWidthChanging);
            this.lW_wheelDown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lW_script_KeyDown);
            this.lW_wheelDown.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lW_script_DrawSubItem);
            // 
            // cH_wheelMove_down
            // 
            this.cH_wheelMove_down.Text = "On Wheel Down Script";
            this.cH_wheelMove_down.Width = 150;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "KeyCode";
            this.columnHeader1.Width = 0;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "KeyAction";
            this.columnHeader3.Width = 0;
            // 
            // lW_triggerUp
            // 
            this.lW_triggerUp.AllowDrop = true;
            this.lW_triggerUp.AllowReorder = true;
            this.lW_triggerUp.BackColor = System.Drawing.SystemColors.Control;
            this.lW_triggerUp.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cH_holdBtn_up,
            this.columnHeader5,
            this.columnHeader6});
            this.lW_triggerUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lW_triggerUp.FullRowSelect = true;
            this.lW_triggerUp.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lW_triggerUp.HideSelection = false;
            this.lW_triggerUp.LineColor = System.Drawing.Color.Blue;
            this.lW_triggerUp.Location = new System.Drawing.Point(0, 0);
            this.lW_triggerUp.Name = "lW_triggerUp";
            this.lW_triggerUp.OwnerDraw = true;
            this.lW_triggerUp.Size = new System.Drawing.Size(178, 118);
            this.lW_triggerUp.TabIndex = 4;
            this.toolTip1.SetToolTip(this.lW_triggerUp, "You can use Drag & Drop to \r\nreorganize keys sequences\r\nand DoubleClick to remove" +
                    " \r\nitem. ");
            this.lW_triggerUp.UseCompatibleStateImageBehavior = false;
            this.lW_triggerUp.View = System.Windows.Forms.View.Details;
            this.lW_triggerUp.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lW_script_MouseDoubleClick);
            this.lW_triggerUp.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lW_script_DrawColumnHeader);
            this.lW_triggerUp.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lW_script_DrawItem);
            this.lW_triggerUp.SelectedIndexChanged += new System.EventHandler(this.lW_script_SelectedIndexChanged);
            this.lW_triggerUp.Enter += new System.EventHandler(this.lW_script_Enter);
            this.lW_triggerUp.DragDrop += new System.Windows.Forms.DragEventHandler(this.lW_script_DragDrop);
            this.lW_triggerUp.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lW_script_ColumnWidthChanging);
            this.lW_triggerUp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lW_script_KeyDown);
            this.lW_triggerUp.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lW_script_DrawSubItem);
            // 
            // cH_holdBtn_up
            // 
            this.cH_holdBtn_up.Text = "On Trigger Up Script";
            this.cH_holdBtn_up.Width = 154;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "KeyCode";
            this.columnHeader5.Width = 0;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "KeyAction";
            this.columnHeader6.Width = 0;
            // 
            // lW_wheelUp
            // 
            this.lW_wheelUp.AllowDrop = true;
            this.lW_wheelUp.AllowReorder = true;
            this.lW_wheelUp.BackColor = System.Drawing.SystemColors.Control;
            this.lW_wheelUp.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cH_wheelMove_up,
            this.columnHeader11,
            this.columnHeader12});
            this.lW_wheelUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lW_wheelUp.FullRowSelect = true;
            this.lW_wheelUp.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lW_wheelUp.HideSelection = false;
            this.lW_wheelUp.LineColor = System.Drawing.Color.Blue;
            this.lW_wheelUp.Location = new System.Drawing.Point(0, 0);
            this.lW_wheelUp.Name = "lW_wheelUp";
            this.lW_wheelUp.OwnerDraw = true;
            this.lW_wheelUp.Size = new System.Drawing.Size(178, 105);
            this.lW_wheelUp.TabIndex = 6;
            this.toolTip1.SetToolTip(this.lW_wheelUp, "You can use Drag & Drop to \r\nreorganize keys sequences\r\nand DoubleClick to remove" +
                    " \r\nitem. ");
            this.lW_wheelUp.UseCompatibleStateImageBehavior = false;
            this.lW_wheelUp.View = System.Windows.Forms.View.Details;
            this.lW_wheelUp.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lW_script_MouseDoubleClick);
            this.lW_wheelUp.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lW_script_DrawColumnHeader);
            this.lW_wheelUp.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lW_script_DrawItem);
            this.lW_wheelUp.SelectedIndexChanged += new System.EventHandler(this.lW_script_SelectedIndexChanged);
            this.lW_wheelUp.Enter += new System.EventHandler(this.lW_script_Enter);
            this.lW_wheelUp.DragDrop += new System.Windows.Forms.DragEventHandler(this.lW_script_DragDrop);
            this.lW_wheelUp.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lW_script_ColumnWidthChanging);
            this.lW_wheelUp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lW_script_KeyDown);
            this.lW_wheelUp.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lW_script_DrawSubItem);
            // 
            // cH_wheelMove_up
            // 
            this.cH_wheelMove_up.Text = "On Wheel Up Script";
            this.cH_wheelMove_up.Width = 151;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "KeyCode";
            this.columnHeader11.Width = 0;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "KeyAction";
            this.columnHeader12.Width = 0;
            // 
            // lW_triggerDown
            // 
            this.lW_triggerDown.AllowDrop = true;
            this.lW_triggerDown.AllowReorder = true;
            this.lW_triggerDown.BackColor = System.Drawing.SystemColors.Control;
            this.lW_triggerDown.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cH_holdBtn_down,
            this.columnHeader14,
            this.columnHeader15});
            this.lW_triggerDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lW_triggerDown.FullRowSelect = true;
            this.lW_triggerDown.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lW_triggerDown.HideSelection = false;
            this.lW_triggerDown.LineColor = System.Drawing.Color.Blue;
            this.lW_triggerDown.Location = new System.Drawing.Point(0, 0);
            this.lW_triggerDown.Name = "lW_triggerDown";
            this.lW_triggerDown.OwnerDraw = true;
            this.lW_triggerDown.Size = new System.Drawing.Size(178, 119);
            this.lW_triggerDown.TabIndex = 6;
            this.toolTip1.SetToolTip(this.lW_triggerDown, "You can use Drag & Drop to \r\nreorganize keys sequences\r\nand DoubleClick to remove" +
                    " \r\nitem. ");
            this.lW_triggerDown.UseCompatibleStateImageBehavior = false;
            this.lW_triggerDown.View = System.Windows.Forms.View.Details;
            this.lW_triggerDown.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lW_script_MouseDoubleClick);
            this.lW_triggerDown.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lW_script_DrawColumnHeader);
            this.lW_triggerDown.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lW_script_DrawItem);
            this.lW_triggerDown.SelectedIndexChanged += new System.EventHandler(this.lW_script_SelectedIndexChanged);
            this.lW_triggerDown.Enter += new System.EventHandler(this.lW_script_Enter);
            this.lW_triggerDown.DragDrop += new System.Windows.Forms.DragEventHandler(this.lW_script_DragDrop);
            this.lW_triggerDown.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lW_script_ColumnWidthChanging);
            this.lW_triggerDown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lW_script_KeyDown);
            this.lW_triggerDown.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lW_script_DrawSubItem);
            // 
            // cH_holdBtn_down
            // 
            this.cH_holdBtn_down.Text = "On Trigger Down Script";
            this.cH_holdBtn_down.Width = 150;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "KeyCode";
            this.columnHeader14.Width = 0;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "KeyAction";
            this.columnHeader15.Width = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btn_remove);
            this.panel4.Controls.Add(this.panel_left);
            this.panel4.Controls.Add(this.btn_add);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(431, 458);
            this.panel4.TabIndex = 0;
            // 
            // btn_remove
            // 
            this.btn_remove.Enabled = false;
            this.btn_remove.Location = new System.Drawing.Point(274, 215);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(85, 27);
            this.btn_remove.TabIndex = 0;
            this.btn_remove.Text = "Remove";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // panel_left
            // 
            this.panel_left.Controls.Add(this.gB_mouseActions);
            this.panel_left.Controls.Add(this.gB_keys);
            this.panel_left.Controls.Add(this.gB_modifiers);
            this.panel_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_left.Location = new System.Drawing.Point(0, 0);
            this.panel_left.Name = "panel_left";
            this.panel_left.Size = new System.Drawing.Size(179, 458);
            this.panel_left.TabIndex = 0;
            // 
            // gB_mouseActions
            // 
            this.gB_mouseActions.Controls.Add(this.lB_special);
            this.gB_mouseActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gB_mouseActions.Location = new System.Drawing.Point(0, 292);
            this.gB_mouseActions.Name = "gB_mouseActions";
            this.gB_mouseActions.Size = new System.Drawing.Size(179, 166);
            this.gB_mouseActions.TabIndex = 3;
            this.gB_mouseActions.TabStop = false;
            this.gB_mouseActions.Text = "Mouse Actions";
            // 
            // gB_keys
            // 
            this.gB_keys.Controls.Add(this.lB_keys);
            this.gB_keys.Dock = System.Windows.Forms.DockStyle.Top;
            this.gB_keys.Location = new System.Drawing.Point(0, 121);
            this.gB_keys.Name = "gB_keys";
            this.gB_keys.Size = new System.Drawing.Size(179, 171);
            this.gB_keys.TabIndex = 3;
            this.gB_keys.TabStop = false;
            this.gB_keys.Text = "Keys";
            // 
            // gB_modifiers
            // 
            this.gB_modifiers.Controls.Add(this.lB_modifiers);
            this.gB_modifiers.Dock = System.Windows.Forms.DockStyle.Top;
            this.gB_modifiers.Location = new System.Drawing.Point(0, 0);
            this.gB_modifiers.Name = "gB_modifiers";
            this.gB_modifiers.Size = new System.Drawing.Size(179, 121);
            this.gB_modifiers.TabIndex = 2;
            this.gB_modifiers.TabStop = false;
            this.gB_modifiers.Text = "Modifiers";
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(274, 181);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(85, 28);
            this.btn_add.TabIndex = 1;
            this.btn_add.Text = "Add >>";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel_scripts);
            this.panel5.Controls.Add(this.panel4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(615, 458);
            this.panel5.TabIndex = 8;
            // 
            // panel_scripts
            // 
            this.panel_scripts.Controls.Add(this.gB_keyScript);
            this.panel_scripts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_scripts.Location = new System.Drawing.Point(431, 0);
            this.panel_scripts.Name = "panel_scripts";
            this.panel_scripts.Size = new System.Drawing.Size(184, 458);
            this.panel_scripts.TabIndex = 2;
            // 
            // gB_keyScript
            // 
            this.gB_keyScript.Controls.Add(this.panel_triggerDown);
            this.gB_keyScript.Controls.Add(this.panel_wheelDown);
            this.gB_keyScript.Controls.Add(this.panel_wheelUp);
            this.gB_keyScript.Controls.Add(this.panel_triggerUp);
            this.gB_keyScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gB_keyScript.Location = new System.Drawing.Point(0, 0);
            this.gB_keyScript.Name = "gB_keyScript";
            this.gB_keyScript.Size = new System.Drawing.Size(184, 458);
            this.gB_keyScript.TabIndex = 2;
            this.gB_keyScript.TabStop = false;
            this.gB_keyScript.Text = "Key Script";
            // 
            // panel_triggerDown
            // 
            this.panel_triggerDown.Controls.Add(this.lW_triggerDown);
            this.panel_triggerDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_triggerDown.Location = new System.Drawing.Point(3, 16);
            this.panel_triggerDown.Name = "panel_triggerDown";
            this.panel_triggerDown.Size = new System.Drawing.Size(178, 119);
            this.panel_triggerDown.TabIndex = 3;
            // 
            // panel_wheelDown
            // 
            this.panel_wheelDown.Controls.Add(this.lW_wheelDown);
            this.panel_wheelDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_wheelDown.Location = new System.Drawing.Point(3, 135);
            this.panel_wheelDown.Name = "panel_wheelDown";
            this.panel_wheelDown.Size = new System.Drawing.Size(178, 97);
            this.panel_wheelDown.TabIndex = 2;
            // 
            // panel_wheelUp
            // 
            this.panel_wheelUp.Controls.Add(this.lW_wheelUp);
            this.panel_wheelUp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_wheelUp.Location = new System.Drawing.Point(3, 232);
            this.panel_wheelUp.Name = "panel_wheelUp";
            this.panel_wheelUp.Size = new System.Drawing.Size(178, 105);
            this.panel_wheelUp.TabIndex = 1;
            // 
            // panel_triggerUp
            // 
            this.panel_triggerUp.Controls.Add(this.lW_triggerUp);
            this.panel_triggerUp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_triggerUp.Location = new System.Drawing.Point(3, 337);
            this.panel_triggerUp.Name = "panel_triggerUp";
            this.panel_triggerUp.Size = new System.Drawing.Size(178, 118);
            this.panel_triggerUp.TabIndex = 0;
            // 
            // UC_customKeystrokes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel5);
            this.Name = "UC_customKeystrokes";
            this.Size = new System.Drawing.Size(615, 458);
            this.VisibleChanged += new System.EventHandler(this.UC_customKeystrokes_VisibleChanged);
            this.panel4.ResumeLayout(false);
            this.panel_left.ResumeLayout(false);
            this.gB_mouseActions.ResumeLayout(false);
            this.gB_keys.ResumeLayout(false);
            this.gB_modifiers.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel_scripts.ResumeLayout(false);
            this.gB_keyScript.ResumeLayout(false);
            this.panel_triggerDown.ResumeLayout(false);
            this.panel_wheelDown.ResumeLayout(false);
            this.panel_wheelUp.ResumeLayout(false);
            this.panel_triggerUp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Panel panel_left;
        private System.Windows.Forms.GroupBox gB_mouseActions;
        private System.Windows.Forms.ListBox lB_special;
        private System.Windows.Forms.GroupBox gB_keys;
        private System.Windows.Forms.ListBox lB_keys;
        private System.Windows.Forms.GroupBox gB_modifiers;
        private System.Windows.Forms.ListBox lB_modifiers;
        private System.Windows.Forms.Button btn_add;
        private JustGestures.GUI.DragAndDropListView lW_wheelDown;
        private System.Windows.Forms.ColumnHeader cH_wheelMove_down;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel_scripts;
        private JustGestures.GUI.DragAndDropListView lW_triggerUp;
        private System.Windows.Forms.ColumnHeader cH_holdBtn_up;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.GroupBox gB_keyScript;
        private JustGestures.GUI.DragAndDropListView lW_triggerDown;
        private System.Windows.Forms.ColumnHeader cH_holdBtn_down;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private JustGestures.GUI.DragAndDropListView lW_wheelUp;
        private System.Windows.Forms.ColumnHeader cH_wheelMove_up;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.Panel panel_triggerDown;
        private System.Windows.Forms.Panel panel_wheelDown;
        private System.Windows.Forms.Panel panel_wheelUp;
        private System.Windows.Forms.Panel panel_triggerUp;
    }
}
