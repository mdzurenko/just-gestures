namespace JustGestures
{
    partial class Form_gestures
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cB_generated = new System.Windows.Forms.CheckBox();
            this.btn_generate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tB_exactPathMax = new System.Windows.Forms.TextBox();
            this.tB_exactPathMin = new System.Windows.Forms.TextBox();
            this.cB_exactPath = new System.Windows.Forms.CheckBox();
            this.tB_approxPoints = new System.Windows.Forms.TextBox();
            this.cB_approxPoints = new System.Windows.Forms.CheckBox();
            this.btn_redraw = new System.Windows.Forms.Button();
            this.cB_originalCurve = new System.Windows.Forms.CheckBox();
            this.cB_bezierCurve = new System.Windows.Forms.CheckBox();
            this.cB_curvePoints = new System.Windows.Forms.CheckBox();
            this.pB_display = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_display)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cB_generated);
            this.groupBox1.Controls.Add(this.btn_generate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tB_exactPathMax);
            this.groupBox1.Controls.Add(this.tB_exactPathMin);
            this.groupBox1.Controls.Add(this.cB_exactPath);
            this.groupBox1.Controls.Add(this.tB_approxPoints);
            this.groupBox1.Controls.Add(this.cB_approxPoints);
            this.groupBox1.Controls.Add(this.btn_redraw);
            this.groupBox1.Controls.Add(this.cB_originalCurve);
            this.groupBox1.Controls.Add(this.cB_bezierCurve);
            this.groupBox1.Controls.Add(this.cB_curvePoints);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 431);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(735, 124);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // cB_generated
            // 
            this.cB_generated.AutoSize = true;
            this.cB_generated.Checked = true;
            this.cB_generated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_generated.Location = new System.Drawing.Point(202, 42);
            this.cB_generated.Name = "cB_generated";
            this.cB_generated.Size = new System.Drawing.Size(107, 17);
            this.cB_generated.TabIndex = 11;
            this.cB_generated.Text = "Generated Curve";
            this.cB_generated.UseVisualStyleBackColor = true;
            // 
            // btn_generate
            // 
            this.btn_generate.Location = new System.Drawing.Point(401, 83);
            this.btn_generate.Name = "btn_generate";
            this.btn_generate.Size = new System.Drawing.Size(75, 23);
            this.btn_generate.TabIndex = 10;
            this.btn_generate.Text = "Generate!";
            this.btn_generate.UseVisualStyleBackColor = true;
            this.btn_generate.Click += new System.EventHandler(this.btn_generate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(329, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "-";
            // 
            // tB_exactPathMax
            // 
            this.tB_exactPathMax.Location = new System.Drawing.Point(345, 17);
            this.tB_exactPathMax.Name = "tB_exactPathMax";
            this.tB_exactPathMax.Size = new System.Drawing.Size(37, 20);
            this.tB_exactPathMax.TabIndex = 8;
            this.tB_exactPathMax.Text = "20";
            // 
            // tB_exactPathMin
            // 
            this.tB_exactPathMin.Location = new System.Drawing.Point(286, 16);
            this.tB_exactPathMin.Name = "tB_exactPathMin";
            this.tB_exactPathMin.Size = new System.Drawing.Size(37, 20);
            this.tB_exactPathMin.TabIndex = 7;
            this.tB_exactPathMin.Text = "20";
            // 
            // cB_exactPath
            // 
            this.cB_exactPath.AutoSize = true;
            this.cB_exactPath.Location = new System.Drawing.Point(202, 19);
            this.cB_exactPath.Name = "cB_exactPath";
            this.cB_exactPath.Size = new System.Drawing.Size(78, 17);
            this.cB_exactPath.TabIndex = 6;
            this.cB_exactPath.Text = "Exact Path";
            this.cB_exactPath.UseVisualStyleBackColor = true;
            // 
            // tB_approxPoints
            // 
            this.tB_approxPoints.Location = new System.Drawing.Point(109, 87);
            this.tB_approxPoints.Name = "tB_approxPoints";
            this.tB_approxPoints.Size = new System.Drawing.Size(62, 20);
            this.tB_approxPoints.TabIndex = 5;
            this.tB_approxPoints.Text = "16";
            // 
            // cB_approxPoints
            // 
            this.cB_approxPoints.AutoSize = true;
            this.cB_approxPoints.Checked = true;
            this.cB_approxPoints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_approxPoints.Location = new System.Drawing.Point(12, 89);
            this.cB_approxPoints.Name = "cB_approxPoints";
            this.cB_approxPoints.Size = new System.Drawing.Size(91, 17);
            this.cB_approxPoints.TabIndex = 4;
            this.cB_approxPoints.Text = "Approx Points";
            this.cB_approxPoints.UseVisualStyleBackColor = true;
            // 
            // btn_redraw
            // 
            this.btn_redraw.Location = new System.Drawing.Point(307, 83);
            this.btn_redraw.Name = "btn_redraw";
            this.btn_redraw.Size = new System.Drawing.Size(75, 23);
            this.btn_redraw.TabIndex = 3;
            this.btn_redraw.Text = "Redraw!";
            this.btn_redraw.UseVisualStyleBackColor = true;
            this.btn_redraw.Click += new System.EventHandler(this.btn_redraw_Click);
            // 
            // cB_originalCurve
            // 
            this.cB_originalCurve.AutoSize = true;
            this.cB_originalCurve.Checked = true;
            this.cB_originalCurve.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_originalCurve.Location = new System.Drawing.Point(12, 19);
            this.cB_originalCurve.Name = "cB_originalCurve";
            this.cB_originalCurve.Size = new System.Drawing.Size(92, 17);
            this.cB_originalCurve.TabIndex = 2;
            this.cB_originalCurve.Text = "Original Curve";
            this.cB_originalCurve.UseVisualStyleBackColor = true;
            // 
            // cB_bezierCurve
            // 
            this.cB_bezierCurve.AutoSize = true;
            this.cB_bezierCurve.Location = new System.Drawing.Point(12, 66);
            this.cB_bezierCurve.Name = "cB_bezierCurve";
            this.cB_bezierCurve.Size = new System.Drawing.Size(85, 17);
            this.cB_bezierCurve.TabIndex = 1;
            this.cB_bezierCurve.Text = "Bezier curve";
            this.cB_bezierCurve.UseVisualStyleBackColor = true;
            // 
            // cB_curvePoints
            // 
            this.cB_curvePoints.AutoSize = true;
            this.cB_curvePoints.Location = new System.Drawing.Point(12, 42);
            this.cB_curvePoints.Name = "cB_curvePoints";
            this.cB_curvePoints.Size = new System.Drawing.Size(85, 17);
            this.cB_curvePoints.TabIndex = 0;
            this.cB_curvePoints.Text = "Curve points";
            this.cB_curvePoints.UseVisualStyleBackColor = true;
            // 
            // pB_display
            // 
            this.pB_display.BackColor = System.Drawing.Color.White;
            this.pB_display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pB_display.Location = new System.Drawing.Point(0, 0);
            this.pB_display.Name = "pB_display";
            this.pB_display.Size = new System.Drawing.Size(735, 431);
            this.pB_display.TabIndex = 2;
            this.pB_display.TabStop = false;
            this.pB_display.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pB_display_MouseMove);
            this.pB_display.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pB_display_MouseDown);
            this.pB_display.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pB_display_MouseUp);
            // 
            // Form_gestures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 555);
            this.Controls.Add(this.pB_display);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form_gestures";
            this.Text = "Form_gestures";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_display)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pB_display;
        private System.Windows.Forms.Button btn_redraw;
        private System.Windows.Forms.CheckBox cB_originalCurve;
        private System.Windows.Forms.CheckBox cB_bezierCurve;
        private System.Windows.Forms.CheckBox cB_curvePoints;
        private System.Windows.Forms.CheckBox cB_approxPoints;
        private System.Windows.Forms.TextBox tB_approxPoints;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tB_exactPathMax;
        private System.Windows.Forms.TextBox tB_exactPathMin;
        private System.Windows.Forms.CheckBox cB_exactPath;
        private System.Windows.Forms.Button btn_generate;
        private System.Windows.Forms.CheckBox cB_generated;
    }
}