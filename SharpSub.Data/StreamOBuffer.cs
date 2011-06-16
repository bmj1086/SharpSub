using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLayer.Decoder;
using System.IO;
using NAudio.Wave;

namespace TestApp
{
    class WaveOutputBuffer : Obuffer
    {
        BinaryWriter baseStream;
        private short[] buffer;
        private short[] bufferp;
        private int channels;

        public WaveOutputBuffer(Stream outputStream, int number_of_channels)
        {
            this.baseStream = new BinaryWriter(outputStream);
            buffer = new short[OBUFFERSIZE];
            bufferp = new short[MAXCHANNELS];
            channels = number_of_channels;

            clear_buffer();
        }

        /// <summary>
        /// Append a 16 bit PCM sample
        /// </summary>
        public override void append(int channel, short value)
        {
            buffer[bufferp[channel]] = value;
            bufferp[channel] += (short)channels;
        }

        /**
         * Write the samples to the file (Random Acces).
         */
        public override void write_buffer(int val)
        {

            for (int sample = 0; sample < bufferp[0]; sample++)
            {
                baseStream.Write(buffer[sample]);
            }

            clear_buffer();
        }

        public override void close()
        {
            baseStream.Close();
        }

        /**
         *
         */
        public override void clear_buffer()
        {
            for (int i = 0; i < channels; ++i) bufferp[i] = (short)i;
        }

        /**
         *
         */
        public override void set_stop_flag()
        { }


    }
}
