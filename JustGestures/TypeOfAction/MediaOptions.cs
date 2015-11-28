using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;
using JustGestures.Properties;
using JustGestures.GestureParts;
using JustGestures.Features;

namespace JustGestures.TypeOfAction
{
    [Serializable]
    class MediaControl : BaseActionClass
    {
        public const string NAME = "media_name";
        const string MEDIA_PLAY = "media_play";
        const string MEDIA_PAUSE = "media_pause";
        const string MEDIA_STOP = "media_stop";
        const string MEDIA_NEXT = "media_next";
        const string MEDIA_PREV = "media_prev";

        public MediaControl()
        {
            m_actions = new List<string>
                (new string[] { 
                    MEDIA_PLAY,
                    MEDIA_PAUSE,
                    MEDIA_NEXT,
                    MEDIA_PREV,
                    MEDIA_STOP                   
                });
        }

        public MediaControl(string action) : base(action) { }

        public MediaControl(MediaControl action) : base(action) { }

        public MediaControl(SerializationInfo info, StreamingContext context)
            : base (info, context)
        {
        }

        public override object Clone()
        {
            return new MediaControl(this);
        }

        public override bool IsSensitiveToMySystemWindows()
        {
            return false;
        }

        public override void ExecuteAction(IntPtr activeWnd, Point location)
        {
            switch (this.Name)
            {
                case MEDIA_PLAY:
                    KeyInput.ExecuteKeyInput(AllKeys.KEY_MEDIA_PLAY_PAUSE);                    
                    break;
                case MEDIA_PAUSE:
                    KeyInput.ExecuteKeyInput(AllKeys.KEY_MEDIA_PLAY_PAUSE);
                    break;
                case MEDIA_STOP:
                    KeyInput.ExecuteKeyInput(AllKeys.KEY_MEDIA_STOP);  
                    break;
                case MEDIA_NEXT:
                    KeyInput.ExecuteKeyInput(AllKeys.KEY_MEDIA_NEXT_TRACK);
                    break;
                case MEDIA_PREV:
                    KeyInput.ExecuteKeyInput(AllKeys.KEY_MEDIA_PREV_TRACK);
                    break;              
            }
        }

        public override Bitmap GetIcon(int size)
        {
            switch (this.Name)
            {
                case MEDIA_PLAY:
                    return Resources.media_play;
                //break;
                case MEDIA_STOP:
                    return Resources.media_stop;
                //break;
                case MEDIA_PAUSE:
                    return Resources.media_pause;
                //break;
                case MEDIA_NEXT:
                    return Resources.media_skip_forward;
                //break;
                case MEDIA_PREV:
                    return Resources.media_skip_back;
                //break;
                default:
                    return Resources.media;
                //break;
            }

        }

    }
}
