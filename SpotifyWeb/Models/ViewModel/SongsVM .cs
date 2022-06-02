using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SpotifyWeb.Models.ViewModel
{
    public class SongsVM
    {
        public IEnumerable<SelectListItem> AlbumList { get; set; }
        public Song Song { get; set; }
    }
}
