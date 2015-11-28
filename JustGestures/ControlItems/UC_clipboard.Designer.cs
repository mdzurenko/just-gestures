namespace JustGestures.ControlItems
{
    partial class UC_clipboard
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
            this.lbl_clipboard = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cB_clipboards = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_clipboard
            // 
            this.lbl_clipboard.AutoSize = true;
            this.lbl_clipboard.Location = new System.Drawing.Point(3, 100);
            this.lbl_clipboard.Name = "lbl_clipboard";
            this.lbl_clipboard.Size = new System.Drawing.Size(100, 13);
            this.lbl_clipboard.TabIndex = 0;
            this.lbl_clipboard.Text = "Copy/Paste to/from";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(477, 118);
            this.panel1.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cB_clipboards);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(115, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(362, 118);
            this.panel3.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbl_clipboard);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(115, 118);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(346, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(16, 118);
            this.panel4.TabIndex = 4;
            // 
            // cB_clipboards
            // 
            this.cB_clipboards.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cB_clipboards.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_clipboards.FormattingEnabled = true;
            this.cB_clipboards.Items.AddRange(new object[] {
            "Clipboard 1",
            "Clipboard 2",
            "Clipboard 3",
            "Clipboard 4",
            "Clipboard 5",
            "Clipboard 6",
            "Clipboard 7",
            "Clipboard 8",
            "Clipboard 9"});
            this.cB_clipboards.Location = new System.Drawing.Point(0, 97);
            this.cB_clipboards.Name = "cB_clipboards";
            this.cB_clipboards.Size = new System.Drawing.Size(346, 21);
            this.cB_clipboards.TabIndex = 5;
            this.cB_clipboards.SelectedIndexChanged += new System.EventHandler(this.cB_clipboards_SelectedIndexChanged);
            // 
            // UC_clipboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UC_clipboard";
            this.Size = new System.Drawing.Size(477, 364);
            this.VisibleChanged += new System.EventHandler(this.UC_clipboard_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_clipboard;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cB_clipboards;
        private System.Windows.Forms.Panel panel4;
    }
}
