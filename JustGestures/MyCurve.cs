using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using JustGestures.Properties;
using System.Diagnostics;


namespace JustGestures
{
    [Serializable]
    public class MyCurve : ISerializable
    {        
        static int m_approxPoints = (Config.User.NnInputSize / 2) + 1;
        static int m_setSize = Config.User.NnTrainingSetSize;
        /// <summary>
        /// Position of 1 in NN output
        /// </summary>
        //int m_position = -1;
        /// <summary>
        /// Unique curve's name
        /// </summary>
        string m_name = string.Empty;
        /// <summary>
        /// Points of original curve
        /// </summary>
        List<PointF> m_points = new List<PointF>();
        /// <summary>
        /// Bezier approximation of original curve
        /// </summary>
        //List<PointF> m_bezierCurve = null;
        /// <summary>
        /// Training set for NN
        /// </summary>
        double[][] m_trainingSet = null;

        //double[][] m_falsePatterns = null;
        
        public string ID { get { return m_name; } set { m_name = value; } }
        //public string Caption { get { return m_caption; } set { m_caption = value; } }
        //public int Position { get { return m_position; } set { m_position = value; } }
        public List<PointF> Points { get { return m_points; } }
        //public List<PointF> BezierCurve { get { return m_bezierCurve; } }
        public double[][] TrainingSet { get { return m_trainingSet; } }

        //public double[][] FalsePatterns { get { return m_falsePatterns; } }


        public MyCurve()
        {
        }

        public MyCurve(List<PointF> points, bool copy)
        {          
            // This constructor is required because when calling it from MyEngine where the enviroment is multithread
            // the copying process might cause exception: Destination array was not long enough. Check destIndex and length, and the array's lower bounds
            // It is very rare.... 
            if (copy)
                m_points.AddRange(points.ToArray());
            else
                m_points = points;
        }

        public MyCurve(string name, List<PointF> points)
        {
            m_points.AddRange(points);
            m_name = name;
        }

        public MyCurve(MyCurve curve)
        {
            m_name = curve.m_name;
            m_points.AddRange(curve.m_points.ToArray());
            m_trainingSet = curve.m_trainingSet;
        }

        public void AddPoint(PointF point)
        {
            m_points.Add(point);
        }       

        /// <summary>
        /// Returns approximed curve scaled to angles for NN input
        /// </summary>
        /// <param name="approxPoints">Amount of approxing points</param>
        /// <returns></returns>
        public double[] GetScaledInput()
        {
            // cannot calculate too long otherwise might be modified via MyEngine with new curve
            //List<PointF> approxedCurve = CreateExactPath(m_points, 20, 20);
            List<PointF> approxedCurve = ApproximateWithPoints(m_points, m_approxPoints);
            return CalculateAngles(approxedCurve);
        }

        /// <summary>
        /// Creates bezier curve for original one (call only for first time)
        /// </summary>
        /// <returns></returns>
        public List<PointF> GetBezierCurve()
        {
            //m_bezierCurve = CreateBezierCurve(m_points);
            return CreateBezierCurve(m_points);
        }

        /// <summary>
        /// Creates traning set for neural network (call only for first time)
        /// </summary>
        /// <param name="count">Amount of patterns to generate</param>
        /// <param name="approxPoints">Amount of approxing points</param>
        /// <returns></returns>
        public double[][] CreateTrainingSet()
        {
            m_trainingSet = GenerateTrainingSet(m_points, m_setSize, m_approxPoints);
            return m_trainingSet;
        }

        public double[][] GenerateFalsePatterns()
        {            
            return GenerateFalsePatterns(m_points, 2/*m_setSize / 10*/, m_approxPoints);
        }

        public static double[][] EmptyTrainingSet()
        {
            double[][] trainingSet = new double[m_setSize][];
            for (int i = 0; i < m_setSize; i++)
            {
                trainingSet[i] = new double[(m_approxPoints - 1) * 2];
            }
            return trainingSet;
        }


        /// <summary>
        /// Sets training set to null
        /// </summary>
        public void ReleaseTrainingSet()
        {
            m_trainingSet = null;
        }

        private static double GetLength(List<PointF> points)
        {
            float length = 0;
            for (int i = 0; i < points.Count - 1; i++)
                length += Distance(points[i], points[i + 1]);
            return Math.Floor((double)length);
        }

        /// <summary>
        /// Returns the length of the curve
        /// </summary>
        /// <returns></returns>
        public double GetLength()
        {
            return GetLength(m_points);
        }

        #region Curve's graphic ouput

