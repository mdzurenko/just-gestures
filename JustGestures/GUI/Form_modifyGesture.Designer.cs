namespace JustGestures.GUI
{
    partial class Form_modifyGesture
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
            this.panel_down = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_buttons = new System.Windows.Forms.Panel();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.uC_infoIcon1 = new JustGestures.ControlItems.UC_infoIcon();
            this.panel_fill = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.panel_down.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel_buttons.SuspendLayout();
            this.panel_fill.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_down
            // 
            this.panel_down.Controls.Add(this.panel1);
            this.panel_down.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_down.Location = new System.Drawing.Point(0, 489);
            this.panel_down.Name = "panel_down";
            this.panel_down.Size = new System.Drawing.Size(574, 36);
            this.panel_down.TabIndex = 1;
            this.panel_down.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_down_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel_buttons);
            this.panel1.Controls.Add(this.uC_infoIcon1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(574, 32);
            this.panel1.TabIndex = 5;
            // 
            // panel_buttons
            // 
            this.panel_buttons.Controls.Add(this.btn_ok);
            this.panel_buttons.Controls.Add(this.btn_cancel);
            this.panel_buttons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_buttons.Location = new System.Drawing.Point(326, 0);
            this.panel_buttons.Name = "panel_buttons";
            this.panel_buttons.Size = new System.Drawing.Size(248, 32);
            this.panel_buttons.TabIndex = 3;
            // 
            // btn_ok
            // 
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(47, 5);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(82, 23);
            this.btn_ok.TabIndex = 0;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(147, 5);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(81, 23);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // uC_infoIcon1
            // 
            this.uC_infoIcon1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uC_infoIcon1.Location = new System.Drawing.Point(0, 0);
            this.uC_infoIcon1.Name = "uC_infoIcon1";
            this.uC_infoIcon1.Size = new System.Drawing.Size(70, 32);
            this.uC_infoIcon1.TabIndex = 4;
            // 
            // panel_fill
            // 
            this.panel_fill.Controls.Add(this.tabControl1);
            this.panel_fill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_fill.Location = new System.Drawing.Point(0, 0);
            this.panel_fill.Name = "panel_fill";
            this.panel_fill.Padding = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.panel_fill.Size = new System.Drawing.Size(574, 489);
            this.panel_fill.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(10, 15);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(10, 3);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(554, 459);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Deselecting);
            // 
            // Form_modifyGesture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 525);
            this.Controls.Add(this.panel_fill);
            this.Controls.Add(this.panel_down);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form_modifyGesture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_modifyGesture";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_modifyGesture_FormClosing);
            this.Load += new System.EventHandler(this.Form_modifyGesture_Load);
            this.panel_down.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel_buttons.ResumeLayout(false);
            this.panel_fill.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_down;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Panel panel_fill;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Panel panel_buttons;
        private JustGestures.ControlItems.UC_infoIcon uC_infoIcon1;
        private System.Windows.Forms.Panel panel1;
    }
}