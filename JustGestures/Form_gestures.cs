using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JustGestures
{
    public partial class Form_gestures : Form
    {
        private const double CURVE_MIN_LENGTH = 100;        
        List<PointF> m_pathPoints = new List<PointF>();
        Graphics m_gp = null;
        Pen m_pen = null;
        bool m_mouseDown = false;
        double m_curveLength = 0;


        public Form_gestures()
        {
            InitializeComponent();

            m_pen = new Pen(Color.Blue, 3);
            m_pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            m_pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            Bitmap bmp = new Bitmap(pB_display.Width, pB_display.Height);
            m_gp = Graphics.FromImage(bmp);
            m_gp.FillRectangle(Brushes.White, 0, 0, pB_display.Width, pB_display.Height);
            //m_gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pB_display.Image = bmp;
            m_gp.Dispose();
        }

        private void pB_display_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_curveLength = 0;
                m_mouseDown = true;
                m_gp = Graphics.FromImage(pB_display.Image);
                m_gp.FillRectangle(Brushes.White, 0, 0, pB_display.Width, pB_display.Height);
                m_pathPoints = new List<PointF>();
                Point point = new Point(e.X, e.Y);
                m_pen.Color = Color.Red;
                m_pathPoints.Add(point);
            }
        }

        private void pB_display_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_mouseDown)
            {
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
                    DrawLine(m_pathPoints, m_pen, true);
                else
                    m_gp.DrawLine(m_pen, m_pathPoints[m_pathPoints.Count - 1], m_pathPoints[m_pathPoints.Count - 2]);
                pB_display.Invalidate();

            }
        }

        private void pB_display_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && m_pathPoints.Count > 1)
            {
                m_gp.Dispose();
                m_mouseDown = false;
                RedrawAll();
            }
        }

        private void DrawLine(List<PointF> points, Pen pen, bool continuous)
        {
            float diff = (float)pen.Width / (float)2;
            for (int i = 0; i < points.Count - 1; i++)
            {
                if (continuous)
                    m_gp.DrawLine(pen, points[i], points[i + 1]);
                else
                    m_gp.FillEllipse(pen.Brush, points[i].X - diff, points[i].Y - diff, pen.Width, pen.Width);
            }
            m_gp.FillEllipse(pen.Brush, points[points.Count - 1].X - diff, points[points.Count - 1].Y - diff, pen.Width, pen.Width);
            
        }


        private void btn_redraw_Click(object sender, EventArgs e)
        {
            RedrawAll();
        }

        private void RedrawAll()
        {
            m_gp = Graphics.FromImage(pB_display.Image);
            m_gp.FillRectangle(Brushes.White, 0, 0, pB_display.Width, pB_display.Height);
            m_pen.Color = Color.Red;

            int count = Int32.Parse(tB_approxPoints.Text);            
            List<PointF> bezierCurve = MyCurve.CreateBezierCurve(m_pathPoints);
            List<PointF> approxPoints = MyCurve.ApproximateWithPoints(m_pathPoints, count);
            List<PointF> exactPath = MyCurve.CreateExactPath(m_pathPoints, Int32.Parse(tB_exactPathMin.Text), Int32.Parse(tB_exactPathMax.Text));

            if (cB_originalCurve.Checked)
            {
                DrawLine(m_pathPoints, m_pen, true);
                DrawLine(m_pathPoints, new Pen(Brushes.Black, 8), false);
            }
            if (cB_curvePoints.Checked)
                DrawLine(m_pathPoints, new Pen(Brushes.Yellow, 4), false);
            if (cB_bezierCurve.Checked)
                DrawLine(bezierCurve, new Pen(Brushes.Blue, 2), true);
            if (cB_approxPoints.Checked)
                DrawLine(approxPoints, new Pen(Brushes.Green, 6), false);
            if (cB_exactPath.Checked)
                DrawLine(exactPath, new Pen(Brushes.Black, 4), false);
            
            pB_display.Invalidate();                
            m_gp.Dispose();
        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            m_gp = Graphics.FromImage(pB_display.Image);
            m_gp.FillRectangle(Brushes.White, 0, 0, pB_display.Width, pB_display.Height);
            m_pen.Color = Color.Red;

            int count = Int32.Parse(tB_approxPoints.Text);
            List<PointF> exactPath = MyCurve.CreateExactPath(m_pathPoints, Int32.Parse(tB_exactPathMin.Text), Int32.Parse(tB_exactPathMax.Text));
            //PointF one = new PointF(exactPath[0].X, exactPath[0].Y);
            //PointF two = new PointF(exactPath[1].X, exactPath[1].Y);
            //PointF last = new PointF(exactPath[exactPath.Count - 1].X, exactPath[exactPath.Count - 1].Y);
            //PointF beforeLast = new PointF(exactPath[exactPath.Count - 2].X, exactPath[exactPath.Count - 2].Y);
            //exactPath.Insert(0, new PointF(one.X - (two.X - one.X), one.Y - (two.Y - one.Y)));
            //exactPath.Add(new PointF(last.X + (last.X - beforeLast.X), last.Y + (last.Y - beforeLast.Y)));

            List<PointF> generated = MyCurve.GenerateFalseCurve(exactPath); 
            //List<PointF> generated = MyCurve.GenerateCurve(exactPath); 

            //List<PointF> bezierCurve = MyCurve.CreateBezierCurve(m_pathPoints);
            List<PointF> approxPoints = MyCurve.ApproximateWithPoints(generated, count);
            

            if (cB_originalCurve.Checked)
                DrawLine(m_pathPoints, m_pen, true);
            if (cB_curvePoints.Checked)
                DrawLine(m_pathPoints, new Pen(Brushes.Yellow, 4), false);
            //if (cB_bezierCurve.Checked)
            //    DrawLine(bezierCurve, new Pen(Brushes.Blue, 2), true);                      
            if (cB_generated.Checked)
                DrawLine(generated, new Pen(Brushes.Blue, 4), false);
            if (cB_exactPath.Checked)
                DrawLine(exactPath, new Pen(Brushes.Black, 4), false);
            if (cB_approxPoints.Checked)
                DrawLine(approxPoints, new Pen(Brushes.Green, 2), true);  


            pB_display.Invalidate();
            m_gp.Dispose();
        }

    }
}
