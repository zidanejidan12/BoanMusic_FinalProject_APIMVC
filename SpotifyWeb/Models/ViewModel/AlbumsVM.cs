using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SpotifyWeb.Models.ViewModel
{
    public class AlbumsVM
    {
        public IEnumerable<SelectListItem> ArtistList { get; set; }
        public Album Album { get; set; }
    }
}
