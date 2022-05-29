using System;
using System.ComponentModel.DataAnnotations;

namespace SpotifyAPI.Models.DTOs
{
    public class SongDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        [Required]
        public int RuntimeInSeconds { get; set; }       
        public bool IsExplicit { get; set; }
        [Required]
        public string SongUrl { get; set; }
    }
}
