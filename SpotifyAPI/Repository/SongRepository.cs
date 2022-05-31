using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Data;
using SpotifyAPI.Models;
using SpotifyAPI.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyAPI.Repository
{
    public class SongRepository : ISongRepository
    {
        private readonly ApplicationDbContext _db;

        public SongRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateSong(Song song)
        {
            _db.Songs.Add(song);
            return Save();
        }

        public bool DeleteSong(Song song)
        {
            _db.Songs.Remove(song);
            return Save();
        }

        public Song GetSong(int songId)
        {
            return _db.Songs.Include(x => x.Album).ThenInclude(x => x.Artist).FirstOrDefault(x => x.Id == songId);
        }

        public ICollection<Song> GetSongs()
        {
            return _db.Songs.Include(x => x.Album).ThenInclude(x=> x.Artist).OrderBy(x => x.Title).ToList();
        }
        public bool SongExists(string name)
        {
            bool value = _db.Songs.Any(x => x.Title.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool SongExists(int id)
        {
            return _db.Songs.Any(x => x.Id == id);
        }
        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateSong(Song song)
        {
            _db.Songs.Update(song);
            return Save();
        }

        public ICollection<Song> GetAlbumSongs(int albumId)
        {
            return _db.Songs.Include(x => x.Album).ThenInclude(x => x.Artist).Where(y => y.AlbumId == albumId).ToList();
        }
    }
}