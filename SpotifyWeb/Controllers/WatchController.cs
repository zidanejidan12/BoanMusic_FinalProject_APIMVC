using Microsoft.AspNetCore.Mvc;
using SpotifyWeb.Models;
using SpotifyWeb.Models.ViewModel;
using SpotifyWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SpotifyWeb.Controllers
{
    public class WatchController : Controller
    {
        private readonly IAlbumRepository _albumRepo;
        private readonly ISongRepository _songRepo;

        public WatchController(IAlbumRepository albumRepo, ISongRepository songRepo)
        {
            _albumRepo = albumRepo;
            _songRepo = songRepo;
        }

        public IActionResult Index()
        {
            return View(new Song());
        }

        public async Task<IActionResult> Show(int? id)
        {
            // Fetch the current song
            Song currentSong = null;
            if (id.HasValue)
            {
                currentSong = await _songRepo.GetAsync(SD.SongAPIPath, id.Value);
                if (currentSong == null)
                {
                    return NotFound();
                }
            }

            // Fetch a random song ID excluding the current song
            int nextSongId = await _songRepo.GetRandomSongIdAsync(id.GetValueOrDefault());

            // Fetch the next song
            Song nextSong = await _songRepo.GetAsync(SD.SongAPIPath, nextSongId);

            // Fetch all songs to populate the song queue
            IEnumerable<Song> songs = await _songRepo.GetAllAsync(SD.SongAPIPath);

            // Create a list to store the song queue
            List<Song> songQueue = new List<Song>();

            // Add the current song to the song queue
            if (currentSong != null)
            {
                songQueue.Add(currentSong);
            }

            // Add the next song to the song queue
            if (nextSong != null)
            {
                songQueue.Add(nextSong);
            }

            // Add all other songs to the song queue
            if (songs != null)
            {
                songQueue.AddRange(songs.Where(song => song.Id != nextSongId && song.Id != currentSong?.Id));
            }

            // Create the view model
            SongsVM objVM = new SongsVM()
            {
                AlbumList = (await _albumRepo.GetAllAsync(SD.AlbumAPIPath))
                    .Select(album => new SelectListItem
                    {
                        Text = album.Title,
                        Value = album.Id.ToString()
                    }),
                Song = currentSong,
                NextSongId = nextSongId,
                SongQueue = songQueue
            };

            return View(objVM);
        }

        // Other actions for managing the watch feature can be added here
    }
}
