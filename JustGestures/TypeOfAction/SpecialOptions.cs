using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using JustGestures.GestureParts;

namespace JustGestures.TypeOfAction
{
    [Serializable]
    class SpecialOptions : BaseActionClass
    {
        public const string NAME = "special_name";

        public const string SPECIAL_ZOOM = "special_zoom_area";

        //public static double[] ZoomLvl = new double[] { 1, 1.5, 2, 3, 4 };
                
        public SpecialOptions()
        {
            m_actions = new List<string>
                (new string[] { 
                    SPECIAL_ZOOM
                });
        }

        public SpecialOptions(string action) : base(action) { }

        public SpecialOptions(SpecialOptions action) : base(action) { }

        public SpecialOptions(SerializationInfo info, StreamingContext context)
            : base (info, context)
        {
        }

        public override object Clone()
        {
            return new SpecialOptions(this);
        }

        public override void ExecuteAction(IntPtr activeWnd, Point location)
        {
            //switch (this.Name)
            //{
            //    case SPECIAL_ZOOM:
            //        Temp.Form_zoom formZoom = new Temp.Form_zoom();
            //        string[] values = this.Details.Split(new char[] { ':' });
            //        if (values.Length != 4) return;
            //        Rectangle area = new Rectangle();
            //        area.X = int.Parse(values[0]);
            //        area.Y = int.Parse(values[1]);
            //        area.Width = int.Parse(values[2]);
            //        area.Height = int.Parse(values[3]);
            //        formZoom.ZoomToArea(area);
            //        break;               
            //}
        }

        public override Bitmap GetIcon(int size)
        {
            return base.GetIcon(size);
        }

      
    }
}