        /// <summary>
        /// Returns bitmap with drawn curve in it
        /// </summary>
        /// <param name="curve">Curve</param>
        /// <param name="size">Size of bitmap</param>
        /// <returns></returns>
        public static Bitmap DrawCurveToBmp(List<PointF> curve, Size size)
        {
            Bitmap bmp = new Bitmap(size.Width, size.Height);
            if (curve == null || curve.Count == 0) return bmp;            

            List<PointF> new_curve = ScaleToCenter(curve, new Rectangle(0, 0, size.Width, size.Height));

            Pen pen = new Pen(Brushes.Blue, 2);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Graphics gp = Graphics.FromImage(bmp);
            gp.FillRectangle(Brushes.Transparent, 0, 0, bmp.Width, bmp.Height);
            gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            for (int i = 0; i < new_curve.Count - 1; i++)
            {                
                gp.DrawLine(pen, new_curve[i], new_curve[i + 1]);
            }

            gp.FillEllipse(Brushes.Green, new_curve[0].X - (float)1.5, new_curve[0].Y - (float)1.5, 3, 3);
            gp.FillEllipse(Brushes.Red, new_curve[new_curve.Count - 1].X - (float)1.5, new_curve[new_curve.Count - 1].Y - (float)1.5, 3, 3);
            gp.Dispose();
            return bmp;
        }

        /// <summary>
        /// Draws curve in to the panel
        /// </summary>
        /// <param name="panel">Panel</param>
        /// <param name="curve">Curve</param>
        /// <param name="delay">Delay if animation is requested</param>
        public static void DrawCurve(PictureBox pictureBox, List<PointF> curve, int delay)
        {
            Bitmap bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            //pictureBox.Image = bmp;            
            Pen pen = new Pen(Brushes.Blue, 6);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Graphics gp = Graphics.FromImage(bmp);
            gp.FillRectangle(Brushes.White, 0, 0, pictureBox.Width, pictureBox.Height);
            gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            pictureBox.Image = bmp;
            if (curve == null || curve.Count == 0)
            {
                gp.Dispose();
                return;
            }

            List<PointF> new_curve = ScaleToCenter(curve, pictureBox.ClientRectangle);
            if (delay != 0)
            {
                List<PointF> exactCurve = CreateExactPath(new_curve, 2, 4);
                new_curve = exactCurve;
            }
            
            //Brush brushStart = new System.Drawing.Drawing2D.LinearGradientBrush(
            //        new_curve[0], new_curve[15], Color.Green, Color.Blue);
            

            for (int i = 0; i < new_curve.Count - 1; i++)
            {
                //int lastIndex = i > 15 ? i - 15 : 0;
                //List<PointF> last10Points = new List<PointF>();
                //last10Points.AddRange(new_curve.GetRange(lastIndex, i - lastIndex + 2));
                ////gp.FillEllipse(Brushes.Blue, new_curve[i].X - 3, new_curve[i].Y - 3, 6, 6);
                //Brush brushEnd = new System.Drawing.Drawing2D.LinearGradientBrush(
                //    last10Points[0], last10Points[last10Points.Count - 1], Color.Blue, Color.Red);

                
                //List<PointF> allPoints =  new_curve.GetRange(0, i + 2);
                //pen.Brush = Brushes.Blue;
                //gp.DrawLines(pen, last10Points.ToArray());
                //pen.Brush = brushEnd;
                //if (last10Points.Count > 2)
                //    last10Points.RemoveAt(0);
                //gp.DrawLines(pen, last10Points.ToArray());

                gp.DrawLine(pen, new_curve[i], new_curve[i + 1]);    
                if (delay != 0)
                {
                    pictureBox.Invalidate();
                    System.Threading.Thread.Sleep(delay);
                }
            }
            gp.FillEllipse(Brushes.Green, new_curve[0].X - 3, new_curve[0].Y - 3, 6, 6);
            gp.FillEllipse(Brushes.Red, new_curve[new_curve.Count - 1].X - 3, new_curve[new_curve.Count - 1].Y - 3, 6, 6);
            gp.Dispose();
            pictureBox.Invalidate();
        }

        /// <summary>
        /// Draws curve in to the panel
        /// </summary>
        /// <param name="panel">Panel</param>
        /// <param name="curve">Curve</param>
        /// <param name="delay">Delay if animation is requested</param>
        public static void DrawCurve(Panel panel, List<PointF> curve, int delay)
        {
            Pen pen = new Pen(Brushes.Blue, 6);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Graphics gp = panel.CreateGraphics();
            gp.FillRectangle(Brushes.White, 0, 0, panel.Width, panel.Height);
            gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (curve == null || curve.Count == 0)
            {
                gp.Dispose();
                return;
            }

            List<PointF> new_curve = ScaleToCenter(curve, panel.ClientRectangle);
            if (delay != 0)
            {
                List<PointF> exactCurve = CreateExactPath(new_curve, 2, 2);
                new_curve = exactCurve;
                
            }
            
            for (int i = 0; i < new_curve.Count - 1; i++)
            {
                //gp.FillEllipse(Brushes.Blue, new_curve[i].X - 3, new_curve[i].Y - 3, 6, 6);
                gp.DrawLine(pen, new_curve[i], new_curve[i + 1]);
                if (delay != 0) System.Threading.Thread.Sleep(delay);
            }
            gp.FillEllipse(Brushes.Green, new_curve[0].X - 3, new_curve[0].Y - 3, 6, 6);
            gp.FillEllipse(Brushes.Red, new_curve[new_curve.Count - 1].X - 3, new_curve[new_curve.Count - 1].Y - 3, 6, 6);
            gp.Dispose();
        }
   
