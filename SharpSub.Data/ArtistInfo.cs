using System.Collections.Generic;
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
        public IEnumerable<string> Tags { get; protected set; }
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
                .Replace("&quot;", "\"").Trim();
            
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
            return xmlDocument.Elements().First().Elements().Where(e => e.Name.LocalName == "artist").Elements().Where(e => e.Name.LocalName == "url").First().Value;
        }

        public Bitmap Image(Size imageSize)
        {
            try
            {
                var imageElements = xmlDocument.Elements().First().Elements().Where(e => e.Name.LocalName == "artist").Elements().Where(e => e.Name.LocalName == "image");
                string imageUrl = null;

                imageUrl = (from imageElement in imageElements
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
            catch
            {
                //TODO: Write to logger
                return null;
            }
        }

        public enum Size
        {
            Small, Medium, Large, ExtraLarge, Mega
        }


    }
}