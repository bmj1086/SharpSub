using System.Collections.Generic;
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

            var paramaters = new List<Paramater>()
                                 {
                                     new Paramater("u", CurrentUser.Username),
                                     new Paramater("p", CurrentUser.Password),
                                     new Paramater("v", "1.5.0"),
                                     new Paramater("c", "SharpSub")
                                 };

            XmlDocument xmlDocument = Ping();

            SubsonicResponse response = new SubsonicResponse(xmlDocument);
            
            if (!response.Successful)
            {
                ResetServerInformation();
            }

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

        private static SubsonicResponse Ping()
        {
            List<Paramater> parameters = new List<Paramater>{
                                                                new Paramater("u", CurrentUser.Username), 
                                                                new Paramater("p", CurrentUser.Password)
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

        private static string BuildRequestURL(RequestType requestType, IEnumerable<Paramater> parameters)
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
