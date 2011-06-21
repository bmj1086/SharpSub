using System.Xml;
using System;
using System.Xml.Linq;

namespace SharpSub.Data
{
    public class Song
    {
        private readonly XElement _itemElement;
        
        public Song(XElement itemElement)
        {
            _itemElement = itemElement;

            ID = Utility.GetElementAttribute(_itemElement, Attribute.ID.ToString().ToLower());
            Parent = Utility.GetElementAttribute(_itemElement, Attribute.Parent.ToString().ToLower());
            Title = Utility.GetElementAttribute(_itemElement, Attribute.Title.ToString().ToLower());
            IsDir = Convert.ToBoolean(Utility.GetElementAttribute(_itemElement, Attribute.IsDir.ToString().ToLower()));
            Album = Utility.GetElementAttribute(_itemElement, Attribute.Album.ToString().ToLower());
            Artist = Utility.GetElementAttribute(_itemElement, Attribute.Artist.ToString().ToLower());
            Duration = Convert.ToInt16(Utility.GetElementAttribute(_itemElement, Attribute.Duration.ToString().ToLower()));
            BitRate = Convert.ToInt16(Utility.GetElementAttribute(_itemElement, Attribute.BitRate.ToString().ToLower()));
            Track = Convert.ToInt16(Utility.GetElementAttribute(_itemElement, Attribute.Track.ToString().ToLower()));
            Year = Convert.ToInt16(Utility.GetElementAttribute(_itemElement, Attribute.Year.ToString().ToLower()));
            Genre = Utility.GetElementAttribute(_itemElement, Attribute.Genre.ToString().ToLower());
            Size = Convert.ToInt32(Utility.GetElementAttribute(_itemElement, Attribute.Size.ToString().ToLower()));
            Suffix = Utility.GetElementAttribute(_itemElement, Attribute.Suffix.ToString().ToLower());
            ContentType = Utility.GetElementAttribute(_itemElement, Attribute.ContentType.ToString().ToLower());
            IsVideo = Convert.ToBoolean(Utility.GetElementAttribute(_itemElement, Attribute.IsVideo.ToString().ToLower()));
            CoverArt = Utility.GetElementAttribute(_itemElement, Attribute.CoverArt.ToString().ToLower());
            Path = Utility.GetElementAttribute(_itemElement, Attribute.Path.ToString().ToLower());
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
        public string CoverArt { get; protected set; }
        public string Path { get; protected set; }

        
    }
}