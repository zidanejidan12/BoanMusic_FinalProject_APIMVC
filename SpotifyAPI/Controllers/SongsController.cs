using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Models;
using SpotifyAPI.Models.DTOs;
using SpotifyAPI.Repository.IRepository;
using System.Collections.Generic;

namespace SpotifyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/songs")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "SpotifyOpenAPISpecSongs")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class SongsController : Controller
    {
        private readonly ISongRepository _songRepo;
        private readonly IMapper _mapper;

        public SongsController(ISongRepository songRepo, IMapper mapper)
        {
            _songRepo = songRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all the songs.
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<SongDto>))]
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

        /// <summary>
        /// Get a song by Id.
        /// </summary>
        /// <param name="songId"> The Id of the song </param>
        /// <returns></returns>

        [HttpGet("{songId:int}", Name = "GetSong")]
        [ProducesResponseType(200, Type = typeof(SongDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetSong(int songId)
        {
            var song = _songRepo.GetSong(songId);

            if (song == null)
            {
                return NotFound();
            }

            var songDto = new SongDto();
            songDto = _mapper.Map<SongDto>(song);
            //var songDto = new SongDto()
            //{
            //    Title = song.Title,
            //    Id = song.Id,
            //    ReleaseDate = song.ReleaseDate,
            //}
            return Ok(songDto);
        }

        /// <summary>
        /// Getting all the songs in an album
        /// </summary>
        /// <param name="albumId">Id of an album</param>
        /// <returns></returns>
        [HttpGet("{albumId:int}", Name = "GetSongInAlbum")]
        [ProducesResponseType(200, Type = typeof(SongDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetSongInAlbum(int albumId)
        {
            var songList = _songRepo.GetAlbumSongs(albumId);

            if (songList == null)
            {
                return NotFound();
            }
            var songDto = new List<SongDto>();
            foreach (var song in songList)
            {
                songDto.Add(_mapper.Map<SongDto>(song));
            }       
            return Ok(songDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(SongDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult CreateSong([FromBody] SongCreateDto songDto)
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

        [HttpPatch("{songId:int}", Name = "UpdateSong")]
        [ProducesResponseType(204)]       
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateSong(int songId, [FromBody] SongUpdateDto songDto)
        {
            if (songDto == null || songId != songDto.Id)
            {
                return BadRequest(ModelState);
            }

            var songObj = _mapper.Map<Song>(songDto);

            if (!_songRepo.UpdateSong(songObj))
            {
                ModelState.AddModelError("", $"Couldnt update the song {songObj.Title}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{songId:int}", Name = "DeleteSong")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteSong(int songId)
        {
            if (!_songRepo.SongExists(songId))
            {
                return NotFound();
            }

            var songObj = _songRepo.GetSong(songId);

            if (!_songRepo.DeleteSong(songObj))
            {
                ModelState.AddModelError("", $"Couldnt delete the song {songObj.Title}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
