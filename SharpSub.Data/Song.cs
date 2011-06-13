using System.Xml;

namespace SharpSub.Data
{
    public class Song
    {
        private readonly XmlElement _itemElement;

        public string ID { get; protected set; }
        public string Parent { get; protected set; }
        public string Title { get; protected set; }
        public string IsDir { get; protected set; }
        public string Album { get; protected set; }
        public string Artist { get; protected set; }
        public string Duration { get; protected set; }
        public string BitRate { get; protected set; }
        public string Track { get; protected set; }
        public string Year { get; protected set; }
        public string Genre { get; protected set; }
        public string Size { get; protected set; }
        public string Suffix { get; protected set; }
        public string ContentType { get; protected set; }
        public string IsVideo { get; protected set; }
        public string CoverArt { get; protected set; }
        public string Path { get; protected set; }

        public Song(XmlElement itemElement)
        {
            _itemElement = itemElement;

            ID = GetAttribute(Attribute.id);
            Parent = GetAttribute(Attribute.parent);
            Title = GetAttribute(Attribute.title);
            Album = GetAttribute(Attribute.album);
            Artist = GetAttribute(Attribute.artist);
            Duration = GetAttribute(Attribute.duration);
            BitRate = GetAttribute(Attribute.bitRate);
            Track = GetAttribute(Attribute.track);
            Year = GetAttribute(Attribute.year);
            Genre = GetAttribute(Attribute.genre);
            Size = GetAttribute(Attribute.size);
            Suffix = GetAttribute(Attribute.suffix);
            ContentType = GetAttribute(Attribute.contentType);
            IsVideo = GetAttribute(Attribute.isVideo);
            CoverArt = GetAttribute(Attribute.coverArt);
            Path = GetAttribute(Attribute.path);
        }

        public string GetAttribute(Attribute attribute)
        {
            return _itemElement.Attributes[attribute.ToString()].InnerText;
        }

        public enum Attribute
        {
            id, parent, title, isDir, album, artist, duration, bitRate, track,
            year, genre, size, suffix, contentType, isVideo, coverArt, path

        }

        public override string ToString()
        {
            return Title;
        }
    }
}