        /// <summary>
        /// Returns curve scaled in to centre of the specific area
        /// </summary>
        /// <param name="originalCurve"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        public static List<PointF> ScaleToCenter(List<PointF> originalCurve, Rectangle area)
        {
            List<PointF> scaledCurve = new List<PointF>(originalCurve);
            float x_min = 1000;
            float x_max = 0;
            float y_min = 1000;
            float y_max = 0;
            float x = 0;
            float y = 0;
            int DIST_FROM_BORDER = Math.Min(area.Width, area.Height) / 5;
            float ratio;

            float area_size = Math.Min(area.Height, area.Width);
            x = (area.Width - area_size + DIST_FROM_BORDER) / (float)2;
            y = (area.Height - area_size + DIST_FROM_BORDER) / (float)2;
            float dx = area.Width - 2 * x;
            float dy = area.Height - 2 * y;
            area_size = Math.Min(dx, dy);


            foreach (PointF point in originalCurve)
            {
                x_max = x_max < point.X ? point.X : x_max;
                x_min = x_min > point.X ? point.X : x_min;
                y_max = y_max < point.Y ? point.Y : y_max;
                y_min = y_min > point.Y ? point.Y : y_min;
            }

            float curve_size = Math.Max(x_max - x_min, y_max - y_min);
            curve_size = curve_size == 0 ? 1 : curve_size;
            ratio = area_size / curve_size;

            for (int i = 0; i < originalCurve.Count; i++)
                scaledCurve[i] = new PointF((originalCurve[i].X - x_min) * ratio, (originalCurve[i].Y - y_min) * ratio);

            x_max = (x_max - x_min) * ratio;
            y_max = (y_max - y_min) * ratio;

            float x_shift = x + (area_size - x_max) / (float)2;
            float y_shift = y + (area_size - y_max) / (float)2;

            for (int i = 0; i < scaledCurve.Count; i++)
                scaledCurve[i] = new PointF(scaledCurve[i].X + x_shift, scaledCurve[i].Y + y_shift);
            
            return scaledCurve;
        }

        #endregion Curve's graphic ouput

        /// <summary>
        /// Converts the path to points with minimum and maximum distance.
        /// First and last point is always the original one.       
        /// </summary>
        /// <param name="points"></param>
        /// <param name="min_dist"></param>
        /// <param name="max_dist"></param>
        /// <returns></returns>
        public static List<PointF> CreateExactPath(List<PointF> points, float min_dist, float max_dist)
        {
            List<PointF> exactPath = new List<PointF>();
            PointF a = new PointF(points[0].X, points[0].Y);
            PointF b;
            for (int i = 1; i < points.Count; i++)
            {
                b = new PointF(points[i].X, points[i].Y);
                PointF vector = new PointF(b.X - a.X, b.Y - a.Y);
                float distance = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                if (distance < min_dist)
                    continue;
                else if (distance > min_dist && distance < max_dist)
                {
                    exactPath.Add(a);
                    a = new PointF(b.X, b.Y);
                }
                else if (distance > max_dist)
                {
                    float step = 1 / (distance / max_dist);
                    float j = 0;
                    while (j + step < 1)
                    {
                        PointF pointOnPath = new PointF(a.X + j * vector.X, a.Y + j * vector.Y);
                        exactPath.Add(pointOnPath);
                        j += step;
                    }
                    a = new PointF(b.X, b.Y);
                }
            }
            PointF last = new PointF(points[points.Count - 1].X, points[points.Count - 1].Y);
            if (Distance(a, last) > (min_dist * 0.9))
                exactPath.Add(CreateMidPoint(exactPath[exactPath.Count - 1], last));
            exactPath.Add(last);

            return exactPath;
        }

   
        public static List<PointF> CreateBezierCurve(List<PointF> leadingPoints)
        {
            List<PointF> bezierCurve = new List<PointF>();
            double step = 1 / (leadingPoints.Count / 1.5);
            double bernPolynom;
            double x, y;
            for (double t = 0; t < 1; t += step)
            {
                t = Math.Round(t * 1000) / 1000;
                x = 0;
                y = 0;
                for (int i = 0; i < leadingPoints.Count; i++)
                {
                    bernPolynom = BernsteinPolynom(leadingPoints.Count - 1, i, t);
                    x += leadingPoints[i].X * bernPolynom;
                    y += leadingPoints[i].Y * bernPolynom;
                }
                bezierCurve.Add(new PointF((float)x, (float)y));
            }
            return bezierCurve;
        }

