using System.Xml;
using System;

namespace SharpSub.Data
{
    public class Song
    {
        private readonly XmlElement _itemElement;
        
        public Song(XmlElement itemElement)
        {
            _itemElement = itemElement;

            ID = GetAttribute(Attribute.id);
            Parent = GetAttribute(Attribute.parent);
            Title = GetAttribute(Attribute.title);
            IsDir = Convert.ToBoolean(GetAttribute(Attribute.isDir));
            Album = GetAttribute(Attribute.album);
            Artist = GetAttribute(Attribute.artist);
            Duration = Convert.ToInt16(GetAttribute(Attribute.duration));
            BitRate = Convert.ToInt16(GetAttribute(Attribute.bitRate));
            Track = Convert.ToInt16(GetAttribute(Attribute.track));
            Year = Convert.ToInt16(GetAttribute(Attribute.year));
            Genre = GetAttribute(Attribute.genre);
            Size = Convert.ToInt32(GetAttribute(Attribute.size));
            Suffix = GetAttribute(Attribute.suffix);
            ContentType = GetAttribute(Attribute.contentType);
            IsVideo = Convert.ToBoolean(GetAttribute(Attribute.isVideo));
            CoverArt = GetAttribute(Attribute.coverArt);
            Path = GetAttribute(Attribute.path);
        }

        private string GetAttribute(Attribute attribute)
        {
            try
            {
                return _itemElement.Attributes[attribute.ToString()].InnerText;
            }
            catch
            {
                return null;
            }
        }

        private enum Attribute
        {
            id, parent, title, isDir, album, artist, duration, bitRate, track,
            year, genre, size, suffix, contentType, isVideo, coverArt, path

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