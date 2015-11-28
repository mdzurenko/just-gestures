namespace JustGestures.ControlItems
{
    partial class UC_mailSearchWeb
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
            this.lbl_icon = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tB_address = new System.Windows.Forms.TextBox();
            this.lbl_address = new System.Windows.Forms.Label();
            this.pB_icon = new System.Windows.Forms.PictureBox();
            this.panel_icon = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btn_obtainIcon = new System.Windows.Forms.Button();
            this.panel_right = new System.Windows.Forms.Panel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label_wndState = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_icon)).BeginInit();
            this.panel_icon.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel_right.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_icon
            // 
            this.lbl_icon.AutoSize = true;
            this.lbl_icon.Location = new System.Drawing.Point(-3, 6);
            this.lbl_icon.Name = "lbl_icon";
            this.lbl_icon.Size = new System.Drawing.Size(28, 13);
            this.lbl_icon.TabIndex = 3;
            this.lbl_icon.Text = "Icon";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tB_address);
            this.panel2.Controls.Add(this.lbl_address);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(429, 103);
            this.panel2.TabIndex = 6;
            // 
            // tB_address
            // 
            this.tB_address.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tB_address.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.tB_address.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tB_address.Location = new System.Drawing.Point(0, 83);
            this.tB_address.Name = "tB_address";
            this.tB_address.Size = new System.Drawing.Size(429, 20);
            this.tB_address.TabIndex = 0;
            this.tB_address.Text = "[Address]";
            this.tB_address.TextChanged += new System.EventHandler(this.tB_address_TextChanged);
            this.tB_address.Leave += new System.EventHandler(this.tB_address_Leave);
            // 
            // lbl_address
            // 
            this.lbl_address.AutoSize = true;
            this.lbl_address.Location = new System.Drawing.Point(-3, 67);
            this.lbl_address.Name = "lbl_address";
            this.lbl_address.Size = new System.Drawing.Size(39, 13);
            this.lbl_address.TabIndex = 1;
            this.lbl_address.Text = "[name]";
            // 
            // pB_icon
            // 
            this.pB_icon.BackColor = System.Drawing.SystemColors.Control;
            this.pB_icon.Location = new System.Drawing.Point(0, 22);
            this.pB_icon.Name = "pB_icon";
            this.pB_icon.Size = new System.Drawing.Size(32, 32);
            this.pB_icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pB_icon.TabIndex = 1;
            this.pB_icon.TabStop = false;
            // 
            // panel_icon
            // 
            this.panel_icon.Controls.Add(this.lbl_icon);
            this.panel_icon.Controls.Add(this.pB_icon);
            this.panel_icon.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_icon.Location = new System.Drawing.Point(0, 103);
            this.panel_icon.Name = "panel_icon";
            this.panel_icon.Size = new System.Drawing.Size(429, 54);
            this.panel_icon.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel_icon);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(429, 397);
            this.panel1.TabIndex = 13;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Programs (*.exe)|*.exe|All Files (*.*)|*.*";
            // 
            // btn_obtainIcon
            // 
            this.btn_obtainIcon.Location = new System.Drawing.Point(6, 82);
            this.btn_obtainIcon.Name = "btn_obtainIcon";
            this.btn_obtainIcon.Size = new System.Drawing.Size(116, 21);
            this.btn_obtainIcon.TabIndex = 2;
            this.btn_obtainIcon.Text = "Get Web Icon";
            this.btn_obtainIcon.UseVisualStyleBackColor = true;
            this.btn_obtainIcon.Click += new System.EventHandler(this.btn_obtainIcon_Click);
            // 
            // panel_right
            // 
            this.panel_right.Controls.Add(this.btn_obtainIcon);
            this.panel_right.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_right.Location = new System.Drawing.Point(429, 0);
            this.panel_right.Name = "panel_right";
            this.panel_right.Size = new System.Drawing.Size(133, 397);
            this.panel_right.TabIndex = 12;
            // 
            // label_wndState
            // 
            this.label_wndState.AutoSize = true;
            this.label_wndState.Location = new System.Drawing.Point(3, 122);
            this.label_wndState.Name = "label_wndState";
            this.label_wndState.Size = new System.Drawing.Size(0, 13);
            this.label_wndState.TabIndex = 11;
            // 
            // UC_mailSearchWeb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_right);
            this.Controls.Add(this.label_wndState);
            this.Name = "UC_mailSearchWeb";
            this.Size = new System.Drawing.Size(562, 397);
            this.VisibleChanged += new System.EventHandler(this.UC_mailSearchWeb_VisibleChanged);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_icon)).EndInit();
            this.panel_icon.ResumeLayout(false);
            this.panel_icon.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel_right.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_icon;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tB_address;
        private System.Windows.Forms.Label lbl_address;
        private System.Windows.Forms.PictureBox pB_icon;
        private System.Windows.Forms.Panel panel_icon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btn_obtainIcon;
        private System.Windows.Forms.Panel panel_right;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label_wndState;
    }
}
