using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Web;

namespace SharpSub.Data
{
    public class ArtistInfo
    {
        private readonly XDocument xmlDocument = null;

        public ArtistInfo(Stream xmlstream)
        {
            StreamReader reader = new StreamReader(xmlstream);
            xmlDocument = XDocument.Parse(reader.ReadToEnd());
        }

        public string Summary()
        {
            string rawBio = xmlDocument.Elements().First().Elements().
                Where(e => e.Name.LocalName == "artist").Elements().
                Where(e => e.Name.LocalName == "bio").Elements().
                Where(e => e.Name.LocalName == "summary").FirstOrDefault().Value;

            return Regex.Replace(rawBio, "<.*?>", string.Empty)
                .Replace("&quot;", "\"")
                .Replace("&amp;", "&").Trim();

        }

        public IEnumerable<SimilarArtist> SimilarArtists()
        {
            var similarArtists = xmlDocument.Elements().First().Elements().
                Where(e => e.Name.LocalName == "artist").Elements().
                Where(e => e.Name.LocalName == "similar").Elements().
                Where(e => e.Name.LocalName == "artist").ToList();

            return similarArtists.Select(element => new SimilarArtist(element)).ToList();
        }

        public string LastFmUrl()
        {
            return xmlDocument.Elements().First().Elements().
                Where(e => e.Name.LocalName == "artist").Elements().
                Where(e => e.Name.LocalName == "url").First().Value;
        }

        public IEnumerable<string> Tags()
        {
            IEnumerable<XElement> tags = xmlDocument.Elements().First().Elements().
                Where(e => e.Name.LocalName == "artist").Elements().
                Where(e => e.Name.LocalName == "tags").Elements();

            IList<string> strTags = null;

            foreach (XElement xElement in tags)
            {
                strTags.Add(xElement.Elements().Where(e => e.Name.LocalName == "name").First().Value);
            }

            return strTags;
        }

        public Bitmap Image(Size imageSize)
        {
            var imageElements = xmlDocument.Elements().First().Elements().
                Where(e => e.Name.LocalName == "artist").Elements().
                Where(e => e.Name.LocalName == "image");

            string imageUrl = (from imageElement in imageElements
                                from xAttribute in
                                    imageElement.Attributes().Where(xAttribute => xAttribute.Name.LocalName == "size").
                                    Where(xAttribute => xAttribute.Value == imageSize.ToString().ToLower())
                                select imageElement.Value).FirstOrDefault();


            WebRequest request = WebRequest.Create(imageUrl);
            Stream responseStream = request.GetResponse().GetResponseStream();

            Bitmap bitmap = new Bitmap(responseStream);

            request = null;
            responseStream = null;

            return bitmap;
        }



        public enum Size
        {
            Small, Medium, Large, ExtraLarge, Mega
        }


    }
}