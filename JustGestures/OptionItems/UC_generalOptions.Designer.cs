namespace JustGestures.OptionItems
{
    partial class UC_generalOptions
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
            this.cB_startup = new System.Windows.Forms.CheckBox();
            this.gB_startUpSettings = new System.Windows.Forms.GroupBox();
            this.cB_startMinimized = new System.Windows.Forms.CheckBox();
            this.cB_checkUpdate = new System.Windows.Forms.CheckBox();
            this.cB_autobehave = new System.Windows.Forms.CheckBox();
            this.gB_systemTraySettings = new System.Windows.Forms.GroupBox();
            this.cB_minToTray = new System.Windows.Forms.CheckBox();
            this.cB_closeToTray = new System.Windows.Forms.CheckBox();
            this.gB_localization = new System.Windows.Forms.GroupBox();
            this.cB_languages = new System.Windows.Forms.ComboBox();
            this.lbl_changeLanguage = new System.Windows.Forms.Label();
            this.gB_startUpSettings.SuspendLayout();
            this.gB_systemTraySettings.SuspendLayout();
            this.gB_localization.SuspendLayout();
            this.SuspendLayout();
            // 
            // cB_startup
            // 
            this.cB_startup.AutoSize = true;
            this.cB_startup.Checked = true;
            this.cB_startup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_startup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cB_startup.Location = new System.Drawing.Point(9, 19);
            this.cB_startup.Name = "cB_startup";
            this.cB_startup.Size = new System.Drawing.Size(181, 17);
            this.cB_startup.TabIndex = 0;
            this.cB_startup.Text = "Start automatically with Windows";
            this.cB_startup.UseVisualStyleBackColor = true;
            this.cB_startup.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // gB_startUpSettings
            // 
            this.gB_startUpSettings.Controls.Add(this.cB_startMinimized);
            this.gB_startUpSettings.Controls.Add(this.cB_checkUpdate);
            this.gB_startUpSettings.Controls.Add(this.cB_autobehave);
            this.gB_startUpSettings.Controls.Add(this.cB_startup);
            this.gB_startUpSettings.Location = new System.Drawing.Point(0, 0);
            this.gB_startUpSettings.Name = "gB_startUpSettings";
            this.gB_startUpSettings.Size = new System.Drawing.Size(400, 115);
            this.gB_startUpSettings.TabIndex = 8;
            this.gB_startUpSettings.TabStop = false;
            this.gB_startUpSettings.Text = "Start Up";
            // 
            // cB_startMinimized
            // 
            this.cB_startMinimized.AutoSize = true;
            this.cB_startMinimized.Location = new System.Drawing.Point(9, 42);
            this.cB_startMinimized.Name = "cB_startMinimized";
            this.cB_startMinimized.Size = new System.Drawing.Size(96, 17);
            this.cB_startMinimized.TabIndex = 8;
            this.cB_startMinimized.Text = "Start minimized";
            this.cB_startMinimized.UseVisualStyleBackColor = true;
            this.cB_startMinimized.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // cB_checkUpdate
            // 
            this.cB_checkUpdate.AutoSize = true;
            this.cB_checkUpdate.Checked = true;
            this.cB_checkUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_checkUpdate.Location = new System.Drawing.Point(9, 88);
            this.cB_checkUpdate.Name = "cB_checkUpdate";
            this.cB_checkUpdate.Size = new System.Drawing.Size(110, 17);
            this.cB_checkUpdate.TabIndex = 3;
            this.cB_checkUpdate.Text = "Check for Update";
            this.cB_checkUpdate.UseVisualStyleBackColor = true;
            this.cB_checkUpdate.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // cB_autobehave
            // 
            this.cB_autobehave.AutoSize = true;
            this.cB_autobehave.Location = new System.Drawing.Point(9, 65);
            this.cB_autobehave.Name = "cB_autobehave";
            this.cB_autobehave.Size = new System.Drawing.Size(187, 17);
            this.cB_autobehave.TabIndex = 2;
            this.cB_autobehave.Text = "Start automatically Autobehaviour ";
            this.cB_autobehave.UseVisualStyleBackColor = true;
            this.cB_autobehave.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // gB_systemTraySettings
            // 
            this.gB_systemTraySettings.Controls.Add(this.cB_minToTray);
            this.gB_systemTraySettings.Controls.Add(this.cB_closeToTray);
            this.gB_systemTraySettings.Location = new System.Drawing.Point(0, 139);
            this.gB_systemTraySettings.Name = "gB_systemTraySettings";
            this.gB_systemTraySettings.Size = new System.Drawing.Size(400, 67);
            this.gB_systemTraySettings.TabIndex = 12;
            this.gB_systemTraySettings.TabStop = false;
            this.gB_systemTraySettings.Text = "System Tray";
            // 
            // cB_minToTray
            // 
            this.cB_minToTray.AutoSize = true;
            this.cB_minToTray.Checked = true;
            this.cB_minToTray.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_minToTray.Location = new System.Drawing.Point(9, 19);
            this.cB_minToTray.Name = "cB_minToTray";
            this.cB_minToTray.Size = new System.Drawing.Size(186, 17);
            this.cB_minToTray.TabIndex = 4;
            this.cB_minToTray.Text = "Minimize minimizes to System Tray";
            this.cB_minToTray.UseVisualStyleBackColor = true;
            this.cB_minToTray.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // cB_closeToTray
            // 
            this.cB_closeToTray.AutoSize = true;
            this.cB_closeToTray.Location = new System.Drawing.Point(9, 42);
            this.cB_closeToTray.Name = "cB_closeToTray";
            this.cB_closeToTray.Size = new System.Drawing.Size(172, 17);
            this.cB_closeToTray.TabIndex = 5;
            this.cB_closeToTray.Text = "Close minimizes to System Tray";
            this.cB_closeToTray.UseVisualStyleBackColor = true;
            this.cB_closeToTray.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // gB_localization
            // 
            this.gB_localization.Controls.Add(this.cB_languages);
            this.gB_localization.Controls.Add(this.lbl_changeLanguage);
            this.gB_localization.Location = new System.Drawing.Point(0, 253);
            this.gB_localization.Name = "gB_localization";
            this.gB_localization.Size = new System.Drawing.Size(400, 48);
            this.gB_localization.TabIndex = 14;
            this.gB_localization.TabStop = false;
            this.gB_localization.Text = "Localization";
            // 
            // cB_languages
            // 
            this.cB_languages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_languages.FormattingEnabled = true;
            this.cB_languages.Location = new System.Drawing.Point(234, 19);
            this.cB_languages.Name = "cB_languages";
            this.cB_languages.Size = new System.Drawing.Size(160, 21);
            this.cB_languages.TabIndex = 1;
            this.cB_languages.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // lbl_changeLanguage
            // 
            this.lbl_changeLanguage.AutoSize = true;
            this.lbl_changeLanguage.Location = new System.Drawing.Point(6, 23);
            this.lbl_changeLanguage.Name = "lbl_changeLanguage";
            this.lbl_changeLanguage.Size = new System.Drawing.Size(109, 13);
            this.lbl_changeLanguage.TabIndex = 0;
            this.lbl_changeLanguage.Text = "Change the language";
            // 
            // UC_generalOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gB_localization);
            this.Controls.Add(this.gB_systemTraySettings);
            this.Controls.Add(this.gB_startUpSettings);
            this.Name = "UC_generalOptions";
            this.Size = new System.Drawing.Size(463, 358);
            this.gB_startUpSettings.ResumeLayout(false);
            this.gB_startUpSettings.PerformLayout();
            this.gB_systemTraySettings.ResumeLayout(false);
            this.gB_systemTraySettings.PerformLayout();
            this.gB_localization.ResumeLayout(false);
            this.gB_localization.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox cB_startup;
        private System.Windows.Forms.GroupBox gB_startUpSettings;
        private System.Windows.Forms.CheckBox cB_autobehave;
        private System.Windows.Forms.CheckBox cB_checkUpdate;
        private System.Windows.Forms.CheckBox cB_startMinimized;
        private System.Windows.Forms.GroupBox gB_systemTraySettings;
        private System.Windows.Forms.CheckBox cB_minToTray;
        private System.Windows.Forms.CheckBox cB_closeToTray;
        private System.Windows.Forms.GroupBox gB_localization;
        private System.Windows.Forms.Label lbl_changeLanguage;
        private System.Windows.Forms.ComboBox cB_languages;
    }
}
