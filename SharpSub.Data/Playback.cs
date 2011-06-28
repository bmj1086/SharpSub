using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMPLib;

namespace SharpSub.Data
{
    [Obsolete("No longer necessary. Use SongPlayer")]
    public static class Playback
    {
        //internal static List<Song> CurrentPlaylist = new List<Song>();
        //private static List<string> playedSongs = new List<string>();
        //private static IList<Song> AvailableSongs
        //{
        //    get { return CurrentPlaylist.Where(s => !playedSongs.Contains(s.ID)).ToList(); }
        //}
        //public static int Volume
        //{
        //    get { return player.Volume; }
        //    set { player.Volume = value; }
        //}
        //public static int CurrentSongIndex = 0;
        //public static bool RandomPlayback { get; set; }
        //public static bool Mute
        //{
        //    get { return player.Mute; }
        //    set { player.Mute = value; }
        //}
        //public static Song CurrentSong;
        //private static SongPlayer player;
        //private static readonly Random random = new Random();

        //private static void Play(Song song)
        //{
        //    CurrentSong = song;
        //    CurrentPlaylist.Add(song);

        //    if (player != null)
        //        player.Dispose();
            
        //    player = new SongPlayer(CurrentSong);
        //    player.Play();
        //}

        ///// <summary>
        ///// Use to play a song already in the current playlist
        ///// </summary>
        ///// <param name="playlistIndex">The index position in the current playlist of the song to play</param>
        //public static void Play(int playlistIndex = 0)
        //{
        //    if (player != null)
        //        player.Dispose();

        //    CurrentSong = CurrentPlaylist[playlistIndex];
        //    player = new SongPlayer(CurrentSong);
        //    player.Play();
        //}

        ///// <summary>
        ///// Create a playlist of songs and start playback
        ///// </summary>
        ///// <param name="songList"></param>
        ///// <param name="randomPlayback"></param>
        ///// <param name="repeat"></param>
        //public static void Play(IList<Song> songList, bool randomPlayback, Repeat repeat)
        //{
        //    CurrentPlaylist.AddRange(songList);
        //    RandomPlayback = randomPlayback;
        //    CurrentSongIndex = 0;

        //    if (RandomPlayback)
        //        CurrentSongIndex = random.Next(CurrentPlaylist.Count);

        //    Play(CurrentSongIndex);

        //}

        ///// <summary>
        ///// Pause the currently playing song.
        ///// </summary>
        //public static void Pause()
        //{
        //    if (player != null)
        //        player.Pause();

        //}

        ///// <summary>
        ///// Pause the currently playing song.
        ///// </summary>
        //public static void Unpause()
        //{
        //    if (player.State == SongPlayer.PlaybackState.Paused)
        //        player.Play();
        //}

        ///// <summary>
        ///// Add song(s) to the current playlist
        ///// </summary>
        ///// <param name="songList">The list of songs to add to the playlist.</param>
        //public static void Add(IList<Song> songList)
        //{
        //    CurrentPlaylist.AddRange(songList);
        //}

        //public static void Stop()
        //{
        //    player.Stop();
        //    player.Dispose();
        //}

        //public static void ClearPlaylist()
        //{
        //    CurrentPlaylist.Clear();
        //}

        //internal static void SongCompleted()
        //{
        //    playedSongs.Add(CurrentSong.ID);
        //    player.Dispose();

        //    Song nextSong = GetNextSong();

        //    if (nextSong != null)
        //    {
        //        player = new SongPlayer(nextSong);
        //        player.Play();
        //    }
        //    else Stop();
        //}

        //private static Song GetNextSong()
        //{
        //    if (RandomPlayback)
        //        return AvailableSongs[random.Next(AvailableSongs.Count)];

        //    return CurrentSongIndex++ > CurrentPlaylist.Count ? null : CurrentPlaylist[CurrentSongIndex];
        //}

        //public enum Repeat
        //{
        //    None, One, All
        //}

        //public static double Position
        //{
        //    get { return player == null ? 0 : player.Position; }
        //    set { if (player != null) player.Position = value; }
        //}

        //public static bool IsPlaying
        //{
        //    get { return player == null ? false : player.State == SongPlayer.PlaybackState.Playing; }
        //}

        //public static string PositionString(string format)
        //{
        //    return player == null ? DateTime.MinValue.ToString(format) : player.PositionString(format);
        //}

        //public static string DurationString(string format)
        //{
        //    return player == null ? DateTime.MinValue.ToString(format) : player.DurationString(format);
        //}

        //public static void Skip()
        //{
        //    if (player != null)
        //        player.Dispose();

        //    CurrentSong = GetNextSong();
        //    player = new SongPlayer(CurrentSong);
        //    player.Play();
        //}
    }
}
