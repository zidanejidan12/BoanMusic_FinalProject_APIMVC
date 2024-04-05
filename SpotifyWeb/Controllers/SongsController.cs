using Microsoft.AspNetCore.Mvc;
using SpotifyWeb.Models;
using SpotifyWeb.Models.ViewModel;
using SpotifyWeb.Repository.IRepository;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SpotifyWeb.Controllers
{
    public class SongsController : Controller
    {
        //private readonly IArtistRepository _artistRepo;
        private readonly IAlbumRepository _albumRepo;
        private readonly ISongRepository _songRepo;

        public SongsController(IArtistRepository artistRepo, IAlbumRepository albumRepo, ISongRepository songRepo)
        {
            //_artistRepo = artistRepo;
            _albumRepo = albumRepo;
            _songRepo = songRepo;
        }
        public IActionResult Index()
        {
            return View(new Song() { });
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<Album> albumList = await _albumRepo.GetAllAsync(SD.AlbumAPIPath);
            SongsVM objVM = new SongsVM()
            {
                AlbumList = albumList.Select(i => new SelectListItem
                {
                    Text = i.Title,
                    Value = i.Id.ToString()
                }),
                Song = new Song()
            };

            if (id==null)
            {
                    return View(objVM);
            }
            objVM.Song = await _songRepo.GetAsync(SD.SongAPIPath, id.GetValueOrDefault());
            if (objVM.Song == null)
            {
                return NotFound();
            }
            return View(objVM);
        }

        public async Task<IActionResult> GetAllSongs()
        {
            return Json(new { data = await _songRepo.GetAllAsync(SD.SongAPIPath) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(SongsVM obj)
        {
            if (ModelState.IsValid)
            {
                var file = HttpContext.Request.Form.Files.FirstOrDefault();
                if (file != null && file.Length > 0)
                {
                    // Get the file name
                    var fileName = Path.GetFileName(file.FileName);

                    // Define the directory path where the file will be saved
                    var directoryPath = @"D:\Users\bsi80210\source\repos\Covers\WebMusicPlayer-SpotifyReplica\SpotifyAPI\wwwroot\music";

                    // Check if the directory exists, if not, create it
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    // Define the path where the file will be saved
                    var filePath = Path.Combine(directoryPath, fileName);

                    // Save the file to the server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Set the file name to the SongMP3 property
                    obj.Song.SongMP3 = fileName;
                }
                else
                {
                    var objFromDb = await _songRepo.GetAsync(SD.SongAPIPath, obj.Song.Id);
                    obj.Song.SongMP3 = objFromDb.SongMP3;
                }

                if (obj.Song.Id == 0)
                {
                    await _songRepo.CreateAsync(SD.SongAPIPath, obj.Song);
                }
                else
                {
                    await _songRepo.UpdateAsync(SD.SongAPIPath + obj.Song.Id, obj.Song);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                IEnumerable<Album> albumList = await _albumRepo.GetAllAsync(SD.AlbumAPIPath);
                SongsVM objVM = new SongsVM()
                {
                    AlbumList = albumList.Select(i => new SelectListItem
                    {
                        Text = i.Title,
                        Value = i.Id.ToString()
                    }),
                    Song = obj.Song
                };
                return View(objVM);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _songRepo.DeleteAsync(SD.SongAPIPath, id);
            if (status)
            {
                return Json(new { success = true, message="Delete successful"});
            }
            return Json(new { success = false, message = "Delete Not successful" });

        }

        public async Task<IActionResult> Hide(int id)
        {
            var song = await _songRepo.GetAsync(SD.SongAPIPath, id);
            if (song.IsHidden == true)
            {
                song.IsHidden = false;
            }
            else if(song.IsHidden == false)
            {
                song.IsHidden = true;
            }
            await _songRepo.UpdateAsync(SD.SongAPIPath, song);

            return RedirectToAction(nameof(Index));

        }
    }
}
