namespace JustGestures.GUI
{
    partial class Form_options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_options));
            this.tW_options = new System.Windows.Forms.TreeView();
            this.panel_left = new System.Windows.Forms.Panel();
            this.panel_down = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uC_infoIcon1 = new JustGestures.ControlItems.UC_infoIcon();
            this.panel_forBtns = new System.Windows.Forms.Panel();
            this.btn_apply = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.panel_mid = new System.Windows.Forms.Panel();
            this.panel_rightMid = new System.Windows.Forms.Panel();
            this.panel_fill = new System.Windows.Forms.Panel();
            this.panel_up = new System.Windows.Forms.Panel();
            this.pB_caption = new System.Windows.Forms.PictureBox();
            this.panel_left.SuspendLayout();
            this.panel_down.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel_forBtns.SuspendLayout();
            this.panel_mid.SuspendLayout();
            this.panel_rightMid.SuspendLayout();
            this.panel_up.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_caption)).BeginInit();
            this.SuspendLayout();
            // 
            // tW_options
            // 
            this.tW_options.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tW_options.Location = new System.Drawing.Point(0, 0);
            this.tW_options.Name = "tW_options";
            this.tW_options.ShowPlusMinus = false;
            this.tW_options.ShowRootLines = false;
            this.tW_options.Size = new System.Drawing.Size(143, 471);
            this.tW_options.TabIndex = 0;
            this.tW_options.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.tW_options_BeforeCollapse);
            this.tW_options.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tW_options_AfterSelect);
            // 
            // panel_left
            // 
            this.panel_left.Controls.Add(this.tW_options);
            this.panel_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_left.Location = new System.Drawing.Point(0, 0);
            this.panel_left.Name = "panel_left";
            this.panel_left.Size = new System.Drawing.Size(143, 471);
            this.panel_left.TabIndex = 1;
            // 
            // panel_down
            // 
            this.panel_down.Controls.Add(this.panel1);
            this.panel_down.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_down.Location = new System.Drawing.Point(0, 471);
            this.panel_down.Name = "panel_down";
            this.panel_down.Size = new System.Drawing.Size(657, 34);
            this.panel_down.TabIndex = 2;
            this.panel_down.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_down_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uC_infoIcon1);
            this.panel1.Controls.Add(this.panel_forBtns);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(657, 29);
            this.panel1.TabIndex = 3;
            // 
            // uC_infoIcon1
            // 
            this.uC_infoIcon1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uC_infoIcon1.Location = new System.Drawing.Point(0, 0);
            this.uC_infoIcon1.Name = "uC_infoIcon1";
            this.uC_infoIcon1.Size = new System.Drawing.Size(70, 29);
            this.uC_infoIcon1.TabIndex = 3;
            // 
            // panel_forBtns
            // 
            this.panel_forBtns.Controls.Add(this.btn_apply);
            this.panel_forBtns.Controls.Add(this.btn_cancel);
            this.panel_forBtns.Controls.Add(this.btn_ok);
            this.panel_forBtns.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_forBtns.Location = new System.Drawing.Point(285, 0);
            this.panel_forBtns.Name = "panel_forBtns";
            this.panel_forBtns.Size = new System.Drawing.Size(372, 29);
            this.panel_forBtns.TabIndex = 2;
            // 
            // btn_apply
            // 
            this.btn_apply.Enabled = false;
            this.btn_apply.Location = new System.Drawing.Point(277, 3);
            this.btn_apply.Name = "btn_apply";
            this.btn_apply.Size = new System.Drawing.Size(92, 21);
            this.btn_apply.TabIndex = 2;
            this.btn_apply.Text = "Apply";
            this.btn_apply.UseVisualStyleBackColor = true;
            this.btn_apply.Click += new System.EventHandler(this.btn_apply_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(178, 3);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(93, 21);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(90, 3);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(82, 21);
            this.btn_ok.TabIndex = 0;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // panel_mid
            // 
            this.panel_mid.Controls.Add(this.panel_rightMid);
            this.panel_mid.Controls.Add(this.panel_left);
            this.panel_mid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_mid.Location = new System.Drawing.Point(0, 0);
            this.panel_mid.Name = "panel_mid";
            this.panel_mid.Size = new System.Drawing.Size(657, 471);
            this.panel_mid.TabIndex = 3;
            // 
            // panel_rightMid
            // 
            this.panel_rightMid.Controls.Add(this.panel_fill);
            this.panel_rightMid.Controls.Add(this.panel_up);
            this.panel_rightMid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_rightMid.Location = new System.Drawing.Point(143, 0);
            this.panel_rightMid.Name = "panel_rightMid";
            this.panel_rightMid.Size = new System.Drawing.Size(514, 471);
            this.panel_rightMid.TabIndex = 2;
            // 
            // panel_fill
            // 
            this.panel_fill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_fill.Location = new System.Drawing.Point(0, 30);
            this.panel_fill.Name = "panel_fill";
            this.panel_fill.Padding = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.panel_fill.Size = new System.Drawing.Size(514, 441);
            this.panel_fill.TabIndex = 1;
            // 
            // panel_up
            // 
            this.panel_up.Controls.Add(this.pB_caption);
            this.panel_up.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_up.Location = new System.Drawing.Point(0, 0);
            this.panel_up.Name = "panel_up";
            this.panel_up.Size = new System.Drawing.Size(514, 30);
            this.panel_up.TabIndex = 0;
            // 
            // pB_caption
            // 
            this.pB_caption.BackColor = System.Drawing.SystemColors.Control;
            this.pB_caption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pB_caption.Location = new System.Drawing.Point(0, 0);
            this.pB_caption.Name = "pB_caption";
            this.pB_caption.Size = new System.Drawing.Size(514, 30);
            this.pB_caption.TabIndex = 0;
            this.pB_caption.TabStop = false;
            // 
            // Form_options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 505);
            this.Controls.Add(this.panel_mid);
            this.Controls.Add(this.panel_down);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_options_FormClosing);
            this.Load += new System.EventHandler(this.Form_options_Load);
            this.panel_left.ResumeLayout(false);
            this.panel_down.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel_forBtns.ResumeLayout(false);
            this.panel_mid.ResumeLayout(false);
            this.panel_rightMid.ResumeLayout(false);
            this.panel_up.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pB_caption)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tW_options;
        private System.Windows.Forms.Panel panel_left;
        private System.Windows.Forms.Panel panel_down;
        private System.Windows.Forms.Panel panel_mid;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Panel panel_rightMid;
        private System.Windows.Forms.Panel panel_up;
        private System.Windows.Forms.Panel panel_fill;
        private System.Windows.Forms.Panel panel_forBtns;
        private System.Windows.Forms.Button btn_apply;
        private System.Windows.Forms.PictureBox pB_caption;
        private System.Windows.Forms.Panel panel1;
        private JustGestures.ControlItems.UC_infoIcon uC_infoIcon1;

    }
}