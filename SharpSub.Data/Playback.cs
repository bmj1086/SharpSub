using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpSub.Data
{
    public static class Playback
    {
        private static List<Song> currentPlaylist;
        private static List<int> playedSongs;
        private static int CurrentSongIndex;
        public static bool RandomPlayback { get; set; }
        private static Song currentSong;
        private static SongPlayer player;
        private static readonly Random random = new Random();

        private static void StartNew(Song song)
        {
            currentSong = song;

            if (player != null)
                player.Dispose();

            player = new SongPlayer(currentSong);
            player.Play();
        }

        /// <summary>
        /// Use to play a song already in the current playlist
        /// </summary>
        /// <param name="playlistIndex">The index position in the current playlist of the song to play</param>
        /// <param name="restart">If the song is currently playing and you want to restart it set this to true</param>
        public static void Play(int? playlistIndex = null)
        {
            if (player != null)
                player.Dispose();


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
        /// <param name="randomPlay">Set to true if the user wants random playback.</param>
        public static void Add(IEnumerable<Song> songList, bool startPlayback, bool randomPlay = false)
        {
            foreach (Song song in songList)
            {
                currentPlaylist.Add(song);
            }

            RandomPlayback = randomPlay;
            CurrentSongIndex = 0;

            if (RandomPlayback)
                CurrentSongIndex = random.Next(currentPlaylist.Count);

            if (startPlayback)
                Play(CurrentSongIndex);
        }

        public static void Stop()
        {
            currentPlaylist.Clear();
            //TODO: Stop playback
        }

        public static IEnumerable<Song> GetPlaylist()
        {
            return currentPlaylist.ToList();
        }

        internal static void SongCompleted()
        {
            playedSongs.Add(CurrentSongIndex); //TODO: Add property protected set.
        }
    }
}
