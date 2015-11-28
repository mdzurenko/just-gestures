namespace JustGestures.ControlItems
{
    partial class UC_infoIcon
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
            this.pB_info = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pB_info)).BeginInit();
            this.SuspendLayout();
            // 
            // pB_info
            // 
            this.pB_info.Dock = System.Windows.Forms.DockStyle.Right;
            this.pB_info.Location = new System.Drawing.Point(40, 0);
            this.pB_info.Name = "pB_info";
            this.pB_info.Size = new System.Drawing.Size(38, 34);
            this.pB_info.TabIndex = 5;
            this.pB_info.TabStop = false;
            this.pB_info.MouseLeave += new System.EventHandler(this.pB_info_MouseLeave);
            this.pB_info.MouseHover += new System.EventHandler(this.pB_info_MouseHover);
            this.pB_info.MouseEnter += new System.EventHandler(this.pB_info_MouseEnter);
            // 
            // UC_infoIcon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pB_info);
            this.Name = "UC_infoIcon";
            this.Size = new System.Drawing.Size(78, 34);
            this.Load += new System.EventHandler(this.UC_infoIcon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pB_info)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pB_info;
    }
}
