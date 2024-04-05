using Microsoft.AspNetCore.Mvc;
using SpotifyWeb.Models;
using SpotifyWeb.Models.ViewModel;
using SpotifyWeb.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SpotifyWeb.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IArtistRepository _artistRepo;
        private readonly IAlbumRepository _albumRepo;

        public AlbumsController(IArtistRepository artistRepo, IAlbumRepository albumRepo)
        {
            _artistRepo = artistRepo;
            _albumRepo = albumRepo;
        }
        public IActionResult Index()
        {
            return View(new Album() { });
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<Artist> artistList = await _artistRepo.GetAllAsync(SD.ArtistAPIPath);
            AlbumsVM objVM = new AlbumsVM()
            {
                ArtistList = artistList.Select(i => new SelectListItem
                {
                    Text = i.FName + i.LName,
                    Value = i.Id.ToString()
                }),
                Album = new Album()
            };

            if (id==null)
            {
                    return View(objVM);
            }
            objVM.Album = await _albumRepo.GetAsync(SD.AlbumAPIPath, id.GetValueOrDefault());
            if (objVM.Album == null)
            {
                return NotFound();
            }
            return View(objVM);
        }

        public async Task<IActionResult> GetAllAlbums()
        {
            return Json(new { data = await _albumRepo.GetAllAsync(SD.AlbumAPIPath) });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(AlbumsVM obj)
        {
            if (ModelState.IsValid)
            {               
                if (obj.Album.Id==0)
                {
                    await _albumRepo.CreateAsync(SD.AlbumAPIPath, obj.Album);
                }
                else
                {
                    await _albumRepo.UpdateAsync(SD.AlbumAPIPath + obj.Album.Id, obj.Album);
                }
                return RedirectToAction(nameof(Index));
                
            }
            else
            {
                IEnumerable<Artist> artistList = await _artistRepo.GetAllAsync(SD.ArtistAPIPath);
                AlbumsVM objVM = new AlbumsVM()
                {
                    ArtistList = artistList.Select(i => new SelectListItem
                    {
                        Text = i.FName + i.LName,
                        Value = i.Id.ToString()
                    }),
                    Album = obj.Album
                };
                return View(objVM);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _albumRepo.DeleteAsync(SD.AlbumAPIPath, id);
            if (status)
            {
                return Json(new { success = true, message="Delete successful"});
            }
            return Json(new { success = false, message = "Delete Not successful" });

        }
    }
}
