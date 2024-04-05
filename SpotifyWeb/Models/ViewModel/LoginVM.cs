using System.ComponentModel.DataAnnotations;

namespace SpotifyWeb.Models.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string User_Type { get; set; }

        public string Token { get; set; }
    }
}
