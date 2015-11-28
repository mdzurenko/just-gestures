namespace JustGestures.OptionItems
{
    partial class UC_visualisation
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
            this.cB_gestureColor = new System.Windows.Forms.ComboBox();
            this.cB_gestureWidth = new System.Windows.Forms.ComboBox();
            this.lbl_color = new System.Windows.Forms.Label();
            this.lbl_width = new System.Windows.Forms.Label();
            this.checkB_showGesture = new System.Windows.Forms.CheckBox();
            this.checkB_showToolTip = new System.Windows.Forms.CheckBox();
            this.lbl_toolTipDelay = new System.Windows.Forms.Label();
            this.gB_toolTipForGestures = new System.Windows.Forms.GroupBox();
            this.nUD_toolTipDelay = new System.Windows.Forms.NumericUpDown();
            this.gB_graphics = new System.Windows.Forms.GroupBox();
            this.gB_toolTipForGestures.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_toolTipDelay)).BeginInit();
            this.gB_graphics.SuspendLayout();
            this.SuspendLayout();
            // 
            // cB_gestureColor
            // 
            this.cB_gestureColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cB_gestureColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_gestureColor.FormattingEnabled = true;
            this.cB_gestureColor.Location = new System.Drawing.Point(234, 42);
            this.cB_gestureColor.Name = "cB_gestureColor";
            this.cB_gestureColor.Size = new System.Drawing.Size(160, 21);
            this.cB_gestureColor.TabIndex = 3;
            this.cB_gestureColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cB_gestureColor_DrawItem);
            this.cB_gestureColor.SelectedIndexChanged += new System.EventHandler(this.comboBox_selectedIndexChanged);
            // 
            // cB_gestureWidth
            // 
            this.cB_gestureWidth.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cB_gestureWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_gestureWidth.FormattingEnabled = true;
            this.cB_gestureWidth.Location = new System.Drawing.Point(234, 69);
            this.cB_gestureWidth.Name = "cB_gestureWidth";
            this.cB_gestureWidth.Size = new System.Drawing.Size(160, 21);
            this.cB_gestureWidth.TabIndex = 4;
            this.cB_gestureWidth.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cB_gestureWidth_DrawItem);
            this.cB_gestureWidth.SelectedIndexChanged += new System.EventHandler(this.comboBox_selectedIndexChanged);
            // 
            // lbl_color
            // 
            this.lbl_color.AutoSize = true;
            this.lbl_color.Location = new System.Drawing.Point(6, 45);
            this.lbl_color.Name = "lbl_color";
            this.lbl_color.Size = new System.Drawing.Size(31, 13);
            this.lbl_color.TabIndex = 2;
            this.lbl_color.Text = "Color";
            // 
            // lbl_width
            // 
            this.lbl_width.AutoSize = true;
            this.lbl_width.Location = new System.Drawing.Point(6, 72);
            this.lbl_width.Name = "lbl_width";
            this.lbl_width.Size = new System.Drawing.Size(35, 13);
            this.lbl_width.TabIndex = 3;
            this.lbl_width.Text = "Width";
            // 
            // checkB_showGesture
            // 
            this.checkB_showGesture.AutoSize = true;
            this.checkB_showGesture.Checked = true;
            this.checkB_showGesture.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkB_showGesture.Location = new System.Drawing.Point(9, 19);
            this.checkB_showGesture.Name = "checkB_showGesture";
            this.checkB_showGesture.Size = new System.Drawing.Size(257, 17);
            this.checkB_showGesture.TabIndex = 2;
            this.checkB_showGesture.Text = "Display graphic representation for Curve gestures";
            this.checkB_showGesture.UseVisualStyleBackColor = true;
            this.checkB_showGesture.CheckedChanged += new System.EventHandler(this.checkB_showGesture_CheckedChanged);
            // 
            // checkB_showToolTip
            // 
            this.checkB_showToolTip.AutoSize = true;
            this.checkB_showToolTip.Checked = true;
            this.checkB_showToolTip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkB_showToolTip.Location = new System.Drawing.Point(9, 19);
            this.checkB_showToolTip.Name = "checkB_showToolTip";
            this.checkB_showToolTip.Size = new System.Drawing.Size(211, 17);
            this.checkB_showToolTip.TabIndex = 0;
            this.checkB_showToolTip.Text = "Show tool tip while creating the gesture";
            this.checkB_showToolTip.UseVisualStyleBackColor = true;
            this.checkB_showToolTip.CheckedChanged += new System.EventHandler(this.checkB_showToolTip_CheckedChanged);
            // 
            // lbl_toolTipDelay
            // 
            this.lbl_toolTipDelay.AutoSize = true;
            this.lbl_toolTipDelay.Location = new System.Drawing.Point(6, 43);
            this.lbl_toolTipDelay.Name = "lbl_toolTipDelay";
            this.lbl_toolTipDelay.Size = new System.Drawing.Size(103, 13);
            this.lbl_toolTipDelay.TabIndex = 8;
            this.lbl_toolTipDelay.Text = "Tool Tip Delay in ms";
            // 
            // gB_toolTipForGestures
            // 
            this.gB_toolTipForGestures.Controls.Add(this.nUD_toolTipDelay);
            this.gB_toolTipForGestures.Controls.Add(this.checkB_showToolTip);
            this.gB_toolTipForGestures.Controls.Add(this.lbl_toolTipDelay);
            this.gB_toolTipForGestures.Location = new System.Drawing.Point(0, 0);
            this.gB_toolTipForGestures.Name = "gB_toolTipForGestures";
            this.gB_toolTipForGestures.Size = new System.Drawing.Size(400, 70);
            this.gB_toolTipForGestures.TabIndex = 9;
            this.gB_toolTipForGestures.TabStop = false;
            this.gB_toolTipForGestures.Text = "Tool Tip of Recognized Gestures";
            // 
            // nUD_toolTipDelay
            // 
            this.nUD_toolTipDelay.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nUD_toolTipDelay.Location = new System.Drawing.Point(234, 41);
            this.nUD_toolTipDelay.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nUD_toolTipDelay.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nUD_toolTipDelay.Name = "nUD_toolTipDelay";
            this.nUD_toolTipDelay.Size = new System.Drawing.Size(160, 20);
            this.nUD_toolTipDelay.TabIndex = 1;
            this.nUD_toolTipDelay.Value = new decimal(new int[] {
            350,
            0,
            0,
            0});
            this.nUD_toolTipDelay.ValueChanged += new System.EventHandler(this.nUD_toolTipDelay_ValueChanged);
            // 
            // gB_graphics
            // 
            this.gB_graphics.Controls.Add(this.checkB_showGesture);
            this.gB_graphics.Controls.Add(this.cB_gestureColor);
            this.gB_graphics.Controls.Add(this.cB_gestureWidth);
            this.gB_graphics.Controls.Add(this.lbl_width);
            this.gB_graphics.Controls.Add(this.lbl_color);
            this.gB_graphics.Location = new System.Drawing.Point(0, 91);
            this.gB_graphics.Name = "gB_graphics";
            this.gB_graphics.Size = new System.Drawing.Size(400, 103);
            this.gB_graphics.TabIndex = 10;
            this.gB_graphics.TabStop = false;
            this.gB_graphics.Text = "Graphics";
            // 
            // UC_visualisation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gB_graphics);
            this.Controls.Add(this.gB_toolTipForGestures);
            this.Name = "UC_visualisation";
            this.Size = new System.Drawing.Size(486, 298);
            this.gB_toolTipForGestures.ResumeLayout(false);
            this.gB_toolTipForGestures.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_toolTipDelay)).EndInit();
            this.gB_graphics.ResumeLayout(false);
            this.gB_graphics.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cB_gestureColor;
        private System.Windows.Forms.ComboBox cB_gestureWidth;
        private System.Windows.Forms.Label lbl_color;
        private System.Windows.Forms.Label lbl_width;
        private System.Windows.Forms.CheckBox checkB_showGesture;
        private System.Windows.Forms.CheckBox checkB_showToolTip;
        private System.Windows.Forms.Label lbl_toolTipDelay;
        private System.Windows.Forms.GroupBox gB_toolTipForGestures;
        private System.Windows.Forms.GroupBox gB_graphics;
        private System.Windows.Forms.NumericUpDown nUD_toolTipDelay;

    }
}
