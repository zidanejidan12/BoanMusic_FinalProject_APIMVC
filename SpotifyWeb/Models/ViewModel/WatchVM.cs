using System.Collections.Generic;

namespace SpotifyWeb.Models.ViewModel
{
    public class WatchVM
    {
        public int SongId { get; set; }
        public int NextSongId { get; set; } // ID of the next song
        public List<int> SongQueue { get; set; } // Queue of song IDs
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public int DurationInSeconds { get; set; }
        public string ImageURL { get; set; }

        public static WatchVM FromSong(Song song, int nextSongId, List<int> songQueue)
        {
            return new WatchVM
            {
                SongId = song.Id,
                NextSongId = nextSongId,
                SongQueue = songQueue,
                Title = song.Title,
                ArtistName = $"{song.Album?.Artist?.FName} {song.Album?.Artist?.LName}",
                DurationInSeconds = song.RuntimeInSeconds,
                ImageURL = song.ImageSongURL
            };
        }
    }
}
