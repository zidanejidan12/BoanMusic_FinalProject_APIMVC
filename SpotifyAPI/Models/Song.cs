using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyAPI.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }       
        public enum GenreType { Rock, Pop, HipHop, Jazz, Electronic, Techno}
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
        [ForeignKey("AlbumId")]
        public Album Album { get; set; }

        [Required]
        public string ImageSongURL { get; set; }

    }
}
