using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SpotifyAPI.Models.DTOs
{
    public class UserDto
    {
        public int User_ID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)] // Example validation: Password must be between 6 and 100 characters long
        public string Password { get; set; }

        [Required]
        [EmailAddress] // Example validation: Email must be in email address format
        public string Email { get; set; }

        public DateTime Date_Of_Birth { get; set; }

        [DefaultValue("regular")] // Example: Default value for User_Type
        public string User_Type { get; set; } = "regular";
    }
}
