namespace JustGestures.WizardItems
{
    partial class UC_W_activation
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
            this.lbl_typeOfWnd = new System.Windows.Forms.Label();
            this.cB_wndHandle = new System.Windows.Forms.ComboBox();
            this.nUD_deactivateTimeout = new System.Windows.Forms.NumericUpDown();
            this.cB_sensitiveZone = new System.Windows.Forms.ComboBox();
            this.lbl_deactivationTimeout = new System.Windows.Forms.Label();
            this.lbl_sensitiveZone = new System.Windows.Forms.Label();
            this.gB_gestureActivation = new System.Windows.Forms.GroupBox();
            this.panel_control_prg = new System.Windows.Forms.Panel();
            this.gB_description2 = new System.Windows.Forms.GroupBox();
            this.rTB_controlPrg = new System.Windows.Forms.RichTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel_zone_and_timeout = new System.Windows.Forms.Panel();
            this.gB_description1 = new System.Windows.Forms.GroupBox();
            this.rTB_zoneTimeout = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_deactivateTimeout)).BeginInit();
            this.gB_gestureActivation.SuspendLayout();
            this.panel_control_prg.SuspendLayout();
            this.gB_description2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel_zone_and_timeout.SuspendLayout();
            this.gB_description1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_typeOfWnd
            // 
            this.lbl_typeOfWnd.AutoSize = true;
            this.lbl_typeOfWnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_typeOfWnd.Location = new System.Drawing.Point(13, 14);
            this.lbl_typeOfWnd.Name = "lbl_typeOfWnd";
            this.lbl_typeOfWnd.Size = new System.Drawing.Size(198, 13);
            this.lbl_typeOfWnd.TabIndex = 16;
            this.lbl_typeOfWnd.Text = "Control Program/Window which is";
            // 
            // cB_wndHandle
            // 
            this.cB_wndHandle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_wndHandle.FormattingEnabled = true;
            this.cB_wndHandle.Location = new System.Drawing.Point(266, 11);
            this.cB_wndHandle.Name = "cB_wndHandle";
            this.cB_wndHandle.Size = new System.Drawing.Size(142, 21);
            this.cB_wndHandle.TabIndex = 17;
            // 
            // nUD_deactivateTimeout
            // 
            this.nUD_deactivateTimeout.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nUD_deactivateTimeout.Location = new System.Drawing.Point(266, 34);
            this.nUD_deactivateTimeout.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nUD_deactivateTimeout.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nUD_deactivateTimeout.Name = "nUD_deactivateTimeout";
            this.nUD_deactivateTimeout.Size = new System.Drawing.Size(142, 20);
            this.nUD_deactivateTimeout.TabIndex = 15;
            this.nUD_deactivateTimeout.Value = new decimal(new int[] {
            350,
            0,
            0,
            0});
            // 
            // cB_sensitiveZone
            // 
            this.cB_sensitiveZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_sensitiveZone.FormattingEnabled = true;
            this.cB_sensitiveZone.Location = new System.Drawing.Point(266, 7);
            this.cB_sensitiveZone.Name = "cB_sensitiveZone";
            this.cB_sensitiveZone.Size = new System.Drawing.Size(142, 21);
            this.cB_sensitiveZone.TabIndex = 14;
            // 
            // lbl_deactivationTimeout
            // 
            this.lbl_deactivationTimeout.AutoSize = true;
            this.lbl_deactivationTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_deactivationTimeout.Location = new System.Drawing.Point(13, 36);
            this.lbl_deactivationTimeout.Name = "lbl_deactivationTimeout";
            this.lbl_deactivationTimeout.Size = new System.Drawing.Size(161, 13);
            this.lbl_deactivationTimeout.TabIndex = 13;
            this.lbl_deactivationTimeout.Text = "Deactivation Timeout in ms";
            // 
            // lbl_sensitiveZone
            // 
            this.lbl_sensitiveZone.AutoSize = true;
            this.lbl_sensitiveZone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_sensitiveZone.Location = new System.Drawing.Point(13, 10);
            this.lbl_sensitiveZone.Name = "lbl_sensitiveZone";
            this.lbl_sensitiveZone.Size = new System.Drawing.Size(92, 13);
            this.lbl_sensitiveZone.TabIndex = 12;
            this.lbl_sensitiveZone.Text = "Sensitive Zone";
            // 
            // gB_gestureActivation
            // 
            this.gB_gestureActivation.Controls.Add(this.panel_control_prg);
            this.gB_gestureActivation.Controls.Add(this.panel_zone_and_timeout);
            this.gB_gestureActivation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gB_gestureActivation.Location = new System.Drawing.Point(0, 0);
            this.gB_gestureActivation.Name = "gB_gestureActivation";
            this.gB_gestureActivation.Size = new System.Drawing.Size(548, 378);
            this.gB_gestureActivation.TabIndex = 18;
            this.gB_gestureActivation.TabStop = false;
            this.gB_gestureActivation.Text = "Gesture Activation";
            // 
            // panel_control_prg
            // 
            this.panel_control_prg.Controls.Add(this.gB_description2);
            this.panel_control_prg.Controls.Add(this.panel4);
            this.panel_control_prg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_control_prg.Location = new System.Drawing.Point(3, 230);
            this.panel_control_prg.Name = "panel_control_prg";
            this.panel_control_prg.Size = new System.Drawing.Size(542, 145);
            this.panel_control_prg.TabIndex = 20;
            // 
            // gB_description2
            // 
            this.gB_description2.Controls.Add(this.rTB_controlPrg);
            this.gB_description2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gB_description2.Location = new System.Drawing.Point(0, 43);
            this.gB_description2.Name = "gB_description2";
            this.gB_description2.Size = new System.Drawing.Size(542, 102);
            this.gB_description2.TabIndex = 1;
            this.gB_description2.TabStop = false;
            this.gB_description2.Text = "Description";
            // 
            // rTB_controlPrg
            // 
            this.rTB_controlPrg.BackColor = System.Drawing.SystemColors.Control;
            this.rTB_controlPrg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rTB_controlPrg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rTB_controlPrg.Location = new System.Drawing.Point(3, 16);
            this.rTB_controlPrg.Name = "rTB_controlPrg";
            this.rTB_controlPrg.ReadOnly = true;
            this.rTB_controlPrg.Size = new System.Drawing.Size(536, 83);
            this.rTB_controlPrg.TabIndex = 0;
            this.rTB_controlPrg.Text = "";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lbl_typeOfWnd);
            this.panel4.Controls.Add(this.cB_wndHandle);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(542, 43);
            this.panel4.TabIndex = 0;
            // 
            // panel_zone_and_timeout
            // 
            this.panel_zone_and_timeout.Controls.Add(this.gB_description1);
            this.panel_zone_and_timeout.Controls.Add(this.panel2);
            this.panel_zone_and_timeout.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_zone_and_timeout.Location = new System.Drawing.Point(3, 16);
            this.panel_zone_and_timeout.Name = "panel_zone_and_timeout";
            this.panel_zone_and_timeout.Size = new System.Drawing.Size(542, 214);
            this.panel_zone_and_timeout.TabIndex = 19;
            // 
            // gB_description1
            // 
            this.gB_description1.Controls.Add(this.rTB_zoneTimeout);
            this.gB_description1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gB_description1.Location = new System.Drawing.Point(0, 63);
            this.gB_description1.Name = "gB_description1";
            this.gB_description1.Size = new System.Drawing.Size(542, 151);
            this.gB_description1.TabIndex = 17;
            this.gB_description1.TabStop = false;
            this.gB_description1.Text = "Description";
            // 
            // rTB_zoneTimeout
            // 
            this.rTB_zoneTimeout.BackColor = System.Drawing.SystemColors.Control;
            this.rTB_zoneTimeout.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rTB_zoneTimeout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rTB_zoneTimeout.Location = new System.Drawing.Point(3, 16);
            this.rTB_zoneTimeout.Name = "rTB_zoneTimeout";
            this.rTB_zoneTimeout.ReadOnly = true;
            this.rTB_zoneTimeout.Size = new System.Drawing.Size(536, 132);
            this.rTB_zoneTimeout.TabIndex = 0;
            this.rTB_zoneTimeout.Text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbl_sensitiveZone);
            this.panel2.Controls.Add(this.cB_sensitiveZone);
            this.panel2.Controls.Add(this.lbl_deactivationTimeout);
            this.panel2.Controls.Add(this.nUD_deactivateTimeout);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(542, 63);
            this.panel2.TabIndex = 16;
            // 
            // UC_W_activation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gB_gestureActivation);
            this.Name = "UC_W_activation";
            this.Size = new System.Drawing.Size(548, 378);
            ((System.ComponentModel.ISupportInitialize)(this.nUD_deactivateTimeout)).EndInit();
            this.gB_gestureActivation.ResumeLayout(false);
            this.panel_control_prg.ResumeLayout(false);
            this.gB_description2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel_zone_and_timeout.ResumeLayout(false);
            this.gB_description1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_typeOfWnd;
        private System.Windows.Forms.ComboBox cB_wndHandle;
        private System.Windows.Forms.NumericUpDown nUD_deactivateTimeout;
        private System.Windows.Forms.ComboBox cB_sensitiveZone;
        private System.Windows.Forms.Label lbl_deactivationTimeout;
        private System.Windows.Forms.Label lbl_sensitiveZone;
        private System.Windows.Forms.GroupBox gB_gestureActivation;
        private System.Windows.Forms.Panel panel_control_prg;
        private System.Windows.Forms.GroupBox gB_description2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel_zone_and_timeout;
        private System.Windows.Forms.GroupBox gB_description1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox rTB_controlPrg;
        private System.Windows.Forms.RichTextBox rTB_zoneTimeout;
    }
}
