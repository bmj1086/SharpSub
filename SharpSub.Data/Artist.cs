using System.Xml;

namespace SharpSub.Data
{
    public class Artist
    {
        public Artist(XmlElement itemElement)
        {
            _itemElement = itemElement;

            ID = GetAttribute(Attribute.id);
            Name = GetAttribute(Attribute.name);
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
            id, name
        }

        public override string ToString()
        {
            return Name;
        }

        public readonly XmlElement _itemElement;
        public string ID { get; protected set; }
        public string Name { get; protected set; }

        
    }
}