        private static double BernsteinPolynom(int n, int i, double t)
        {
            double binomCoeff = BinomialCoeff(n, i);
            double first_part = (t == 0 && i == 0) ? 1 : Math.Pow(t, i);
            double second_part = (1 - t == 0 && n - i == 0) ? 1 : Math.Pow(1 - t, n - i);
            return binomCoeff * first_part * second_part;
        }

        private static double BinomialCoeff(int n, int k)
        {
            if ((k < 0) || (k > n)) return 0;
            k = (k > n / 2) ? n - k : k;
            double a = 1;
            for (int i = 1; i <= k; i++)
                a = (a * (n - k + i)) / i;
            return Math.Floor(a + 0.5);
        }

        //public static List<PointF> ApproximateWithPoints(List<PointF> points, int count)
        //{
        //    List<PointF> approx = new List<PointF>();
        //    approx.AddRange(points);
        //    if (approx.Count > 1)
        //    {
        //        if (count < points.Count)
        //            DecreasePointsTo(approx, count);
        //        else
        //            IncreasePointsTo(approx, count);
        //    }
        //    return approx;
        //}

        

        public static List<PointF> ApproximateWithPoints(List<PointF> points, int count)
        {
            List<PointF> approx = new List<PointF>();
            if (points.Count > 2)
            {
                approx = IdentifyKeyPoints(points, 20, 20);
                if (count < approx.Count)
                    DecreasePointsTo(approx, count);
                else
                    IncreasePointsTo(approx, count);
            }
            return approx;
        }

        public static List<PointF> ApproximateAndRemoveOne(List<PointF> points, int count)
        {
            List<PointF> approx = new List<PointF>();
            if (points.Count > 2)
            {
                approx = IdentifyKeyPoints(points, 20, 20);
                if (count < approx.Count)
                    DecreasePointsTo(approx, count);
                else
                {
                    List<PointF> keyPoints = new List<PointF>(approx.ToArray());
                    IncreasePointsTo(approx, count + 1);
                    RemoveOneExtraPoint(approx, keyPoints);
                }
            }
            return approx;
        }

        private static void RemoveOneExtraPoint(List<PointF> allPoints, List<PointF> keyPoints)
        {
            List<PointF> pointsFromRemove = new List<PointF>();
            foreach (PointF point in allPoints)
            {
                int keyPos = -1;
                for (int i = 0; i < keyPoints.Count; i++)
                {
                    if (keyPoints[i].X == point.X && keyPoints[i].Y == point.Y)
                    {
                        keyPos = i;
                        break;
                    }
                }
                if (keyPos == -1)
                    pointsFromRemove.Add(point);
            }
            int pos = StaticRandom.RandomInteger(0, pointsFromRemove.Count);
            PointF pointToRemove = pointsFromRemove[pos];
            allPoints.Remove(pointToRemove);
            //pointsFromRemove.RemoveAt(pos);

            //pos = StaticRandom.RandomInteger(0, pointsFromRemove.Count);
            //pointToRemove = pointsFromRemove[pos];
            //allPoints.Remove(pointToRemove);
        }


        private static List<PointF> IdentifyKeyPoints(List<PointF> points, double angle, double distance)
        {
            List<PointF> keyPoints = new List<PointF>();
            keyPoints.Add(points[0]);
            PointF keyPoint = points[0];            
            for (int i = 1; i < points.Count - 1; i++)
            {
                PointF a = keyPoint;
                PointF b = points[i];
                PointF c = points[i + 1];
                if (Distance(a, b) >= distance && AngleBetweenSegments(a, b, c) >= angle)
                {
                    keyPoints.Add(b);
                    keyPoint = b;
                }
            }
            PointF lastPoint = points[points.Count - 1];
            if ((Distance(keyPoint, lastPoint) < distance) && keyPoints.Count > 1)
                keyPoints[keyPoints.Count - 1] = lastPoint;
            else
                keyPoints.Add(lastPoint);
            return keyPoints;
        }

        private static double AngleBetweenSegments(PointF a, PointF b, PointF c)
        {
            double angle = Math.Abs(LineAngle(a, b) - LineAngle(b, c));
            if (angle > 180) angle = 360 - angle;
            return angle;
        }

