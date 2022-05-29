using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Models;
using SpotifyAPI.Models.DTOs;
using SpotifyAPI.Repository.IRepository;
using System.Collections.Generic;

namespace SpotifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : Controller
    {
        private ISongRepository _songRepo;
        private readonly IMapper _mapper;

        public SongsController(ISongRepository songRepo, IMapper mapper)
        {
            _songRepo = songRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetSongs()
        {
            var songList = _songRepo.GetSongs();

            var songDto = new List<SongDto>();
            foreach (var song in songList)
            {
                songDto.Add(_mapper.Map<SongDto>(song));
            }
            return Ok(songDto);
        }

        [HttpGet("{songId:int}", Name = "GetSong")]
        public IActionResult GetSong(int songId)
        {
            var song = _songRepo.GetSong(songId);

            if (song == null)
            {
                return NotFound();
            }

            var songDto = new SongDto();
            songDto = _mapper.Map<SongDto>(song);

            return Ok(songDto);
        }

        [HttpPost]
        public IActionResult CreateSong([FromBody] SongDto songDto)
        {
            if (songDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_songRepo.SongExists(songDto.Title))
            {
                ModelState.AddModelError("","Song already Exists!");
                return StatusCode(404, ModelState);
            }

            var songObj = _mapper.Map<Song>(songDto);

            if (!_songRepo.CreateSong(songObj))
            {
                ModelState.AddModelError("", $"Couldnt save the song {songObj.Title}");
                return StatusCode(500,ModelState);
            }

            return CreatedAtRoute("GetSong", new { songId = songObj.Id}, songObj);
        }
    }
}
