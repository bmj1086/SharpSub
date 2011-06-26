using System;
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
        private string _apiKey;
        private string _secretKey;

        public LastFm(string apikey, string secretkey)
        {
            _apiKey = apikey;
            _secretKey = secretkey;
        }

        public ArtistInfo GetArtistInfo(Artist artist)
        {
            StringBuilder urlBuilder = new StringBuilder("https://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=");
            urlBuilder.Append(HttpUtility.UrlEncode(artist.Name));
            urlBuilder.Append("&api_key=");
            urlBuilder.Append(_apiKey);

            WebRequest request = WebRequest.Create(urlBuilder.ToString());
            using (Stream response = request.GetResponse().GetResponseStream())
            {
                return new ArtistInfo(response);
            }
        }

        
    }
}
