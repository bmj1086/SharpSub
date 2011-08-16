using System.Drawing;
using System.Xml;
using System;
using System.Xml.Linq;
using WMPLib;

namespace SharpSub.Data
{
    [Serializable]
    public class Song
    {
        private readonly XElement _itemElement;
        internal const string XmlTag = "song";
        
        
        public Song(XElement itemElement)
        {
            _itemElement = itemElement;

            ID = Utility.GetElementAttribute(_itemElement, Attribute.ID.ToString());
            Parent = Utility.GetElementAttribute(_itemElement, Attribute.Parent.ToString());
            Title = Utility.GetElementAttribute(_itemElement, Attribute.Title.ToString());
            IsDir = Convert.ToBoolean(Utility.GetElementAttribute(_itemElement, Attribute.IsDir.ToString()));
            Album = Utility.GetElementAttribute(_itemElement, Attribute.Album.ToString());
            Artist = Utility.GetElementAttribute(_itemElement, Attribute.Artist.ToString());
            Duration = Convert.ToInt16(Utility.GetElementAttribute(_itemElement, Attribute.Duration.ToString()));
            BitRate = Convert.ToInt16(Utility.GetElementAttribute(_itemElement, Attribute.BitRate.ToString()));
            Track = Convert.ToInt16(Utility.GetElementAttribute(_itemElement, Attribute.Track.ToString()));
            Year = Convert.ToInt16(Utility.GetElementAttribute(_itemElement, Attribute.Year.ToString()));
            Genre = Utility.GetElementAttribute(_itemElement, Attribute.Genre.ToString());
            Size = Convert.ToInt32(Utility.GetElementAttribute(_itemElement, Attribute.Size.ToString()));
            Suffix = Utility.GetElementAttribute(_itemElement, Attribute.Suffix.ToString());
            ContentType = Utility.GetElementAttribute(_itemElement, Attribute.ContentType.ToString());
            IsVideo = Convert.ToBoolean(Utility.GetElementAttribute(_itemElement, Attribute.IsVideo.ToString()));
            CoverArtID = Utility.GetElementAttribute(_itemElement, Attribute.CoverArt.ToString());
            Path = Utility.GetElementAttribute(_itemElement, Attribute.Path.ToString());
            Url = SubsonicRequest.GetSongUrl(this);
        }

       private enum Attribute
        {
            ID, Parent, Title, IsDir, Album, Artist, Duration, BitRate, Track,
            Year, Genre, Size, Suffix, ContentType, IsVideo, CoverArt, Path
        }

        public override string ToString()
        {
            return Title;
        }

        public string ID { get; protected set; }
        public string Url { get; protected set; }
        public string Parent { get; protected set; }
        public string Title { get; protected set; }
        public bool? IsDir { get; protected set; }
        public string Album { get; protected set; }
        public string Artist { get; protected set; }
        public int? Duration { get; protected set; }
        public int? BitRate { get; protected set; }
        public int? Track { get; protected set; }
        public int? Year { get; protected set; }
        public string Genre { get; protected set; }
        public int? Size { get; protected set; }
        public string Suffix { get; protected set; }
        public string ContentType { get; protected set; }
        public bool? IsVideo { get; protected set; }
        public string CoverArtID { get; protected set; }
        public string Path { get; protected set; }

        public Bitmap CoverArt(int? size = null)
        {
            return SubsonicRequest.GetAlbumArt(this, size);
        }
    }
}