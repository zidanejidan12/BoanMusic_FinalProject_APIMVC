using SpotifyWeb.Models;
using SpotifyWeb.Repository.IRepository;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace SpotifyWeb.Repository
{
    public class SongRepository : Repository<Song>, ISongRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public SongRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<int> GetRandomSongIdAsync(int currentSongId)
        {
            // Get all songs from the API
            IEnumerable<Song> allSongs = await GetAllAsync("https://localhost:44300/api/v1/songs");

            // Filter out the current song
            IEnumerable<Song> songsExceptCurrent = allSongs.Where(s => s.Id != currentSongId);

            // Get a random song from the remaining songs
            Random random = new Random();
            int randomIndex = random.Next(0, songsExceptCurrent.Count());
            int randomSongId = songsExceptCurrent.ElementAt(randomIndex).Id;

            return randomSongId;
        }
    }
}
