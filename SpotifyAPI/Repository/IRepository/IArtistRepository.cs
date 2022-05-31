using SpotifyAPI.Models;
using System.Collections.Generic;

namespace SpotifyAPI.Repository.IRepository
{
    public interface IArtistRepository
    {
        ICollection<Artist> GetArtists();

        Artist GetArtist(int artistId);
        bool ArtistExists(string name);
        bool ArtistExists(int id);
        bool CreateArtist(Artist artist);
        bool UpdateArtist(Artist artist);
        bool DeleteArtist(Artist artist);
        bool Save();
    }
}
