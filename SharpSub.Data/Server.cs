using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.IO;
using System.Web;
using System.Xml;
using System.Diagnostics;
using NAudio.Wave;

namespace SharpSub
{
    public class Server
    {
        public static string CurrentUrl { get; set; }
        public static string CurrentUsername { get; set; }
        public static string CurrentPassword { get; set; }
        public static bool Connected { get; private set; }


        // returns true if the connection worked, else returns false
        public static bool TestConnection(string url, string username, string password)
        {
            try
            {
                CurrentUrl = url.Replace("http://", String.Empty);
                CurrentUsername = username;
                CurrentPassword = HttpUtility.UrlEncode(password);

                string sTmpUrl = BuildGenericUrl(RequestType.ping);
                
                HttpWebRequest req = WebRequest.Create(new Uri(sTmpUrl)) 
                                     as HttpWebRequest;

                XmlDocument xmlResult = new XmlDocument();

                if (req == null)
                    return false;

                using (HttpWebResponse resp = req.GetResponse()
                                                as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(resp.GetResponseStream());
                    var responseText = reader.ReadToEnd().Normalize();
                    xmlResult.LoadXml(responseText);
                }

                var status = xmlResult.GetElementsByTagName("subsonic-response")[0].Attributes["status"].Value;

                Connected = (status == "ok");
                return (status == "ok");

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
            //TODO: add app version and app name to appconfig
            var theReturn = String.Format(@"http://{0}/rest/{1}.view?u={2}&p={3}&v={4}.0&c={5}", 
                                          CurrentUrl, requestType, CurrentUsername, CurrentPassword, 
                                          ConfigurationManager.AppSettings["RestVersion"], 
                                          ConfigurationManager.AppSettings["AppName"]);

            return theReturn;
        }

        public static void ResetServerSettings()
        {
            CurrentUrl = String.Empty;
            CurrentUsername = String.Empty;
            CurrentPassword = String.Empty;
            Connected = false;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="songId">A string which uniquely identifies the file to stream. Obtained by calls to getMusicDirectory.</param>
        /// <param name="maxBitRate">(Since 1.2.0) If specified, the server will attempt to limit the bitrate to this value, in kilobits per second. If set to zero, no limit is imposed. Legal values are: 0, 32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256 and 320.</param>
        /// <returns></returns>
        public static Stream StreamSong(string songId, int maxBitRate = 0)
        {
            try
            {
                if (!Connected)
                    return null;

                string requestUrl = String.Format("{0}&maxBitRate={1}", BuildGenericUrl(RequestType.stream), maxBitRate);

                var request = WebRequest.Create(requestUrl);
                var response = request.GetResponse();

                var dataStream = response.GetResponseStream();
                return dataStream;
            }
            catch (Exception)
            {
                return null;
            }
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
