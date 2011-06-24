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
        private static readonly string CacheFileName ="ListCacheFile.xml";
        //private static readonly string AppDirectory = 

        internal void CacheArtistList(IEnumerable<Artist> artistList)
        {
            
        }

        static internal void CacheArtistList(string xmlString)
        {
            XDocument cacheFile = CacheDocument() ?? new XDocument();
            XDocument toCache = XDocument.Parse(xmlString);

            var cachedArtists = cacheFile.Descendants().Where(e => e.Name.LocalName == Artist.XmlTag);

            var toAdd = (from artist in toCache.Descendants().Where(e => e.Name.LocalName == Artist.XmlTag)
                         where !cachedArtists.Contains(artist)
                         select artist).ToList();

            foreach (var xElement in toAdd)
            {
                cacheFile.Add(xElement);
            }

            cacheFile.Save(GetCacheFilePath());
        }

        static private XDocument CacheDocument()
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

        static private string GetCacheFilePath()
        {
            string path = Path.Combine(GetAppDirectory(), CacheFileName);
            return File.Exists(path) ? path : null;
        }

    }
}
