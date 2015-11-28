namespace JustGestures.WizardItems
{
    partial class UC_W_baseGesture
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
            this.components = new System.ComponentModel.Container();
            this.pB_animation = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gB_description = new System.Windows.Forms.GroupBox();
            this.rTB_description = new System.Windows.Forms.RichTextBox();
            this.gB_activation = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gB_gesture = new System.Windows.Forms.GroupBox();
            this.rTB_instructions = new System.Windows.Forms.RichTextBox();
            this.lbl_instructions = new System.Windows.Forms.Label();
            this.cB_gesture = new System.Windows.Forms.CheckBox();
            this.gB_preview = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pB_animation)).BeginInit();
            this.panel1.SuspendLayout();
            this.gB_description.SuspendLayout();
            this.gB_gesture.SuspendLayout();
            this.gB_preview.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pB_animation
            // 
            this.pB_animation.BackColor = System.Drawing.Color.White;
            this.pB_animation.Dock = System.Windows.Forms.DockStyle.Left;
            this.pB_animation.Location = new System.Drawing.Point(3, 16);
            this.pB_animation.Name = "pB_animation";
            this.pB_animation.Size = new System.Drawing.Size(220, 251);
            this.pB_animation.TabIndex = 0;
            this.pB_animation.TabStop = false;
            this.pB_animation.Click += new System.EventHandler(this.pB_animation_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gB_description);
            this.panel1.Controls.Add(this.gB_activation);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(223, 16);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.panel1.Size = new System.Drawing.Size(415, 251);
            this.panel1.TabIndex = 1;
            // 
            // gB_description
            // 
            this.gB_description.Controls.Add(this.rTB_description);
            this.gB_description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gB_description.Location = new System.Drawing.Point(3, 77);
            this.gB_description.Name = "gB_description";
            this.gB_description.Size = new System.Drawing.Size(412, 174);
            this.gB_description.TabIndex = 1;
            this.gB_description.TabStop = false;
            this.gB_description.Text = "Description";
            // 
            // rTB_description
            // 
            this.rTB_description.BackColor = System.Drawing.SystemColors.Control;
            this.rTB_description.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rTB_description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rTB_description.Location = new System.Drawing.Point(3, 16);
            this.rTB_description.Name = "rTB_description";
            this.rTB_description.ReadOnly = true;
            this.rTB_description.Size = new System.Drawing.Size(406, 155);
            this.rTB_description.TabIndex = 0;
            this.rTB_description.Text = "";
            // 
            // gB_activation
            // 
            this.gB_activation.Dock = System.Windows.Forms.DockStyle.Top;
            this.gB_activation.Location = new System.Drawing.Point(3, 0);
            this.gB_activation.Name = "gB_activation";
            this.gB_activation.Size = new System.Drawing.Size(412, 77);
            this.gB_activation.TabIndex = 0;
            this.gB_activation.TabStop = false;
            this.gB_activation.Text = "Activation";
            // 
            // gB_gesture
            // 
            this.gB_gesture.Controls.Add(this.rTB_instructions);
            this.gB_gesture.Controls.Add(this.lbl_instructions);
            this.gB_gesture.Controls.Add(this.cB_gesture);
            this.gB_gesture.Dock = System.Windows.Forms.DockStyle.Top;
            this.gB_gesture.Location = new System.Drawing.Point(0, 0);
            this.gB_gesture.Name = "gB_gesture";
            this.gB_gesture.Size = new System.Drawing.Size(641, 105);
            this.gB_gesture.TabIndex = 2;
            this.gB_gesture.TabStop = false;
            this.gB_gesture.Text = "Gesture";
            // 
            // rTB_instructions
            // 
            this.rTB_instructions.BackColor = System.Drawing.SystemColors.Control;
            this.rTB_instructions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rTB_instructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rTB_instructions.Location = new System.Drawing.Point(19, 55);
            this.rTB_instructions.Name = "rTB_instructions";
            this.rTB_instructions.ReadOnly = true;
            this.rTB_instructions.Size = new System.Drawing.Size(514, 40);
            this.rTB_instructions.TabIndex = 2;
            this.rTB_instructions.Text = "";
            // 
            // lbl_instructions
            // 
            this.lbl_instructions.AutoSize = true;
            this.lbl_instructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_instructions.Location = new System.Drawing.Point(16, 39);
            this.lbl_instructions.Name = "lbl_instructions";
            this.lbl_instructions.Size = new System.Drawing.Size(64, 13);
            this.lbl_instructions.TabIndex = 1;
            this.lbl_instructions.Text = "Instructions:";
            // 
            // cB_gesture
            // 
            this.cB_gesture.AutoSize = true;
            this.cB_gesture.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cB_gesture.Location = new System.Drawing.Point(19, 19);
            this.cB_gesture.Name = "cB_gesture";
            this.cB_gesture.Size = new System.Drawing.Size(96, 17);
            this.cB_gesture.TabIndex = 0;
            this.cB_gesture.Text = "gesture type";
            this.cB_gesture.UseVisualStyleBackColor = true;
            // 
            // gB_preview
            // 
            this.gB_preview.Controls.Add(this.panel1);
            this.gB_preview.Controls.Add(this.pB_animation);
            this.gB_preview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gB_preview.Location = new System.Drawing.Point(0, 0);
            this.gB_preview.Name = "gB_preview";
            this.gB_preview.Size = new System.Drawing.Size(641, 270);
            this.gB_preview.TabIndex = 3;
            this.gB_preview.TabStop = false;
            this.gB_preview.Text = "Preview (Example)";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gB_gesture);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(641, 115);
            this.panel2.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gB_preview);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 115);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(641, 270);
            this.panel3.TabIndex = 5;
            // 
            // UC_W_baseGesture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "UC_W_baseGesture";
            this.Size = new System.Drawing.Size(641, 385);
            this.Load += new System.EventHandler(this.UC_W_baseGesture_Load);
            this.VisibleChanged += new System.EventHandler(this.UC_W_baseGesture_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pB_animation)).EndInit();
            this.panel1.ResumeLayout(false);
            this.gB_description.ResumeLayout(false);
            this.gB_gesture.ResumeLayout(false);
            this.gB_gesture.PerformLayout();
            this.gB_preview.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.GroupBox gB_description;
        protected System.Windows.Forms.GroupBox gB_activation;
        protected System.Windows.Forms.PictureBox pB_animation;
        protected System.Windows.Forms.RichTextBox rTB_description;
        private System.Windows.Forms.ToolTip toolTip1;
        protected System.Windows.Forms.GroupBox gB_gesture;
        protected System.Windows.Forms.GroupBox gB_preview;
        protected System.Windows.Forms.RichTextBox rTB_instructions;
        protected System.Windows.Forms.Label lbl_instructions;
        protected System.Windows.Forms.CheckBox cB_gesture;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}
