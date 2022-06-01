using System;
using System.ComponentModel.DataAnnotations;

namespace SpotifyWeb.Models
{
    public class Artist
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; }
        [Display(Name = "Last Name")]
        public string LName { get; set; }
        public int ActiveYears { get; set; }
        public string Description { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public string ImageURL { get; set; }
    }
}
