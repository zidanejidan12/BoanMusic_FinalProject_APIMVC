using System;
using System.ComponentModel.DataAnnotations;


namespace SpotifyWeb.Models
{
    public class Album
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public enum GenreType { Rock, Pop, HipHop, Jazz, Electronic, Techno }
        [Required]
        public GenreType Genre { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsExplicit { get; set; }

        [Required]
        public int ArtistId { get; set; }

        public Artist Artist { get; set; }
    }
}
