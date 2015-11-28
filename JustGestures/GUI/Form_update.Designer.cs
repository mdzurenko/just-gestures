namespace JustGestures.GUI
{
    partial class Form_update
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
            this.components = new System.ComponentModel.Container();
            this.lbl_newUpdate = new System.Windows.Forms.Label();
            this.btn_download = new System.Windows.Forms.Button();
            this.lbl_version = new System.Windows.Forms.Label();
            this.lbl_features = new System.Windows.Forms.Label();
            this.lbl_versionNumber = new System.Windows.Forms.Label();
            this.rTB_features = new System.Windows.Forms.RichTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lbl_newUpdate
            // 
            this.lbl_newUpdate.AutoSize = true;
            this.lbl_newUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_newUpdate.Location = new System.Drawing.Point(77, 19);
            this.lbl_newUpdate.Name = "lbl_newUpdate";
            this.lbl_newUpdate.Size = new System.Drawing.Size(202, 20);
            this.lbl_newUpdate.TabIndex = 1;
            this.lbl_newUpdate.Text = "New update is available!";
            // 
            // btn_download
            // 
            this.btn_download.AutoSize = true;
            this.btn_download.Location = new System.Drawing.Point(110, 180);
            this.btn_download.Name = "btn_download";
            this.btn_download.Size = new System.Drawing.Size(133, 23);
            this.btn_download.TabIndex = 2;
            this.btn_download.Text = "Download Now!";
            this.btn_download.UseVisualStyleBackColor = true;
            this.btn_download.Click += new System.EventHandler(this.btn_download_Click);
            // 
            // lbl_version
            // 
            this.lbl_version.AutoSize = true;
            this.lbl_version.Location = new System.Drawing.Point(14, 65);
            this.lbl_version.Name = "lbl_version";
            this.lbl_version.Size = new System.Drawing.Size(45, 13);
            this.lbl_version.TabIndex = 3;
            this.lbl_version.Text = "Version:";
            // 
            // lbl_features
            // 
            this.lbl_features.AutoSize = true;
            this.lbl_features.Location = new System.Drawing.Point(14, 89);
            this.lbl_features.Name = "lbl_features";
            this.lbl_features.Size = new System.Drawing.Size(51, 13);
            this.lbl_features.TabIndex = 4;
            this.lbl_features.Text = "Features:";
            // 
            // lbl_versionNumber
            // 
            this.lbl_versionNumber.AutoSize = true;
            this.lbl_versionNumber.Location = new System.Drawing.Point(92, 65);
            this.lbl_versionNumber.Name = "lbl_versionNumber";
            this.lbl_versionNumber.Size = new System.Drawing.Size(82, 13);
            this.lbl_versionNumber.TabIndex = 5;
            this.lbl_versionNumber.Text = "version_number";
            // 
            // rTB_features
            // 
            this.rTB_features.BackColor = System.Drawing.SystemColors.Control;
            this.rTB_features.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rTB_features.Location = new System.Drawing.Point(95, 89);
            this.rTB_features.Name = "rTB_features";
            this.rTB_features.Size = new System.Drawing.Size(198, 85);
            this.rTB_features.TabIndex = 6;
            this.rTB_features.Text = "- features 1\n- feature 2\n- bugs fixed\n";
            // 
            // Form_update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 219);
            this.Controls.Add(this.rTB_features);
            this.Controls.Add(this.lbl_versionNumber);
            this.Controls.Add(this.lbl_features);
            this.Controls.Add(this.lbl_version);
            this.Controls.Add(this.btn_download);
            this.Controls.Add(this.lbl_newUpdate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_update";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Just Gestures Update";
            this.Load += new System.EventHandler(this.Form_update_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_newUpdate;
        private System.Windows.Forms.Button btn_download;
        private System.Windows.Forms.Label lbl_version;
        private System.Windows.Forms.Label lbl_features;
        private System.Windows.Forms.Label lbl_versionNumber;
        private System.Windows.Forms.RichTextBox rTB_features;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}