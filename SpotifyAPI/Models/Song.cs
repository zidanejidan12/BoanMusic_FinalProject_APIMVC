using System;
using System.ComponentModel.DataAnnotations;

namespace SpotifyAPI.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(25)]
        public string Genre { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public int RuntimeInSeconds { get; set; }       
        [Required]
        public bool IsExplicit { get; set; }
        [Required]
        [StringLength(2048)]
        public string SongUrl { get; set; }


    }
}
