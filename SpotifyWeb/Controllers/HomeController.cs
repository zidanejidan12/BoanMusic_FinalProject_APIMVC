using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpotifyWeb.Models;
using SpotifyWeb.Models.ViewModel;
using SpotifyWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()
        {
            IndexVM listOfSongInfo = new IndexVM()
            {
                SongList = await _songRepo.GetAllAsync(SD.SongAPIPath),
                AlbumList = await _albumRepo.GetAllAsync(SD.AlbumAPIPath)
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
