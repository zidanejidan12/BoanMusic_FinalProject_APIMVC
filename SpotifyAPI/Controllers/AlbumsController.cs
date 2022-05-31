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
    [ApiExplorerSettings(GroupName = "SpotifyOpenAPISpecAlbums")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class AlbumsController : Controller
    {
        private readonly IAlbumRepository _albumRepo;
        private readonly IMapper _mapper;

        public AlbumsController(IAlbumRepository albumRepo, IMapper mapper)
        {
            _albumRepo = albumRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all the albums.
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<AlbumDto>))]
        public IActionResult GetAlbums()
        {
            var albumList = _albumRepo.GetAlbums();

            var albumDto = new List<AlbumDto>();
            foreach (var album in albumList)
            {
                albumDto.Add(_mapper.Map<AlbumDto>(album));
            }
            return Ok(albumDto);
        }

        /// <summary>
        /// Get a album by Id.
        /// </summary>
        /// <param name="albumId"> The Id of the album </param>
        /// <returns></returns>

        [HttpGet("{albumId:int}", Name = "GetAlbum")]
        [ProducesResponseType(200, Type = typeof(AlbumDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetAlbum(int albumId)
        {
            var album = _albumRepo.GetAlbum(albumId);

            if (album == null)
            {
                return NotFound();
            }

            var albumDto = new AlbumDto();
            albumDto = _mapper.Map<AlbumDto>(album);
            return Ok(albumDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(AlbumDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult CreateAlbum([FromBody] AlbumCreateDto albumDto)
        {
            if (albumDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_albumRepo.AlbumExists(albumDto.Title))
            {
                ModelState.AddModelError("","Album already Exists!");
                return StatusCode(404, ModelState);
            }

            var albumObj = _mapper.Map<Album>(albumDto);

            if (!_albumRepo.CreateAlbum(albumObj))
            {
                ModelState.AddModelError("", $"Couldnt save the album {albumObj.Title}");
                return StatusCode(500,ModelState);
            }

            return CreatedAtRoute("GetAlbum", new { albumId = albumObj.Id}, albumObj);
        }

        [HttpPatch("{albumId:int}", Name = "UpdateAlbum")]
        [ProducesResponseType(204)]       
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateAlbum(int albumId, [FromBody] AlbumUpdateDto albumDto)
        {
            if (albumDto == null || albumId != albumDto.Id)
            {
                return BadRequest(ModelState);
            }

            var albumObj = _mapper.Map<Album>(albumDto);

            if (!_albumRepo.UpdateAlbum(albumObj))
            {
                ModelState.AddModelError("", $"Couldnt update the album {albumObj.Title}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{albumId:int}", Name = "DeleteAlbum")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAlbum(int albumId)
        {
            if (!_albumRepo.AlbumExists(albumId))
            {
                return NotFound();
            }

            var albumObj = _albumRepo.GetAlbum(albumId);

            if (!_albumRepo.DeleteAlbum(albumObj))
            {
                ModelState.AddModelError("", $"Couldnt delete the album {albumObj.Title}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
