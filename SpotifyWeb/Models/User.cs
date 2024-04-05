using System.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace SpotifyWeb.Models
{
    public class User
    {
        [Key] // Mark User_ID as the primary key
        public int User_ID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required] // Mark Email as required
        public string Email { get; set; }

        public DateTime Date_of_Birth { get; set; }

        // Specify the default value using data annotations
        [DefaultValue("regular")]
        public string User_Type { get; set; }

        public string Password { get; set; }
    }
}
