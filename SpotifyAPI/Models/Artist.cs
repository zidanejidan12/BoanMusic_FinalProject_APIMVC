using System;
using System.ComponentModel.DataAnnotations;

namespace SpotifyAPI.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FName { get; set; }
        public string LName { get; set; }
        public int ActiveYears  { get; set; }
        public string Description { get; set; }
        public DateTime BirthDate { get; set; }
        public string ImageURL { get; set; }
        public bool IsActive { get; set; }

    }
}
