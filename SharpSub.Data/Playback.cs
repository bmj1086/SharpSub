//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace SharpSub.Data
//{
//    public static class Playback
//    {
//        private static IList<Song> _currentPlaylist;
//        private static IList<int> _playedSongs;
//        private static int CurrentSongIndex;
//        public static bool RandomPlay { get; set; }
//        private static Song _currentSong;
//        private static Mp3Player mp3player;
//        private static Random random = new Random();

//        private static void StartNew(Song song)
//        {
//            _currentSong = song;

//            if (mp3player != null)
//                mp3player.StopAndDispose();

//            mp3player = new Mp3Player(_currentSong);
//            mp3player.Play();
//        }

//        /// <summary>
//        /// Use to play a song already in the current playlist
//        /// </summary>
//        /// <param name="playlistIndex">The index position in the current playlist of the song to play</param>
//        /// <param name="restart">If the song is currently playing and you want to restart it set this to true</param>
//        public static void Play(int? playlistIndex = null)
//        {
//            if (mp3player != null)
//                mp3player.StopAndDispose();


//        }

//        /// <summary>
//        /// Pause the currently playing song.
//        /// </summary>
//        public static void Pause()
//        {
            
//        }

//        /// <summary>
//        /// Add song(s) to the current playlist
//        /// </summary>
//        /// <param name="songList">The list of songs to add to the playlist.</param>
//        /// <param name="startPlayback">Set to true if you want the playlist to start playing immediately.</param>
//        /// <param name="randomPlay">Set to true if the user wants random playback.</param>
//        public static void Add(IEnumerable<Song> songList, bool startPlayback, bool randomPlay = false)
//        {
//            foreach (Song song in songList)
//            {
//                _currentPlaylist.Add(song);
//            }

//            RandomPlay = randomPlay;
//            CurrentSongIndex = 0;

//            if (RandomPlay)
//                CurrentSongIndex = random.Next(_currentPlaylist.Count);
            
//            if (startPlayback)
//                Play(CurrentSongIndex);
//        }

//        //private static void Randomize()
//        //{
//        //    Random r = new Random();
//        //    var playListList = _currentPlaylist.ToList();
//        //    var tempPlaylist = new Queue<Song>();

//        //    while (playListList.Count > 0)
//        //    {
//        //        int randomInt = r.Next(_currentPlaylist.Count);
//        //        tempPlaylist.Enqueue(playListList[randomInt]);
//        //        playListList.RemoveAt(randomInt);
//        //    }

//        //    _currentPlaylist = tempPlaylist;
//        //}

//        public static void Stop()
//        {
//            _currentPlaylist.Clear();
//            //TODO: Stop playback
//        }

//        public static IList<Song> GetPlaylist()
//        {
//            return _currentPlaylist.ToList();
//        }

//        internal static void SongCompleted()
//        {
//            _playedSongs.Add(CurrentSongIndex); //TODO: Add property protected set.
//        }
//    }
//}
