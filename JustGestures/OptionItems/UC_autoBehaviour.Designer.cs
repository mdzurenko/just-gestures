namespace JustGestures.OptionItems
{
    partial class UC_autoBehaviour
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
            this.panel_top = new System.Windows.Forms.Panel();
            this.gB_conditions = new System.Windows.Forms.GroupBox();
            this.nUD_autocheckTime = new System.Windows.Forms.NumericUpDown();
            this.lbl_2AutoState = new System.Windows.Forms.Label();
            this.cB_fullscreen = new System.Windows.Forms.ComboBox();
            this.lbl_defaultState = new System.Windows.Forms.Label();
            this.lbl_fullscreenState = new System.Windows.Forms.Label();
            this.lbl_autocheck = new System.Windows.Forms.Label();
            this.cB_auto1 = new System.Windows.Forms.ComboBox();
            this.cB_default = new System.Windows.Forms.ComboBox();
            this.cB_auto2 = new System.Windows.Forms.ComboBox();
            this.lbl_1AutoState = new System.Windows.Forms.Label();
            this.panel_fill = new System.Windows.Forms.Panel();
            this.gB_finalList = new System.Windows.Forms.GroupBox();
            this.lV_programs = new System.Windows.Forms.ListView();
            this.cH_appState = new System.Windows.Forms.ColumnHeader();
            this.cH_name = new System.Windows.Forms.ColumnHeader();
            this.cH_path = new System.Windows.Forms.ColumnHeader();
            this.panel_top.SuspendLayout();
            this.gB_conditions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_autocheckTime)).BeginInit();
            this.panel_fill.SuspendLayout();
            this.gB_finalList.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_top
            // 
            this.panel_top.Controls.Add(this.gB_conditions);
            this.panel_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_top.Location = new System.Drawing.Point(0, 0);
            this.panel_top.Name = "panel_top";
            this.panel_top.Size = new System.Drawing.Size(513, 175);
            this.panel_top.TabIndex = 0;
            // 
            // gB_conditions
            // 
            this.gB_conditions.Controls.Add(this.nUD_autocheckTime);
            this.gB_conditions.Controls.Add(this.lbl_2AutoState);
            this.gB_conditions.Controls.Add(this.cB_fullscreen);
            this.gB_conditions.Controls.Add(this.lbl_defaultState);
            this.gB_conditions.Controls.Add(this.lbl_fullscreenState);
            this.gB_conditions.Controls.Add(this.lbl_autocheck);
            this.gB_conditions.Controls.Add(this.cB_auto1);
            this.gB_conditions.Controls.Add(this.cB_default);
            this.gB_conditions.Controls.Add(this.cB_auto2);
            this.gB_conditions.Controls.Add(this.lbl_1AutoState);
            this.gB_conditions.Location = new System.Drawing.Point(0, 0);
            this.gB_conditions.Name = "gB_conditions";
            this.gB_conditions.Size = new System.Drawing.Size(400, 175);
            this.gB_conditions.TabIndex = 13;
            this.gB_conditions.TabStop = false;
            this.gB_conditions.Text = "Conditions";
            // 
            // nUD_autocheckTime
            // 
            this.nUD_autocheckTime.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nUD_autocheckTime.Location = new System.Drawing.Point(234, 26);
            this.nUD_autocheckTime.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nUD_autocheckTime.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nUD_autocheckTime.Name = "nUD_autocheckTime";
            this.nUD_autocheckTime.Size = new System.Drawing.Size(160, 20);
            this.nUD_autocheckTime.TabIndex = 1;
            this.nUD_autocheckTime.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nUD_autocheckTime.ValueChanged += new System.EventHandler(this.nUD_autocheckTime_ValueChanged);
            // 
            // lbl_2AutoState
            // 
            this.lbl_2AutoState.AutoSize = true;
            this.lbl_2AutoState.Location = new System.Drawing.Point(6, 137);
            this.lbl_2AutoState.Name = "lbl_2AutoState";
            this.lbl_2AutoState.Size = new System.Drawing.Size(81, 13);
            this.lbl_2AutoState.TabIndex = 7;
            this.lbl_2AutoState.Text = "4.) Auto State 2";
            // 
            // cB_fullscreen
            // 
            this.cB_fullscreen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_fullscreen.FormattingEnabled = true;
            this.cB_fullscreen.Items.AddRange(new object[] {
            "(none)",
            "Auto Enable",
            "Auto Disable"});
            this.cB_fullscreen.Location = new System.Drawing.Point(234, 80);
            this.cB_fullscreen.Name = "cB_fullscreen";
            this.cB_fullscreen.Size = new System.Drawing.Size(160, 21);
            this.cB_fullscreen.TabIndex = 3;
            this.cB_fullscreen.SelectedIndexChanged += new System.EventHandler(this.comboBox_selectedIndexChanged);
            // 
            // lbl_defaultState
            // 
            this.lbl_defaultState.AutoSize = true;
            this.lbl_defaultState.Location = new System.Drawing.Point(6, 54);
            this.lbl_defaultState.Name = "lbl_defaultState";
            this.lbl_defaultState.Size = new System.Drawing.Size(84, 13);
            this.lbl_defaultState.TabIndex = 1;
            this.lbl_defaultState.Text = "1.) Default State";
            // 
            // lbl_fullscreenState
            // 
            this.lbl_fullscreenState.AutoSize = true;
            this.lbl_fullscreenState.Location = new System.Drawing.Point(6, 83);
            this.lbl_fullscreenState.Name = "lbl_fullscreenState";
            this.lbl_fullscreenState.Size = new System.Drawing.Size(98, 13);
            this.lbl_fullscreenState.TabIndex = 3;
            this.lbl_fullscreenState.Text = "2.) Fullscreen State";
            // 
            // lbl_autocheck
            // 
            this.lbl_autocheck.AutoSize = true;
            this.lbl_autocheck.Location = new System.Drawing.Point(6, 28);
            this.lbl_autocheck.Name = "lbl_autocheck";
            this.lbl_autocheck.Size = new System.Drawing.Size(86, 13);
            this.lbl_autocheck.TabIndex = 9;
            this.lbl_autocheck.Text = "Autocheck in ms";
            // 
            // cB_auto1
            // 
            this.cB_auto1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_auto1.FormattingEnabled = true;
            this.cB_auto1.Items.AddRange(new object[] {
            "(none)",
            "White List",
            "Black List"});
            this.cB_auto1.Location = new System.Drawing.Point(234, 107);
            this.cB_auto1.Name = "cB_auto1";
            this.cB_auto1.Size = new System.Drawing.Size(160, 21);
            this.cB_auto1.TabIndex = 4;
            this.cB_auto1.SelectedIndexChanged += new System.EventHandler(this.comboBox_selectedIndexChanged);
            // 
            // cB_default
            // 
            this.cB_default.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_default.FormattingEnabled = true;
            this.cB_default.Items.AddRange(new object[] {
            "(none)",
            "Auto Enable",
            "Auto Disable"});
            this.cB_default.Location = new System.Drawing.Point(234, 51);
            this.cB_default.Name = "cB_default";
            this.cB_default.Size = new System.Drawing.Size(160, 21);
            this.cB_default.TabIndex = 2;
            this.cB_default.SelectedIndexChanged += new System.EventHandler(this.comboBox_selectedIndexChanged);
            // 
            // cB_auto2
            // 
            this.cB_auto2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_auto2.FormattingEnabled = true;
            this.cB_auto2.Items.AddRange(new object[] {
            "(none)",
            "White List",
            "Black List"});
            this.cB_auto2.Location = new System.Drawing.Point(234, 134);
            this.cB_auto2.Name = "cB_auto2";
            this.cB_auto2.Size = new System.Drawing.Size(160, 21);
            this.cB_auto2.TabIndex = 5;
            this.cB_auto2.SelectedIndexChanged += new System.EventHandler(this.comboBox_selectedIndexChanged);
            // 
            // lbl_1AutoState
            // 
            this.lbl_1AutoState.AutoSize = true;
            this.lbl_1AutoState.Location = new System.Drawing.Point(6, 110);
            this.lbl_1AutoState.Name = "lbl_1AutoState";
            this.lbl_1AutoState.Size = new System.Drawing.Size(81, 13);
            this.lbl_1AutoState.TabIndex = 5;
            this.lbl_1AutoState.Text = "3.) Auto State 1";
            // 
            // panel_fill
            // 
            this.panel_fill.Controls.Add(this.gB_finalList);
            this.panel_fill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_fill.Location = new System.Drawing.Point(0, 175);
            this.panel_fill.Name = "panel_fill";
            this.panel_fill.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.panel_fill.Size = new System.Drawing.Size(513, 241);
            this.panel_fill.TabIndex = 1;
            // 
            // gB_finalList
            // 
            this.gB_finalList.Controls.Add(this.lV_programs);
            this.gB_finalList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gB_finalList.Location = new System.Drawing.Point(0, 1);
            this.gB_finalList.Name = "gB_finalList";
            this.gB_finalList.Size = new System.Drawing.Size(513, 240);
            this.gB_finalList.TabIndex = 1;
            this.gB_finalList.TabStop = false;
            this.gB_finalList.Text = "Final List";
            // 
            // lV_programs
            // 
            this.lV_programs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cH_appState,
            this.cH_name,
            this.cH_path});
            this.lV_programs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lV_programs.FullRowSelect = true;
            this.lV_programs.Location = new System.Drawing.Point(3, 16);
            this.lV_programs.Name = "lV_programs";
            this.lV_programs.Size = new System.Drawing.Size(507, 221);
            this.lV_programs.TabIndex = 6;
            this.lV_programs.UseCompatibleStateImageBehavior = false;
            this.lV_programs.View = System.Windows.Forms.View.Details;
            // 
            // cH_appState
            // 
            this.cH_appState.Text = "Application State";
            this.cH_appState.Width = 123;
            // 
            // cH_name
            // 
            this.cH_name.Text = "Name";
            this.cH_name.Width = 126;
            // 
            // cH_path
            // 
            this.cH_path.Text = "Path";
            this.cH_path.Width = 219;
            // 
            // UC_autoBehaviour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_fill);
            this.Controls.Add(this.panel_top);
            this.Name = "UC_autoBehaviour";
            this.Size = new System.Drawing.Size(513, 416);
            this.Load += new System.EventHandler(this.UC_autoBehaviour_Load);
            this.VisibleChanged += new System.EventHandler(this.UC_autoBehaviour_VisibleChanged);
            this.panel_top.ResumeLayout(false);
            this.gB_conditions.ResumeLayout(false);
            this.gB_conditions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_autocheckTime)).EndInit();
            this.panel_fill.ResumeLayout(false);
            this.gB_finalList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_top;
        private System.Windows.Forms.Panel panel_fill;
        private System.Windows.Forms.ListView lV_programs;
        private System.Windows.Forms.ColumnHeader cH_name;
        private System.Windows.Forms.ColumnHeader cH_path;
        private System.Windows.Forms.ColumnHeader cH_appState;
        private System.Windows.Forms.ComboBox cB_auto2;
        private System.Windows.Forms.Label lbl_2AutoState;
        private System.Windows.Forms.ComboBox cB_auto1;
        private System.Windows.Forms.Label lbl_1AutoState;
        private System.Windows.Forms.ComboBox cB_fullscreen;
        private System.Windows.Forms.Label lbl_fullscreenState;
        private System.Windows.Forms.ComboBox cB_default;
        private System.Windows.Forms.Label lbl_defaultState;
        private System.Windows.Forms.Label lbl_autocheck;
        private System.Windows.Forms.GroupBox gB_finalList;
        private System.Windows.Forms.GroupBox gB_conditions;
        private System.Windows.Forms.NumericUpDown nUD_autocheckTime;
    }
}
