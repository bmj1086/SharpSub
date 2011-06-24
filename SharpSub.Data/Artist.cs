﻿using System.Xml;
using System.Xml.Linq;

namespace SharpSub.Data
{
    public class Artist
    {
        public readonly XElement _itemElement;
        public string ID { get; protected set; }
        public string Name { get; protected set; }
        internal const string XmlTag = "artist";
        
        public Artist(XElement itemElement)
        {
            _itemElement = itemElement;

            ID = Utility.GetElementAttribute(_itemElement, Attribute.ID.ToString());
            Name = Utility.GetElementAttribute(_itemElement, Attribute.Name.ToString());
        }

        private enum Attribute
        {
            ID, Name
        }

        public override string ToString()
        {
            return Name;
        }

        
    }
}