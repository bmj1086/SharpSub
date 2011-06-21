using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Xml.Linq;

namespace SharpSub.Data
{
    public class LastFm
    {
        internal static readonly string API_KEY = ConfigurationManager.AppSettings["lastfm_api_key"];
        internal static readonly string SECRET_KEY = ConfigurationManager.AppSettings["lastfm_secret_key"];

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
             */
            
            return null;
        }


        public class ArtistInfo
        {
            public IList<string> SimilarArtists { get; protected set; }
            public IList<string> Tags { get; protected set; }
            public string Summary { get; protected set; }
            private readonly XDocument xDocument = null;

            public ArtistInfo(string xml)
            {
                xDocument = XDocument.Parse(xml);
            }

            
            public Bitmap Image(ImageSize imageSize)
            {
                try
                {
                    var imageElements = xDocument.Elements().Where(e => e.Name == "artist" && e.Parent.Name == "lfm")
                        .First().Elements().Where(el => el.Name == "image").ToList();

                    var imageUrl = (from a in imageElements.Attributes()
                                   where (a.Name == "size") && (a.Value == imageSize.ToString().ToLower())
                                   select a).ToList().FirstOrDefault().Value.ToString();

                    // Old XmlDocument style
                    //string imageUrl = imageElements.Where(
                    //                    imageElement => imageElement.Attributes["size"].InnerText == imageSize.ToString().ToLower()).
                    //                        FirstOrDefault().InnerText;

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