        private static double LineAngle(PointF a, PointF b)
        {
            double angle = Math.Atan2(b.Y - a.Y, b.X - a.X) * (180 / Math.PI);
            if (angle < 0) angle += 360;
            return angle;
        }

        /// <summary>
        /// Calculates angles for curve
        /// </summary>
        /// <param name="points">Curve</param>
        /// <returns></returns>
        private double[] CalculateAngles(List<PointF> points)
        {
            if (points.Count == 0) return null;
            double[] angles = new double[(points.Count - 1) * 2];
            double angle;
            for (int i = 0; i < points.Count - 1; i++)
            {
                angle = Math.Atan2(points[i + 1].Y - points[i].Y, points[i + 1].X - points[i].X);
                angles[2 * i] = Math.Cos(angle);
                angles[2 * i + 1] = Math.Sin(angle);
            }
            return angles;
        }

        private double[][] GenerateFalsePatterns(List<PointF> originalCurve, int count, int approxPoints)
        {
            List<PointF> falsePatterns;
            double[][] trainingSet = new double[count][];
            for (int i = 0; i < count; i++)
            {
                falsePatterns = GenerateFalseCurve(originalCurve);
                falsePatterns = ApproximateWithPoints(falsePatterns, approxPoints);
                trainingSet[i] = CalculateAngles(falsePatterns);
            }
            return trainingSet;

        }

        /// <summary>
        /// Generates training set for neural network
        /// </summary>
        /// <param name="points">Original curve</param>
        /// <param name="count">Count of sets</param>
        /// <returns></returns>
        private double[][] GenerateTrainingSet(List<PointF> originalCurve, int count, int approxPoints)
        {
            List<PointF> similarCurve;
            List<PointF> positiveRotatedCurve = RotateCurve(originalCurve, 12);
            List<PointF> negativeRotatedCurve = RotateCurve(originalCurve, -12);
            List<PointF> positiveHorizontalCurve = ResizeCurve(originalCurve, 0.25, 0);
            List<PointF> negativeHorizontalCurve = ResizeCurve(originalCurve, -0.25, 0);
            List<PointF> positiveVerticalCurve = ResizeCurve(originalCurve, 0, 0.25);
            List<PointF> negativeVerticalCurve = ResizeCurve(originalCurve, 0, -0.25);
            double[][] trainingSet = new double[count][];
            bool removeFirst = true;
            bool horizontalPositive = true;
            bool verticalPositive = true;

            for (int i = 0; i < count; i++)
            {
                if (i % 6 == 1)
                {
                    // generate from curve that was rotated in positive direction
                    similarCurve = GenerateCurve(positiveRotatedCurve, 7);
                }
                else if (i % 6 == 2)
                {
                    // generate from curve that was rotated in negative direction
                    similarCurve = GenerateCurve(negativeRotatedCurve, 7);
                }
                else if (i % 6 == 3)
                {
                    if (horizontalPositive)
                    {
                        // generate from curve that was positively resized in width
                        similarCurve = GenerateCurve(positiveHorizontalCurve, 5);
                    }
                    else
                    {
                        // generate from curve that was negatively resized in width
                        similarCurve = GenerateCurve(negativeHorizontalCurve, 5);
                    }
                    horizontalPositive = !horizontalPositive;
                }
                else if (i % 6 == 4)
                {
                    if (verticalPositive)
                    {
                        // generate from curve that was positively resized in height
                        similarCurve = GenerateCurve(positiveVerticalCurve, 5);
                    }
                    else
                    {
                        // generate from curve that was negatively resized in height
                        similarCurve = GenerateCurve(negativeVerticalCurve, 5);
                    }
                    verticalPositive = !verticalPositive;
                }        
                else
                {
                    // generate from original curve
                    similarCurve = GenerateCurve(originalCurve, 4);
                }

                // every second cycle 
                if (i % 2 == 0)
                {
                    similarCurve = ApproximateAndRemoveOne(similarCurve, approxPoints + 1);
                    if (removeFirst)
                    {
                        // remove the first point 
                        similarCurve.RemoveAt(0);
                    }
                    else
                    {
                        // remove the last point 
                        similarCurve.RemoveAt(similarCurve.Count - 1);
                    }
                    removeFirst = !removeFirst;
                }
                //else if (i % 3 == 1)
                //    similarCurve = ApproximateAndRemoveOne(similarCurve, approxPoints);
                else
                    similarCurve = ApproximateWithPoints(similarCurve, approxPoints);
                
                trainingSet[i] = CalculateAngles(similarCurve);
            }
            return trainingSet;
        }

