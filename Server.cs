using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

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
        internal static bool TextConnection(string url, string username, string password)
        {
            CurrentUrl = url;
            CurrentUsername = username;
            CurrentPassword = password;
            
            try
            {
                HttpWebRequest req = WebRequest.Create(BuildUrl(RequestType.ping, url:url, username:username, password:password)) 
                                     as HttpWebRequest;

                XmlDocument xmlResult = new XmlDocument();

                if (req == null)
                    return false;

                using (HttpWebResponse resp = req.GetResponse()
                                                as HttpWebResponse)
                {
                    StreamReader reader =
                        new StreamReader(resp.GetResponseStream());
                    xmlResult.Load(reader.ReadToEnd());
                }

                Connected = true;
                return true;
            }
            catch (Exception)
            {
                Connected = false;
                return false;
            }
        }

        private static Uri BuildUrl(RequestType requestType, string url, string username, string password)
        {
            if (requestType == RequestType.ping)
            {
                
            }
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
