namespace JustGestures.ControlItems
{
    partial class UC_TP_wheelBtn
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
            this.lbl_holdBtn = new System.Windows.Forms.Label();
            this.cB_holdBtn = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rTB_pushHoldBtn = new System.Windows.Forms.RichTextBox();
            this.rTB_releaseHoldBtn = new System.Windows.Forms.RichTextBox();
            this.rTB_scrollWheel = new System.Windows.Forms.RichTextBox();
            this.lV_buttonMatchedGestures = new System.Windows.Forms.ListView();
            this.cH_associatedActions = new System.Windows.Forms.ColumnHeader();
            this.cH_group = new System.Windows.Forms.ColumnHeader();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_holdBtn
            // 
            this.lbl_holdBtn.AutoSize = true;
            this.lbl_holdBtn.Location = new System.Drawing.Point(17, 31);
            this.lbl_holdBtn.Name = "lbl_holdBtn";
            this.lbl_holdBtn.Size = new System.Drawing.Size(74, 13);
            this.lbl_holdBtn.TabIndex = 7;
            this.lbl_holdBtn.Text = "Trigger Button";
            // 
            // cB_holdBtn
            // 
            this.cB_holdBtn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_holdBtn.FormattingEnabled = true;
            this.cB_holdBtn.Location = new System.Drawing.Point(20, 47);
            this.cB_holdBtn.Name = "cB_holdBtn";
            this.cB_holdBtn.Size = new System.Drawing.Size(154, 21);
            this.cB_holdBtn.TabIndex = 6;
            this.cB_holdBtn.SelectedIndexChanged += new System.EventHandler(this.cB_trigger_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.rTB_pushHoldBtn);
            this.panel1.Controls.Add(this.rTB_releaseHoldBtn);
            this.panel1.Controls.Add(this.rTB_scrollWheel);
            this.panel1.Controls.Add(this.cB_holdBtn);
            this.panel1.Controls.Add(this.lbl_holdBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(214, 270);
            this.panel1.TabIndex = 9;
            // 
            // rTB_pushHoldBtn
            // 
            this.rTB_pushHoldBtn.BackColor = System.Drawing.Color.White;
            this.rTB_pushHoldBtn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rTB_pushHoldBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rTB_pushHoldBtn.Location = new System.Drawing.Point(20, 74);
            this.rTB_pushHoldBtn.Name = "rTB_pushHoldBtn";
            this.rTB_pushHoldBtn.ReadOnly = true;
            this.rTB_pushHoldBtn.Size = new System.Drawing.Size(168, 37);
            this.rTB_pushHoldBtn.TabIndex = 13;
            this.rTB_pushHoldBtn.Text = "1.) Push and hold this button";
            // 
            // rTB_releaseHoldBtn
            // 
            this.rTB_releaseHoldBtn.BackColor = System.Drawing.Color.White;
            this.rTB_releaseHoldBtn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rTB_releaseHoldBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rTB_releaseHoldBtn.Location = new System.Drawing.Point(20, 179);
            this.rTB_releaseHoldBtn.Name = "rTB_releaseHoldBtn";
            this.rTB_releaseHoldBtn.ReadOnly = true;
            this.rTB_releaseHoldBtn.Size = new System.Drawing.Size(168, 37);
            this.rTB_releaseHoldBtn.TabIndex = 12;
            this.rTB_releaseHoldBtn.Text = "3.) Release Trigger button";
            // 
            // rTB_scrollWheel
            // 
            this.rTB_scrollWheel.BackColor = System.Drawing.Color.White;
            this.rTB_scrollWheel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rTB_scrollWheel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rTB_scrollWheel.Location = new System.Drawing.Point(20, 112);
            this.rTB_scrollWheel.Name = "rTB_scrollWheel";
            this.rTB_scrollWheel.ReadOnly = true;
            this.rTB_scrollWheel.Size = new System.Drawing.Size(168, 61);
            this.rTB_scrollWheel.TabIndex = 11;
            this.rTB_scrollWheel.Text = "2.) Move wheel up or down to invoke appropriate keyscript/action.";
            // 
            // lV_buttonMatchedGestures
            // 
            this.lV_buttonMatchedGestures.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cH_associatedActions,
            this.cH_group});
            this.lV_buttonMatchedGestures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lV_buttonMatchedGestures.Location = new System.Drawing.Point(0, 270);
            this.lV_buttonMatchedGestures.Name = "lV_buttonMatchedGestures";
            this.lV_buttonMatchedGestures.Size = new System.Drawing.Size(214, 141);
            this.lV_buttonMatchedGestures.TabIndex = 11;
            this.lV_buttonMatchedGestures.UseCompatibleStateImageBehavior = false;
            this.lV_buttonMatchedGestures.View = System.Windows.Forms.View.Details;
            // 
            // cH_associatedActions
            // 
            this.cH_associatedActions.Text = "Associated Actions";
            this.cH_associatedActions.Width = 121;
            // 
            // cH_group
            // 
            this.cH_group.Text = "Group";
            this.cH_group.Width = 82;
            // 
            // UC_TP_wheelBtn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lV_buttonMatchedGestures);
            this.Controls.Add(this.panel1);
            this.Name = "UC_TP_wheelBtn";
            this.Size = new System.Drawing.Size(214, 411);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_holdBtn;
        private System.Windows.Forms.ComboBox cB_holdBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lV_buttonMatchedGestures;
        private System.Windows.Forms.ColumnHeader cH_associatedActions;
        private System.Windows.Forms.RichTextBox rTB_scrollWheel;
        private System.Windows.Forms.RichTextBox rTB_releaseHoldBtn;
        private System.Windows.Forms.ColumnHeader cH_group;
        private System.Windows.Forms.RichTextBox rTB_pushHoldBtn;
    }
}