        /// <summary>
        /// Generates false curves for input
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<PointF> GenerateFalseCurve(List<PointF> points)
        {
            List<PointF> autoCurve = new List<PointF>();
            float error = GetError(points) * 5.5F;
            float x;
            float y;
            for (int i = 0; i < points.Count; i++)
            {
                x = points[i].X + (2 * (float)StaticRandom.RandomDouble() - 1) * error;
                y = points[i].Y + (2 * (float)StaticRandom.RandomDouble() - 1) * error;
                autoCurve.Add(new PointF(x, y));
            }
            return autoCurve;
        }

        /// <summary>
        /// Rotate curve around the middle point for the angle 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static List<PointF> RotateCurve(List<PointF> points, double alpha)
        {
            float x_min = 1000;
            float x_max = 0;
            float y_min = 1000;
            float y_max = 0;
            // get bounds of points
            foreach (PointF point in points)
            {
                x_max = x_max < point.X ? point.X : x_max;
                x_min = x_min > point.X ? point.X : x_min;
                y_max = y_max < point.Y ? point.Y : y_max;
                y_min = y_min > point.Y ? point.Y : y_min;
            }
            // get the middle point 
            float x_middle = x_min + (x_max - x_min) / 2F;
            float y_middle = y_min + (y_max - y_min) / 2F;

            List<PointF> rotatedPoints = new List<PointF>();
            double radians = Math.PI * alpha / 180.0;

            for (int i = 0; i < points.Count; i++)
            {
                // original x
                float o_x = points[i].X;
                // original y
                float o_y = points[i].Y;

                // rotated x
                double r_x = Math.Cos(radians) * (o_x - x_middle) - Math.Sin(radians) * (o_y - y_middle) + x_middle;
                // rotated y 
                double r_y = Math.Sin(radians) * (o_x - x_middle) + Math.Cos(radians) * (o_y - y_middle) + y_middle;

                rotatedPoints.Add(new PointF((float)r_x, (float)r_y));
            }
            return rotatedPoints;
        }

        /// <summary>
        /// Resize curve in horizontal dimension in percentages
        /// </summary>
        /// <param name="points"></param>
        /// <param name="widthPercents"></param>
        /// <param name="heightPercents"></param>
        /// <returns></returns>
        public static List<PointF> ResizeCurve(List<PointF> points, double widthPercents, double heightPercents)
        {
            float x_min = 1000;
            float x_max = 0;
            float y_min = 1000;
            float y_max = 0;
            // get bounds of points
            foreach (PointF point in points)
            {
                x_max = x_max < point.X ? point.X : x_max;
                x_min = x_min > point.X ? point.X : x_min;
                y_max = y_max < point.Y ? point.Y : y_max;
                y_min = y_min > point.Y ? point.Y : y_min;
            }    
             // get the middle point 
            float x_middle = x_min + (x_max - x_min) / 2F;
            float y_middle = y_min + (y_max - y_min) / 2F;

            List<PointF> resizedCurve = new List<PointF>();
            for (int i = 0; i < points.Count; i++)
            {
                // original x
                float o_x = points[i].X;
                // original y
                float o_y = points[i].Y;

                // resized x
                double r_x = o_x + (o_x - x_middle) * widthPercents;
                // resized y 
                double r_y = o_y + (o_y - y_middle) * heightPercents;

                resizedCurve.Add(new PointF((float)r_x, (float)r_y));
            }
            return resizedCurve;
        }




