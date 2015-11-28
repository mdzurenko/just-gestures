namespace JustGestures.ControlItems
{
    partial class UC_name
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
            this.tB_name = new System.Windows.Forms.TextBox();
            this.cB_active = new System.Windows.Forms.CheckBox();
            this.panel_LeftFill = new System.Windows.Forms.Panel();
            this.cB_priority = new System.Windows.Forms.ComboBox();
            this.lbl_priority = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_name = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_uncheckToDeactivate = new System.Windows.Forms.Label();
            this.panel_LeftFill.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tB_name
            // 
            this.tB_name.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tB_name.Location = new System.Drawing.Point(0, 111);
            this.tB_name.MaxLength = 100;
            this.tB_name.Name = "tB_name";
            this.tB_name.Size = new System.Drawing.Size(546, 20);
            this.tB_name.TabIndex = 0;
            this.tB_name.TextChanged += new System.EventHandler(this.tB_name_TextChanged);
            // 
            // cB_active
            // 
            this.cB_active.AutoSize = true;
            this.cB_active.Checked = true;
            this.cB_active.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_active.Location = new System.Drawing.Point(0, 184);
            this.cB_active.Name = "cB_active";
            this.cB_active.Size = new System.Drawing.Size(56, 17);
            this.cB_active.TabIndex = 1;
            this.cB_active.Text = "Active";
            this.cB_active.UseVisualStyleBackColor = true;
            this.cB_active.CheckedChanged += new System.EventHandler(this.checkBox_active_CheckedChanged);
            // 
            // panel_LeftFill
            // 
            this.panel_LeftFill.Controls.Add(this.cB_priority);
            this.panel_LeftFill.Controls.Add(this.lbl_priority);
            this.panel_LeftFill.Controls.Add(this.panel2);
            this.panel_LeftFill.Controls.Add(this.panel1);
            this.panel_LeftFill.Controls.Add(this.lbl_uncheckToDeactivate);
            this.panel_LeftFill.Controls.Add(this.cB_active);
            this.panel_LeftFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_LeftFill.Location = new System.Drawing.Point(0, 0);
            this.panel_LeftFill.Name = "panel_LeftFill";
            this.panel_LeftFill.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.panel_LeftFill.Size = new System.Drawing.Size(585, 449);
            this.panel_LeftFill.TabIndex = 6;
            // 
            // cB_priority
            // 
            this.cB_priority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_priority.FormattingEnabled = true;
            this.cB_priority.Location = new System.Drawing.Point(77, 146);
            this.cB_priority.Name = "cB_priority";
            this.cB_priority.Size = new System.Drawing.Size(146, 21);
            this.cB_priority.TabIndex = 8;
            this.cB_priority.SelectedIndexChanged += new System.EventHandler(this.cB_priority_SelectedIndexChanged);
            // 
            // lbl_priority
            // 
            this.lbl_priority.AutoSize = true;
            this.lbl_priority.Location = new System.Drawing.Point(-3, 149);
            this.lbl_priority.Name = "lbl_priority";
            this.lbl_priority.Size = new System.Drawing.Size(38, 13);
            this.lbl_priority.TabIndex = 7;
            this.lbl_priority.Text = "Priority";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tB_name);
            this.panel2.Controls.Add(this.lbl_name);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(546, 131);
            this.panel2.TabIndex = 6;
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_name.Location = new System.Drawing.Point(-3, 95);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(35, 13);
            this.lbl_name.TabIndex = 0;
            this.lbl_name.Text = "Name";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(546, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(29, 449);
            this.panel1.TabIndex = 5;
            // 
            // lbl_uncheckToDeactivate
            // 
            this.lbl_uncheckToDeactivate.AutoSize = true;
            this.lbl_uncheckToDeactivate.Location = new System.Drawing.Point(-3, 204);
            this.lbl_uncheckToDeactivate.Name = "lbl_uncheckToDeactivate";
            this.lbl_uncheckToDeactivate.Size = new System.Drawing.Size(258, 13);
            this.lbl_uncheckToDeactivate.TabIndex = 4;
            this.lbl_uncheckToDeactivate.Text = "(Uncheck if you don\'t want the gesture to be actived)";
            // 
            // UC_name
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_LeftFill);
            this.Name = "UC_name";
            this.Size = new System.Drawing.Size(585, 449);
            this.Load += new System.EventHandler(this.UserControl_name_Load);
            this.VisibleChanged += new System.EventHandler(this.UserControl_name_VisibleChanged);
            this.panel_LeftFill.ResumeLayout(false);
            this.panel_LeftFill.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tB_name;
        private System.Windows.Forms.CheckBox cB_active;
        private System.Windows.Forms.Panel panel_LeftFill;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Label lbl_uncheckToDeactivate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cB_priority;
        private System.Windows.Forms.Label lbl_priority;
    }
}
