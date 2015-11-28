namespace JustGestures.GUI
{
    partial class Form_about
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
            this.components = new System.ComponentModel.Container();
            this.label_version = new System.Windows.Forms.Label();
            this.lbl_createdBy = new System.Windows.Forms.Label();
            this.pB_paypal = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.linkLabel_forum = new System.Windows.Forms.LinkLabel();
            this.btn_checkUpdate = new System.Windows.Forms.Button();
            this.pB_logo = new System.Windows.Forms.PictureBox();
            this.pB_flattr = new System.Windows.Forms.PictureBox();
            this.rTB_submitBugs = new System.Windows.Forms.RichTextBox();
            this.panel_createdBy = new System.Windows.Forms.Panel();
            this.linkLabel_author = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pB_paypal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB_logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB_flattr)).BeginInit();
            this.panel_createdBy.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_version
            // 
            this.label_version.AutoSize = true;
            this.label_version.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_version.Location = new System.Drawing.Point(97, 28);
            this.label_version.Name = "label_version";
            this.label_version.Size = new System.Drawing.Size(113, 13);
            this.label_version.TabIndex = 0;
            this.label_version.Text = "Just Gestures v1.0";
            // 
            // lbl_createdBy
            // 
            this.lbl_createdBy.AutoSize = true;
            this.lbl_createdBy.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_createdBy.Location = new System.Drawing.Point(0, 0);
            this.lbl_createdBy.Name = "lbl_createdBy";
            this.lbl_createdBy.Size = new System.Drawing.Size(58, 13);
            this.lbl_createdBy.TabIndex = 1;
            this.lbl_createdBy.Text = "Created by";
            // 
            // pB_paypal
            // 
            this.pB_paypal.BackColor = System.Drawing.SystemColors.Control;
            this.pB_paypal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pB_paypal.Image = global::JustGestures.Properties.Resources.Paypal_DonateButton;
            this.pB_paypal.Location = new System.Drawing.Point(310, 56);
            this.pB_paypal.Name = "pB_paypal";
            this.pB_paypal.Size = new System.Drawing.Size(64, 32);
            this.pB_paypal.TabIndex = 6;
            this.pB_paypal.TabStop = false;
            this.pB_paypal.Click += new System.EventHandler(this.pB_paypal_Click);
            // 
            // linkLabel_forum
            // 
            this.linkLabel_forum.AutoSize = true;
            this.linkLabel_forum.Location = new System.Drawing.Point(97, 85);
            this.linkLabel_forum.Name = "linkLabel_forum";
            this.linkLabel_forum.Size = new System.Drawing.Size(146, 13);
            this.linkLabel_forum.TabIndex = 7;
            this.linkLabel_forum.TabStop = true;
            this.linkLabel_forum.Text = "http://forum.justgestures.com";
            this.linkLabel_forum.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_forum_LinkClicked);
            // 
            // btn_checkUpdate
            // 
            this.btn_checkUpdate.AutoSize = true;
            this.btn_checkUpdate.Location = new System.Drawing.Point(131, 162);
            this.btn_checkUpdate.Name = "btn_checkUpdate";
            this.btn_checkUpdate.Size = new System.Drawing.Size(124, 23);
            this.btn_checkUpdate.TabIndex = 9;
            this.btn_checkUpdate.Text = "Check for Update";
            this.btn_checkUpdate.UseVisualStyleBackColor = true;
            this.btn_checkUpdate.Click += new System.EventHandler(this.btn_checkUpdate_Click);
            // 
            // pB_logo
            // 
            this.pB_logo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pB_logo.Location = new System.Drawing.Point(19, 28);
            this.pB_logo.Name = "pB_logo";
            this.pB_logo.Size = new System.Drawing.Size(48, 48);
            this.pB_logo.TabIndex = 8;
            this.pB_logo.TabStop = false;
            this.pB_logo.Click += new System.EventHandler(this.pB_logo_Click);
            // 
            // pB_flattr
            // 
            this.pB_flattr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pB_flattr.Image = global::JustGestures.Properties.Resources.flattr_badge_large;
            this.pB_flattr.Location = new System.Drawing.Point(294, 28);
            this.pB_flattr.Name = "pB_flattr";
            this.pB_flattr.Size = new System.Drawing.Size(95, 22);
            this.pB_flattr.TabIndex = 11;
            this.pB_flattr.TabStop = false;
            this.pB_flattr.Click += new System.EventHandler(this.pB_flattr_Click);
            // 
            // rTB_submitBugs
            // 
            this.rTB_submitBugs.BackColor = System.Drawing.SystemColors.Control;
            this.rTB_submitBugs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rTB_submitBugs.Location = new System.Drawing.Point(71, 116);
            this.rTB_submitBugs.Name = "rTB_submitBugs";
            this.rTB_submitBugs.ReadOnly = true;
            this.rTB_submitBugs.Size = new System.Drawing.Size(238, 40);
            this.rTB_submitBugs.TabIndex = 12;
            this.rTB_submitBugs.Text = "(Please submit any suggestions or bugs in forum.)";
            // 
            // panel_createdBy
            // 
            this.panel_createdBy.Controls.Add(this.linkLabel_author);
            this.panel_createdBy.Controls.Add(this.panel1);
            this.panel_createdBy.Controls.Add(this.lbl_createdBy);
            this.panel_createdBy.Location = new System.Drawing.Point(97, 56);
            this.panel_createdBy.Name = "panel_createdBy";
            this.panel_createdBy.Size = new System.Drawing.Size(204, 15);
            this.panel_createdBy.TabIndex = 13;
            // 
            // linkLabel_author
            // 
            this.linkLabel_author.AutoSize = true;
            this.linkLabel_author.Dock = System.Windows.Forms.DockStyle.Left;
            this.linkLabel_author.Location = new System.Drawing.Point(68, 0);
            this.linkLabel_author.Name = "linkLabel_author";
            this.linkLabel_author.Size = new System.Drawing.Size(95, 13);
            this.linkLabel_author.TabIndex = 11;
            this.linkLabel_author.TabStop = true;
            this.linkLabel_author.Text = "Miroslav Dzurenko";
            this.linkLabel_author.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_author_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(58, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 15);
            this.panel1.TabIndex = 2;
            // 
            // Form_about
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 209);
            this.Controls.Add(this.panel_createdBy);
            this.Controls.Add(this.rTB_submitBugs);
            this.Controls.Add(this.pB_flattr);
            this.Controls.Add(this.btn_checkUpdate);
            this.Controls.Add(this.pB_logo);
            this.Controls.Add(this.linkLabel_forum);
            this.Controls.Add(this.pB_paypal);
            this.Controls.Add(this.label_version);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_about";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.Load += new System.EventHandler(this.Form_about_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pB_paypal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB_logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB_flattr)).EndInit();
            this.panel_createdBy.ResumeLayout(false);
            this.panel_createdBy.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_version;
        private System.Windows.Forms.Label lbl_createdBy;
        private System.Windows.Forms.PictureBox pB_paypal;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.LinkLabel linkLabel_forum;
        private System.Windows.Forms.Button btn_checkUpdate;
        private System.Windows.Forms.PictureBox pB_logo;
        private System.Windows.Forms.PictureBox pB_flattr;
        private System.Windows.Forms.RichTextBox rTB_submitBugs;
        private System.Windows.Forms.Panel panel_createdBy;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel linkLabel_author;
    }
}