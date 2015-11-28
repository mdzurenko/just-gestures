namespace JustGestures.ControlItems
{
    partial class UC_actions
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
            this.tV_category = new System.Windows.Forms.TreeView();
            this.iL_actions = new System.Windows.Forms.ImageList(this.components);
            this.gB_useOfAction = new System.Windows.Forms.GroupBox();
            this.cCB_groups = new JustGestures.ControlItems.CheckComboBox();
            this.btn_addApp = new System.Windows.Forms.Button();
            this.rB_local = new System.Windows.Forms.RadioButton();
            this.rB_global = new System.Windows.Forms.RadioButton();
            this.iL_applications = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.gB_action = new System.Windows.Forms.GroupBox();
            this.tV_actions = new System.Windows.Forms.TreeView();
            this.gB_category = new System.Windows.Forms.GroupBox();
            this.gB_useOfAction.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gB_action.SuspendLayout();
            this.gB_category.SuspendLayout();
            this.SuspendLayout();
            // 
            // tV_category
            // 
            this.tV_category.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tV_category.HideSelection = false;
            this.tV_category.ImageIndex = 0;
            this.tV_category.ImageList = this.iL_actions;
            this.tV_category.Location = new System.Drawing.Point(3, 16);
            this.tV_category.Name = "tV_category";
            this.tV_category.SelectedImageIndex = 0;
            this.tV_category.ShowPlusMinus = false;
            this.tV_category.ShowRootLines = false;
            this.tV_category.Size = new System.Drawing.Size(319, 372);
            this.tV_category.TabIndex = 0;
            this.tV_category.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.tV_category_BeforeCollapse);
            this.tV_category.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tV_category_AfterSelect);
            // 
            // iL_actions
            // 
            this.iL_actions.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.iL_actions.ImageSize = new System.Drawing.Size(20, 20);
            this.iL_actions.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // gB_useOfAction
            // 
            this.gB_useOfAction.Controls.Add(this.cCB_groups);
            this.gB_useOfAction.Controls.Add(this.btn_addApp);
            this.gB_useOfAction.Controls.Add(this.rB_local);
            this.gB_useOfAction.Controls.Add(this.rB_global);
            this.gB_useOfAction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gB_useOfAction.Location = new System.Drawing.Point(0, 391);
            this.gB_useOfAction.Name = "gB_useOfAction";
            this.gB_useOfAction.Size = new System.Drawing.Size(675, 80);
            this.gB_useOfAction.TabIndex = 1;
            this.gB_useOfAction.TabStop = false;
            this.gB_useOfAction.Text = "Use of action";
            // 
            // cCB_groups
            // 
            this.cCB_groups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cCB_groups.FormattingEnabled = true;
            this.cCB_groups.Location = new System.Drawing.Point(228, 48);
            this.cCB_groups.Name = "cCB_groups";
            this.cCB_groups.Size = new System.Drawing.Size(211, 21);
            this.cCB_groups.TabIndex = 5;
            // 
            // btn_addApp
            // 
            this.btn_addApp.Location = new System.Drawing.Point(455, 46);
            this.btn_addApp.Name = "btn_addApp";
            this.btn_addApp.Size = new System.Drawing.Size(98, 23);
            this.btn_addApp.TabIndex = 4;
            this.btn_addApp.Text = "Add New";
            this.btn_addApp.UseVisualStyleBackColor = true;
            this.btn_addApp.Click += new System.EventHandler(this.btn_addApp_Click);
            // 
            // rB_local
            // 
            this.rB_local.AutoSize = true;
            this.rB_local.Location = new System.Drawing.Point(6, 49);
            this.rB_local.Name = "rB_local";
            this.rB_local.Size = new System.Drawing.Size(152, 17);
            this.rB_local.TabIndex = 2;
            this.rB_local.Text = "Under specific applications";
            this.rB_local.UseVisualStyleBackColor = true;
            this.rB_local.CheckedChanged += new System.EventHandler(this.rB_GlobalLocal_CheckedChanged);
            // 
            // rB_global
            // 
            this.rB_global.AutoSize = true;
            this.rB_global.Location = new System.Drawing.Point(6, 26);
            this.rB_global.Name = "rB_global";
            this.rB_global.Size = new System.Drawing.Size(151, 17);
            this.rB_global.TabIndex = 1;
            this.rB_global.Text = "Globally - in all applications";
            this.rB_global.UseVisualStyleBackColor = true;
            this.rB_global.CheckedChanged += new System.EventHandler(this.rB_GlobalLocal_CheckedChanged);
            // 
            // iL_applications
            // 
            this.iL_applications.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.iL_applications.ImageSize = new System.Drawing.Size(20, 20);
            this.iL_applications.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gB_action);
            this.panel1.Controls.Add(this.gB_category);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(675, 391);
            this.panel1.TabIndex = 2;
            // 
            // gB_action
            // 
            this.gB_action.Controls.Add(this.tV_actions);
            this.gB_action.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gB_action.Location = new System.Drawing.Point(325, 0);
            this.gB_action.Name = "gB_action";
            this.gB_action.Size = new System.Drawing.Size(350, 391);
            this.gB_action.TabIndex = 1;
            this.gB_action.TabStop = false;
            this.gB_action.Text = "Action";
            // 
            // tV_actions
            // 
            this.tV_actions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tV_actions.ImageIndex = 0;
            this.tV_actions.ImageList = this.iL_actions;
            this.tV_actions.Location = new System.Drawing.Point(3, 16);
            this.tV_actions.Name = "tV_actions";
            this.tV_actions.SelectedImageIndex = 0;
            this.tV_actions.ShowPlusMinus = false;
            this.tV_actions.ShowRootLines = false;
            this.tV_actions.Size = new System.Drawing.Size(344, 372);
            this.tV_actions.TabIndex = 0;
            this.tV_actions.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.tV_actions_BeforeCollapse);
            this.tV_actions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tV_actions_AfterSelect);
            // 
            // gB_category
            // 
            this.gB_category.Controls.Add(this.tV_category);
            this.gB_category.Dock = System.Windows.Forms.DockStyle.Left;
            this.gB_category.Location = new System.Drawing.Point(0, 0);
            this.gB_category.Name = "gB_category";
            this.gB_category.Size = new System.Drawing.Size(325, 391);
            this.gB_category.TabIndex = 0;
            this.gB_category.TabStop = false;
            this.gB_category.Text = "Category";
            // 
            // UC_actions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gB_useOfAction);
            this.Name = "UC_actions";
            this.Size = new System.Drawing.Size(675, 471);
            this.Load += new System.EventHandler(this.UserControl_actions_Load);
            this.VisibleChanged += new System.EventHandler(this.UserControl_actions_VisibleChanged);
            this.gB_useOfAction.ResumeLayout(false);
            this.gB_useOfAction.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gB_action.ResumeLayout(false);
            this.gB_category.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tV_category;
        private System.Windows.Forms.ImageList iL_actions;
        private System.Windows.Forms.GroupBox gB_useOfAction;
        private System.Windows.Forms.RadioButton rB_local;
        private System.Windows.Forms.RadioButton rB_global;
        private System.Windows.Forms.Button btn_addApp;
        private System.Windows.Forms.ImageList iL_applications;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gB_action;
        private System.Windows.Forms.GroupBox gB_category;
        private System.Windows.Forms.TreeView tV_actions;
        private CheckComboBox cCB_groups;

    }
}
