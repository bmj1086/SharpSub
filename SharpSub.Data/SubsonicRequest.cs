﻿using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace SharpSub.Data
{
    public static class SubsonicRequest
    {
        public static bool Connected { get; private set; }
        static public string ServerURL { get; private set;}
        private static User CurrentUser { get; set; }


        public static SubsonicResponse LogIn(string serverURL, string username, string password)
        {
            ServerURL = serverURL;
            CurrentUser = new User(username, password);
            Connected = true;

            var paramaters = new List<Parameter>()
                                 {
                                     new Parameter("u", CurrentUser.Username),
                                     new Parameter("p", CurrentUser.Password),
                                     new Parameter("v", "1.5.0"),
                                     new Parameter("c", "SharpSub")
                                 };

            SubsonicResponse response = Ping();

            if (!response.Successful)
            {
                ResetServerInformation();
            }

            return response;

        }

        /// <summary>
        /// Gets the stream for the specified song
        /// </summary>
        /// <param name="id">A string which uniquely identifies the file to stream. Obtained by calls to getMusicDirectory.</param>
        /// <param name="maxBitRate">If specified, the server will attempt to limit the bitrate to this value, in kilobits per second. If set to zero, no limit is imposed. Legal values are: 0, 32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256 and 320.</param>
        /// <returns>MP3 Stream</returns>
        public static Stream GetSongStream(string id, int maxBitRate = 0)
        {
            List<Parameter> parameters = new List<Parameter>()
                                             {
                                                 new Parameter("id", id),
                                                 new Parameter("maxBitRate", maxBitRate.ToString())
                                             };

            string requestURL = BuildRequestURL(RequestType.stream, parameters);
            WebRequest request = WebRequest.Create(requestURL);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            return dataStream;
        }
        
        private static SubsonicResponse Ping()
        {
            List<Parameter> parameters = new List<Parameter>{
                                                                new Parameter("u", CurrentUser.Username), 
                                                                new Parameter("p", CurrentUser.Password)
                                                            };

            string requestURL = BuildRequestURL(RequestType.ping, parameters);
            WebRequest theRequest = WebRequest.Create(requestURL);
            WebResponse response = theRequest.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            var xmlResultString = sr.ReadToEnd();
            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(xmlResultString);
            return new SubsonicResponse(responseXml);

        }

        /// <summary>
        /// Changes the server state to not connected and removes the current
        /// server url and user information.
        /// </summary>
        public static void ResetServerInformation()
        {
            Connected = false;
            ServerURL = null;
            CurrentUser = null;
            
        }

        private static string BuildRequestURL(RequestType requestType, IEnumerable<Parameter> parameters)
        {
            StringBuilder sUrlBuilder = new StringBuilder();
            sUrlBuilder.Append("http://");
            sUrlBuilder.Append(ServerURL);
            sUrlBuilder.Append("/rest/");
            sUrlBuilder.Append(requestType);
            sUrlBuilder.Append(".view");
            sUrlBuilder.Append("?u=");
            sUrlBuilder.Append(CurrentUser.Username);
            sUrlBuilder.Append("&p=");
            sUrlBuilder.Append(CurrentUser.Password);
            //http: //192.168.1.4/music/rest/getMusicDirectory.view?&u=Guest&p=notbrett&v=1.5.0&c=SharpSub
            //Guest?p=notbrett" + "?v=" + apiVersion + "&c=" + appName)

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    sUrlBuilder.Append("&");
                    sUrlBuilder.Append(parameter.Key);
                    sUrlBuilder.Append("=");
                    sUrlBuilder.Append(parameter.Value);
                }
            }

            sUrlBuilder.Append("&v=");
            sUrlBuilder.Append(Subsonic.APIVersion);
            sUrlBuilder.Append("&c=");
            sUrlBuilder.Append(Subsonic.AppName);

            return sUrlBuilder.ToString();

        }
    }

    /// <summary>
    /// Type of request to send to the server.
    /// </summary>
    public enum RequestType
    {
        ping,
        getLicense,
        getMusicFolders,
        getNowPlaying,
        getIndexes,
        getMusicDirectory,
        search2,
        getPlaylists,
        getPlaylist,
        createPlaylist,
        deletePlaylist,
        download,
        stream,
        getCoverArt,
        scrobble,
        changePassword,
        getUser,
        createUser,
        deleteUser,
        getChatMessages,
        addChatMessage,
        getAlbumList,
        getRandomSongs,
        getLyrics,
        jukeboxControl

    }

    

}
