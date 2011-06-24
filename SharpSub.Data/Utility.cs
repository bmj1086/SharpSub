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
                                  where e.Name.LocalName.ToLower() == elementTag
                                  select e).ToList().FirstOrDefault().Attributes().ToList();

                return (from attribute in attributes
                        where attribute.Name.LocalName.ToLower() == attributeName
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
                        where attribute.Name.LocalName.ToLower().ToLower() == attributeName.ToLower()
                        select attribute).FirstOrDefault().Value;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a list of XElements from an XDocument
        /// </summary>
        /// <param name="xDocument">The XDocument to parse</param>
        /// <param name="xmlTag">The xml tag of the XElement to find</param>
        /// <param name="parent">If specified, returns XElements matching the xmltag only in the specified parent</param>
        /// <returns></returns>
        internal static IList<XElement> GetElementsFromDocument(XDocument xDocument, string xmlTag, string parent = null)
        {
            try
            {
                if (parent == null)
                    return (from e in xDocument.Descendants()
                            where e.Name.LocalName.ToLower() == xmlTag
                            select e).ToList();

                return (from e in xDocument.Descendants()
                        where e.Name.LocalName.ToLower() == xmlTag && e.Parent.Name == parent
                        select e).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
