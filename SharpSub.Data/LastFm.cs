using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Configuration;
using System.Xml.Linq;

namespace SharpSub.Data
{
    public class LastFm
    {
        private string _apiKey;
        private string _secretKey;

        public LastFm(string apikey, string secretkey)
        {
            _apiKey = apikey;
            _secretKey = secretkey;
        }

        public ArtistInfo GetArtistInfo(Artist artist)
        {
            /*
             * Params::
             * artist (Required (unless mbid)] : The artist name
             * mbid (Optional) : The musicbrainz id for the artist
             * lang (Optional) : The language to return the biography in, expressed as an ISO 639 alpha-2 code.
             * autocorrect[0|1] (Optional) : Transform misspelled artist names into correct artist names, returning the correct version instead. The corrected artist name will be returned in the response.
             * username (Optional) : The username for the context of the request. If supplied, the user's playcount for this artist is included in the response.
             * api_key (Required) : A Last.fm API key.
             * http://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=Cher&api_key=b25b959554ed76058ac220b7b2e0a026
             */
            string url = BuildUrl(artist);
            WebRequest request = WebRequest.Create(url);
            using (Stream response = request.GetResponse().GetResponseStream())
            {
                return new ArtistInfo(response);
            }
        }

        private string BuildUrl(Artist artist)
        {
            StringBuilder sb = new StringBuilder("https://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=");
            sb.Append(HttpUtility.UrlEncode(artist.Name));
            sb.Append("&api_key=");
            sb.Append(_apiKey);
            return sb.ToString();
        }

        public class ArtistInfo
        {
            public IList<string> Tags { get; protected set; }
            private readonly XDocument xmlDocument = null;
            
            public ArtistInfo(Stream xmlstream)
            {
                StreamReader reader = new StreamReader(xmlstream);
                xmlDocument = XDocument.Parse(reader.ReadToEnd());
            }

            public string Summary()
            {
                string rawBio =  xmlDocument.Elements().First().Elements().
                    Where(e => e.Name.LocalName == "artist").Elements().
                    Where(e => e.Name.LocalName == "bio").Elements().
                    Where(e => e.Name.LocalName == "summary").FirstOrDefault().Value;

                return Regex.Replace(rawBio, "<.*?>", string.Empty).Replace("&quot;", "\"").Trim();
            }

            public string LastFmUrl()
            {
                return xmlDocument.Elements().First().Elements().Where(e => e.Name.LocalName == "artist").Elements().Where(e => e.Name.LocalName == "url").First().Value;
            }

            public Bitmap Image(ImageSize imageSize)
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

            public enum ImageSize
            {
                Small, Medium, Large, ExtraLarge, Mega
            }


        }
    }
}
