using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Models;
using SpotifyAPI.Models.DTOs;
using SpotifyAPI.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/artists")]
    [ApiVersion("2.0")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ArtistsV2Controller : Controller
    {
        private readonly IArtistRepository _artistRepo;
        private readonly IMapper _mapper;

        public ArtistsV2Controller(IArtistRepository artistRepo, IMapper mapper)
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
            var artist = _artistRepo.GetArtists().FirstOrDefault();
            return Ok(_mapper.Map<ArtistDto>(artist));
        }

    }
}
