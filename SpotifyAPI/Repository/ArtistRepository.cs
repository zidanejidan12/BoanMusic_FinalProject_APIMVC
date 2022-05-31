using SpotifyAPI.Data;
using SpotifyAPI.Models;
using SpotifyAPI.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyAPI.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly ApplicationDbContext _db;

        public ArtistRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateArtist(Artist artist)
        {
            _db.Artists.Add(artist);
            return Save();
        }

        public bool DeleteArtist(Artist artist)
        {
            _db.Artists.Remove(artist);
            return Save();
        }

        public Artist GetArtist(int artistId)
        {
            return _db.Artists.FirstOrDefault(x => x.Id == artistId);
        }

        public ICollection<Artist> GetArtists()
        {
            return _db.Artists.OrderBy(x => x.FName).ThenBy(y=>y.LName).ToList();
        }
        public bool ArtistExists(string name)
        {
            bool value = _db.Artists.Any(x => x.FName.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool ArtistExists(int id)
        {
            return _db.Artists.Any(x => x.Id == id);
        }
        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateArtist(Artist artist)
        {
            _db.Artists.Update(artist);
            return Save();
        }
    }
}