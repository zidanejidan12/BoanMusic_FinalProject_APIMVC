using SpotifyAPI.Models;
using System.Collections.Generic;

namespace SpotifyAPI.Repository.IRepository
{
    public interface IAlbumRepository
    {
        ICollection<Album> GetAlbums();
        ICollection<Album> GetArtistAlbums(int albumId);

        Album GetAlbum(int albumId);
        bool AlbumExists(string name);
        bool AlbumExists(int id);
        bool CreateAlbum(Album album);
        bool UpdateAlbum(Album album);
        bool DeleteAlbum(Album album);
        bool Save();
    }
}
