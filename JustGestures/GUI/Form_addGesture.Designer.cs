namespace JustGestures.GUI
{
    partial class Form_addGesture
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_next = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.panel_ForButtons = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uC_infoIcon1 = new JustGestures.ControlItems.UC_infoIcon();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_Up = new System.Windows.Forms.Panel();
            this.pB_about = new System.Windows.Forms.PictureBox();
            this.panel_Middle = new System.Windows.Forms.Panel();
            this.groupBox_MainPanel = new System.Windows.Forms.Panel();
            this.panel_ForButtons.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel_Up.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_about)).BeginInit();
            this.panel_Middle.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_next
            // 
            this.btn_next.Location = new System.Drawing.Point(108, 3);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(89, 25);
            this.btn_next.TabIndex = 0;
            this.btn_next.Text = "Next >";
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.button_next_Click);
            // 
            // btn_back
            // 
            this.btn_back.Location = new System.Drawing.Point(3, 3);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(99, 25);
            this.btn_back.TabIndex = 1;
            this.btn_back.Text = "< Back";
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.button_back_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(228, 3);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(92, 25);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // panel_ForButtons
            // 
            this.panel_ForButtons.Controls.Add(this.panel1);
            this.panel_ForButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_ForButtons.Location = new System.Drawing.Point(0, 474);
            this.panel_ForButtons.Name = "panel_ForButtons";
            this.panel_ForButtons.Size = new System.Drawing.Size(540, 38);
            this.panel_ForButtons.TabIndex = 6;
            this.panel_ForButtons.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_ForButtons_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uC_infoIcon1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(540, 33);
            this.panel1.TabIndex = 3;
            // 
            // uC_infoIcon1
            // 
            this.uC_infoIcon1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uC_infoIcon1.Location = new System.Drawing.Point(0, 0);
            this.uC_infoIcon1.Name = "uC_infoIcon1";
            this.uC_infoIcon1.Size = new System.Drawing.Size(70, 33);
            this.uC_infoIcon1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_cancel);
            this.panel2.Controls.Add(this.btn_back);
            this.panel2.Controls.Add(this.btn_next);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(217, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(323, 33);
            this.panel2.TabIndex = 3;
            // 
            // panel_Up
            // 
            this.panel_Up.Controls.Add(this.pB_about);
            this.panel_Up.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Up.Location = new System.Drawing.Point(0, 0);
            this.panel_Up.Name = "panel_Up";
            this.panel_Up.Size = new System.Drawing.Size(540, 39);
            this.panel_Up.TabIndex = 7;
            // 
            // pB_about
            // 
            this.pB_about.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pB_about.Location = new System.Drawing.Point(0, 0);
            this.pB_about.Name = "pB_about";
            this.pB_about.Size = new System.Drawing.Size(540, 39);
            this.pB_about.TabIndex = 0;
            this.pB_about.TabStop = false;
            // 
            // panel_Middle
            // 
            this.panel_Middle.Controls.Add(this.groupBox_MainPanel);
            this.panel_Middle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Middle.Location = new System.Drawing.Point(0, 39);
            this.panel_Middle.Name = "panel_Middle";
            this.panel_Middle.Size = new System.Drawing.Size(540, 435);
            this.panel_Middle.TabIndex = 8;
            // 
            // groupBox_MainPanel
            // 
            this.groupBox_MainPanel.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox_MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_MainPanel.Location = new System.Drawing.Point(0, 0);
            this.groupBox_MainPanel.Name = "groupBox_MainPanel";
            this.groupBox_MainPanel.Padding = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.groupBox_MainPanel.Size = new System.Drawing.Size(540, 435);
            this.groupBox_MainPanel.TabIndex = 0;
            // 
            // Form_addGesture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 512);
            this.Controls.Add(this.panel_Middle);
            this.Controls.Add(this.panel_Up);
            this.Controls.Add(this.panel_ForButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form_addGesture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add New Gesture";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_addGesture_FormClosing);
            this.Load += new System.EventHandler(this.Form_addGesture_Load);
            this.panel_ForButtons.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel_Up.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pB_about)).EndInit();
            this.panel_Middle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Panel panel_ForButtons;
        private System.Windows.Forms.Panel panel_Up;
        private System.Windows.Forms.Panel panel_Middle;
        private System.Windows.Forms.PictureBox pB_about;
        private System.Windows.Forms.Panel groupBox_MainPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private JustGestures.ControlItems.UC_infoIcon uC_infoIcon1;
    }
}