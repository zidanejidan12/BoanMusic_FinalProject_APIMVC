using System.ComponentModel.DataAnnotations;
using static SpotifyAPI.Models.User;

namespace SpotifyAPI.Models.DTOs
{
    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
