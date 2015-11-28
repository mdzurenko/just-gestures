namespace JustGestures.ControlItems
{
    partial class UC_plainText
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
            this.tB_text = new System.Windows.Forms.TextBox();
            this.lbl_text = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tB_text
            // 
            this.tB_text.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tB_text.Location = new System.Drawing.Point(0, 115);
            this.tB_text.Name = "tB_text";
            this.tB_text.Size = new System.Drawing.Size(550, 20);
            this.tB_text.TabIndex = 0;
            this.tB_text.TextChanged += new System.EventHandler(this.tB_text_TextChanged);
            // 
            // lbl_text
            // 
            this.lbl_text.AutoSize = true;
            this.lbl_text.Location = new System.Drawing.Point(-3, 99);
            this.lbl_text.Name = "lbl_text";
            this.lbl_text.Size = new System.Drawing.Size(28, 13);
            this.lbl_text.TabIndex = 1;
            this.lbl_text.Text = "Text";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tB_text);
            this.panel1.Controls.Add(this.lbl_text);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(550, 135);
            this.panel1.TabIndex = 2;
            // 
            // UC_plainText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UC_plainText";
            this.Size = new System.Drawing.Size(550, 380);
            this.Load += new System.EventHandler(this.UC_plainText_Load);
            this.VisibleChanged += new System.EventHandler(this.UC_plainText_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tB_text;
        private System.Windows.Forms.Label lbl_text;
        private System.Windows.Forms.Panel panel1;
    }
}
