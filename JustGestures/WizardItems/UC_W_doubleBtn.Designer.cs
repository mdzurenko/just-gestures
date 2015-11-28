namespace JustGestures.WizardItems
{
    partial class UC_W_doubleBtn
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
            this.lbl_triggerBtn = new System.Windows.Forms.Label();
            this.cB_triggerBtn = new System.Windows.Forms.ComboBox();
            this.cB_modifierBtn = new System.Windows.Forms.ComboBox();
            this.lbl_modifierBtn = new System.Windows.Forms.Label();
            this.lbl_and = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.gB_description.SuspendLayout();
            this.gB_activation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_animation)).BeginInit();
            this.gB_gesture.SuspendLayout();
            this.gB_preview.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(414, 251);
            // 
            // gB_description
            // 
            this.gB_description.Size = new System.Drawing.Size(411, 174);
            // 
            // gB_activation
            // 
            this.gB_activation.Controls.Add(this.lbl_and);
            this.gB_activation.Controls.Add(this.cB_modifierBtn);
            this.gB_activation.Controls.Add(this.lbl_modifierBtn);
            this.gB_activation.Controls.Add(this.lbl_triggerBtn);
            this.gB_activation.Controls.Add(this.cB_triggerBtn);
            this.gB_activation.Size = new System.Drawing.Size(411, 77);
            // 
            // rTB_description
            // 
            this.rTB_description.Size = new System.Drawing.Size(405, 155);
            // 
            // gB_gesture
            // 
            this.gB_gesture.Size = new System.Drawing.Size(640, 100);
            // 
            // gB_preview
            // 
            this.gB_preview.Size = new System.Drawing.Size(640, 270);
            // 
            // rTB_instructions
            // 
            this.rTB_instructions.Location = new System.Drawing.Point(19, 68);
            this.rTB_instructions.Size = new System.Drawing.Size(514, 26);
            // 
            // lbl_instructions
            // 
            this.lbl_instructions.Location = new System.Drawing.Point(16, 52);
            // 
            // cB_gesture
            // 
            this.cB_gesture.Size = new System.Drawing.Size(180, 17);
            this.cB_gesture.Text = "Double Button Combination";
            // 
            // lbl_triggerBtn
            // 
            this.lbl_triggerBtn.AutoSize = true;
            this.lbl_triggerBtn.Location = new System.Drawing.Point(30, 22);
            this.lbl_triggerBtn.Name = "lbl_triggerBtn";
            this.lbl_triggerBtn.Size = new System.Drawing.Size(74, 13);
            this.lbl_triggerBtn.TabIndex = 4;
            this.lbl_triggerBtn.Text = "Trigger Button";
            // 
            // cB_triggerBtn
            // 
            this.cB_triggerBtn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_triggerBtn.Enabled = false;
            this.cB_triggerBtn.FormattingEnabled = true;
            this.cB_triggerBtn.Location = new System.Drawing.Point(33, 38);
            this.cB_triggerBtn.Name = "cB_triggerBtn";
            this.cB_triggerBtn.Size = new System.Drawing.Size(130, 21);
            this.cB_triggerBtn.TabIndex = 3;
            // 
            // cB_modifierBtn
            // 
            this.cB_modifierBtn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_modifierBtn.Enabled = false;
            this.cB_modifierBtn.FormattingEnabled = true;
            this.cB_modifierBtn.Location = new System.Drawing.Point(253, 38);
            this.cB_modifierBtn.Margin = new System.Windows.Forms.Padding(0);
            this.cB_modifierBtn.Name = "cB_modifierBtn";
            this.cB_modifierBtn.Size = new System.Drawing.Size(130, 21);
            this.cB_modifierBtn.TabIndex = 5;
            // 
            // lbl_modifierBtn
            // 
            this.lbl_modifierBtn.AutoSize = true;
            this.lbl_modifierBtn.Location = new System.Drawing.Point(250, 22);
            this.lbl_modifierBtn.Name = "lbl_modifierBtn";
            this.lbl_modifierBtn.Size = new System.Drawing.Size(78, 13);
            this.lbl_modifierBtn.TabIndex = 6;
            this.lbl_modifierBtn.Text = "Modifier Button";
            // 
            // lbl_and
            // 
            this.lbl_and.AutoSize = true;
            this.lbl_and.Location = new System.Drawing.Point(187, 22);
            this.lbl_and.Name = "lbl_and";
            this.lbl_and.Size = new System.Drawing.Size(25, 13);
            this.lbl_and.TabIndex = 9;
            this.lbl_and.Text = "and";
            // 
            // UC_W_doubleBtn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UC_W_doubleBtn";
            this.Size = new System.Drawing.Size(640, 385);
            this.panel1.ResumeLayout(false);
            this.gB_description.ResumeLayout(false);
            this.gB_activation.ResumeLayout(false);
            this.gB_activation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_animation)).EndInit();
            this.gB_gesture.ResumeLayout(false);
            this.gB_gesture.PerformLayout();
            this.gB_preview.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_triggerBtn;
        private System.Windows.Forms.ComboBox cB_triggerBtn;
        private System.Windows.Forms.ComboBox cB_modifierBtn;
        private System.Windows.Forms.Label lbl_modifierBtn;
        private System.Windows.Forms.Label lbl_and;
    }
}
