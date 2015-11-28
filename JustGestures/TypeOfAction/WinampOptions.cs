using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Drawing;
using JustGestures.Properties;
using JustGestures.GestureParts;

namespace JustGestures.TypeOfAction
{
    [Serializable]
    class WinampOptions : BaseActionClass
    {
        public const string NAME = "winamp_name";

        const string WINAMP_PLAY = "winamp_play";
        const string WINAMP_PAUSE = "winamp_pause";
        const string WINAMP_STOP = "winamp_stop";
        const string WINAMP_NEXT = "winamp_next";
        const string WINAMP_PREV = "winamp_prev";
        const string WINAMP_CLOSE = "winamp_close";

        public WinampOptions()
        {
            m_actions = new List<string>
                (new string[] { 
                    WINAMP_PLAY,
                    WINAMP_PAUSE,
                    WINAMP_NEXT,
                    WINAMP_PREV,
                    WINAMP_STOP,
                    WINAMP_CLOSE                    
                });
        }

        public WinampOptions(string action) : base(action) { }

        public WinampOptions(WinampOptions action) : base(action) { }

        public WinampOptions(SerializationInfo info, StreamingContext context)
            : base (info, context)
        {
        }

        public override object Clone()
        {
            return new WinampOptions(this);
        }

        public override bool IsSensitiveToMySystemWindows()
        {
            return false;
        }

        public override void ExecuteAction(IntPtr activeWnd, Point location)
        {
            IntPtr winampHwnd = Win32.FindWindow("Winamp v1.x", null);
            if (winampHwnd == IntPtr.Zero) return;
            switch (this.Name)
            {
                case WINAMP_PLAY:
                    Win32.SendMessage(winampHwnd, Win32.WM_COMMAND, Win32.WA_PLAY, 0);
                    break;
                case WINAMP_PAUSE:
                    Win32.SendMessage(winampHwnd, Win32.WM_COMMAND, Win32.WA_PAUSE, 0);
                    break;
                case WINAMP_STOP:
                    Win32.SendMessage(winampHwnd, Win32.WM_COMMAND, Win32.WA_STOP, 0);
                    break;
                case WINAMP_NEXT:
                    Win32.SendMessage(winampHwnd, Win32.WM_COMMAND, Win32.WA_NEXT, 0);
                    break;
                case WINAMP_PREV:
                    Win32.SendMessage(winampHwnd, Win32.WM_COMMAND, Win32.WA_PREVIOUS, 0);                    
                    break;
                case WINAMP_CLOSE:
                    Win32.SendMessage(winampHwnd, Win32.WM_COMMAND, Win32.WA_CLOSE, 0);
                    break;
            }
        }

        public override Bitmap GetIcon(int size)
        {
            switch (this.Name)
            {
                case WINAMP_PLAY:
                    return Resources.media_play;
                    //break;
                case WINAMP_STOP:
                    return Resources.media_stop;
                    //break;
                case WINAMP_PAUSE:
                    return Resources.media_pause;
                    //break;
                case WINAMP_NEXT:
                    return Resources.media_skip_forward;
                    //break;
                case WINAMP_PREV:
                    return Resources.media_skip_back;
                    //break;
                default:
                    return Resources.media;
                    //break;
            }
            
        }

    }
}
