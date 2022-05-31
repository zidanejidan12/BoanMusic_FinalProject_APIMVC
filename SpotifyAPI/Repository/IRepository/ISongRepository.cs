using SpotifyAPI.Models;
using System.Collections.Generic;

namespace SpotifyAPI.Repository.IRepository
{
    public interface ISongRepository
    {
        ICollection<Song> GetSongs();
        ICollection<Song> GetAlbumSongs(int albumId);

        Song GetSong(int songId);
        bool SongExists(string name);
        bool SongExists(int id);
        bool CreateSong(Song song);
        bool UpdateSong(Song song);
        bool DeleteSong(Song song);
        bool Save();
    }
}
