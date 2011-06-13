using System.Xml;

namespace SharpSub.Data
{
    public class Artist
    {
        public readonly XmlElement _itemElement;
        public string ID { get; protected set; }
        public string Name { get; protected set; }

        public Artist(XmlElement itemElement)
        {
            _itemElement = itemElement;

            ID = GetAttribute(Attribute.id);
            Name = GetAttribute(Attribute.name);
        }

        public string GetAttribute(Attribute attribute)
        {
            return _itemElement.Attributes[attribute.ToString()].InnerText;
        }

        public enum Attribute
        {
            id, name
        }

        public override string ToString()
        {
            return Name;
        }

    }
}