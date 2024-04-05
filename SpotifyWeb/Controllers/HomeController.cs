using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpotifyWeb.Models;
using SpotifyWeb.Models.ViewModel;
using SpotifyWeb.Repository.IRepository;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static SpotifyWeb.Models.ViewModel.IndexVM;

namespace SpotifyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAlbumRepository _albumRepo;
        private readonly ISongRepository _songRepo;

        public HomeController(ILogger<HomeController> logger, IAlbumRepository albumRepo, ISongRepository songRepo)
        {
            _logger = logger;
            _albumRepo = albumRepo;
            _songRepo = songRepo;
        }

        public async Task<IActionResult> Index(string searchTerm, GenreSorting sorting)
        {
            IndexVM listOfSongInfo = new IndexVM()
            {
                SongList = await _songRepo.GetAllAsync(SD.SongAPIPath),
                AlbumList = await _albumRepo.GetAllAsync(SD.AlbumAPIPath)
            };

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower(); // Convert search term to lowercase for case-insensitive search
                listOfSongInfo.SongList = listOfSongInfo.SongList.Where(x =>
                    x.Title.ToLower().Contains(searchTerm) || // Search in song title
                    x.Album.Title.ToLower().Contains(searchTerm) || // Search in album title
                    x.Album.Artist.FName.ToLower().Contains(searchTerm)); // Search in artist's first name
            }

            listOfSongInfo.SongList = sorting switch
            {
                GenreSorting.Title => listOfSongInfo.SongList.OrderBy(m => m.Title),
                GenreSorting.Year => listOfSongInfo.SongList.OrderByDescending(m => m.ReleaseDate),
                GenreSorting.Runtime => listOfSongInfo.SongList.OrderBy(m => m.RuntimeInSeconds)
            };

            return View(listOfSongInfo);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
