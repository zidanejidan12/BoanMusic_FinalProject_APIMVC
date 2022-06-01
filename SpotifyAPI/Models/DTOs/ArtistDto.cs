using System;
using System.ComponentModel.DataAnnotations;

namespace SpotifyAPI.Models.DTOs
{
    public class ArtistDto
    {
        public int Id { get; set; }
        [Required]
        public string FName { get; set; }
        public string LName { get; set; }
        public int ActiveYears { get; set; }
        public string Description { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public string ImageURL { get; set; }

    }
}
