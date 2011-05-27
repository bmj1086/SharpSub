using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using NAudio.Wave;
using SubsonicAPI;

namespace SharpSub.Data
{
    public class MusicPlayer
    {
        private Song currentSong;
        private Queue<Song> playlist;

        private BackgroundWorker playerThread;

        public PlaybackState playState;

        public MusicPlayer()
        {

        }

        /// <summary>
        /// Public method called by the main view to start playing the playlist
        /// </summary>
        public void Play()
        {
            // If there is no backgroundworker initialized, do that
            if (playerThread == null)
            {
                playerThread = new BackgroundWorker();
                playerThread.DoWork += new DoWorkEventHandler(playerThread_DoWork);
                playerThread.ProgressChanged += new ProgressChangedEventHandler(playerThread_ProgressChanged);
                playerThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(playerThread_RunWorkerCompleted);
                playerThread.WorkerReportsProgress = true;

            }

            // Set state to playing
            playState = PlaybackState.Playing;

            // start playing the entire queue
            playQueue();
        }

        private void playQueue()
        {
            if (playlist.Count > 0 && this.playState == PlaybackState.Playing)
            {
                currentSong = playlist.Peek();

                if (waveOut == null || waveOut.PlaybackState != PlaybackState.Playing)
                    NewPlaySong();
                // If the player is not busy yet then start it
                if (!playerThread.IsBusy)
                    playerThread.RunWorkerAsync();
            }
            else
            {
                this.playState = PlaybackState.Stopped;
            }
        }

        IWavePlayer waveOut;
        WaveStream mainOutputStream;
        WaveChannel32 volumeStream;

        private void NewPlaySong()
        {
            if (waveOut != null)
            {
                if (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    return;
                }
                else if (waveOut.PlaybackState == PlaybackState.Paused)
                {
                    waveOut.Play();
                    return;
                }
            }
            else
            {
                CreateWaveOut();

                mainOutputStream = CreateInputStream();
                //trackBarPosition.Maximum = (int)mainOutputStream.TotalTime.TotalSeconds;
                //labelTotalTime.Text = String.Format("{0:00}:{1:00}", (int)mainOutputStream.TotalTime.TotalMinutes,
                //    mainOutputStream.TotalTime.Seconds);
                //trackBarPosition.TickFrequency = trackBarPosition.Maximum / 30;

                waveOut.Init(mainOutputStream);

                volumeStream.Volume = 15; //volumeSlider1.Volume;
                waveOut.Play();
            }
        }

        private WaveStream CreateInputStream()
        {
            WaveChannel32 inputStream;

            Stream stream = Subsonic.StreamSong(currentSong.id);

            // Try to move this filling of memory stream into the background...
            Stream ms = new MemoryStream();
            byte[] buffer = new byte[32768];
            int read;
            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                playerThread.ReportProgress(50);

                ms.Write(buffer, 0, read);
            }

            ms.Position = 0;

            WaveStream mp3Reader = new Mp3FileReader(ms);
            WaveStream pcmStream = WaveFormatConversionStream.CreatePcmStream(mp3Reader);
            WaveStream blockAlignedStream = new BlockAlignReductionStream(pcmStream);
            inputStream = new WaveChannel32(blockAlignedStream);

            // we are not going into a mixer so we don't need to zero pad
            //((WaveChannel32)inputStream).PadWithZeroes = false;
            volumeStream = inputStream;

            //var meteringStream = new MeteringStream(inputStream, inputStream.WaveFormat.SampleRate / 10);
            //meteringStream.StreamVolume += new EventHandler<StreamVolumeEventArgs>(meteringStream_StreamVolume);

            return volumeStream;
        }

        private void CreateWaveOut()
        {
            CloseWaveOut();

            if (true)
            {
                WaveCallbackInfo callbackInfo = WaveCallbackInfo.FunctionCallback();
                WaveOut outputDevice = new WaveOut(callbackInfo);
                //outputDevice.NumberOfBuffers = 1;
                //outputDevice.DesiredLatency = latency;
                waveOut = outputDevice;
            }
        }

