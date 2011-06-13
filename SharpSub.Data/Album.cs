using System.Xml;

namespace SharpSub.Data
{
    public class Album
    {
        public readonly XmlElement _itemElement;

        public string ID { get; protected set; }
        public string Parent { get; protected set; }
        public string Title { get; protected set; }
        public string IsDir { get; protected set; }
        public string CoverArt { get; protected set; }
        public string Artist { get; protected set; }

        public Album(XmlElement itemElement)
        {
            _itemElement = itemElement;

            ID = GetAttribute(Attribute.id);
            Parent = GetAttribute(Attribute.parent);
            Title = GetAttribute(Attribute.title);
            IsDir = GetAttribute(Attribute.isDir);
            CoverArt = GetAttribute(Attribute.coverArt);
            Artist = GetAttribute(Attribute.artist);
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