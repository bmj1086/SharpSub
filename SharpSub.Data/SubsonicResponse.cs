using System;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace SharpSub.Data
{
    public class SubsonicResponse
    {
        public XDocument ResponseXml { get; protected set; }

        public SubsonicResponse(XDocument responseXml)
        {
            ResponseXml = responseXml;
            
        }

        /// <summary>
        /// Will return false if the xml is null or the status is not "ok"
        /// </summary>
        public bool Successful
        {
            get
            {
                return Status == null ? false : Status == "ok";
            }
        }

        public string Status
        {
            get { return Utility.GetElementAttribute(ResponseXml, "subsonic-response", "status"); }
        }

        
        public string ResponseString
        {
            get { return ResponseXml == null ? string.Empty : ResponseXml.ToString(); }
        }

        public int? GetErrorCode
        {
            get
            {
                try
                {
                    return Int32.Parse(Utility.GetElementAttribute(ResponseXml, "element", "code"));
                }
                catch
                {
                    return null;
                }
            }
        }
        

        public string ErrorMessage
        {
            get { return Utility.GetElementAttribute(ResponseXml, "error", "message"); }
        }


    }
}