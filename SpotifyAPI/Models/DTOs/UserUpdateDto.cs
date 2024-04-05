using System.ComponentModel.DataAnnotations;
using static SpotifyAPI.Models.User;
using System;
using System.ComponentModel;

namespace SpotifyAPI.Models.DTOs
{
public class UserUpdateDto
    {
        [Required]
        public int User_ID { get; set; }

        [Required]
        public string Username { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        public DateTime Date_Of_Birth { get; set; }
        
        [Required]
        public string Password { get; set; }

        // Specify the default value using data annotations
        [DefaultValue("regular")]
        public string User_Type { get; set; }
    }
}