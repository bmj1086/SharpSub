using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SharpSub.Data.SubsonicAPIClasses
{
    public class Song : SubsonicObject
    {
        public Song(XmlElement songElement)
        {
            xmlElement = songElement;
            ItemType = SubsonicItemType.Song;
        }

        public string ID
        {
            get
            {
                foreach (XmlAttribute attribute in
                    xmlElement.Attributes.Cast<XmlAttribute>().Where(attribute => attribute.Name.ToLower() == "id"))
                {
                    return attribute.Value;
                }

            }
            protected set;
        }

        public SubsonicItemType ItemType { get; protected set; }

        /// <summary>
        /// Used to store the xml node passed in
        /// </summary>
        private XmlElement xmlElement;

        public string GetAttribute(SongAttribute attribute)
        {
            
        }
    }

    public enum SongAttribute
    {
        id, parent, title, isDir, album, artist, duration,
        bitRate, track, year, genre, size, suffix, contentType,
        isVideo, coverArt, path
    }
}
