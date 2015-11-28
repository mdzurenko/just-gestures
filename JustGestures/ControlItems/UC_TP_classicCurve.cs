using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using JustGestures.GestureParts;
using JustGestures.Languages;

namespace JustGestures.ControlItems
{
    public partial class UC_TP_classicCurve : UC_TP_baseActivator
    {
        private const double MAX_DIVERGENCE = 1.5;
        private const double CURVE_MIN_LENGTH = 100;
        private List<PointF> m_pathPoints = new List<PointF>();
        private Pen m_pen = null;
        MyNeuralNetwork m_network;
        MyNeuralNetwork m_networkBackup;
        MyNeuralNetwork m_networkTrained;
        MyNeuralNetwork m_networkOriginal;
        ClassicCurve m_newCurve = null;
        ClassicCurve m_recognizedCurve = null;
        bool m_newCurveUsed = true;
        bool m_tooMuchSimilar = false;
        bool m_shortCurve = false;
        bool m_mouseDown = false;
        bool m_maximumLimitReached = false;
        double m_curveLength = 0;

        public MyNeuralNetwork MyNNetwork 
        { 
            get { return m_network; } set { m_network = value; } 
        }

        public MyNeuralNetwork MyNNetworkOriginal 
        { 
            set { m_networkOriginal = value; } 
        }

        public UC_TP_classicCurve()
        {
            InitializeComponent();
        }

        public override void Initialize()           
        {
            base.Initialize();
            if (m_gesturesCollection.GetCurves().Count >= Config.User.NnOutputSize)
                m_maximumLimitReached = true;

            m_pen = new Pen(Color.Blue, 3);
            m_pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            m_pen.EndCap = System.Drawing.Drawing2D.LineCap.Round; 

            m_networkBackup = new MyNeuralNetwork(m_network);
            m_networkTrained = new MyNeuralNetwork(m_network);
            foreach (MyGesture gest in m_gesturesCollection.Gestures)
                if (gest.Activator.Type == MouseActivator.Types.ClassicCurve && !imageList1.Images.ContainsKey(gest.Activator.ID))
                {
                    ClassicCurve classicCurve = gest.Activator;
                    imageList1.Images.Add(gest.Activator.ID, gest.Activator.ExtractIcon(imageList1.ImageSize));
                    ListViewItem item = new ListViewItem(new string[] { "", classicCurve.Caption }, gest.Activator.ID);
                    item.Name = gest.Activator.ID;
                    lv_curvesList.Items.Add(item);
                }

            if (m_tempGesture.Activator != null && m_tempGesture.Activator.Type == BaseActivator.Types.ClassicCurve)
            {
                ClassicCurve classicCurve = m_tempGesture.Activator;
                m_newCurve = m_tempGesture.Activator;
                tB_newName.Text = classicCurve.Caption;
                if (lv_curvesList.Items.ContainsKey(m_tempGesture.Activator.ID))
                {
                    //sameCurve = true;
                    lv_curvesList.Focus();
                    lv_curvesList.EnsureVisible(lv_curvesList.Items[m_tempGesture.Activator.ID].Index);
                    lv_curvesList.Items[m_tempGesture.Activator.ID].Selected = true;
                    rB_newCurve.Enabled = false;
                }
                else
                {
                    gB_use.Enabled = true;
                    m_newCurveUsed = false;
                    m_networkTrained = new MyNeuralNetwork(m_networkOriginal);
                    rB_newCurve.Checked = true;
                    //SetNewGesture();
                }
            }
            else
            {
                rB_newCurve.Enabled = false;
                tB_newName.Enabled = false;
                tB_newName.Text = "curve_name";
            }
            gB_alreadyInUse.Text = Translation.GetText("C_CurveG_gB_alreadyInUse");
            cH_curve.Text = Translation.GetText("C_CurveG_cH_curve");
            cH_name.Text = Translation.GetText("C_CurveG_cH_name");
            cH_associatedActions.Text = Translation.GetText("C_Gestures_cH_associatedActions");
            cH_group.Text = Translation.GetText("C_Gestures_cH_group");
            gB_use.Text = Translation.GetText("C_CurveG_gB_useCurve");
            rB_newCurve.Text = Translation.GetText("C_CurveG_rB_newOne");
            rB_suggestedCurve.Text = Translation.GetText("C_CurveG_rB_selected");
            lbl_name.Text = Translation.GetText("C_CurveG_lbl_name");

        }

