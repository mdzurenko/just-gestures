namespace JustGestures.ControlItems
{
    partial class UC_TP_doubleBtn
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
            this.rTB_notes = new System.Windows.Forms.RichTextBox();
            this.lbl_executeBtn = new System.Windows.Forms.Label();
            this.lbl_holdBtn = new System.Windows.Forms.Label();
            this.cB_executeBtn = new System.Windows.Forms.ComboBox();
            this.cB_holdBtn = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rTB_clickExecuteBtn = new System.Windows.Forms.RichTextBox();
            this.rTB_pushHoldBtn = new System.Windows.Forms.RichTextBox();
            this.lV_buttonMatchedGestures = new System.Windows.Forms.ListView();
            this.cH_associatedActions = new System.Windows.Forms.ColumnHeader();
            this.cH_group = new System.Windows.Forms.ColumnHeader();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rTB_notes
            // 
            this.rTB_notes.BackColor = System.Drawing.Color.White;
            this.rTB_notes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rTB_notes.Location = new System.Drawing.Point(15, 154);
            this.rTB_notes.Name = "rTB_notes";
            this.rTB_notes.ReadOnly = true;
            this.rTB_notes.Size = new System.Drawing.Size(168, 78);
            this.rTB_notes.TabIndex = 7;
            this.rTB_notes.Text = "Note:\n- Hold down the Modifier to show notification \n- Release the Trigger first " +
                "to cancel the action\n";
            // 
            // lbl_executeBtn
            // 
            this.lbl_executeBtn.AutoSize = true;
            this.lbl_executeBtn.Location = new System.Drawing.Point(12, 82);
            this.lbl_executeBtn.Name = "lbl_executeBtn";
            this.lbl_executeBtn.Size = new System.Drawing.Size(78, 13);
            this.lbl_executeBtn.TabIndex = 3;
            this.lbl_executeBtn.Text = "Modifier Button";
            // 
            // lbl_holdBtn
            // 
            this.lbl_holdBtn.AutoSize = true;
            this.lbl_holdBtn.Location = new System.Drawing.Point(12, 7);
            this.lbl_holdBtn.Name = "lbl_holdBtn";
            this.lbl_holdBtn.Size = new System.Drawing.Size(74, 13);
            this.lbl_holdBtn.TabIndex = 2;
            this.lbl_holdBtn.Text = "Trigger Button";
            // 
            // cB_executeBtn
            // 
            this.cB_executeBtn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_executeBtn.FormattingEnabled = true;
            this.cB_executeBtn.Location = new System.Drawing.Point(15, 98);
            this.cB_executeBtn.Margin = new System.Windows.Forms.Padding(0);
            this.cB_executeBtn.Name = "cB_executeBtn";
            this.cB_executeBtn.Size = new System.Drawing.Size(154, 21);
            this.cB_executeBtn.TabIndex = 1;
            this.cB_executeBtn.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // cB_holdBtn
            // 
            this.cB_holdBtn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_holdBtn.FormattingEnabled = true;
            this.cB_holdBtn.Location = new System.Drawing.Point(15, 23);
            this.cB_holdBtn.Name = "cB_holdBtn";
            this.cB_holdBtn.Size = new System.Drawing.Size(154, 21);
            this.cB_holdBtn.TabIndex = 0;
            this.cB_holdBtn.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.rTB_clickExecuteBtn);
            this.panel1.Controls.Add(this.rTB_pushHoldBtn);
            this.panel1.Controls.Add(this.lbl_holdBtn);
            this.panel1.Controls.Add(this.cB_holdBtn);
            this.panel1.Controls.Add(this.cB_executeBtn);
            this.panel1.Controls.Add(this.rTB_notes);
            this.panel1.Controls.Add(this.lbl_executeBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(195, 238);
            this.panel1.TabIndex = 9;
            // 
            // rTB_clickExecuteBtn
            // 
            this.rTB_clickExecuteBtn.BackColor = System.Drawing.Color.White;
            this.rTB_clickExecuteBtn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rTB_clickExecuteBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rTB_clickExecuteBtn.Location = new System.Drawing.Point(15, 122);
            this.rTB_clickExecuteBtn.Name = "rTB_clickExecuteBtn";
            this.rTB_clickExecuteBtn.ReadOnly = true;
            this.rTB_clickExecuteBtn.Size = new System.Drawing.Size(168, 30);
            this.rTB_clickExecuteBtn.TabIndex = 14;
            this.rTB_clickExecuteBtn.Text = "2.) Click to invoke the action";
            // 
            // rTB_pushHoldBtn
            // 
            this.rTB_pushHoldBtn.BackColor = System.Drawing.Color.White;
            this.rTB_pushHoldBtn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rTB_pushHoldBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rTB_pushHoldBtn.Location = new System.Drawing.Point(15, 48);
            this.rTB_pushHoldBtn.Name = "rTB_pushHoldBtn";
            this.rTB_pushHoldBtn.ReadOnly = true;
            this.rTB_pushHoldBtn.Size = new System.Drawing.Size(168, 30);
            this.rTB_pushHoldBtn.TabIndex = 13;
            this.rTB_pushHoldBtn.Text = "1.) Push and hold this button";
            // 
            // lV_buttonMatchedGestures
            // 
            this.lV_buttonMatchedGestures.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cH_associatedActions,
            this.cH_group});
            this.lV_buttonMatchedGestures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lV_buttonMatchedGestures.Location = new System.Drawing.Point(0, 238);
            this.lV_buttonMatchedGestures.Name = "lV_buttonMatchedGestures";
            this.lV_buttonMatchedGestures.Size = new System.Drawing.Size(195, 155);
            this.lV_buttonMatchedGestures.TabIndex = 10;
            this.lV_buttonMatchedGestures.UseCompatibleStateImageBehavior = false;
            this.lV_buttonMatchedGestures.View = System.Windows.Forms.View.Details;
            // 
            // cH_associatedActions
            // 
            this.cH_associatedActions.Text = "Associated Actions";
            this.cH_associatedActions.Width = 113;
            // 
            // cH_group
            // 
            this.cH_group.Text = "Group";
            this.cH_group.Width = 72;
            // 
            // UC_TP_doubleBtn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lV_buttonMatchedGestures);
            this.Controls.Add(this.panel1);
            this.Name = "UC_TP_doubleBtn";
            this.Size = new System.Drawing.Size(195, 393);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rTB_notes;
        private System.Windows.Forms.Label lbl_executeBtn;
        private System.Windows.Forms.Label lbl_holdBtn;
        private System.Windows.Forms.ComboBox cB_executeBtn;
        private System.Windows.Forms.ComboBox cB_holdBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lV_buttonMatchedGestures;
        private System.Windows.Forms.ColumnHeader cH_associatedActions;
        private System.Windows.Forms.ColumnHeader cH_group;
        private System.Windows.Forms.RichTextBox rTB_clickExecuteBtn;
        private System.Windows.Forms.RichTextBox rTB_pushHoldBtn;
    }
}
