namespace JustGestures.ControlItems
{
    partial class UC_openPrgFld
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_openPrgFld));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tB_path = new System.Windows.Forms.TextBox();
            this.lbl_path = new System.Windows.Forms.Label();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.cB_wndStyle = new System.Windows.Forms.ComboBox();
            this.label_wndState = new System.Windows.Forms.Label();
            this.lbl_wndStyle = new System.Windows.Forms.Label();
            this.panel_right = new System.Windows.Forms.Panel();
            this.crossCursorMode1 = new JustGestures.ControlItems.CrossCursorMode();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_icon = new System.Windows.Forms.Panel();
            this.lbl_icon = new System.Windows.Forms.Label();
            this.pB_icon = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel_cmdArguments = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbl_arguments = new System.Windows.Forms.Label();
            this.tB_cmdArguments = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel_right.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crossCursorMode1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel_icon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_icon)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel_cmdArguments.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Programs (*.exe)|*.exe|All Files (*.*)|*.*";
            // 
            // tB_path
            // 
            this.tB_path.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tB_path.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tB_path.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tB_path.Location = new System.Drawing.Point(0, 83);
            this.tB_path.Name = "tB_path";
            this.tB_path.Size = new System.Drawing.Size(470, 20);
            this.tB_path.TabIndex = 0;
            this.tB_path.Text = "[Path]";
            this.tB_path.TextChanged += new System.EventHandler(this.tB_path_TextChanged);
            this.tB_path.Leave += new System.EventHandler(this.tB_path_Leave);
            // 
            // lbl_path
            // 
            this.lbl_path.AutoSize = true;
            this.lbl_path.Location = new System.Drawing.Point(-3, 67);
            this.lbl_path.Name = "lbl_path";
            this.lbl_path.Size = new System.Drawing.Size(39, 13);
            this.lbl_path.TabIndex = 1;
            this.lbl_path.Text = "[name]";
            // 
            // btn_Browse
            // 
            this.btn_Browse.Location = new System.Drawing.Point(12, 82);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(105, 21);
            this.btn_Browse.TabIndex = 2;
            this.btn_Browse.Text = "Browse";
            this.btn_Browse.UseVisualStyleBackColor = true;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // cB_wndStyle
            // 
            this.cB_wndStyle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cB_wndStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_wndStyle.FormattingEnabled = true;
            this.cB_wndStyle.Location = new System.Drawing.Point(0, 26);
            this.cB_wndStyle.Name = "cB_wndStyle";
            this.cB_wndStyle.Size = new System.Drawing.Size(165, 21);
            this.cB_wndStyle.TabIndex = 3;
            this.cB_wndStyle.SelectedIndexChanged += new System.EventHandler(this.cB_wndStyle_SelectedIndexChanged);
            // 
            // label_wndState
            // 
            this.label_wndState.AutoSize = true;
            this.label_wndState.Location = new System.Drawing.Point(3, 122);
            this.label_wndState.Name = "label_wndState";
            this.label_wndState.Size = new System.Drawing.Size(0, 13);
            this.label_wndState.TabIndex = 4;
            // 
            // lbl_wndStyle
            // 
            this.lbl_wndStyle.AutoSize = true;
            this.lbl_wndStyle.Location = new System.Drawing.Point(-3, 10);
            this.lbl_wndStyle.Name = "lbl_wndStyle";
            this.lbl_wndStyle.Size = new System.Drawing.Size(112, 13);
            this.lbl_wndStyle.TabIndex = 5;
            this.lbl_wndStyle.Text = "Open in Window Style";
            // 
            // panel_right
            // 
            this.panel_right.Controls.Add(this.crossCursorMode1);
            this.panel_right.Controls.Add(this.btn_Browse);
            this.panel_right.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_right.Location = new System.Drawing.Point(470, 0);
            this.panel_right.Name = "panel_right";
            this.panel_right.Size = new System.Drawing.Size(120, 406);
            this.panel_right.TabIndex = 9;
            // 
            // crossCursorMode1
            // 
            this.crossCursorMode1.Image = ((System.Drawing.Image)(resources.GetObject("crossCursorMode1.Image")));
            this.crossCursorMode1.Location = new System.Drawing.Point(45, 119);
            this.crossCursorMode1.Name = "crossCursorMode1";
            this.crossCursorMode1.Size = new System.Drawing.Size(32, 32);
            this.crossCursorMode1.TabIndex = 3;
            this.crossCursorMode1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel_icon);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel_cmdArguments);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(470, 406);
            this.panel1.TabIndex = 10;
            // 
            // panel_icon
            // 
            this.panel_icon.Controls.Add(this.lbl_icon);
            this.panel_icon.Controls.Add(this.pB_icon);
            this.panel_icon.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_icon.Location = new System.Drawing.Point(0, 198);
            this.panel_icon.Name = "panel_icon";
            this.panel_icon.Size = new System.Drawing.Size(470, 54);
            this.panel_icon.TabIndex = 11;
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
            // panel5
            // 
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 151);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(470, 47);
            this.panel5.TabIndex = 10;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.lbl_wndStyle);
            this.panel6.Controls.Add(this.cB_wndStyle);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(165, 47);
            this.panel6.TabIndex = 0;
            // 
            // panel_cmdArguments
            // 
            this.panel_cmdArguments.Controls.Add(this.panel4);
            this.panel_cmdArguments.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_cmdArguments.Location = new System.Drawing.Point(0, 103);
            this.panel_cmdArguments.Name = "panel_cmdArguments";
            this.panel_cmdArguments.Size = new System.Drawing.Size(470, 48);
            this.panel_cmdArguments.TabIndex = 9;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lbl_arguments);
            this.panel4.Controls.Add(this.tB_cmdArguments);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(293, 48);
            this.panel4.TabIndex = 9;
            // 
            // lbl_arguments
            // 
            this.lbl_arguments.AutoSize = true;
            this.lbl_arguments.Location = new System.Drawing.Point(-3, 12);
            this.lbl_arguments.Name = "lbl_arguments";
            this.lbl_arguments.Size = new System.Drawing.Size(126, 13);
            this.lbl_arguments.TabIndex = 11;
            this.lbl_arguments.Text = "Command-line Arguments";
            // 
            // tB_cmdArguments
            // 
            this.tB_cmdArguments.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tB_cmdArguments.Location = new System.Drawing.Point(0, 28);
            this.tB_cmdArguments.Name = "tB_cmdArguments";
            this.tB_cmdArguments.Size = new System.Drawing.Size(293, 20);
            this.tB_cmdArguments.TabIndex = 10;
            this.tB_cmdArguments.TextChanged += new System.EventHandler(this.tB_cmdArguments_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tB_path);
            this.panel2.Controls.Add(this.lbl_path);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(470, 103);
            this.panel2.TabIndex = 6;
            // 
            // UC_openPrgFld
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_right);
            this.Controls.Add(this.label_wndState);
            this.Name = "UC_openPrgFld";
            this.Size = new System.Drawing.Size(590, 406);
            this.Load += new System.EventHandler(this.UC_openPrgWwwFld_Load);
            this.VisibleChanged += new System.EventHandler(this.UC_openPrgWwwFld_VisibleChanged);
            this.panel_right.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.crossCursorMode1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel_icon.ResumeLayout(false);
            this.panel_icon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_icon)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel_cmdArguments.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox tB_path;
        private System.Windows.Forms.Label lbl_path;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.ComboBox cB_wndStyle;
        private System.Windows.Forms.Label label_wndState;
        private System.Windows.Forms.Label lbl_wndStyle;
        private System.Windows.Forms.Panel panel_right;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel_cmdArguments;
        private System.Windows.Forms.Label lbl_arguments;
        private System.Windows.Forms.TextBox tB_cmdArguments;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel_icon;
        private System.Windows.Forms.Label lbl_icon;
        private System.Windows.Forms.PictureBox pB_icon;
        private CrossCursorMode crossCursorMode1;
    }
}
