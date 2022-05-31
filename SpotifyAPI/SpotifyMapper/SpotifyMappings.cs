using AutoMapper;
using SpotifyAPI.Models;
using SpotifyAPI.Models.DTOs;

namespace SpotifyAPI.SpotifyMapper
{
    public class SpotifyMappings : Profile
    {
        public SpotifyMappings()
        {
            CreateMap<Artist, ArtistDto>().ReverseMap();
            CreateMap<Artist, ArtistCreateDto>().ReverseMap();

            CreateMap<Album, AlbumDto>().ReverseMap();
            CreateMap<Album, AlbumCreateDto>().ReverseMap();
            CreateMap<Album, AlbumUpdateDto>().ReverseMap();

            CreateMap<Song, SongDto>().ReverseMap();
            CreateMap<Song, SongCreateDto>().ReverseMap();
            CreateMap<Song, SongUpdateDto>().ReverseMap();          
        }
    }
}
