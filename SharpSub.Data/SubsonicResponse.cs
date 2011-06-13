using System;
using System.Xml;

namespace SharpSub.Data
{
    public class SubsonicResponse
    {
        private const int NO_ERROR = -1;

        /* Example of a failed response
        <subsonic-response status="failed" version="1.5.0">
            <error code="10" message="Required parameter is missing."/>
        </subsonic-response>
        */
        public SubsonicResponse(XmlDocument responseXml)
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
            get
            {
                try
                {
                    string status = ResponseXml.GetElementsByTagName("subsonic-response")[0].Attributes["status"].Value;
                    return ResponseXml == null ? string.Empty : status;
                }
                catch
                {
                    return null;
                }

            }
        }

        public XmlDocument ResponseXml { get; protected set; }

        public string ResponseString
        {
            get
            {
                return ResponseXml == null ? string.Empty : ResponseXml.ToString();
            }
        }

        public RestError? GetError()
        {
            try
            {
                RestError error = (RestError)Enum.ToObject(typeof(RestError), Int32.Parse(ResponseXml.GetElementsByTagName("error")[0].Attributes["code"].Value));
                return error;
            }
            catch
            {
                return null;
            }
        }

        public string GetErrorMessage(int errorCode = NO_ERROR)
        {
            if (errorCode.Equals(NO_ERROR))
                return ErrorAttribute.GetValue(GetError());

            return ErrorAttribute.GetValue((RestError)Enum.ToObject(typeof(RestError), errorCode));
        }


    }
}