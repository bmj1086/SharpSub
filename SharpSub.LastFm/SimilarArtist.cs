﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

/*
<artist>
<name>Brandon Boyd</name>
<url>http://www.last.fm/music/Brandon+Boyd</url>
<image size="small">http://userserve-ak.last.fm/serve/34/198417.jpg</image>
<image size="medium">http://userserve-ak.last.fm/serve/64/198417.jpg</image>
<image size="large">http://userserve-ak.last.fm/serve/126/198417.jpg</image>
<image size="extralarge">http://userserve-ak.last.fm/serve/252/198417.jpg</image>
<image size="mega">
http://userserve-ak.last.fm/serve/_/198417/Brandon+Boyd.jpg
</image>
</artist>
*/

namespace SharpSub.LastFm
{
    public class SimilarArtist
    {
        public string Name { get; protected set; }
        public string Url { get; protected set; }

        private readonly XElement artistElement;

        public SimilarArtist(XElement similarArtistElement)
        {
            artistElement = similarArtistElement;
            Name = artistElement.Elements().Where(e => e.Name.LocalName == "name").First().Value;
            Url = artistElement.Elements().Where(e => e.Name.LocalName == "url").First().Value;
        }
        
        public Bitmap GetImage(Size size)
        {
            try
            {
                IEnumerable<XElement> images = artistElement.Elements().Where(e => e.Name.LocalName == "image");
                string imageUrl = (from xElement in images
                                   where xElement.Attributes().Where(a => a.Name.LocalName == "size").First().Value == size.ToString().ToLower()
                                   select xElement.Value).FirstOrDefault();

                WebRequest request = WebRequest.Create(imageUrl);
                Stream responseStream = request.GetResponse().GetResponseStream();

                return new Bitmap(responseStream);
            }
            catch (Exception)
            {
                //TODO: write to logger
                return null;
            }
            
        }

        public enum Size
        {
            Small, Medium, Large, ExtraLarge, Mega
        }
        
        public override string ToString()
        {
            return Name;
        }

    }

}
