using System.Collections.Generic;

namespace SpotifyWeb.Models.ViewModel
{
    public class IndexVM
    {
        public IEnumerable<Song> SongList { get; set; }
        public IEnumerable<Album> AlbumList { get; set; }
    }
}
