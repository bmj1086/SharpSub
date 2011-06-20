using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace SharpSub.Data
{
    /*
     * Your API Key is 15790800a7aebdfd99d3328353b09f07
     * Your secret is 70573d54a452a512c1fbae5fdfaeddb4
     */

    public class LastFm
    {
        private static readonly string API_KEY = "15790800a7aebdfd99d3328353b09f07";
        private static readonly string SECRET_KEY = "70573d54a452a512c1fbae5fdfaeddb4";

        public ArtistInfo GetArtistInfo(Artist artist)
        {
            /*
             * https://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=Cher&api_key=15790800a7aebdfd99d3328353b09f07
             * 
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
            private readonly XmlDocument xmlDocument;
            
            public ArtistInfo(string xml)
            {
                xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xml);
            }

            public Bitmap Image(ImageSize imageSize)
            {
                XmlElement element = xmlDocument.GetElementsByTagName("artist")[0] as XmlElement;
                XmlNodeList imageElements = element.GetElementsByTagName("image");
                
                string imageUrl = imageElements.Cast<XmlElement>().Where(
                                    imageElement => imageElement.Attributes["size"].InnerText == imageSize.ToString().ToLower()).
                                        FirstOrDefault().InnerText;

                WebRequest request = WebRequest.Create(imageUrl);
                Stream responseStream = request.GetResponse().GetResponseStream();

                Bitmap bitmap = new Bitmap(responseStream);
                return bitmap;

            }

            public enum ImageSize
            {
                Small, Medium, Large, ExtraLarge, Mega
            }

            private IList<string> GetAttributes(string attribute)
            {
                throw new NotImplementedException();
            }

            
        }
    }
}
