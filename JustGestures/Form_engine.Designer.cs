namespace JustGestures
{
    partial class Form_engine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_engine));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.cMS_SystemTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tSMI_show = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMI_about = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMI_autobehave_on = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMI_autobehave_off = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMI_enable = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMI_disable = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMI_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.cMS_SystemTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.cMS_SystemTray;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Just Gestures";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // cMS_SystemTray
            // 
            this.cMS_SystemTray.ImageScalingSize = new System.Drawing.Size(17, 17);
            this.cMS_SystemTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMI_show,
            this.tSMI_about,
            this.tSMI_autobehave_on,
            this.tSMI_autobehave_off,
            this.tSMI_enable,
            this.tSMI_disable,
            this.tSMI_exit});
            this.cMS_SystemTray.Name = "contextMenuStrip1";
            this.cMS_SystemTray.Size = new System.Drawing.Size(167, 194);
            // 
            // tSMI_show
            // 
            this.tSMI_show.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tSMI_show.Image = global::JustGestures.Properties.Resources.main_form;
            this.tSMI_show.Name = "tSMI_show";
            this.tSMI_show.Size = new System.Drawing.Size(166, 24);
            this.tSMI_show.Text = "Show";
            this.tSMI_show.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // tSMI_about
            // 
            this.tSMI_about.Image = global::JustGestures.Properties.Resources.about;
            this.tSMI_about.Name = "tSMI_about";
            this.tSMI_about.Size = new System.Drawing.Size(166, 24);
            this.tSMI_about.Text = "About";
            this.tSMI_about.Click += new System.EventHandler(this.tSMI_about_Click);
            // 
            // tSMI_autobehave_on
            // 
            this.tSMI_autobehave_on.Image = global::JustGestures.Properties.Resources.auto_on;
            this.tSMI_autobehave_on.Name = "tSMI_autobehave_on";
            this.tSMI_autobehave_on.Size = new System.Drawing.Size(166, 24);
            this.tSMI_autobehave_on.Text = "Autobehave Start";
            this.tSMI_autobehave_on.Click += new System.EventHandler(this.tSMI_cM_autobehave_Click);
            // 
            // tSMI_autobehave_off
            // 
            this.tSMI_autobehave_off.Image = global::JustGestures.Properties.Resources.auto_off;
            this.tSMI_autobehave_off.Name = "tSMI_autobehave_off";
            this.tSMI_autobehave_off.Size = new System.Drawing.Size(166, 24);
            this.tSMI_autobehave_off.Text = "Autobehave Stop";
            this.tSMI_autobehave_off.Visible = false;
            this.tSMI_autobehave_off.Click += new System.EventHandler(this.tSMI_cM_autobehave_Click);
            // 
            // tSMI_enable
            // 
            this.tSMI_enable.Image = global::JustGestures.Properties.Resources.enable;
            this.tSMI_enable.Name = "tSMI_enable";
            this.tSMI_enable.Size = new System.Drawing.Size(166, 24);
            this.tSMI_enable.Text = "Enable";
            this.tSMI_enable.Visible = false;
            this.tSMI_enable.Click += new System.EventHandler(this.tSMI_disable_Click);
            // 
            // tSMI_disable
            // 
            this.tSMI_disable.Image = global::JustGestures.Properties.Resources.disable;
            this.tSMI_disable.Name = "tSMI_disable";
            this.tSMI_disable.Size = new System.Drawing.Size(166, 24);
            this.tSMI_disable.Tag = "enableGestures";
            this.tSMI_disable.Text = "Disable";
            this.tSMI_disable.Click += new System.EventHandler(this.tSMI_disable_Click);
            // 
            // tSMI_exit
            // 
            this.tSMI_exit.Image = global::JustGestures.Properties.Resources.turnoff;
            this.tSMI_exit.Name = "tSMI_exit";
            this.tSMI_exit.Size = new System.Drawing.Size(166, 24);
            this.tSMI_exit.Text = "Exit";
            this.tSMI_exit.Click += new System.EventHandler(this.tSMI_exit_Click);
            // 
            // Form_engine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 62);
            this.Name = "Form_engine";
            this.Text = "Form_engine";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_engine_FormClosing);
            this.cMS_SystemTray.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip cMS_SystemTray;
        private System.Windows.Forms.ToolStripMenuItem tSMI_autobehave_on;
        private System.Windows.Forms.ToolStripMenuItem tSMI_disable;
        private System.Windows.Forms.ToolStripMenuItem tSMI_exit;
        private System.Windows.Forms.ToolStripMenuItem tSMI_about;
        private System.Windows.Forms.ToolStripMenuItem tSMI_show;
        private System.Windows.Forms.ToolStripMenuItem tSMI_autobehave_off;
        private System.Windows.Forms.ToolStripMenuItem tSMI_enable;
    }
}