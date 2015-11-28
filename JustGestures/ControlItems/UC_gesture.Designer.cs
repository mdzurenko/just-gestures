namespace JustGestures.ControlItems
{
    partial class UC_gesture
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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl_invokeAction = new System.Windows.Forms.TabControl();
            this.tP_classicCurves = new System.Windows.Forms.TabPage();
            this.m_tpClassicCurve = new JustGestures.ControlItems.UC_TP_classicCurve();
            this.tP_rockerGesture = new System.Windows.Forms.TabPage();
            this.m_tPdoubleBtn = new JustGestures.ControlItems.UC_TP_doubleBtn();
            this.tP_wheelGesture = new System.Windows.Forms.TabPage();
            this.m_tpWheelButton = new JustGestures.ControlItems.UC_TP_wheelBtn();
            this.pb_Display = new System.Windows.Forms.PictureBox();
            this.tabControl_invokeAction.SuspendLayout();
            this.tP_classicCurves.SuspendLayout();
            this.tP_rockerGesture.SuspendLayout();
            this.tP_wheelGesture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Display)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tabControl_invokeAction
            // 
            this.tabControl_invokeAction.Controls.Add(this.tP_classicCurves);
            this.tabControl_invokeAction.Controls.Add(this.tP_rockerGesture);
            this.tabControl_invokeAction.Controls.Add(this.tP_wheelGesture);
            this.tabControl_invokeAction.Dock = System.Windows.Forms.DockStyle.Right;
            this.tabControl_invokeAction.Location = new System.Drawing.Point(372, 0);
            this.tabControl_invokeAction.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl_invokeAction.Name = "tabControl_invokeAction";
            this.tabControl_invokeAction.Padding = new System.Drawing.Point(0, 0);
            this.tabControl_invokeAction.SelectedIndex = 0;
            this.tabControl_invokeAction.Size = new System.Drawing.Size(224, 478);
            this.tabControl_invokeAction.TabIndex = 1;
            this.tabControl_invokeAction.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl_invokeAction_DrawItem);
            this.tabControl_invokeAction.SelectedIndexChanged += new System.EventHandler(this.tabControl_invokeAction_SelectedIndexChanged);
            // 
            // tP_classicCurves
            // 
            this.tP_classicCurves.Controls.Add(this.m_tpClassicCurve);
            this.tP_classicCurves.Location = new System.Drawing.Point(4, 22);
            this.tP_classicCurves.Name = "tP_classicCurves";
            this.tP_classicCurves.Padding = new System.Windows.Forms.Padding(3);
            this.tP_classicCurves.Size = new System.Drawing.Size(216, 452);
            this.tP_classicCurves.TabIndex = 0;
            this.tP_classicCurves.Text = "Classic Curves";
            this.tP_classicCurves.UseVisualStyleBackColor = true;
            // 
            // m_tpClassicCurve
            // 
            this.m_tpClassicCurve.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tpClassicCurve.Gestures = null;
            this.m_tpClassicCurve.Location = new System.Drawing.Point(3, 3);
            this.m_tpClassicCurve.Margin = new System.Windows.Forms.Padding(0);
            this.m_tpClassicCurve.MyNNetwork = null;
            this.m_tpClassicCurve.Name = "m_tpClassicCurve";
            this.m_tpClassicCurve.PB_Display = null;
            this.m_tpClassicCurve.Size = new System.Drawing.Size(210, 446);
            this.m_tpClassicCurve.TabIndex = 0;
            this.m_tpClassicCurve.TempGesture = null;
            // 
            // tP_rockerGesture
            // 
            this.tP_rockerGesture.Controls.Add(this.m_tPdoubleBtn);
            this.tP_rockerGesture.Location = new System.Drawing.Point(4, 22);
            this.tP_rockerGesture.Name = "tP_rockerGesture";
            this.tP_rockerGesture.Padding = new System.Windows.Forms.Padding(3);
            this.tP_rockerGesture.Size = new System.Drawing.Size(216, 452);
            this.tP_rockerGesture.TabIndex = 1;
            this.tP_rockerGesture.Text = "Double Button Combo";
            this.tP_rockerGesture.UseVisualStyleBackColor = true;
            // 
            // m_tPdoubleBtn
            // 
            this.m_tPdoubleBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tPdoubleBtn.Gestures = null;
            this.m_tPdoubleBtn.Location = new System.Drawing.Point(3, 3);
            this.m_tPdoubleBtn.Margin = new System.Windows.Forms.Padding(0);
            this.m_tPdoubleBtn.Name = "m_tPdoubleBtn";
            this.m_tPdoubleBtn.PB_Display = null;
            this.m_tPdoubleBtn.Size = new System.Drawing.Size(210, 446);
            this.m_tPdoubleBtn.TabIndex = 0;
            this.m_tPdoubleBtn.TempGesture = null;
            // 
            // tP_wheelGesture
            // 
            this.tP_wheelGesture.Controls.Add(this.m_tpWheelButton);
            this.tP_wheelGesture.Location = new System.Drawing.Point(4, 22);
            this.tP_wheelGesture.Name = "tP_wheelGesture";
            this.tP_wheelGesture.Size = new System.Drawing.Size(216, 452);
            this.tP_wheelGesture.TabIndex = 2;
            this.tP_wheelGesture.Text = "Wheel & Button Combo";
            this.tP_wheelGesture.UseVisualStyleBackColor = true;
            // 
            // m_tpWheelButton
            // 
            this.m_tpWheelButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tpWheelButton.Gestures = null;
            this.m_tpWheelButton.Location = new System.Drawing.Point(0, 0);
            this.m_tpWheelButton.Margin = new System.Windows.Forms.Padding(0);
            this.m_tpWheelButton.Name = "m_tpWheelButton";
            this.m_tpWheelButton.PB_Display = null;
            this.m_tpWheelButton.Size = new System.Drawing.Size(216, 452);
            this.m_tpWheelButton.TabIndex = 0;
            this.m_tpWheelButton.TempGesture = null;
            // 
            // pb_Display
            // 
            this.pb_Display.BackColor = System.Drawing.Color.White;
            this.pb_Display.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pb_Display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_Display.Location = new System.Drawing.Point(0, 0);
            this.pb_Display.Margin = new System.Windows.Forms.Padding(0);
            this.pb_Display.Name = "pb_Display";
            this.pb_Display.Size = new System.Drawing.Size(372, 478);
            this.pb_Display.TabIndex = 0;
            this.pb_Display.TabStop = false;
            this.pb_Display.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_recognizedGesture_MouseMove);
            this.pb_Display.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_recognizedGesture_MouseClick);
            this.pb_Display.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_recognizedGesture_MouseDown);
            this.pb_Display.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_recognizedGesture_MouseUp);
            this.pb_Display.SizeChanged += new System.EventHandler(this.pb_Display_SizeChanged);
            // 
            // UC_gesture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pb_Display);
            this.Controls.Add(this.tabControl_invokeAction);
            this.Name = "UC_gesture";
            this.Size = new System.Drawing.Size(596, 478);
            this.Load += new System.EventHandler(this.UserControl_gesture_Load);
            this.VisibleChanged += new System.EventHandler(this.UC_gesture_VisibleChanged);
            this.tabControl_invokeAction.ResumeLayout(false);
            this.tP_classicCurves.ResumeLayout(false);
            this.tP_rockerGesture.ResumeLayout(false);
            this.tP_wheelGesture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Display)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pb_Display;
        private System.Windows.Forms.TabControl tabControl_invokeAction;
        private System.Windows.Forms.TabPage tP_classicCurves;
        private System.Windows.Forms.TabPage tP_rockerGesture;
        private JustGestures.ControlItems.UC_TP_classicCurve m_tpClassicCurve;
        private JustGestures.ControlItems.UC_TP_doubleBtn m_tPdoubleBtn;
        private System.Windows.Forms.TabPage tP_wheelGesture;
        private UC_TP_wheelBtn m_tpWheelButton;
    }
}
