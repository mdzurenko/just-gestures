namespace JustGestures.ControlItems
{
    partial class UC_selectProgram
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_selectProgram));
            this.tB_path = new System.Windows.Forms.TextBox();
            this.btn_browse = new System.Windows.Forms.Button();
            this.tB_name = new System.Windows.Forms.TextBox();
            this.lbl_name = new System.Windows.Forms.Label();
            this.lbl_path = new System.Windows.Forms.Label();
            this.lbl_icon = new System.Windows.Forms.Label();
            this.pB_icon = new System.Windows.Forms.PictureBox();
            this.cB_active = new System.Windows.Forms.CheckBox();
            this.lbl_uncheckToDeactivate = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.crossCursorMode1 = new JustGestures.ControlItems.CrossCursorMode();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pB_icon)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crossCursorMode1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tB_path
            // 
            this.tB_path.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tB_path.Location = new System.Drawing.Point(0, 17);
            this.tB_path.Name = "tB_path";
            this.tB_path.ReadOnly = true;
            this.tB_path.Size = new System.Drawing.Size(444, 20);
            this.tB_path.TabIndex = 0;
            // 
            // btn_browse
            // 
            this.btn_browse.Location = new System.Drawing.Point(6, 45);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(95, 23);
            this.btn_browse.TabIndex = 1;
            this.btn_browse.Text = "Browse";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // tB_name
            // 
            this.tB_name.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tB_name.Location = new System.Drawing.Point(0, 19);
            this.tB_name.Name = "tB_name";
            this.tB_name.Size = new System.Drawing.Size(444, 20);
            this.tB_name.TabIndex = 3;
            this.tB_name.TextChanged += new System.EventHandler(this.tB_name_TextChanged);
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(-3, 5);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(35, 13);
            this.lbl_name.TabIndex = 4;
            this.lbl_name.Text = "Name";
            // 
            // lbl_path
            // 
            this.lbl_path.AutoSize = true;
            this.lbl_path.Location = new System.Drawing.Point(-3, 1);
            this.lbl_path.Name = "lbl_path";
            this.lbl_path.Size = new System.Drawing.Size(29, 13);
            this.lbl_path.TabIndex = 5;
            this.lbl_path.Text = "Path";
            // 
            // lbl_icon
            // 
            this.lbl_icon.AutoSize = true;
            this.lbl_icon.Location = new System.Drawing.Point(-3, 109);
            this.lbl_icon.Name = "lbl_icon";
            this.lbl_icon.Size = new System.Drawing.Size(28, 13);
            this.lbl_icon.TabIndex = 7;
            this.lbl_icon.Text = "Icon";
            // 
            // pB_icon
            // 
            this.pB_icon.BackColor = System.Drawing.SystemColors.Control;
            this.pB_icon.Location = new System.Drawing.Point(0, 125);
            this.pB_icon.Name = "pB_icon";
            this.pB_icon.Size = new System.Drawing.Size(32, 32);
            this.pB_icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pB_icon.TabIndex = 6;
            this.pB_icon.TabStop = false;
            // 
            // cB_active
            // 
            this.cB_active.AutoSize = true;
            this.cB_active.Checked = true;
            this.cB_active.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_active.Location = new System.Drawing.Point(0, 169);
            this.cB_active.Name = "cB_active";
            this.cB_active.Size = new System.Drawing.Size(56, 17);
            this.cB_active.TabIndex = 8;
            this.cB_active.Text = "Active";
            this.cB_active.UseVisualStyleBackColor = true;
            this.cB_active.CheckedChanged += new System.EventHandler(this.cB_active_CheckedChanged);
            // 
            // lbl_uncheckToDeactivate
            // 
            this.lbl_uncheckToDeactivate.AutoSize = true;
            this.lbl_uncheckToDeactivate.Location = new System.Drawing.Point(-3, 189);
            this.lbl_uncheckToDeactivate.Name = "lbl_uncheckToDeactivate";
            this.lbl_uncheckToDeactivate.Size = new System.Drawing.Size(307, 13);
            this.lbl_uncheckToDeactivate.TabIndex = 9;
            this.lbl_uncheckToDeactivate.Text = "(Uncheck if you don\'t want the application group to be actived.)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.crossCursorMode1);
            this.panel1.Controls.Add(this.btn_browse);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(444, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(104, 311);
            this.panel1.TabIndex = 11;
            // 
            // crossCursorMode1
            // 
            this.crossCursorMode1.Image = ((System.Drawing.Image)(resources.GetObject("crossCursorMode1.Image")));
            this.crossCursorMode1.Location = new System.Drawing.Point(35, 74);
            this.crossCursorMode1.Name = "crossCursorMode1";
            this.crossCursorMode1.Size = new System.Drawing.Size(32, 32);
            this.crossCursorMode1.TabIndex = 2;
            this.crossCursorMode1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(444, 106);
            this.panel2.TabIndex = 12;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tB_path);
            this.panel4.Controls.Add(this.lbl_path);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 30);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(444, 37);
            this.panel4.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tB_name);
            this.panel3.Controls.Add(this.lbl_name);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 67);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(444, 39);
            this.panel3.TabIndex = 0;
            // 
            // UC_selectProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbl_uncheckToDeactivate);
            this.Controls.Add(this.cB_active);
            this.Controls.Add(this.lbl_icon);
            this.Controls.Add(this.pB_icon);
            this.Name = "UC_selectProgram";
            this.Size = new System.Drawing.Size(548, 311);
            this.Load += new System.EventHandler(this.UC_selectProgram_Load);
            this.VisibleChanged += new System.EventHandler(this.UC_selectProgram_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pB_icon)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.crossCursorMode1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tB_path;
        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.TextBox tB_name;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Label lbl_path;
        private System.Windows.Forms.PictureBox pB_icon;
        private System.Windows.Forms.Label lbl_icon;
        private System.Windows.Forms.CheckBox cB_active;
        private System.Windows.Forms.Label lbl_uncheckToDeactivate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private CrossCursorMode crossCursorMode1;
    }
}
