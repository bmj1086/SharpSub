using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace SharpSub.Data
{
    public class Subsonic
    {
        /// <summary>
        /// Gets the current API Version from app.config. This method should never change.
        /// If you want to change the API version change it in the app.config.
        /// </summary>
        public static string APIVersion
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["APIVersion"]; }
        }

        /// <summary>
        /// Gets the current Application name from app.config. This method should never change.
        /// If you want to change the Application name change it in the app.config.
        /// </summary>
        public static string AppName
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["AppName"]; }
        }
    }

    public class User
    {
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// Use for subsonic objects
    /// </summary>
    public class SubsonicItem
    {
        public SubsonicItem(XmlElement itemElement, SubsonicItemType itemType)
        {
            Element = itemElement;
            ItemType = itemType;
        }

        public SubsonicItemType ItemType { get; protected set; }
        XmlElement Element { get; set; }
        XmlAttributeCollection ObjectAttributes { get; set; }
        
        public enum SubsonicItemType
        {
            Artist, Album, Song
        }

    }

    public class SubsonicResponse
    {
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

        public XmlDocument ResponseXml { get; set; }
        
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

        public string GetErrorMessage(int errorCode = -1)
        {
            if (errorCode == -1)
            {
                return ErrorAttribute.GetValue(GetError());
            }

            RestError error = (RestError) Enum.ToObject(typeof (RestError), errorCode);
            return ErrorAttribute.GetValue(error);
        }
        
        
    }



}
