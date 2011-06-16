using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace SharpSub.Data
{
    class MP3 : IDisposable
    {
        IWavePlayer waveOutDevice;
        WaveStream mainOutputStream;
        WaveChannel32 volumeStream;
        private WaveChannel32 inputStream;

        public MP3(Song song)
        {
            
        }

        public void Dispose()
        {
            //TODO: Dispose of the player and other objects that encode/decode the stream, etc.
        }
    }

}