        private void lv_curvesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            lV_curvesMatchedGestures.Items.Clear();
            if (lv_curvesList.SelectedItems.Count != 1) return;
            //int count = 0;            
            List<MyGesture> matchedGest = m_gesturesCollection.MatchedGestures(lv_curvesList.SelectedItems[0].Name);
            if (matchedGest != null)
                foreach (MyGesture gest in matchedGest)
                {
                    lV_curvesMatchedGestures.Items.Add(new ListViewItem(new string[] { gest.Caption, gest.AppGroup.Caption }));
                }
            m_recognizedCurve = m_gesturesCollection.GetCurve(lv_curvesList.SelectedItems[0].Name);
            rB_suggestedCurve.Enabled = true;
            if (!rB_suggestedCurve.Checked) rB_suggestedCurve.Checked = true;
            else
            {
                Debug.WriteLine("Drawing - SelectedIndexChanged");
                m_recognizedCurve.DrawToPictureBox(m_pbDisplay);
                SetFromRecognizedGesture();
                if (!m_maximumLimitReached)
                    SetInfoValues();
            }
        }

        public override void Display_MouseDown(object sender, MouseEventArgs e)
        {
            //if (m_tempGesture.ScriptContainsMouse)
            //{
            //    SetInfoValues();
            //    OnCanContinue(false);
            //    return;
            //}
            if (m_maximumLimitReached)
            {
                //SetInfoValues();
                //OnCanContinue(false);
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                m_mouseDown = true;
                m_gp = Graphics.FromImage(m_pbDisplay.Image);
                if (m_tempGesture.Activator != null && (m_tempGesture.Activator.ThreadAnimating == null ||
                    !m_tempGesture.Activator.ThreadAnimating.IsAlive))
                {
                    m_gp.FillRectangle(Brushes.White, 0, 0, m_pbDisplay.Width, m_pbDisplay.Height);
                }
                m_pathPoints = new List<PointF>();
                Point point = new Point(e.X, e.Y);
                m_curveLength = 0;
                m_pen.Color = Color.Red;
                m_pathPoints.Add(point);
            }
        }

        public override void Display_MouseMove(object sender, MouseEventArgs e)
        {            
            if (e.Button == MouseButtons.Left && m_mouseDown && m_pathPoints != null && m_pen != null)
            {
                if (m_tempGesture.Activator != null && m_tempGesture.Activator.AbortAnimating())
                    m_gp.FillRectangle(Brushes.White, 0, 0, m_pbDisplay.Width, m_pbDisplay.Height);
                Point point = new Point(e.X, e.Y);
                m_pathPoints.Add(point);
                m_curveLength += MyCurve.Distance(m_pathPoints[m_pathPoints.Count - 2], m_pathPoints[m_pathPoints.Count - 1]);
                
                Color penColor = m_pen.Color;
                if (m_curveLength < CURVE_MIN_LENGTH || m_curveLength >= 1500)
                    m_pen.Color = Color.Red;
                else if (m_curveLength >= 100 && m_curveLength < 300 ||
                    m_curveLength >= 1300 && m_curveLength < 1500)
                    m_pen.Color = Color.Orange;
                else
                    m_pen.Color = Color.Green;
                
                if (penColor != m_pen.Color)
                    for (int i = 0; i < m_pathPoints.Count - 1; i++)
                        m_gp.DrawLine(m_pen, m_pathPoints[i], m_pathPoints[i + 1]);
        
                m_gp.DrawLine(m_pen, m_pathPoints[m_pathPoints.Count - 1], m_pathPoints[m_pathPoints.Count - 2]);
                m_pbDisplay.Invalidate();
            }
        }

        #region Not in use
        //private double GetOutPointsRatio(string learntCurveName)
        //{
        //    ClassicCurve learntCurve = m_gesturesCollection.GetCurve(learntCurveName);
        //    if (learntCurve != null)
        //    {
        //        Rectangle rect = new Rectangle(0, 0, 20, 20);
        //        int parts = 5;

        //        List<PointF> centerLearnPoints = MyCurve.ScaleToCenter(MyCurve.CreateExactPath(learntCurve.Points, 1, 2), rect);            
        //        int centerCount = centerLearnPoints.Count / parts;

        //        List<PointF> centerPoints = MyCurve.ScaleToCenter(MyCurve.CreateExactPath(m_pathPoints, 1, 2), rect);
        //        int testCount = centerPoints.Count / parts;
                
        //        int outPoints = 0;
        //        for (int i = 0; i < parts; i++)
        //        {
        //            int centerMax = centerCount;
        //            int testMax =testCount;
        //            if (i + 1 == parts)
        //            {
        //                centerMax = centerLearnPoints.Count - i * centerCount;
        //                testMax = centerPoints.Count - i * testCount;
        //            }
                    
        //            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
        //            List<PointF> center10 = centerLearnPoints.GetRange(i * centerCount, centerMax);
        //            path.AddLines(center10.ToArray());
        //            path.Widen(new Pen(Color.Green, 3));

