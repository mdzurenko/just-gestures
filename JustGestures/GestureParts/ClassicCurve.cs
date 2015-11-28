using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;

namespace JustGestures.GestureParts
{
    [Serializable]
    public class ClassicCurve : BaseActivator, ISerializable
    {
        const int DELAY = 10;
        int m_nnIndex = -1;
        string m_caption = string.Empty;
        List<PointF> m_bezierCurve = new List<PointF>();
        List<PointF> m_points = new List<PointF>();



        public int NnIndex { get { return m_nnIndex; } set { m_nnIndex = value; } }
        public List<PointF> BezierCurve { get { return m_bezierCurve; } }
        public List<PointF> Points { get { return m_points; } }
        public string Caption { get { return m_caption; } set { m_caption = value; } }

        public ClassicCurve(ClassicCurve curve)
        {
            m_id = curve.ID;
            m_type = curve.Type;
            m_nnIndex = curve.NnIndex;
            m_caption = curve.Caption;
            m_bezierCurve = new List<PointF>(curve.BezierCurve);
            m_points = new List<PointF>(curve.Points);
        }

        public ClassicCurve(List<PointF> points, GesturesCollection gestures)
        {
            string name = "curve_name";
            int postfix = 1;
            while (gestures.GetCurve(name + postfix.ToString()) != null) postfix++;
            m_id = name + postfix.ToString();
            m_points = MyCurve.CreateExactPath(points, 20, 20);
            m_bezierCurve = MyCurve.CreateBezierCurve(points);
            m_type = Types.ClassicCurve;
        }


        public static implicit operator MouseActivator(ClassicCurve curve)
        {
            if (curve == null)
                return null;
            else
                return new MouseActivator((object)curve);
        }

        public override Bitmap ExtractIcon(Size size)
        {
            return MyCurve.DrawCurveToBmp(m_bezierCurve, size);
        }

        public override void DrawToPictureBox(System.Windows.Forms.PictureBox pictureBox)
        {
            MyCurve.DrawCurve(pictureBox, m_bezierCurve, 0);
        }

        public override void AnimateToPictureBox(System.Windows.Forms.PictureBox pictureBox)
        {
            MyCurve.DrawCurve(pictureBox, m_bezierCurve, DELAY);
        }

        public ClassicCurve(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            try { m_nnIndex = info.GetInt32("NnIndex"); }
            catch { m_nnIndex = -1; }
            try { m_caption = info.GetString("Caption"); }
            catch { m_caption = string.Empty; }
            try { m_points = (List<PointF>)info.GetValue("Points", typeof(List<PointF>)); }
            catch { m_points = null; }
            try { m_bezierCurve = (List<PointF>)info.GetValue("BezierCurve", typeof(List<PointF>)); }
            catch { m_bezierCurve = null; }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("NnIndex", m_nnIndex);
            info.AddValue("Caption", m_caption);
            info.AddValue("Points", m_points, typeof(List<PointF>));
            info.AddValue("BezierCurve", m_bezierCurve, typeof(List<PointF>));
        }

    }


}
