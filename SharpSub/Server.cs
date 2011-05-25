using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace SharpSub
{
    class Server
    {
        public static string CurrentUrl { get; set; }
        public static string CurrentUsername { get; set; }
        public static string CurrentPassword { get; set; }
        public static bool Connected { get; private set; }


        private const string TEST_URL = "http://{URL}/rest/{TYPE}.view?u={USERNAME}&p={ENCODEDURL}&v={VERSION}.0&c={APPNAME}";

        // returns true if the connection worked, else returns false
        internal static bool TestConnection(string url, string username, string password)
        {
            CurrentUrl = url.Replace("http://", String.Empty);
            CurrentUsername = username;
            CurrentPassword = System.Web.HttpUtility.UrlEncode(password);
            
            try
            {
                string sTmpUrl = BuildGenericUrl(RequestType.ping);
                
                HttpWebRequest req = WebRequest.Create(new Uri(sTmpUrl)) 
                                     as HttpWebRequest;

                XmlDocument xmlResult = new XmlDocument();

                if (req == null)
                    return false;

                using (HttpWebResponse resp = req.GetResponse()
                                                as HttpWebResponse)
                {
                    StreamReader reader =
                        new StreamReader(resp.GetResponseStream());
                    string test = reader.ReadToEnd();
                    xmlResult.Load(reader.ReadToEnd());
                }

                Connected = true;
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Connected = false;
                return false;
            }
        }

        private static string BuildGenericUrl(RequestType requestType)
        {
            //http://{URL}/rest/{TYPE}.view?u={USERNAME}&p={ENCODEDURL}&v={VERSION}.0&c={APPNAME}
            //TODO: add app version and app name to appconfig
            var theReturn = String.Format(@"http://{0}/rest/{1}.view?u={2}&p={3}&v={4}.0&c={5}", 
                                          CurrentUrl, requestType, CurrentUsername, CurrentPassword, "1.5", "SharpSub");

            return theReturn;
        }

        public static void ResetServerSettings()
        {
            CurrentUrl = String.Empty;
            CurrentUsername = String.Empty;
            CurrentPassword = String.Empty;
            Connected = false;

        }
    }

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
