using System;
using System.ComponentModel.DataAnnotations;
using static SpotifyWeb.Models.Album;

namespace SpotifyWeb.Models
{
    public class Song
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public GenreType Genre { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public int RuntimeInSeconds { get; set; }
        [Required]
        public bool IsExplicit { get; set; }
        [Required]
        [StringLength(2048)]
        public string SongUrl { get; set; }

        [Required]
        public int AlbumId { get; set; }

        public Album Album { get; set; }
    }
}
