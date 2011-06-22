using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using System.Reflection;

namespace SharpSub.Data
{
    public class SubsonicRequest
    {
        public static bool Connected { get; private set; }
        public static string ServerURL { get; private set; }
        public static string Username { get; set; }
        private static string Password { get; set; }
        private const string NOT_CONNECTED_MESSAGE = "The server is not connected. Use the Login method first.";
        private const string SongXMLTag = "child";
        private const string AlbumXMLTag = "child";
        private const string ArtistXMLTag = "artist";
        private const string API_VERSION = "1.5.0";
        private const string APP_NAME = "SharpSub";

        private static readonly List<int> AllowedBitrates = new List<int> 
            {0, 32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320};

        /// <summary>
        /// If successful, sets the Connected property to true and allows
        /// other server requests to be performed.
        /// </summary>
        /// <param name="serverURL">The URL to the server. Do not include http://</param>
        /// <param name="username"></param>
        /// <param name="password">Raw password. This method will encode the password automatically</param>
        /// <returns></returns>
        public static SubsonicResponse Login(string serverURL, string username, string password)
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
        /// <param name="song"></param>
        /// <param name="maxBitRate">If specified, the server will attempt to limit the bitrate to this value, in kilobits per second. If set to zero, no limit is imposed. Legal values are: 0, 32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256 and 320.</param>
        /// <exception cref="WebException">Thrown when the user is not logged in. This should be caught by the UI thread.</exception>
        /// <returns>MP3 Stream</returns>
        public static Stream GetSongStream(Song song, int maxBitRate = 0)
        {
            if (!Connected)
                throw new InvalidCredentialException(NOT_CONNECTED_MESSAGE);
                

            if (!SupportedBitRate(maxBitRate))
                throw new ArgumentOutOfRangeException(
                    String.Format("This bitrate is not allowed. Allowed bitrates are {0}", 
                                  String.Join(", ", AllowedBitrates)));

            var parameters = new Dictionary<string, string>
                                 {
                                    {"id", song.ID},
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


        private static SubsonicResponse SendRequest(string requestURL)
        {
            WebRequest theRequest = WebRequest.Create(requestURL);
            Stream responseStream = theRequest.GetResponse().GetResponseStream();
            XDocument xdoc = XDocument.Load(responseStream);
            return new SubsonicResponse(xdoc);
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
            sUrlBuilder.Append(API_VERSION);
            sUrlBuilder.Append("&c=");
            sUrlBuilder.Append(APP_NAME);

            return sUrlBuilder.ToString();

        }

        public static IList<Artist> GetArtistList()
        {
            string requestURL = BuildRequestURL(RequestType.getIndexes);
            var response = SendRequest(requestURL);

            if (!response.Successful)
                throw new Exception(String.Format("Error returned from Subsonic server : {0}", response.ErrorMessage));

            IList<XElement> artistElements = Utility.GetElementsFromDocument(response.ResponseXml, ArtistXMLTag);
            return (from artistElement in artistElements select new Artist(artistElement)).ToList();
        }

        public static IList<Album> GetAllAlbums()
        {
            return GetArtistList().SelectMany(GetArtistAlbums).ToList();
        }

        /// <summary>
        /// Gets all songs on the server. Since this method takes some time to 
        /// run the methodCallback delegate is optional to get called each
        /// time an artist is retrieved from the server. An example is creating
        /// a method to add the artist to a list on the main UI. This prevents
        /// the UI from locking while retrieving artists.
        /// </summary>
        /// <param name="methodCallback"></param>
        /// <returns></returns>
        public static IList<Song> GetAllSongs(Action<Song> methodCallback)
        {
            if (methodCallback == null) 
                throw new ArgumentNullException("methodCallback");

            var songs = new List<Song>();
            foreach (Song song in GetAllAlbums().SelectMany(GetAlbumSongs))
            {
                songs.Add(song);
                methodCallback(song);
            }
            return songs;
        }

        public static IList<Song> GetAlbumSongs(Album album)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>{{"id", album.ID}};
            string url = BuildRequestURL(RequestType.getMusicDirectory, paramaters);
            var response = SendRequest(url);

            if (!response.Successful)
                throw new SubsonicException(response);
            //throw new Exception(String.Format("Error returned from Subsonic server: {0}", response.ErrorMessage));

            IList<XElement> songElements = Utility.GetElementsFromDocument(response.ResponseXml, SongXMLTag);
            return (from songElement in songElements select new Song(songElement)).ToList();
        }

        
        public static IList<Album> GetArtistAlbums(Artist artist)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string> { { "id", artist.ID } };
            string url = BuildRequestURL(RequestType.getMusicDirectory, paramaters);
            var response = SendRequest(url);

            if (!response.Successful)
                throw new Exception(String.Format("Error returned from Subsonic server :{0}", response.ErrorMessage));

            IList<XElement> albumElements = Utility.GetElementsFromDocument(response.ResponseXml, AlbumXMLTag);
            return (from albumElement in albumElements select new Album(albumElement)).ToList();
        }



        internal static Bitmap GetAlbumArt(Album album, int? size = null)
        {
            var param = new Dictionary<string, string> { {"id", album.CoverArtID} };
            
            if (size != null)
                param.Add("size", size.ToString());

            try
            {
                string requestURL = BuildRequestURL(RequestType.getCoverArt, param);
                WebRequest theRequest = WebRequest.Create(requestURL);
                WebResponse response = theRequest.GetResponse();
                return new Bitmap(response.GetResponseStream());
            }
            catch (Exception ex)
            {
                //TODO: Write to logger
                return null;
            }
            
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
