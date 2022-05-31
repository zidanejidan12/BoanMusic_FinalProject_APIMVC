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
            CreateMap<Artist, ArtistDto>().ReverseMap();
            CreateMap<Album, AlbumDto>().ReverseMap();
            CreateMap<Song, SongCreateDto>().ReverseMap();
            CreateMap<Album, AlbumCreateDto>().ReverseMap();
            CreateMap<Album, AlbumUpdateDto>().ReverseMap();
            CreateMap<Song, SongUpdateDto>().ReverseMap();
        }
    }
}
