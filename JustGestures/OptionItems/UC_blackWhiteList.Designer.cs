namespace JustGestures.OptionItems
{
    partial class UC_blackWhiteList
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
            this.btn_add = new System.Windows.Forms.Button();
            this.lV_list = new System.Windows.Forms.ListView();
            this.cH_name = new System.Windows.Forms.ColumnHeader();
            this.cH_path = new System.Windows.Forms.ColumnHeader();
            this.btn_remove = new System.Windows.Forms.Button();
            this.panel_down = new System.Windows.Forms.Panel();
            this.panel_mid = new System.Windows.Forms.Panel();
            this.panel_down.SuspendLayout();
            this.panel_mid.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_add
            // 
            this.btn_add.AutoSize = true;
            this.btn_add.Location = new System.Drawing.Point(3, 6);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(100, 23);
            this.btn_add.TabIndex = 0;
            this.btn_add.Text = "Add New";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // lV_list
            // 
            this.lV_list.CheckBoxes = true;
            this.lV_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cH_name,
            this.cH_path});
            this.lV_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lV_list.Location = new System.Drawing.Point(0, 0);
            this.lV_list.Name = "lV_list";
            this.lV_list.Size = new System.Drawing.Size(477, 346);
            this.lV_list.TabIndex = 4;
            this.lV_list.UseCompatibleStateImageBehavior = false;
            this.lV_list.View = System.Windows.Forms.View.Details;
            this.lV_list.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lV_list_ItemChecked);
            // 
            // cH_name
            // 
            this.cH_name.Text = "Name";
            this.cH_name.Width = 112;
            // 
            // cH_path
            // 
            this.cH_path.Text = "Path";
            this.cH_path.Width = 302;
            // 
            // btn_remove
            // 
            this.btn_remove.AutoSize = true;
            this.btn_remove.Location = new System.Drawing.Point(130, 6);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(121, 23);
            this.btn_remove.TabIndex = 1;
            this.btn_remove.Text = "Remove Selected";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // panel_down
            // 
            this.panel_down.Controls.Add(this.btn_remove);
            this.panel_down.Controls.Add(this.btn_add);
            this.panel_down.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_down.Location = new System.Drawing.Point(0, 346);
            this.panel_down.Name = "panel_down";
            this.panel_down.Size = new System.Drawing.Size(477, 29);
            this.panel_down.TabIndex = 7;
            // 
            // panel_mid
            // 
            this.panel_mid.Controls.Add(this.lV_list);
            this.panel_mid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_mid.Location = new System.Drawing.Point(0, 0);
            this.panel_mid.Name = "panel_mid";
            this.panel_mid.Size = new System.Drawing.Size(477, 346);
            this.panel_mid.TabIndex = 9;
            // 
            // UC_blackWhiteList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_mid);
            this.Controls.Add(this.panel_down);
            this.Name = "UC_blackWhiteList";
            this.Size = new System.Drawing.Size(477, 375);
            this.Load += new System.EventHandler(this.UC_blackWhiteList_Load);
            this.panel_down.ResumeLayout(false);
            this.panel_down.PerformLayout();
            this.panel_mid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.ListView lV_list;
        private System.Windows.Forms.ColumnHeader cH_name;
        private System.Windows.Forms.ColumnHeader cH_path;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Panel panel_down;
        private System.Windows.Forms.Panel panel_mid;
    }
}
