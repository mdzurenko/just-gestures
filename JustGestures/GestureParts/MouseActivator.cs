using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Threading;
using JustGestures.Properties;

namespace JustGestures.GestureParts
{
    [Serializable]
    public class MouseActivator : BaseActivator, ISerializable
    {   
        Object m_objActivator;
        Thread threadDrawGesture;
        PictureBox m_pbAnimating;

        public Thread ThreadAnimating { get { return threadDrawGesture; } }       
        

        public MouseActivator(string id, Types type)
        {
            m_id = id;
            m_type = type;
        }

        public MouseActivator(object activator)
            : base(activator)
        {
            m_objActivator = activator;
        }

        public MouseActivator(string id, BaseActivator.Types type, object activator)
        {
            m_id = id;
            m_type = type;
            m_objActivator = activator;
        }

        public MouseActivator(MouseActivator _activator)
        {
            m_id = _activator.ID;
            m_type = _activator.Type;
            m_objActivator = _activator.m_objActivator;
        }

        public static implicit operator DoubleButton(MouseActivator doubleButton)
        {
            if (doubleButton == null)
                return null;
            else
                return (DoubleButton)doubleButton.m_objActivator;
        }

        public static implicit operator ClassicCurve(MouseActivator classicCurve)
        {
            if (classicCurve == null)
                return null;
            else
                return (ClassicCurve)classicCurve.m_objActivator;
        }

        public static implicit operator WheelButton(MouseActivator wheelButton)
        {
            if (wheelButton == null)
                return null;
            else
                return (WheelButton)wheelButton.m_objActivator;
        }

        public override Bitmap ExtractIcon(Size size)
        {
            if (m_objActivator == null) 
                return new Bitmap(size.Width, size.Height);
            else
                return ((BaseActivator)m_objActivator).ExtractIcon(size);
        }

        public override void DrawToPictureBox(PictureBox pictureBox)
        {
            AbortAnimating();
            if (m_objActivator == null)
                pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            else
                ((BaseActivator)m_objActivator).DrawToPictureBox(pictureBox);
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if thread was aborted</returns>
        public bool AbortAnimating()
        {
            if (threadDrawGesture != null && threadDrawGesture.IsAlive)
            {
                threadDrawGesture.Abort();
                return true;
            }
            return false;
        }

        public override void AnimateToPictureBox(PictureBox pictureBox)
        {
            bool animate = true;
            if (threadDrawGesture != null && threadDrawGesture.IsAlive)
            {
                animate = false;
                threadDrawGesture.Abort();
            }
            if (animate)
            {
                m_pbAnimating = pictureBox;
                threadDrawGesture = new Thread(new ThreadStart(Animate));
                threadDrawGesture.Start();
            }
            else
                DrawToPictureBox(pictureBox);
        }

        private void Animate()
        {
            if (m_objActivator == null)
                return;
            else
                ((BaseActivator)m_objActivator).AnimateToPictureBox(m_pbAnimating);
        }

        public System.Drawing.Bitmap GetIcon(Size size)
        {
            return this.ExtractIcon(size);
        }


        public MouseActivator(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            try { m_objActivator = (Object)info.GetValue("Value", typeof(Object)); }
            catch { m_objActivator = null; }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Value", m_objActivator);
        }
    }
}
