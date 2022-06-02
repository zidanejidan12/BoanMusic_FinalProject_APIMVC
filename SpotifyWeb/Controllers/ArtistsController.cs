using Microsoft.AspNetCore.Mvc;
using SpotifyWeb.Models;
using SpotifyWeb.Repository.IRepository;
using System.IO;
using System.Threading.Tasks;

namespace SpotifyWeb.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly IArtistRepository _artistRepo;

        public ArtistsController(IArtistRepository artistRepo)
        {
            _artistRepo = artistRepo;
        }
        public IActionResult Index()
        {
            return View(new Artist() { });
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Artist obj = new Artist();
            if (id==null)
            {
                    return View(obj);
            }
            obj = await _artistRepo.GetAsync(SD.ArtistAPIPath, id.GetValueOrDefault());
            if (obj==null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public async Task<IActionResult> GetAllArtists()
        {
            return Json(new { data = await _artistRepo.GetAllAsync(SD.ArtistAPIPath) });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Artist obj)
        {
            if (ModelState.IsValid)
            {
                //var files = HttpContext.Request.Form.Files;
                //if (files.Count>0)
                //{
                //    byte[] p1 = null;
                //    using(var fs1 = files[0].OpenReadStream())
                //    {
                //        using var ms1 = new MemoryStream();
                //        fs1.CopyTo(ms1);
                //        p1=ms1.ToArray();
                //    }
                //    obj.Picture = p1;
                //}
                //else
                //{
                //    var objFromDb = await _artistRepo.GetAsync(SD.ArtistAPIPath, obj.Id);
                //    obj.Picture = objFromDb.Picture;
                //}
                if (obj.Id==0)
                {
                    await _artistRepo.CreateAsync(SD.ArtistAPIPath, obj);
                }
                else
                {
                    await _artistRepo.UpdateAsync(SD.ArtistAPIPath+obj.Id, obj);
                }
                return RedirectToAction(nameof(Index));
                
            }
            else
            {
                return View(obj);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _artistRepo.DeleteAsync(SD.ArtistAPIPath, id);
            if (status)
            {
                return Json(new { success = true, message="Delete successful"});
            }
            return Json(new { success = false, message = "Delete Not successful" });

        }

        //public async Task<IActionResult> Hide(int id)
        //{
        //    var artist = await _artistRepo.GetAsync(SD.ArtistAPIPath, id);

        //    if (artist.)
        //    {

        //    }
        //}
    }
}
