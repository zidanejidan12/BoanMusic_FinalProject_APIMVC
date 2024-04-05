using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SpotifyWeb.Models.ViewModel
{
    public class SongsVM
    {
        public IEnumerable<SelectListItem> AlbumList { get; set; }
        public Song Song { get; set; }
        public int NextSongId { get; set; } // ID of the next song
        public List<Song> SongQueue { get; set; } // Queue of song details
        public IEnumerable<Album> AlbumLists { get; set; }
    }
}