        //            List<PointF> test10 = centerPoints.GetRange(i * testCount, testMax);
        //            foreach (PointF point in test10)
        //            {
        //                if (!path.IsVisible(point))
        //                    outPoints++;
        //            }
        //            m_gp.DrawPath(new Pen(Brushes.Yellow, 1), path);
        //            m_gp.DrawLines(new Pen(Brushes.Red, 1), test10.ToArray());
        //        }
        //        m_pbDisplay.Invalidate();
                

        //        double ratio = outPoints / (double)centerPoints.Count;
        //        Debug.WriteLine("Ratio of points out of region is: " + ratio);
        //        return ratio;

        //    }
        //    return 1;
        //}
        #endregion Not in use

        public override void Display_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && m_pathPoints.Count > 1)
            {
                m_mouseDown = false;
                MyCurve curve = new MyCurve(m_pathPoints, true);
                if (m_curveLength > CURVE_MIN_LENGTH)
                {
                    //if (curve != null && curve.Points.Count > 1)
                    //{
                    //    List<PointF> rotated = MyCurve.RotateCurve(curve.Points, 10);
                    //    m_gp.DrawLines(new Pen(Brushes.Yellow, 2), rotated.ToArray());
                    //    rotated = MyCurve.RotateCurve(curve.Points, -10);
                    //    m_gp.DrawLines(new Pen(Brushes.Pink, 2), rotated.ToArray());                        
                    //    m_pbDisplay.Invalidate();
                    //}
                    //return;

                    //if (curve != null && curve.Points.Count > 1)
                    //{
                    //    List<PointF> resized = MyCurve.ResizeCurve(curve.Points, 0.25, 0);
                    //    m_gp.DrawLines(new Pen(Brushes.Yellow, 2), resized.ToArray());
                    //    resized = MyCurve.ResizeCurve(curve.Points, -0.25, 0);
                    //    m_gp.DrawLines(new Pen(Brushes.Pink, 2), resized.ToArray());
                    //    m_pbDisplay.Invalidate();
                    //}
                    //return;

                    //m_network.StopLearning();
                    m_newCurveUsed = true;
                    m_networkTrained.StopLearning();
                    m_network = new MyNeuralNetwork(m_networkBackup);
                    m_shortCurve = false;
                    
                    double divergence;
                    string name = m_network.RecognizeCurve(curve.GetScaledInput(), out divergence);
                    m_tooMuchSimilar = (divergence >= 0 && divergence < MAX_DIVERGENCE); 

                    if (name != string.Empty)
                    {
                        if (m_tooMuchSimilar)
                            m_newCurve = m_gesturesCollection.GetCurve(name);
                        else
                            m_newCurve = new ClassicCurve(m_pathPoints, m_gesturesCollection); 
                        rB_newCurve.Enabled = !m_tooMuchSimilar;
                        lv_curvesList.Focus();
                        lv_curvesList.EnsureVisible(lv_curvesList.Items[name].Index);
                        if (!lv_curvesList.Items[name].Selected)
                            lv_curvesList.Items[name].Selected = true;
                        else
                        {
                            if (!rB_suggestedCurve.Checked) rB_suggestedCurve.Checked = true;
                            else
                            {
                                lv_curvesList.Items[name].Selected = false;
                                lv_curvesList.Items[name].Selected = true;
                                //panel_curveDraw.Refresh();
                            }
                        }
                    }
                    else
                    {
                        m_newCurve = new ClassicCurve(m_pathPoints, m_gesturesCollection);
                        rB_newCurve.Enabled = true;
                        if (!rB_newCurve.Checked) rB_newCurve.Checked = true;
                        else
                        {
                            Debug.WriteLine("Drawing - MouseUp (new gesture)");
                            m_newCurve.DrawToPictureBox(m_pbDisplay);                          
                            SetNewGesture();
                        }
                    }
                    OnCanContinue(true);
                    SetInfoValues();
                }
                else
                {                    
                    m_newCurve = null;
                    m_shortCurve = true;
                    //Debug.WriteLine("Drawing - MouseUp (short gesture)");  
                    m_gp = Graphics.FromImage(m_pbDisplay.Image);
                    m_gp.FillRectangle(Brushes.White, 0, 0, m_pbDisplay.Width, m_pbDisplay.Height);
                    m_pbDisplay.Invalidate();
                    m_pbDisplay.Focus();
                    rB_newCurve.Enabled = false;
                    OnCanContinue(false);
                    SetInfoValues();
                }             
                m_gp.Dispose();
            }
        }

        public override void Display_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && m_pathPoints.Count <= 1 && !m_shortCurve)
            {
                base.Display_MouseClick(sender, e);
            }
        }

        private void SetInfoValues()
        {
            if (m_maximumLimitReached)
                OnChangeInfoText(ToolTipIcon.Warning, Translation.Text_warning, Translation.GetText("C_CurveG_warnCurvesLimitReached")); //"You have reached maximum amount of Classic Curve Gestures. \nIt is possible to chose only from already learnt ones.");
            else
            {
                if (m_shortCurve)
                    OnChangeInfoText(ToolTipIcon.Error, Translation.Text_error, Translation.GetText("C_CurveG_errTooShortCurve"));// "Curve is too short. You cannot continue.");
                else
                {
                    if (Config.User.UsingClassicCurve)
                        OnChangeInfoText(ToolTipIcon.Info, Translation.Text_info, Translation.GetText("C_CurveG_info"));// "To invoke this gesture push the Toggle button and move mouse out of the Sensitive Zone in the Deactivation Timeout. \nYou can change this parameters via: Options -> Gestures");
                    else
                        OnChangeInfoText(ToolTipIcon.Warning, Translation.Text_warning, Translation.GetText("C_CurveG_warnCurveGDeactivated"));// "Gestures - Classic Curve - are currently deactivated, you may still add it but will not be functional till it is activated. \nIt is possible to change this property via: Options -> Gestures");
                }
            }
        }

        public override void SetValues()
        {
            OnChangeAboutText(Translation.GetText("C_CurveG_about"));// "Use LEFT mouse button to draw gesture on panel");
            SetInfoValues();

            m_pbDisplay.Enabled = true;
            gB_alreadyInUse.Enabled = true;
            gB_use.Enabled = true;

            if (rB_newCurve.Checked)
                m_tempGesture.Activator = m_newCurve;
            else
                m_tempGesture.Activator = m_recognizedCurve;
            if (m_tempGesture.Activator == null) OnCanContinue(false);
            else
            {
                SetCurveCaption();
                OnCanContinue(true);
            }
            base.RedrawDisplay();

        }

        private void SetNewGesture()
        {
            if (m_newCurveUsed)
            {                
                m_networkTrained.StopLearning();
                m_networkTrained = new MyNeuralNetwork(m_networkBackup);
                m_networkTrained.LearnNewCurve(m_newCurve);
                m_newCurveUsed = false;
            }
            m_network = m_networkTrained;
            //tB_newName.Enabled = true;
            m_tempGesture.Activator = new MouseActivator(m_newCurve);            
            SetCurveCaption();
        }

        private void SetFromRecognizedGesture()
        {
            //m_network.StopLearning();
            m_network = new MyNeuralNetwork(m_networkBackup);
            m_shortCurve = false;
            m_tempGesture.Activator = new MouseActivator(m_recognizedCurve);
            OnCanContinue(true);
        }

        private void rB_CheckedChanged(object sender, EventArgs e)
        {
            if (!((RadioButton)sender).Checked) return;

            rB_suggestedCurve.Font = new Font(rB_suggestedCurve.Font, FontStyle.Regular);
            rB_newCurve.Font = new Font(rB_newCurve.Font, FontStyle.Regular);
            tB_newName.Enabled = rB_newCurve.Checked;
            if (rB_newCurve.Checked)
            {
                rB_newCurve.Font = new Font(rB_newCurve.Font, FontStyle.Bold);
                if (m_newCurve != null && m_newCurve.Points.Count != 0)
                {                    
                    SetNewGesture();
                    base.RedrawDisplay();
                    tB_newName.Focus();
                }
                else
                {
                    m_gp = Graphics.FromImage(m_pbDisplay.Image);
                    m_gp.FillRectangle(Brushes.White, 0, 0, m_pbDisplay.Width, m_pbDisplay.Height);
                    m_pbDisplay.Invalidate();
                    m_pbDisplay.Focus();
                    OnCanContinue(false);
                }
            }
            else
            {
                rB_suggestedCurve.Font = new Font(rB_suggestedCurve.Font, FontStyle.Bold);
                //tB_newName.Enabled = false;
                SetFromRecognizedGesture(); 
                base.RedrawDisplay();
                lv_curvesList.Focus();
            }
        }

        private void SetCurveCaption()
        {
            if (m_tempGesture.Activator != null && m_tempGesture.Activator.Type == BaseActivator.Types.ClassicCurve)
                ((ClassicCurve)m_tempGesture.Activator).Caption = tB_newName.Text;
        }

        private void tB_newName_TextChanged(object sender, EventArgs e)
        {
            SetCurveCaption();
        }

        private void lv_curvesList_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewItem item = lv_curvesList.GetItemAt(e.X, e.Y);
            if (item != null && item.Selected)
            {
                item.Selected = false;
                item.Selected = true;
            }
        }
    }
}
