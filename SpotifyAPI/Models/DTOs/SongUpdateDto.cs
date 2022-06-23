using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SpotifyAPI.Models.Album;
using static SpotifyAPI.Models.Song;

namespace SpotifyAPI.Models.DTOs
{
    public class SongUpdateDto
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
        public int AlbumId { get; set; }

        [Required]
        public string ImageSongURL { get; set; }

        public bool IsHidden { get; set; }

        public string SongMP3 { get; set; }
    }
}
