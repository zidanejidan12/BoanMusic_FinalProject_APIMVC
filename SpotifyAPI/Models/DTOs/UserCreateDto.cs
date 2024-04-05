using System;
using System.ComponentModel.DataAnnotations;
using static SpotifyAPI.Models.User;

namespace SpotifyAPI.Models.DTOs
{
    public class UserCreateDto
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        public DateTime Date_Of_Birth { get; set; }
    
        public string User_Type { get; set; } = "regular";
        
        [Required]
        public string Password { get; set; }
    }
}
