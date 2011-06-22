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
                var attributes = (from e in xDocument.Descendants().ToList()
                                  where e.Name.LocalName == elementTag
                                  select e).ToList().FirstOrDefault().Attributes().ToList();

                return (from attribute in attributes
                        where attribute.Name.LocalName == attributeName
                        select attribute).FirstOrDefault().Value;
            }
            catch
            {
                return null;
            }
        }

        public static string GetElementAttribute(XElement xElement, string attributeName)
        {
            try
            {
                return (from attribute in xElement.Attributes().ToList()
                        where attribute.Name.LocalName == attributeName
                        select attribute).FirstOrDefault().Value;
            }
            catch
            {
                return null;
            }
        }

        internal static IList<XElement> GetElementsFromDocument(XDocument xDocument, string xmlTag)
        {
            try
            {
                return (from e in xDocument.Descendants()
                        where e.Name.LocalName == xmlTag
                        select e).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