        /// <summary>
        /// Generates similar curve to original one
        /// </summary>
        /// <param name="points">Original curve</param>
        /// <returns></returns>
        public static List<PointF> GenerateCurve(List<PointF> points, double errorReduction)
        {
            List<PointF> autoCurve = new List<PointF>();
            float error = GetError(points) / (float)errorReduction;
            float x;
            float y;
            for (int i = 0; i < points.Count; i++)
            {
                float extra = 1.5F;
                //if (i == 0 || i == points.Count - 1)
                //{
                //    continue;
                //}
                //if (i == 0)
                //{
                //    //x = points[i].X + (2 * (float)StaticRandom.RandomDouble() - 1) * (error * extra);
                //    //y = points[i].Y + (2 * (float)StaticRandom.RandomDouble() - 1) * (error * extra);
                //    //x = points[i + 1].X - ((points[i + 1].X - points[i].X) / 2) + (2 * (float)StaticRandom.RandomDouble() - 1) * (error * extra);
                //    //y = points[i + 1].Y - ((points[i + 1].Y - points[i].Y) / 2) + (2 * (float)StaticRandom.RandomDouble() - 1) * (error * extra);
                //    x = points[i].X - ((points[i + 1].X - points[i].X) / 4) + (2 * (float)StaticRandom.RandomDouble() - 1) * (error * extra);
                //    y = points[i].Y - ((points[i + 1].Y - points[i].Y) / 4) + (2 * (float)StaticRandom.RandomDouble() - 1) * (error * extra);
                //}
                //else if (i == 1 || i == points.Count - 2)
                //{
                //    //continue;
                //    extra = 0.6f;
                //    x = points[i].X + (2 * (float)StaticRandom.RandomDouble() - 1) * (error * extra);
                //    y = points[i].Y + (2 * (float)StaticRandom.RandomDouble() - 1) * (error * extra);
                //}
                //else if (i == points.Count - 1)
                //{
                //    //x = points[i].X + (2 * (float)StaticRandom.RandomDouble() - 1) * (error * extra);
                //    //y = points[i].Y + (2 * (float)StaticRandom.RandomDouble() - 1) * (error * extra); 
                //    //x = points[i - 1].X + ((points[i].X - points[i - 1].X) / 2) + (2 * (float)StaticRandom.RandomDouble() - 1) * (error * extra);
                //    //y = points[i - 1].Y + ((points[i].Y - points[i - 1].Y) / 2) + (2 * (float)StaticRandom.RandomDouble() - 1) * (error * extra);                 
                //    x = points[i].X + ((points[i].X - points[i - 1].X) / 4) + (2 * (float)StaticRandom.RandomDouble() - 1) * (error * extra);
                //    y = points[i].Y + ((points[i].Y - points[i - 1].Y) / 4) + (2 * (float)StaticRandom.RandomDouble() - 1) * (error * extra);
                //}
                //else
                {
                    x = points[i].X + (2 * (float)StaticRandom.RandomDouble() - 1) * error;
                    y = points[i].Y + (2 * (float)StaticRandom.RandomDouble() - 1) * error;
                }
                //float extra = 1;
                //if (i == 0 || i == points.Count - 1) extra = 1.6F;
                //else if (i == 1 || i == points.Count - 2) extra = 1.2F;
                //x = points[i].X + (2 * (float)StaticRandom.RandomDouble() - 1) * (error * extra);
                //y = points[i].Y + (2 * (float)StaticRandom.RandomDouble() - 1) * (error * extra);
                autoCurve.Add(new PointF(x, y));
            }
            return autoCurve;    
        }

        /// <summary>
        /// Calculate error for generating the curves
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private static float GetError(List<PointF> points)
        {
            float error_x = 0;
            float error_y = 0;
            for (int i = 1; i < points.Count; i++)
            {
                float diff_x = Math.Abs(points[i].X - points[i - 1].X);
                float diff_y = Math.Abs(points[i].Y - points[i - 1].Y);
                error_x = error_x < diff_x ? diff_x : error_x;
                error_y = error_y < diff_y ? diff_y : error_y;                
            }
            float error = error_x < error_y ? error_x : error_y;
            error = error_x > error_y ? error_x : error_y;
            //error = error / (float)4;
            return error;
        }


        /// <summary>
        /// Reduce the curve to specific amount of points
        /// </summary>
        /// <param name="points">List of points, that create curve</param>
        /// <param name="count">New amount of points</param>
        private static void DecreasePointsTo(List<PointF> points, int count)
        {
            List<float> pointsDistance = new List<float>();
            pointsDistance.AddRange(DistanceBetweenPoints(points));
            while (points.Count > count && points.Count > 2)
            {
                int minPos = MinPosition(pointsDistance);
                ReplaceWithMidPoint(minPos, points, pointsDistance);                 
            }
        }

        /// <summary>
        /// Expands the curve to specific amount of points
        /// </summary>
        /// <param name="points">List of points, that create curve</param>
        /// <param name="count">New amount of points</param>
        /// <returns></returns>
        private static void IncreasePointsTo(List<PointF> points, int count)
        {
            List<float> pointsDistance = new List<float>(DistanceBetweenPoints(points));
            while (points.Count < count)
            {
                int maxPos = MaxPosition(pointsDistance);
                AddMidPoint(maxPos, points, pointsDistance);
            }
        }

        /// <summary>
        /// Inserts point in the middle of two points
        /// </summary>
        /// <param name="position">Position where to insert new point</param>
        /// <param name="points">List of points, that create curve</param>
        /// <param name="pointsDistance">Distances list between points of the curve</param>
        private static void AddMidPoint(int position, List<PointF> points, List<float> pointsDistance)
        {
            PointF midPoint = CreateMidPoint(points[position], points[position + 1]);
            pointsDistance[position] = Distance(points[position], midPoint);
            pointsDistance.Insert(position + 1, Distance(midPoint, points[position + 1]));
            points.Insert(position + 1, midPoint);
            
        }

