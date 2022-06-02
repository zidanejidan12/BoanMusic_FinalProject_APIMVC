using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpotifyWeb.Models.ViewModel
{
    public class IndexVM
    {
        public IEnumerable<Song> SongList { get; set; }
        public IEnumerable<Album> AlbumList { get; set; }

        public IEnumerable<Artist> ArtistList { get; set; }
        public bool IsHidden { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public string Genre { get; set; }
        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public enum GenreSorting { Title,Year,Runtime }
        [Display(Name = "Sort By")]
        public GenreSorting Sorting { get; set; }
    }
}
