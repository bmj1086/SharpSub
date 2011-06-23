using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Configuration;

namespace SharpSub.Data
{
    class Cache
    {
        private readonly string CacheFileName ="ListCacheFile.xml";
        //private static readonly string AppDirectory = 

        internal void CacheArtistList(IEnumerable<Artist> artistList)
        {
            
        }

        internal void CacheArtistList(string xmlString)
        {
            XDocument cacheFile = CacheDocument() ?? new XDocument();

            var elems = (from artistElement in cacheFile.Descendants().ToList()
                         where artistElement.Name.LocalName == Artist.XmlTag
                         select new Artist(artistElement));

            foreach (Artist artist in from artist in elems
                                      let matchingArtists = cacheFile.Elements().Where(e => Utility.GetElementAttribute(e, "id") == artist.ID).ToList()
                                      where matchingArtists.Count == 0
                                      select artist)
            {
                cacheFile.Add(artist._itemElement);
            }


        }

        private XDocument CacheDocument()
        {
            string filePath = GetCacheFilePath();

            return String.IsNullOrEmpty(filePath) ? null : XDocument.Load(new StreamReader(filePath));
        }

        internal void CacheArtistList(Stream xmlStream)
        {
            
        }

        internal void CacheArtistList(XDocument xDocument)
        {
            
        }

        internal static string GetAppDirectory()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString(),
                ConfigurationManager.AppSettings["AppName"]);
        }

        private string GetCacheFilePath()
        {
            string path = Path.Combine(GetAppDirectory(), CacheFileName);
            return File.Exists(path) ? path : null;
        }

    }
}