        private void CloseWaveOut()
        {
            if (waveOut != null)
            {
                waveOut.Stop();
            }
            if (mainOutputStream != null)
            {
                // this one really closes the file and ACM conversion
                volumeStream.Close();
                volumeStream = null;
                // this one does the metering stream
                mainOutputStream.Close();
                mainOutputStream = null;
            }
            if (waveOut != null)
            {
                waveOut.Dispose();
                waveOut = null;
            }
        }

        /// <summary>
        /// Defines what should be done when the playerThread starts working
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playerThread_DoWork(object sender, DoWorkEventArgs e)
        {
            //playSong();
            TrackPlayer();
        }

        /// <summary>
        /// Updates the main thread on the progress of the player thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playerThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //TODO: implement this //mainForm.updateSongProgress(e.ProgressPercentage);
        }

        /// <summary>
        /// Defines what should be done when the player thread finishes working
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playerThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SongFinished();
        }

        private void SongFinished()
        {
            // Remove the top song from the playlist
            currentSong = playlist.Dequeue();
            
            // Start playing the next song
            if (playState == PlaybackState.Playing)
                playQueue();
        }

        private void TrackPlayer()
        {
            while (waveOut != null && waveOut.PlaybackState != PlaybackState.Stopped)
            {
                int progress = (int)((double)mainOutputStream.CurrentTime.TotalSeconds * 100.0 / (double)mainOutputStream.TotalTime.TotalSeconds);
                playerThread.ReportProgress(progress);
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Public method to add a song to the playlist
        /// </summary>
        /// <param name="theSong"></param>
        public void addToPlaylist(Song theSong)
        {
            if (playlist == null)
                playlist = new Queue<Song>();
            playlist.Enqueue(theSong);

        }

        bool skipThis;

        /// <summary>
        /// Method that plays whatever the current song is
        /// </summary>
        private void playSong()
        {
            skipThis = false;
            Stream stream = Server.StreamSong(currentSong.id); //Subsonic.StreamSong(currentSong.id);

            // Try to move this filling of memory stream into the background...
            Stream ms = new MemoryStream();
            byte[] buffer = new byte[32768];
            int read;
            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                playerThread.ReportProgress(50);

                ms.Write(buffer, 0, read);
            }

            ms.Position = 0;
            Mp3FileReader mp3Reader = new Mp3FileReader(ms);
            WaveStream blockAlignedStream =
                new BlockAlignReductionStream(
                    WaveFormatConversionStream.CreatePcmStream(mp3Reader));
            WaveOut waveOut;
            waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback());
            waveOut.Init(blockAlignedStream);
            waveOut.Play();
            playState = PlaybackState.Playing;
            bool songEnd = false;
            while (playState != PlaybackState.Stopped && !songEnd && !skipThis)
            {
                if (waveOut.PlaybackState == PlaybackState.Stopped)
                    songEnd = true;
                else
                {
                    switch (playState)
                    {
                        case PlaybackState.Paused:
                            waveOut.Pause();
                            break;
                        case PlaybackState.Playing:
                            if (waveOut.PlaybackState != PlaybackState.Playing)
                                waveOut.Play();
                            else
                            {
                                int progress = (int)(100.0 * mp3Reader.CurrentTime.TotalSeconds / mp3Reader.TotalTime.TotalSeconds);
                                playerThread.ReportProgress(progress);
                                Thread.Sleep(100);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            waveOut.Stop();
            //waveOut.Dispose();
        }

        internal void pause()
        {
            if (waveOut != null)
            {
                if (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    playState = PlaybackState.Paused;
                    waveOut.Pause();
                }
                else if (waveOut.PlaybackState == PlaybackState.Paused)
                {
                    playState = PlaybackState.Playing;
                    waveOut.Play();
                }
            }
        }

        internal void skipSong()
        {
            if (waveOut != null)
            {
                if (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    waveOut.Stop();
                }
                else if (waveOut.PlaybackState == PlaybackState.Paused)
                {
                    waveOut.Stop();
                }
            }
        }

        internal void stop()
        {
            playState = PlaybackState.Stopped;
            if (waveOut != null)
            {
                if (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    waveOut.Stop();
                }
                else if (waveOut.PlaybackState == PlaybackState.Paused)
                {
                    waveOut.Stop();
                }
            }
        }
    }
}
