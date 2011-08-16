using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;

namespace SharpSub.Data
{
    [Serializable]
    public class Album
    {
        #region Attribute enum

        public enum Attribute
        {
            Id,
            Parent,
            Title,
            IsDir,
            Album,
            Artist,
            Duration,
            BitRate,
            Track,
            Year,
            Genre,
            Size,
            Suffix,
            ContentType,
            IsVideo,
            CoverArt,
            Path
        }

        #endregion

        internal const string XmlTag = "child";

        public readonly XElement ItemElement;

        public Album(XElement itemElement)
        {
            ItemElement = itemElement;

            Id = Utility.GetElementAttribute(itemElement, Attribute.Id.ToString());
            CoverArtId = Utility.GetElementAttribute(itemElement, Attribute.CoverArt.ToString());
            Parent = Utility.GetElementAttribute(itemElement, Attribute.Parent.ToString());
            Title = Utility.GetElementAttribute(itemElement, Attribute.Title.ToString());
            IsDir = Convert.ToBoolean(Utility.GetElementAttribute(itemElement, Attribute.IsDir.ToString()));
            Artist = Utility.GetElementAttribute(itemElement, Attribute.Artist.ToString());
        }

        public string Id { get; protected set; }
        public string Parent { get; protected set; }
        public string Title { get; protected set; }
        public bool? IsDir { get; protected set; }
        public string Artist { get; protected set; }
        internal string CoverArtId { get; set; }

        public IEnumerable<Song> GetSongs()
        {
            return SubsonicRequest.GetAlbumSongs(this);
        }

        public override string ToString()
        {
            return Title;
        }

        public Bitmap CoverArt(int? size = null)
        {
            return SubsonicRequest.GetAlbumArt(this, size);
        }
    }
}