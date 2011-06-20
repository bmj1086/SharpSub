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
    class Mp3Player : IDisposable
    {
        private WaveOut _waveOut;
        private WaveStream _waveStream;
        private Stream _memoryStream;
        public PlaybackState PlaybackState { get { return _waveOut.PlaybackState; } }
        public string playingName;
        public readonly Song CurrentSong;

        public Mp3Player(Song song)
        {
            CurrentSong = song;
            _memoryStream = new MemoryStream();

            using (Stream stream = SubsonicRequest.GetSongStream(CurrentSong))
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
        }
        
        /// <summary>
        /// Only use this method to start playing the song. For unpausing use Resume()
        /// </summary>
        public void Play()
        {
            if (this.PlaybackState == PlaybackState.Stopped)
                _waveOut.Play();
        }

        /// <summary>
        /// Stops the playback and disposes of the instance
        /// </summary>
        public void Stop()
        {
            _waveOut.Stop();
            Dispose();
        }

        /// <summary>
        /// Pauses the song. To unpause use Resume()
        /// </summary>
        public void Pause()
        {
            _waveOut.Pause();
        }

        /// <summary>
        /// Resumes the song after pausing has been initiated
        /// </summary>
        public void Resume()
        {
            _waveOut.Resume();
        }

        /// <summary>
        /// Sets the volume. The Max is 1.0, the Min is 0.0
        /// </summary>
        public float Volume
        {
            get { return _waveOut.Volume; }
            set { _waveOut.Volume = value; }
        }
        
        public void Dispose()
        {
            _waveOut.Dispose();
            _waveStream.Dispose();
            _memoryStream.Dispose();
        }
    }

}
