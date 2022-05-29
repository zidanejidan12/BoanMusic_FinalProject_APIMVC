using System;
using System.ComponentModel.DataAnnotations;

namespace SpotifyAPI.Models.DTOs
{
    public class SongDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int RuntimeInSeconds { get; set; }       
        public bool IsExplicit { get; set; }
        public string SongUrl { get; set; }
    }
}
