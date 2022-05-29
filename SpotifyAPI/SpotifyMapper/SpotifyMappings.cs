using AutoMapper;
using SpotifyAPI.Models;
using SpotifyAPI.Models.DTOs;

namespace SpotifyAPI.SpotifyMapper
{
    public class SpotifyMappings : Profile
    {
        public SpotifyMappings()
        {
            CreateMap<Song, SongDto>().ReverseMap();
        }
    }
}
