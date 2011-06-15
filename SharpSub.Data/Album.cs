using System;
using System.Drawing;
using System.Xml;

namespace SharpSub.Data
{
    public class Album
    {
        public Album(XmlElement itemElement)
        {
            _itemElement = itemElement;

            ID = GetAttribute(Attribute.id);
            Parent = GetAttribute(Attribute.parent);
            Title = GetAttribute(Attribute.title);
            IsDir = Convert.ToBoolean(GetAttribute(Attribute.isDir));
            Artist = GetAttribute(Attribute.artist);
        }

        public string GetAttribute(Attribute attribute)
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

        public enum Attribute
        {
            id, parent, title, isDir, album, artist, duration, bitRate, track,
            year, genre, size, suffix, contentType, isVideo, coverArt, path

        }

        public override string ToString()
        {
            return Title;
        }

        public readonly XmlElement _itemElement;
        public string ID { get; protected set; }
        public string Parent { get; protected set; }
        public string Title { get; protected set; }
        public bool? IsDir { get; protected set; }
        public string Artist { get; protected set; }

        public Bitmap CoverArt
        {
            get { return SubsonicRequest.GetAlbumArt(this); }
        }
        
    }
}