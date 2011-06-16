using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Wave;
using System.IO;
using NLayer.Decoder;

namespace TestApp
{
    class ManagedMp3Stream : WaveStream
    {
        private Stream source;
        Bitstream bitStream;
        WaveFormat waveFormat;
        Decoder decoder;
        WaveOutputBuffer outputBuffer;
        MemoryStream decodedStream;

        public ManagedMp3Stream(Stream source)
        {
            this.source = source;
            decoder = new Decoder(); //decoderParams);			
			bitStream = new Bitstream(source);
            
            Header header = bitStream.readFrame();
            int channels = (header.mode() == Header.SINGLE_CHANNEL) ? 1 : 2;
            int freq = header.frequency();
            int bits = 16;
            waveFormat = new WaveFormat(freq, bits, channels);
            decodedStream = new MemoryStream();
            outputBuffer = new WaveOutputBuffer(decodedStream, channels);
            source.Position = 0;

        }

        public override WaveFormat WaveFormat
        {
            get { return waveFormat; }
        }

        long readPos;
        public override int Read(byte[] buffer, int offset, int count)
        {
            decodedStream.Position = decodedStream.Length;
            
            Header header = bitStream.readFrame();
            if (header == null)
                return 0;
            
            decoder.setOutputBuffer(outputBuffer);
            Obuffer decoderOutput = decoder.decodeFrame(header, bitStream);
            bitStream.closeFrame();
            decodedStream.Position = readPos;

            int bytesRead = decodedStream.Read(buffer,offset,count);
            readPos += bytesRead;
            return bytesRead;
        }

        protected override void Dispose(bool disposing)
        {
            if (source != null)
            {
                source.Dispose();
                source = null;
            }
            base.Dispose(disposing);
        }

        public override long Length
        {
            get { return decodedStream.Length; }
        }

        public override long Position
        {
            get
            {
                return decodedStream.Position;
            }
            set
            {
                decodedStream.Position = 0;
            }
        }
    }
}
