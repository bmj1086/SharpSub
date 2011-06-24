using System;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;

namespace SharpSub.Data
{
    public class Album
    {
        internal const string XmlTag = "child";

        public Album(XElement itemElement)
        {
            _itemElement = itemElement;

            ID = Utility.GetElementAttribute(itemElement, Attribute.ID.ToString());
            CoverArtID = Utility.GetElementAttribute(itemElement, Attribute.CoverArt.ToString());
            Parent = Utility.GetElementAttribute(itemElement, Attribute.Parent.ToString());
            Title = Utility.GetElementAttribute(itemElement, Attribute.Title.ToString());
            IsDir = Convert.ToBoolean(Utility.GetElementAttribute(itemElement, Attribute.IsDir.ToString()));
            Artist = Utility.GetElementAttribute(itemElement, Attribute.Artist.ToString());}

        public enum Attribute
        {
            ID, Parent, Title, IsDir, Album, Artist, Duration, BitRate, Track,
            Year, Genre, Size, Suffix, ContentType, IsVideo, CoverArt, Path

        }

        public override string ToString()
        {
            return Title;
        }

        public readonly XElement _itemElement;
        public string ID { get; protected set; }
        public string Parent { get; protected set; }
        public string Title { get; protected set; }
        public bool? IsDir { get; protected set; }
        public string Artist { get; protected set; }
        internal string CoverArtID { get; set; }

        public Bitmap CoverArt(int? size = null)
        {
            return SubsonicRequest.GetAlbumArt(this, size);
        }
        
    }
}