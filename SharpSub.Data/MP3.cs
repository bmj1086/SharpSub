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
using TestApp;

namespace SharpSub.Data
{
    class MP3 : IDisposable
    {
        public MP3(Song song)
        {
            using (Stream ms = new MemoryStream())
            {
                using (Stream stream = SubsonicRequest.GetSongStream(song))
                {
                    byte[] buffer = new byte[32768];
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                }
                ms.Position = 0;

                using (WaveStream stream = new ManagedMp3Stream(ms))

                //using (WaveStream blockAlignedStream =
                //    new BlockAlignReductionStream(
                //        WaveFormatConversionStream.CreatePcmStream(
                //            new Mp3FileReader(ms))))
                {
                    using (WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                    {
                        waveOut.Init(stream);
                        waveOut.Play();
                        while (waveOut.PlaybackState == PlaybackState.Playing)
                        {
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                }
            }
        }

        public void Play()
        {

        }

        public void Stop()
        {

            Dispose();
        }

        public void Pause()
        {

        }

        public void Dispose()
        {
            //TODO: Dispose of the player and other objects that encode/decode the stream, etc.
        }
    }

}
