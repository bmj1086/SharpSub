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
        private const string INDEX_CACHE_FILENAME = "Index.xml";

        internal void CacheArtistList(IEnumerable<Artist> artistList)
        {

        }

        static internal void CacheArtistList(string xmlString)
        {
            XDocument cacheFile = CurrentCacheDocument() ?? new XDocument();
            XDocument toCache = XDocument.Parse(xmlString);

            var cachedArtists = cacheFile.Descendants().Where(e => e.Name.LocalName == Artist.XmlTag);

            toCache.Descendants().
                Where(e => e.Name.LocalName == Artist.XmlTag).
                Where(a => !cachedArtists.Contains(a)).
                ToList().ForEach(cacheFile.Add);
            
            cacheFile.Save(GetCacheFilePath());
            
        }

        static private XDocument CurrentCacheDocument()
        {
            string filePath = GetCacheFilePath();
            return String.IsNullOrEmpty(filePath) ? null : XDocument.Load(new StreamReader(filePath));
        }

        internal void CacheArtistList(Stream xmlStream)
        {
            throw new NotImplementedException();
        }

        internal void CacheArtistList(XDocument xDocument)
        {
            throw new NotImplementedException();
        }

        internal static string GetCacheDirectory()
        {
            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString(),
                "cache",
                ConfigurationManager.AppSettings["AppName"]);

            if (!Directory.Exists(path)) 
                Directory.CreateDirectory(path);
            return path;
        }

        static private string GetCacheFilePath()
        {
            return Path.Combine(GetCacheDirectory(), INDEX_CACHE_FILENAME);
        }
        
        public static bool CashExists(CacheType cacheType)
        {
            return File.Exists(INDEX_CACHE_FILENAME);
        }

        public static IEnumerable<Artist> GetCachedArtists()
        {
            XDocument cacheFile = CurrentCacheDocument() ?? new XDocument();

            return cacheFile.Descendants().Where(e => e.Name.LocalName == Artist.XmlTag).Select(xElement => new Artist(xElement));
        }

        public static IEnumerable<Album> GetCachedAlbums(Artist artist)
        {
            throw new NotImplementedException();
        }
    }
}
