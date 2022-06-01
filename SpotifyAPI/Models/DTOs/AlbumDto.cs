using System;
using System.ComponentModel.DataAnnotations;
using static SpotifyAPI.Models.Album;
using static SpotifyAPI.Models.Song;

namespace SpotifyAPI.Models.DTOs
{
    public class AlbumDto
    {

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public GenreType Genre { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsExplicit { get; set; }

        [Required]
        public int ArtistId { get; set; }

        public Artist Artist { get; set; }

        [Required]
        public string ImageCoverURL { get; set; }
    }
}
