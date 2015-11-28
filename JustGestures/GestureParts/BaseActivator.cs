using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization;

namespace JustGestures.GestureParts
{
    [Serializable]
    public class BaseActivator : ISerializable
    {        
        public enum Types
        {
            Undefined,
            ClassicCurve,
            DoubleButton,
            WheelButton,
            WheelOnly
        }

        protected string m_id = string.Empty;
        protected Types m_type = Types.ClassicCurve;

        public string ID { get { return m_id; } }
        public Types Type { get { return m_type; } }
        

        public BaseActivator() { }

        public BaseActivator(Object _activator)
        {
            BaseActivator activator = (BaseActivator)_activator;
            m_id = activator.ID;
            m_type = activator.Type;
        }

        public virtual Bitmap ExtractIcon(Size size) { return null; }

        public virtual void DrawToPictureBox(PictureBox pictureBox) {}

        public virtual void AnimateToPictureBox(PictureBox pictureBox) { }

        public BaseActivator(SerializationInfo info, StreamingContext context)
        {
            try { m_id = info.GetString("ID"); }
            catch { m_id = string.Empty; }
            try { m_type = (Types)info.GetValue("Type", typeof(Types)); }
            catch { m_type = Types.Undefined; }
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID", m_id);
            info.AddValue("Type", m_type, typeof(Types));
        }
      
        
    }
}
