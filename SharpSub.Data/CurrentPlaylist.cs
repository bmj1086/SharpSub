using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpSub.Data
{
    public static class CurrentPlaylist
    {
        private static Queue<Song> _currentPlaylist;
        public static bool RandomPlayback { get; set; }
        private static IList<int> _playedSongs;
        private static int CurrentSong;

        private static void Play(Song song)
        {
            var songStream = SubsonicRequest.GetSongStream(song);
        }

        /// <summary>
        /// Use to play a song already in the current playlist
        /// </summary>
        /// <param name="positionInPlaylist">The index position in the current playlist of the song to play</param>
        /// <param name="restart">If the song is currently playing and you want to restart it set this to true</param>
        public static void Play(int? positionInPlaylist = null, bool restart = false)
        {
            
        }

        /// <summary>
        /// Pause the currently playing song.
        /// </summary>
        public static void Pause()
        {
            
        }

        /// <summary>
        /// Add song(s) to the current playlist
        /// </summary>
        /// <param name="songList">The list of songs to add to the playlist.</param>
        /// <param name="startPlayback">Set to true if you want the playlist to start playing immediately.</param>
        /// <param name="random">Set to true if the user wants random playback.</param>
        public static void Add(IEnumerable<Song> songList, bool startPlayback, bool random = false)
        {
            foreach (Song song in songList)
            {
                _currentPlaylist.Enqueue(song);
            }

            RandomPlayback = random;

            if (startPlayback)
                Play(_currentPlaylist.First());
        }

        //private static void Randomize()
        //{
        //    Random r = new Random();
        //    var playListList = _currentPlaylist.ToList();
        //    var tempPlaylist = new Queue<Song>();

        //    while (playListList.Count > 0)
        //    {
        //        int randomInt = r.Next(_currentPlaylist.Count);
        //        tempPlaylist.Enqueue(playListList[randomInt]);
        //        playListList.RemoveAt(randomInt);
        //    }

        //    _currentPlaylist = tempPlaylist;
        //}

        public static void Stop()
        {
            _currentPlaylist.Clear();
            //TODO: Stop playback
        }

        public static IList<Song> GetPlaylist()
        {
            return _currentPlaylist.ToList();
        }

        internal static void SongCompleted()
        {
            _playedSongs.Add(CurrentSong); //TODO: Add property protected set.
        }
    }
}