        /// <summary>
        /// Replaces two points with lowest distance with one point in the middle
        /// </summary>
        /// <param name="position">Position where to insert new point</param>
        /// <param name="points">List of points, that create curve</param>
        /// <param name="pointsDistance">Distances list between points of the curve</param>
        private static void ReplaceWithMidPoint(int position, List<PointF> points, List<float> pointsDistance)
        {
            //if position is in the begining
            if (position == 0)
            {
                pointsDistance[0] = Distance(points[0], points[2]);
                pointsDistance.RemoveAt(1);
                points.RemoveAt(1);                                
            }
            //if position is in the end
            else if (position == pointsDistance.Count - 1)
            {
                pointsDistance[position - 1] = Distance(points[position - 1], points[position + 1]);
                pointsDistance.RemoveAt(position);
                points.RemoveAt(position);                
            }
            //otherwise
            else
            {
                PointF midPoint = CreateMidPoint(points[position], points[position + 1]);
                pointsDistance[position - 1] = Distance(points[position - 1], midPoint);
                pointsDistance[position + 1] = Distance(midPoint, points[position + 2]);
                pointsDistance.RemoveAt(position);
                points[position] = midPoint;
                points.RemoveAt(position + 1);
            }
        }

        /// <summary>
        /// Creates point in the middle of twho points
        /// </summary>
        /// <param name="a">First point</param>
        /// <param name="b">Second point</param>
        /// <returns></returns>
        private static PointF CreateMidPoint(PointF a, PointF b)
        {
            float x = a.X + (b.X - a.X) / (float)2;
            float y = a.Y + (b.Y - a.Y) / (float)2;
            return new PointF(x, y);
        }
        
        /// <summary>
        /// Finds position of minimal distance between points
        /// </summary>
        /// <param name="pointsDistances">List of points</param>
        /// <returns></returns>
        private static int MinPosition(List<float> pointsDistances)
        {
            int min = 0;
            for (int i = 1; i < pointsDistances.Count; i++)
            {
                min = pointsDistances[min] > pointsDistances[i] ? i : min;
            }
            return min;            
        }

        /// <summary>
        /// Finds position of maximal distance between points
        /// </summary>
        /// <param name="pointsDistances">List of points</param>
        /// <returns></returns>
        private static int MaxPosition(List<float> pointsDistances)
        {
            int max = 0;
            for (int i = 1; i < pointsDistances.Count; i++)
            {
                max = pointsDistances[max] < pointsDistances[i] ? i : max;
            }
            return max;
        }

        private static void MinMaxPosition(List<float> pointsDistances, ref int minPos, ref int maxPos)
        {
            maxPos = 0;
            minPos = 0;
            for (int i = 1; i < pointsDistances.Count; i++)
            {
                maxPos = pointsDistances[maxPos] < pointsDistances[i] ? i : maxPos;
                minPos = pointsDistances[minPos] > pointsDistances[i] ? i : minPos;
            }
        }


        /// <summary>
        /// Creates array of distances between points in the list
        /// </summary>
        /// <param name="points">List of points</param>
        /// <returns></returns>
        private static float[] DistanceBetweenPoints(List<PointF> points)
        {
            float[] distances = new float[points.Count - 1];
            for (int i = 0; i < points.Count - 1; i++)
                distances[i] = Distance(points[i], points[i + 1]);
            return distances;
        }

        /// <summary>
        /// Distance of two points
        /// </summary>
        /// <param name="a">One point</param>
        /// <param name="b">Second point</param>
        /// <returns></returns>
        public static float Distance(PointF a, PointF b)
        {
            float x = a.X - b.X;
            float y = a.Y - b.Y;
            return (float)Math.Sqrt(x * x + y * y);
        }



        public MyCurve(SerializationInfo info, StreamingContext context)
        {
            //try { m_position = info.GetInt32("NNIndex"); }
            //catch { m_position = -1; }
            try { m_name = info.GetString("Name"); }
            catch { m_name = string.Empty; }
            //try { m_caption = info.GetString("Caption"); }
            //catch { m_caption = string.Empty; }
            try { m_points = (List<PointF>)info.GetValue("OriginalCurve", typeof(List<PointF>)); }
            catch { m_points = null; }
            //try { m_bezierCurve = (List<PointF>)info.GetValue("BezierCurve", typeof(List<PointF>)); }
            //catch { m_bezierCurve = null; }
            try { m_trainingSet = (double[][])info.GetValue("TrainingSet", typeof(double[][])); }
            catch { m_trainingSet = null; }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //info.AddValue("NNIndex", m_position);
            info.AddValue("Name", m_name);
            //info.AddValue("Caption", m_caption);
            info.AddValue("OriginalCurve", m_points, typeof(List<PointF>));
            //info.AddValue("BezierCurve", m_bezierCurve, typeof(List<PointF>));
            info.AddValue("TrainingSet", m_trainingSet, typeof(double[][]));
        }

    
    }

}
