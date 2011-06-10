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

        #region Subsonic Items eg. Album, Song, Artist
        public class Album : SubsonicItem
        {
            public Album(XmlElement xmlElement)
            {
                ObjectElement = xmlElement;
            }

            public enum AlbumAttribute
            {
                id, parent, title, isDir, coverArt, artist
            }

            public string GetAttribute(AlbumAttribute attribute)
            {
                return base.GetAttribute(attribute.ToString());
            }
            
        }

        public class Song : SubsonicItem
        {
            public Song(XmlElement itemElement)
            {
                ObjectElement = itemElement;
            }

            public string GetAttribute(SongAttribute attribute)
            {
                return base.GetAttribute(attribute.ToString());
            }

            public enum SongAttribute
            {
                id, parent, title, isDir, album, artist, duration, bitRate, track,
                year, genre, size, suffix, contentType, isVideo, coverArt, path
            }
        }

        public class Artist : SubsonicItem
        {
            public Artist(XmlElement itemElement)
            {
                ObjectElement = itemElement;
            }

            public string GetAttribute(ArtistAttribute attribute)
            {
                return base.GetAttribute(attribute.ToString());
            }

            public enum ArtistAttribute
            {
                id, name
            }
            
        }
        #endregion

        public abstract class SubsonicItem
        {
            internal XmlElement ObjectElement { get; set; }
            
            internal string GetAttribute(string attribute)
            {
                try
                {
                    return ObjectElement.Attributes[attribute].ToString();
                }
                catch
                {
                    return null;
                }
            }

            internal List<XmlAttribute> GetAttributes()
            {
                return ObjectElement.Attributes.Cast<XmlAttribute>().ToList();
            }
        }

        public class Response
        {
            private const int NO_ERROR = -1;

            /* Example of a failed response
            <subsonic-response status="failed" version="1.5.0">
                <error code="10" message="Required parameter is missing."/>
            </subsonic-response>
            */
            public Response(XmlDocument responseXml)
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

    

}
