using SpotifyWeb.Models;
using SpotifyWeb.Repository.IRepository;
using System.Net.Http;

namespace SpotifyWeb.Repository
{
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public AlbumRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
