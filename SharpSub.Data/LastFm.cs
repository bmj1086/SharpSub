using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using System.Configuration;

namespace SharpSub.Data
{
    public class LastFm
    {
        private readonly string apiKey;
        private string secretKey;

        public LastFm(string apikey, string secretkey)
        {
            apiKey = apikey;
            secretKey = secretkey;
        }

        public ArtistInfo GetArtistInfo(Artist artist)
        {
            try
            {
                StringBuilder urlBuilder = new StringBuilder("https://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=");
                urlBuilder.Append(HttpUtility.UrlEncode(artist.Name));
                urlBuilder.Append("&api_key=");
                urlBuilder.Append(apiKey);

                WebRequest request = WebRequest.Create(urlBuilder.ToString());
                using (Stream response = request.GetResponse().GetResponseStream())
                    return new ArtistInfo(response);

            }
            catch (Exception ex)
            {
                //TODO: Write to logger
                Debug.WriteLine("An exception occured in LastFm. StackTrace: {0}", ex.StackTrace);
                return null;
            }
        }

        
    }
}
