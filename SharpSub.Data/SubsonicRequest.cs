using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Xml;

namespace SharpSub.Data
{
    public class SubsonicRequest
    {
        public static bool Connected { get; private set; }
        public static string ServerURL { get; private set; }
        public static string Username { get; set; }
        private static string Password { get; set; }
        private const string NOT_CONNECTED_MESSAGE = "The server is not connected. Use the Login method first.";
        private static readonly List<int> AllowedBitrates = new List<int>() {0, 32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320};

        /// <summary>
        /// If successful, sets the Connected property to true and allows
        /// other server requests to be performed.
        /// </summary>
        /// <param name="serverURL">The URL to the server. Do not include http://</param>
        /// <param name="username"></param>
        /// <param name="password">Raw password. This method will encode the password automatically</param>
        /// <returns></returns>
        public static Subsonic.Response Login(string serverURL, string username, string password)
        {
            ServerURL = serverURL;
            Username = username; 
            Password = EncodePassword(password);
            Connected = true;

            string requestURL = BuildRequestURL(RequestType.ping);
            var response = SendRequest(requestURL);

            if (!response.Successful)
                Logout();
            
            return response;

        }

        private static string EncodePassword(string password)
        {
            byte[] binary = Encoding.UTF8.GetBytes(password);
            return BitConverter.ToString(binary).Replace("-", String.Empty);
        }

        /// <summary>
        /// Gets the stream for the specified song
        /// </summary>
        /// <param name="id">A string which uniquely identifies the file to stream. Obtained by calls to getMusicDirectory.</param>
        /// <param name="maxBitRate">If specified, the server will attempt to limit the bitrate to this value, in kilobits per second. If set to zero, no limit is imposed. Legal values are: 0, 32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256 and 320.</param>
        /// <exception cref="WebException">Thrown when the user is not logged in. This should be caught by the UI thread.</exception>
        /// <returns>MP3 Stream</returns>
        public static Stream GetSongStream(string id, int maxBitRate = 0)
        {
            if (!Connected)
                throw new InvalidCredentialException(NOT_CONNECTED_MESSAGE);
                

            if (String.IsNullOrEmpty(id))
                throw new ArgumentNullException(id);

            if (!SupportedBitRate(maxBitRate))
                throw new ArgumentOutOfRangeException(
                    String.Format("This bitrate is not allowed. Allowed bitrates are {0}", 
                                  String.Join(", ", AllowedBitrates)));

            var parameters = new Dictionary<string, string>
                                 {
                                    {"id", id},
                                    {"maxBitRate", maxBitRate.ToString()}
                                 };

            string requestURL = BuildRequestURL(RequestType.stream, parameters);
            WebRequest request = WebRequest.Create(requestURL);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            return dataStream;
        }

        private static bool SupportedBitRate(int maxBitRate)
        {
            return AllowedBitrates.Contains(maxBitRate);
        }
        
        public static IList<Subsonic.Artist> GetArtists()
        {
            string requestURL = BuildRequestURL(RequestType.getIndexes);
            var response = SendRequest(requestURL);

            if (!response.Successful)
                throw new Exception(String.Format("Error returned from Subsonic server : {0}", response.GetErrorMessage()));

            var artistElements = response.ResponseXml.GetElementsByTagName("artist");
            return (from XmlElement artistElement in artistElements select new Subsonic.Artist(artistElement)).ToList();
        }

        public static Subsonic.Response SendRequest(string requestURL)
        {
            WebRequest theRequest = WebRequest.Create(requestURL);
            WebResponse response = theRequest.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            var xmlResultString = sr.ReadToEnd();
            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(xmlResultString);
            return new Subsonic.Response(responseXml);
        }

        /// <summary>
        /// Changes the server state to not connected and removes the current
        /// server url and user information.
        /// </summary>
        public static void Logout()
        {
            Connected = false;
            ServerURL = null;
            Username = null;
            Password = null;

        }

        /// <summary>
        /// Used to build a URL (in string format) to request information from the server. 
        /// Should not be used by the EDT. Username and password are not additional params.
        /// </summary>
        /// <param name="requestType">The request type. Comes after /rest/ in the url.</param>
        /// <param name="additionalParameters">Parameters required by the request type</param>
        /// <returns>URL in string format to use to get the request.</returns>
        private static string BuildRequestURL(RequestType requestType, Dictionary<string, string> additionalParameters = null)
        {
            if (!Connected)
                throw new WebException(NOT_CONNECTED_MESSAGE);
            
            StringBuilder sUrlBuilder = new StringBuilder();
            sUrlBuilder.Append("http://");
            sUrlBuilder.Append(ServerURL);
            sUrlBuilder.Append("/rest/");
            sUrlBuilder.Append(requestType);
            sUrlBuilder.Append(".view");
            sUrlBuilder.Append("?u=");
            sUrlBuilder.Append(Username);
            sUrlBuilder.Append("&p=enc:");
            sUrlBuilder.Append(Password);
            
            if (additionalParameters != null)
            {
                foreach (var parameter in additionalParameters)
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
    /// Type of request to send to the server. This is a list of APIs available
    /// on the current build of Subsonic Server.
    /// </summary>
    enum RequestType
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
