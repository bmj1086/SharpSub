using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace SharpSub.Data
{
    class MP3 : IDisposable
    {
        private WaveOut _waveOut;
        private WaveStream _waveStream;
        private Stream _memoryStream;
        public PlaybackState PlaybackState { get { return _waveOut.PlaybackState; } }
        public string playingName;

        public MP3(Song song)
        {
            _memoryStream = new MemoryStream();

            using (Stream stream = SubsonicRequest.GetSongStream(song))
            {
                byte[] buffer = new byte[32768];
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    _memoryStream.Write(buffer, 0, read);
                }
            }
            _memoryStream.Position = 0;

            _waveStream = new Mp3FileReader(_memoryStream);
            _waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback());
            _waveOut.Init(_waveStream);
            _waveOut.Play();
            

        }
        
        public void Play()
        {

        }

        public void Stop()
        {
            _waveOut.Stop();
            Dispose();
        }

        public void Pause()
        {
            _waveOut.Pause();
        }

        public void Resume()
        {
            _waveOut.Resume();
        }

        public float Volume
        {
            get { return _waveOut.Volume; }
            set { _waveOut.Volume = value; }
        }

        

        public void Dispose()
        {
            _waveOut.Dispose();
            
            //TODO: Dispose of the player and other objects that encode/decode the stream, etc.
        }
    }

}
