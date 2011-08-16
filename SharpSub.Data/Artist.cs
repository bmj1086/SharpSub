using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace SharpSub.Data
{
    [Serializable]
    public class Artist
    {
        internal static readonly string XmlTag = "artist";
        public readonly XElement _itemElement;

        public Artist(XElement itemElement)
        {
            _itemElement = itemElement;
            ID = Utility.GetElementAttribute(_itemElement, Attribute.ID.ToString());
            Name = Utility.GetElementAttribute(_itemElement, Attribute.Name.ToString());
        }

        public string ID { get; protected set; }
        public string Name { get; protected set; }

        public IEnumerable<Song> AllSongs
        {
            get
            {
                //List<Song> songs = new List<Song>();
                //foreach (Album artistAlbum in SubsonicRequest.GetArtistAlbums(this))
                //    songs.AddRange(artistAlbum.Songs);
                //return songs;

                return SubsonicRequest.GetArtistAlbums(this).SelectMany(a => a.GetSongs());
            }
        }

        public IEnumerable<Album> GetAlbums()
        {
            return SubsonicRequest.GetArtistAlbums(this);
        }

        public override string ToString()
        {
            return Name;
        }

        #region Nested type: Attribute

        private enum Attribute
        {
            ID, Name
        }

        #endregion
    }
}