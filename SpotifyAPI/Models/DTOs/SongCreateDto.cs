using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SpotifyAPI.Models.Album;
using static SpotifyAPI.Models.Song;

namespace SpotifyAPI.Models.DTOs
{
    public class SongCreateDto
    {
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
    }
}
