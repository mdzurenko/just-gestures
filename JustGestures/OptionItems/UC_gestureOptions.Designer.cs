namespace JustGestures.OptionItems
{
    partial class UC_gestureOptions
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
            this.gB_gestureTypes = new System.Windows.Forms.GroupBox();
            this.lbl_wheelBtn = new System.Windows.Forms.Label();
            this.lbl_doubleBtn = new System.Windows.Forms.Label();
            this.lbl_classicCurve = new System.Windows.Forms.Label();
            this.cB_wheelBtn = new System.Windows.Forms.ComboBox();
            this.cB_doubleBtn = new System.Windows.Forms.ComboBox();
            this.cB_classicCurve = new System.Windows.Forms.ComboBox();
            this.gB_activation = new System.Windows.Forms.GroupBox();
            this.lbl_typeOfWnd = new System.Windows.Forms.Label();
            this.cB_wndHandle = new System.Windows.Forms.ComboBox();
            this.nUD_deactivateTimeout = new System.Windows.Forms.NumericUpDown();
            this.cB_sensitiveZone = new System.Windows.Forms.ComboBox();
            this.cB_toggleBtn = new System.Windows.Forms.ComboBox();
            this.lbl_deactivationTimeout = new System.Windows.Forms.Label();
            this.lbl_sensitiveZone = new System.Windows.Forms.Label();
            this.lbl_toggleButton = new System.Windows.Forms.Label();
            this.btn_wizard = new System.Windows.Forms.Button();
            this.gB_gestureTypes.SuspendLayout();
            this.gB_activation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_deactivateTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // gB_gestureTypes
            // 
            this.gB_gestureTypes.Controls.Add(this.lbl_wheelBtn);
            this.gB_gestureTypes.Controls.Add(this.lbl_doubleBtn);
            this.gB_gestureTypes.Controls.Add(this.lbl_classicCurve);
            this.gB_gestureTypes.Controls.Add(this.cB_wheelBtn);
            this.gB_gestureTypes.Controls.Add(this.cB_doubleBtn);
            this.gB_gestureTypes.Controls.Add(this.cB_classicCurve);
            this.gB_gestureTypes.Location = new System.Drawing.Point(0, 0);
            this.gB_gestureTypes.Name = "gB_gestureTypes";
            this.gB_gestureTypes.Size = new System.Drawing.Size(400, 109);
            this.gB_gestureTypes.TabIndex = 3;
            this.gB_gestureTypes.TabStop = false;
            this.gB_gestureTypes.Text = "Gesture Types";
            // 
            // lbl_wheelBtn
            // 
            this.lbl_wheelBtn.AutoSize = true;
            this.lbl_wheelBtn.Location = new System.Drawing.Point(6, 76);
            this.lbl_wheelBtn.Name = "lbl_wheelBtn";
            this.lbl_wheelBtn.Size = new System.Drawing.Size(112, 13);
            this.lbl_wheelBtn.TabIndex = 5;
            this.lbl_wheelBtn.Text = "Wheel Button Gesture";
            // 
            // lbl_doubleBtn
            // 
            this.lbl_doubleBtn.AutoSize = true;
            this.lbl_doubleBtn.Location = new System.Drawing.Point(6, 49);
            this.lbl_doubleBtn.Name = "lbl_doubleBtn";
            this.lbl_doubleBtn.Size = new System.Drawing.Size(115, 13);
            this.lbl_doubleBtn.TabIndex = 4;
            this.lbl_doubleBtn.Text = "Double Button Gesture";
            // 
            // lbl_classicCurve
            // 
            this.lbl_classicCurve.AutoSize = true;
            this.lbl_classicCurve.Location = new System.Drawing.Point(6, 22);
            this.lbl_classicCurve.Name = "lbl_classicCurve";
            this.lbl_classicCurve.Size = new System.Drawing.Size(111, 13);
            this.lbl_classicCurve.TabIndex = 3;
            this.lbl_classicCurve.Text = "Classic Curve Gesture";
            // 
            // cB_wheelBtn
            // 
            this.cB_wheelBtn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_wheelBtn.FormattingEnabled = true;
            this.cB_wheelBtn.Location = new System.Drawing.Point(234, 73);
            this.cB_wheelBtn.Name = "cB_wheelBtn";
            this.cB_wheelBtn.Size = new System.Drawing.Size(160, 21);
            this.cB_wheelBtn.TabIndex = 2;
            this.cB_wheelBtn.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // cB_doubleBtn
            // 
            this.cB_doubleBtn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_doubleBtn.FormattingEnabled = true;
            this.cB_doubleBtn.Location = new System.Drawing.Point(234, 46);
            this.cB_doubleBtn.Name = "cB_doubleBtn";
            this.cB_doubleBtn.Size = new System.Drawing.Size(160, 21);
            this.cB_doubleBtn.TabIndex = 1;
            this.cB_doubleBtn.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // cB_classicCurve
            // 
            this.cB_classicCurve.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_classicCurve.FormattingEnabled = true;
            this.cB_classicCurve.Location = new System.Drawing.Point(234, 19);
            this.cB_classicCurve.Name = "cB_classicCurve";
            this.cB_classicCurve.Size = new System.Drawing.Size(160, 21);
            this.cB_classicCurve.TabIndex = 0;
            this.cB_classicCurve.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // gB_activation
            // 
            this.gB_activation.Controls.Add(this.lbl_typeOfWnd);
            this.gB_activation.Controls.Add(this.cB_wndHandle);
            this.gB_activation.Controls.Add(this.nUD_deactivateTimeout);
            this.gB_activation.Controls.Add(this.cB_sensitiveZone);
            this.gB_activation.Controls.Add(this.cB_toggleBtn);
            this.gB_activation.Controls.Add(this.lbl_deactivationTimeout);
            this.gB_activation.Controls.Add(this.lbl_sensitiveZone);
            this.gB_activation.Controls.Add(this.lbl_toggleButton);
            this.gB_activation.Location = new System.Drawing.Point(0, 135);
            this.gB_activation.Name = "gB_activation";
            this.gB_activation.Size = new System.Drawing.Size(400, 138);
            this.gB_activation.TabIndex = 4;
            this.gB_activation.TabStop = false;
            this.gB_activation.Text = "Activation";
            // 
            // lbl_typeOfWnd
            // 
            this.lbl_typeOfWnd.AutoSize = true;
            this.lbl_typeOfWnd.Location = new System.Drawing.Point(6, 22);
            this.lbl_typeOfWnd.Name = "lbl_typeOfWnd";
            this.lbl_typeOfWnd.Size = new System.Drawing.Size(167, 13);
            this.lbl_typeOfWnd.TabIndex = 10;
            this.lbl_typeOfWnd.Text = "Control Program/Window which is";
            // 
            // cB_wndHandle
            // 
            this.cB_wndHandle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_wndHandle.FormattingEnabled = true;
            this.cB_wndHandle.Location = new System.Drawing.Point(234, 19);
            this.cB_wndHandle.Name = "cB_wndHandle";
            this.cB_wndHandle.Size = new System.Drawing.Size(160, 21);
            this.cB_wndHandle.TabIndex = 11;
            this.cB_wndHandle.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // nUD_deactivateTimeout
            // 
            this.nUD_deactivateTimeout.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nUD_deactivateTimeout.Location = new System.Drawing.Point(234, 101);
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
            this.nUD_deactivateTimeout.Size = new System.Drawing.Size(160, 20);
            this.nUD_deactivateTimeout.TabIndex = 5;
            this.nUD_deactivateTimeout.Value = new decimal(new int[] {
            350,
            0,
            0,
            0});
            this.nUD_deactivateTimeout.ValueChanged += new System.EventHandler(this.nUD_deactivateTimeout_ValueChanged);
            // 
            // cB_sensitiveZone
            // 
            this.cB_sensitiveZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_sensitiveZone.FormattingEnabled = true;
            this.cB_sensitiveZone.Location = new System.Drawing.Point(234, 73);
            this.cB_sensitiveZone.Name = "cB_sensitiveZone";
            this.cB_sensitiveZone.Size = new System.Drawing.Size(160, 21);
            this.cB_sensitiveZone.TabIndex = 4;
            this.cB_sensitiveZone.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // cB_toggleBtn
            // 
            this.cB_toggleBtn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_toggleBtn.FormattingEnabled = true;
            this.cB_toggleBtn.Location = new System.Drawing.Point(234, 46);
            this.cB_toggleBtn.Name = "cB_toggleBtn";
            this.cB_toggleBtn.Size = new System.Drawing.Size(160, 21);
            this.cB_toggleBtn.TabIndex = 3;
            this.cB_toggleBtn.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // lbl_deactivationTimeout
            // 
            this.lbl_deactivationTimeout.AutoSize = true;
            this.lbl_deactivationTimeout.Location = new System.Drawing.Point(6, 103);
            this.lbl_deactivationTimeout.Name = "lbl_deactivationTimeout";
            this.lbl_deactivationTimeout.Size = new System.Drawing.Size(135, 13);
            this.lbl_deactivationTimeout.TabIndex = 2;
            this.lbl_deactivationTimeout.Text = "Deactivation Timeout in ms";
            // 
            // lbl_sensitiveZone
            // 
            this.lbl_sensitiveZone.AutoSize = true;
            this.lbl_sensitiveZone.Location = new System.Drawing.Point(6, 76);
            this.lbl_sensitiveZone.Name = "lbl_sensitiveZone";
            this.lbl_sensitiveZone.Size = new System.Drawing.Size(118, 13);
            this.lbl_sensitiveZone.TabIndex = 1;
            this.lbl_sensitiveZone.Text = "Sensitive Zone in pixels";
            // 
            // lbl_toggleButton
            // 
            this.lbl_toggleButton.AutoSize = true;
            this.lbl_toggleButton.Location = new System.Drawing.Point(6, 49);
            this.lbl_toggleButton.Name = "lbl_toggleButton";
            this.lbl_toggleButton.Size = new System.Drawing.Size(74, 13);
            this.lbl_toggleButton.TabIndex = 0;
            this.lbl_toggleButton.Text = "Toggle Button";
            // 
            // btn_wizard
            // 
            this.btn_wizard.AutoSize = true;
            this.btn_wizard.Location = new System.Drawing.Point(0, 295);
            this.btn_wizard.Name = "btn_wizard";
            this.btn_wizard.Size = new System.Drawing.Size(106, 23);
            this.btn_wizard.TabIndex = 5;
            this.btn_wizard.Text = "Run Wizard";
            this.btn_wizard.UseVisualStyleBackColor = true;
            this.btn_wizard.Click += new System.EventHandler(this.btn_wizard_Click);
            // 
            // UC_gestureOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_wizard);
            this.Controls.Add(this.gB_activation);
            this.Controls.Add(this.gB_gestureTypes);
            this.Name = "UC_gestureOptions";
            this.Size = new System.Drawing.Size(532, 432);
            this.gB_gestureTypes.ResumeLayout(false);
            this.gB_gestureTypes.PerformLayout();
            this.gB_activation.ResumeLayout(false);
            this.gB_activation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_deactivateTimeout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gB_gestureTypes;
        private System.Windows.Forms.ComboBox cB_doubleBtn;
        private System.Windows.Forms.ComboBox cB_classicCurve;
        private System.Windows.Forms.ComboBox cB_wheelBtn;
        private System.Windows.Forms.Label lbl_wheelBtn;
        private System.Windows.Forms.Label lbl_doubleBtn;
        private System.Windows.Forms.Label lbl_classicCurve;
        private System.Windows.Forms.GroupBox gB_activation;
        private System.Windows.Forms.Label lbl_deactivationTimeout;
        private System.Windows.Forms.Label lbl_sensitiveZone;
        private System.Windows.Forms.Label lbl_toggleButton;
        private System.Windows.Forms.ComboBox cB_toggleBtn;
        private System.Windows.Forms.ComboBox cB_sensitiveZone;
        private System.Windows.Forms.NumericUpDown nUD_deactivateTimeout;
        private System.Windows.Forms.Label lbl_typeOfWnd;
        private System.Windows.Forms.ComboBox cB_wndHandle;
        private System.Windows.Forms.Button btn_wizard;
    }
}
