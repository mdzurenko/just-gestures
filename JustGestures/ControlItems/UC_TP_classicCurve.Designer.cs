namespace JustGestures.ControlItems
{
    partial class UC_TP_classicCurve
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
            this.lV_curvesMatchedGestures = new System.Windows.Forms.ListView();
            this.cH_associatedActions = new System.Windows.Forms.ColumnHeader();
            this.cH_group = new System.Windows.Forms.ColumnHeader();
            this.lv_curvesList = new System.Windows.Forms.ListView();
            this.cH_curve = new System.Windows.Forms.ColumnHeader();
            this.cH_name = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lbl_name = new System.Windows.Forms.Label();
            this.tB_newName = new System.Windows.Forms.TextBox();
            this.rB_newCurve = new System.Windows.Forms.RadioButton();
            this.gB_use = new System.Windows.Forms.GroupBox();
            this.rB_suggestedCurve = new System.Windows.Forms.RadioButton();
            this.gB_alreadyInUse = new System.Windows.Forms.GroupBox();
            this.gB_use.SuspendLayout();
            this.gB_alreadyInUse.SuspendLayout();
            this.SuspendLayout();
            // 
            // lV_curvesMatchedGestures
            // 
            this.lV_curvesMatchedGestures.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cH_associatedActions,
            this.cH_group});
            this.lV_curvesMatchedGestures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lV_curvesMatchedGestures.Location = new System.Drawing.Point(3, 210);
            this.lV_curvesMatchedGestures.Name = "lV_curvesMatchedGestures";
            this.lV_curvesMatchedGestures.Size = new System.Drawing.Size(198, 103);
            this.lV_curvesMatchedGestures.TabIndex = 3;
            this.lV_curvesMatchedGestures.UseCompatibleStateImageBehavior = false;
            this.lV_curvesMatchedGestures.View = System.Windows.Forms.View.Details;
            // 
            // cH_matchedGestures
            // 
            this.cH_associatedActions.Text = "Associated Actions";
            this.cH_associatedActions.Width = 117;
            // 
            // cH_group
            // 
            this.cH_group.Text = "Group";
            this.cH_group.Width = 76;
            // 
            // lv_curvesList
            // 
            this.lv_curvesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cH_curve,
            this.cH_name});
            this.lv_curvesList.Dock = System.Windows.Forms.DockStyle.Top;
            this.lv_curvesList.FullRowSelect = true;
            this.lv_curvesList.HideSelection = false;
            this.lv_curvesList.Location = new System.Drawing.Point(3, 16);
            this.lv_curvesList.Margin = new System.Windows.Forms.Padding(0);
            this.lv_curvesList.MultiSelect = false;
            this.lv_curvesList.Name = "lv_curvesList";
            this.lv_curvesList.Size = new System.Drawing.Size(198, 194);
            this.lv_curvesList.SmallImageList = this.imageList1;
            this.lv_curvesList.TabIndex = 0;
            this.lv_curvesList.UseCompatibleStateImageBehavior = false;
            this.lv_curvesList.View = System.Windows.Forms.View.Details;
            this.lv_curvesList.SelectedIndexChanged += new System.EventHandler(this.lv_curvesList_SelectedIndexChanged);
            this.lv_curvesList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lv_curvesList_MouseDown);
            // 
            // cH_curve
            // 
            this.cH_curve.Text = "Curve";
            this.cH_curve.Width = 72;
            // 
            // cH_name
            // 
            this.cH_name.Text = "Name";
            this.cH_name.Width = 85;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(24, 24);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(3, 45);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(35, 13);
            this.lbl_name.TabIndex = 0;
            this.lbl_name.Text = "Name";
            // 
            // tB_newName
            // 
            this.tB_newName.Location = new System.Drawing.Point(78, 42);
            this.tB_newName.Name = "tB_newName";
            this.tB_newName.Size = new System.Drawing.Size(116, 20);
            this.tB_newName.TabIndex = 1;
            this.tB_newName.TextChanged += new System.EventHandler(this.tB_newName_TextChanged);
            // 
            // rB_newCurve
            // 
            this.rB_newCurve.AutoSize = true;
            this.rB_newCurve.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rB_newCurve.Location = new System.Drawing.Point(6, 19);
            this.rB_newCurve.Name = "rB_newCurve";
            this.rB_newCurve.Size = new System.Drawing.Size(71, 17);
            this.rB_newCurve.TabIndex = 0;
            this.rB_newCurve.Tag = "0";
            this.rB_newCurve.Text = "New  one";
            this.rB_newCurve.UseVisualStyleBackColor = true;
            this.rB_newCurve.CheckedChanged += new System.EventHandler(this.rB_CheckedChanged);
            // 
            // gB_use
            // 
            this.gB_use.Controls.Add(this.rB_suggestedCurve);
            this.gB_use.Controls.Add(this.lbl_name);
            this.gB_use.Controls.Add(this.rB_newCurve);
            this.gB_use.Controls.Add(this.tB_newName);
            this.gB_use.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gB_use.Location = new System.Drawing.Point(0, 316);
            this.gB_use.Name = "gB_use";
            this.gB_use.Size = new System.Drawing.Size(204, 73);
            this.gB_use.TabIndex = 10;
            this.gB_use.TabStop = false;
            this.gB_use.Text = "Use Curve";
            // 
            // rB_suggestedCurve
            // 
            this.rB_suggestedCurve.AutoSize = true;
            this.rB_suggestedCurve.Enabled = false;
            this.rB_suggestedCurve.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rB_suggestedCurve.Location = new System.Drawing.Point(103, 19);
            this.rB_suggestedCurve.Name = "rB_suggestedCurve";
            this.rB_suggestedCurve.Size = new System.Drawing.Size(67, 17);
            this.rB_suggestedCurve.TabIndex = 2;
            this.rB_suggestedCurve.Tag = "1";
            this.rB_suggestedCurve.Text = "Selected";
            this.rB_suggestedCurve.UseVisualStyleBackColor = true;
            this.rB_suggestedCurve.CheckedChanged += new System.EventHandler(this.rB_CheckedChanged);
            // 
            // gB_alreadyInUse
            // 
            this.gB_alreadyInUse.Controls.Add(this.lV_curvesMatchedGestures);
            this.gB_alreadyInUse.Controls.Add(this.lv_curvesList);
            this.gB_alreadyInUse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gB_alreadyInUse.Location = new System.Drawing.Point(0, 0);
            this.gB_alreadyInUse.Name = "gB_alreadyInUse";
            this.gB_alreadyInUse.Size = new System.Drawing.Size(204, 316);
            this.gB_alreadyInUse.TabIndex = 11;
            this.gB_alreadyInUse.TabStop = false;
            this.gB_alreadyInUse.Text = "Already in use";
            // 
            // UC_TP_classicCurve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gB_alreadyInUse);
            this.Controls.Add(this.gB_use);
            this.Name = "UC_TP_classicCurve";
            this.Size = new System.Drawing.Size(204, 389);
            this.gB_use.ResumeLayout(false);
            this.gB_use.PerformLayout();
            this.gB_alreadyInUse.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lV_curvesMatchedGestures;
        private System.Windows.Forms.ColumnHeader cH_associatedActions;
        private System.Windows.Forms.ListView lv_curvesList;
        private System.Windows.Forms.ColumnHeader cH_curve;
        private System.Windows.Forms.ColumnHeader cH_name;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.TextBox tB_newName;
        private System.Windows.Forms.RadioButton rB_newCurve;
        private System.Windows.Forms.GroupBox gB_use;
        private System.Windows.Forms.GroupBox gB_alreadyInUse;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader cH_group;
        private System.Windows.Forms.RadioButton rB_suggestedCurve;
    }
}
