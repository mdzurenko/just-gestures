using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Drawing;
using JustGestures.Properties;
using JustGestures.GestureParts;
using JustGestures.Features;

namespace JustGestures.TypeOfAction
{
    [Serializable]
    class VolumeOptions : BaseActionClass
    {
        public const string NAME = "volume_name";

        public const string VOLUME_UP = "volume_up";
        public const string VOLUME_DOWN = "volume_down";
        public const string VOLUME_MUTE = "volume_mute";
        public const string VOLUME_SET = "volume_set";

        public enum Volume
        {
            Up,
            Down,
            Mute,
            Set
        }

        public static double[] VolumeLvl = new double[] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };

        public VolumeOptions()
        {
            m_actions = new List<string>
                (new string[] { 
                    VOLUME_UP,
                    VOLUME_DOWN,
                    VOLUME_MUTE,
                    VOLUME_SET              
                });
        }

        public VolumeOptions(string action) : base(action) { }

        public VolumeOptions(VolumeOptions action) : base(action) { }

        public VolumeOptions(SerializationInfo info, StreamingContext context)
            : base (info, context)
        {
        }

        public override object Clone()
        {
            return new VolumeOptions(this);
        }

        public override bool IsSensitiveToMySystemWindows()
        {
            return false;
        }

        public override void ExecuteAction(IntPtr activeWnd, Point location)
        {
            switch (this.Name)
            {
                case VOLUME_UP:
                    TurnVolume(Volume.Up, 0);
                    break;
                case VOLUME_DOWN:
                    TurnVolume(Volume.Down, 0);
                    break;
                case VOLUME_MUTE:
                    TurnVolume(Volume.Mute, 0);
                    break;
                case VOLUME_SET:
                    TurnVolume(Volume.Set, float.Parse(this.Details) / 100);                   
                    break;
            }
        }

        public override Bitmap GetIcon(int size)
        {
            switch (this.Name)
            {
                case NAME:
                    return Resources.volume_control2;
                    //break;
                case VOLUME_UP:
                    return Resources.volume_up;
                    //break;
                case VOLUME_DOWN:
                    return Resources.volume_down;
                    //break;
                case VOLUME_MUTE:
                    return Resources.volume_mute;
                    //break;
                case VOLUME_SET:
                    return Resources.volume_control;
                    //break;
                default:
                    return Resources.volume_control2;
                    //break;
            }

        }

        public static void TurnVolume(Volume vol, float value)
        {
            OperatingSystem os = Environment.OSVersion;
            //Windows Vista & 7
            bool vistaVolume = (os.Platform == PlatformID.Win32NT && os.Version.Major == 6);
            VistaVolumeControl vistaControl = new VistaVolumeControl();
            XpVolumeControl xpControl = new XpVolumeControl();
            IVolumeControl volumeControl;
            if (vistaVolume) volumeControl = vistaControl;
            else volumeControl = xpControl;
            switch (vol)
            {
                case Volume.Down:
                    if (volumeControl.Mute) volumeControl.Mute = false;
                    else volumeControl.VolumeDown();
                    break;
                case Volume.Up:
                    if (volumeControl.Mute) volumeControl.Mute = false;
                    else volumeControl.VolumeUp();
                    break;
                case Volume.Mute:
                    volumeControl.Mute = !volumeControl.Mute;
                    break;
                case Volume.Set:
                    if (volumeControl.Mute) volumeControl.Mute = false;
                    volumeControl.MasterVolume = value;
                    break;
            }            
        }
    }
}
