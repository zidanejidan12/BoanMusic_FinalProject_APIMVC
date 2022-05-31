using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Data;
using SpotifyAPI.Models;
using SpotifyAPI.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyAPI.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ApplicationDbContext _db;

        public AlbumRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateAlbum(Album album)
        {
            _db.Albums.Add(album);
            return Save();
        }

        public bool DeleteAlbum(Album album)
        {
            _db.Albums.Remove(album);
            return Save();
        }

        public Album GetAlbum(int albumId)
        {
            return _db.Albums.Include(x => x.Artist).FirstOrDefault(x => x.Id == albumId);
        }

        public ICollection<Album> GetAlbums()
        {
            return _db.Albums.Include(x => x.Artist).OrderBy(x => x.Title).ToList();
        }
        public bool AlbumExists(string name)
        {
            bool value = _db.Albums.Any(x => x.Title.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool AlbumExists(int id)
        {
            return _db.Albums.Any(x => x.Id == id);
        }
        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateAlbum(Album album)
        {
            _db.Albums.Update(album);
            return Save();
        }

        public ICollection<Album> GetArtistAlbums(int artistId)
        {
            return _db.Albums.Include(x=>x.Artist).Where(y=>y.ArtistId == artistId).ToList();
        }
    }
}