using System;
using System.Collections.Generic;
using System.Text;

namespace JustGestures.Features
{
    public interface IVolumeControl
    {
        float MasterVolume { get; set; }

        bool Mute { get; set; }

        void VolumeUp();

        void VolumeDown();
    }
}
