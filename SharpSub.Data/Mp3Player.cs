using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using SharpSub.Data;

namespace SharpSub.Data
{
    [Obsolete("This class has been replaced by SongPlayer", true)]
    internal class Mp3Player : IDisposable
    {
        private WaveOut _waveOut;
        private WaveStream _waveStream;
        private Stream _memoryStream;
        internal PlaybackState PlaybackState { get { return _waveOut.PlaybackState; } }
        internal readonly Song CurrentSong;

        internal int Duration
        {
            //not sure if this is right or not yet. Still working on it
            get { return (int)_waveStream.Length; }
        }

        internal long Position
        {
            //not sure if this is right or not yet. Still working on it
            get { return _waveStream.Position; }
            set { _waveStream.Position = value; }
        }

        internal Mp3Player(Song song)
        {
            CurrentSong = song;
            _memoryStream = new MemoryStream();

            using (Stream stream = SubsonicRequest.GetSongStream(CurrentSong/*, format:"mp3"*/))
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
        internal void Play()
        {
            if (this.PlaybackState == PlaybackState.Stopped)
                _waveOut.Play();

        }

        /// <summary>
        /// Stops the playback and disposes of the instance
        /// </summary>
        internal void StopAndDispose()
        {
            if (PlaybackState != PlaybackState.Stopped)
                _waveOut.Stop();

            Dispose();
        }

        /// <summary>
        /// Pauses the song. To unpause use Resume()
        /// </summary>
        internal void Pause()
        {
            if (PlaybackState == PlaybackState.Playing)
                _waveOut.Pause();
            throw new ConstraintException("The player is currently not playing. Pausing is not possible.");
        }

        /// <summary>
        /// Resumes the song after pausing has been initiated
        /// </summary>
        internal void Resume()
        {
            if (PlaybackState == PlaybackState.Paused)
                _waveOut.Resume();
            throw new ConstraintException("The player is currently not paused. Resuming is not possible.");
        }

        /// <summary>
        /// Sets the volume. The Max is 1.0, the Min is 0.0
        /// </summary>
        internal float Volume
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
