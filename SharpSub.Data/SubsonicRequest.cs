using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Web.UI;
using System.Windows.Forms;
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
        private const string API_VERSION = "1.5.0";
        private const string APP_NAME = "SharpSub";

        private static readonly List<int> AllowedBitrates = new List<int> { 0, 32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320 };

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
        public static Stream GetSongStream(Song song, int maxBitRate = 0/*, string format = null*/)
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
            /*if (!String.IsNullOrEmpty(format))
                parameters.Add("format", format);*/

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

            IList<XElement> artistElements = Utility.GetElementsFromDocument(response.ResponseXml, Artist.XmlTag);
            return (from artistElement in artistElements select new Artist(artistElement)).ToList();
        }

        public static IList<Album> GetAllAlbums()
        {
            return GetArtistList().SelectMany(GetArtistAlbums).ToList();
        }

        /// <summary>
        /// Gets all songs on the server. Since this method takes some time to 
        /// run the methodCallback delegate is called each time an artist is retrieved 
        /// This prevents the UI from locking while retrieving artists.
        /// </summary>
        /// <param name="methodCallback">The delegate to call when an artist is retrieved</param>
        /// <example>SubsonicRequest.GetAllSongs(song => listBox1.Invoke((MethodInvoker)(()=>listBox1.Items.Add(song))));</example>
        public static void GetAllSongs(Action<Song> methodCallback)
        {
            if (methodCallback == null)
                throw new ArgumentNullException("methodCallback");

            ThreadPool.QueueUserWorkItem(delegate
            {
                foreach (Song song in GetAllSongs())
                {
                    methodCallback(song);
                }
            });
        }

        /// <summary>
        /// This method takes a long time to run until a method is implemented into the API.
        /// I highly suggest that you either use the GetAllSongs(methodCallback) method or 
        /// run this in a separate thread from the UI.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Song> GetAllSongs()
        {
            return GetAllAlbums().SelectMany(GetAlbumSongs);
        }

        public static IList<Song> GetAlbumSongs(Album album)
        {
            return GetAlbumSongs(album.ID);
        }

        public static IList<Song> GetAlbumSongs(string albumid)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string> { { "id", albumid } };
            string url = BuildRequestURL(RequestType.getMusicDirectory, paramaters);
            var response = SendRequest(url);

            if (!response.Successful)
                throw new SubsonicException(response);

            IList<XElement> songElements = Utility.GetElementsFromDocument(response.ResponseXml, Song.XmlTag);
            return (from songElement in songElements select new Song(songElement)).ToList();
        }

        public static IList<Album> GetArtistAlbums(Artist artist)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string> { { "id", artist.ID } };
            string url = BuildRequestURL(RequestType.getMusicDirectory, paramaters);
            var response = SendRequest(url);

            if (!response.Successful)
                throw new Exception(String.Format("Error returned from Subsonic server :{0}", response.ErrorMessage));

            IList<XElement> albumElements = Utility.GetElementsFromDocument(response.ResponseXml, Album.XmlTag);
            return (from albumElement in albumElements select new Album(albumElement)).ToList();
        }



        internal static Bitmap GetAlbumArt(object albumOrSong, int? size = null)
        {
            string coverArtID = String.Empty;

            if (!(albumOrSong is Album) && !(albumOrSong is Song))
                throw new Exception("albumOrSong must be an instance of an Album or a Song");
            if (albumOrSong is Album)
                coverArtID = (albumOrSong as Album).CoverArtID;
            if (albumOrSong is Song)
                coverArtID = (albumOrSong as Song).CoverArtID;

            var param = new Dictionary<string, string> { { "id", coverArtID } };

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

        /*
        Parameter	    Required	Default	Comment
        size	        No	        10	    The maximum number of songs to return. Max 500.
        genre	        No		            Only returns songs belonging to this genre.
        fromYear	    No		            Only return songs published after or in this year.
        toYear	        No		            Only return songs published before or in this year.
        musicFolderId	No		            Only return songs in the music folder with the given ID. See getMusicFolders.
         */
        public static IEnumerable<Song> GetRandomSongs(int? size = null, string genre = null, int? fromYear = null, int? toYear = null)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>();
            if (size != null)
                paramaters.Add("size", size.ToString());
            if (genre != null)
                paramaters.Add("size", genre.ToString());
            if (fromYear != null)
                paramaters.Add("size", fromYear.ToString());
            if (toYear != null)
                paramaters.Add("size", toYear.ToString());


            string url = BuildRequestURL(RequestType.getRandomSongs, paramaters);
            SubsonicResponse response = SendRequest(url);

            if (!response.Successful)
                throw new SubsonicException(response);

            IList<XElement> songElements = Utility.GetElementsFromDocument(response.ResponseXml, "song");
            return (from songElement in songElements select new Song(songElement)).ToList();
        }

        internal static string GetSongUrl(Song song)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", song.ID);
            return BuildRequestURL(RequestType.stream, parameters);
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
