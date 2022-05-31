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
    //[ApiExplorerSettings(GroupName = "SpotifyOpenAPISpecArtists")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ArtistsController : Controller
    {
        private readonly IArtistRepository _artistRepo;
        private readonly IMapper _mapper;

        public ArtistsController(IArtistRepository artistRepo, IMapper mapper)
        {
            _artistRepo = artistRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all the artists.
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ArtistDto>))]
        public IActionResult GetArtists()
        {
            var artistList = _artistRepo.GetArtists();

            var artistDto = new List<ArtistDto>();
            foreach (var artist in artistList)
            {
                artistDto.Add(_mapper.Map<ArtistDto>(artist));
            }
            return Ok(artistDto);
        }

        /// <summary>
        /// Get a artist by Id.
        /// </summary>
        /// <param name="artistId"> The Id of the artist </param>
        /// <returns></returns>

        [HttpGet("{artistId:int}", Name = "GetArtist")]
        [ProducesResponseType(200, Type = typeof(ArtistDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetArtist(int artistId)
        {
            var artist = _artistRepo.GetArtist(artistId);

            if (artist == null)
            {
                return NotFound();
            }

            var  artistDto = new ArtistDto();
            artistDto = _mapper.Map<ArtistDto>(artist);
            return Ok(artistDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ArtistDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult CreateArtist([FromBody] ArtistDto artistDto)
        {
            if (artistDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_artistRepo.ArtistExists(artistDto.FName))
            {
                ModelState.AddModelError("","Artist already Exists!");
                return StatusCode(404, ModelState);
            }

            var artistObj = _mapper.Map<Artist>(artistDto);

            if (!_artistRepo.CreateArtist(artistObj))
            {
                ModelState.AddModelError("", $"Couldnt save the artist {artistObj.FName} {artistObj.LName}");
                return StatusCode(500,ModelState);
            }

            return CreatedAtRoute("GetArtist", new { artistId = artistObj.Id}, artistObj);
        }

        [HttpPatch("{artistId:int}", Name = "UpdateArtist")]
        [ProducesResponseType(204)]       
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateArtist(int artistId, [FromBody] ArtistDto artistDto)
        {
            if (artistDto == null || artistId != artistDto.Id)
            {
                return BadRequest(ModelState);
            }

            var artistObj = _mapper.Map<Artist>(artistDto);

            if (!_artistRepo.UpdateArtist(artistObj))
            {
                ModelState.AddModelError("", $"Couldnt update the artist {artistObj.FName} {artistObj.LName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{artistId:int}", Name = "DeleteArtist")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteArtist(int artistId)
        {
            if (!_artistRepo.ArtistExists(artistId))
            {
                return NotFound();
            }

            var artistObj = _artistRepo.GetArtist(artistId);

            if (!_artistRepo.DeleteArtist(artistObj))
            {
                ModelState.AddModelError("", $"Couldnt delete the artist {artistObj.FName} {artistObj.LName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
