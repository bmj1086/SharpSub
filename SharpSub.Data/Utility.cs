using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SharpSub.Data
{
    class Utility
    {
        public static string GetElementAttribute(XDocument xDocument, string elementTag, string attributeName)
        {
            try
            {
                return (from attribute in
                            (from e in xDocument.Elements().ToList()
                             where e.Name == elementTag
                             select e).ToList().FirstOrDefault().Attributes().ToList()
                        where attribute.Name == attributeName
                        select attribute).FirstOrDefault().Value;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public static string GetElementAttribute(XElement xElement, string attributeName)
        {
            try
            {
                return (from attribute in xElement.Attributes().ToList()
                        where attribute.Name == attributeName
                        select attribute).FirstOrDefault().Value;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        internal static IList<XElement> GetElementsFromDocument(XDocument xDocument, string xmlTag)
        {
            return (from e in xDocument.Elements()
                    where e.Name == xmlTag
                    select e).ToList();
        }
    }
}
