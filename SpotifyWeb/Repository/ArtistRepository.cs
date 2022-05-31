using SpotifyWeb.Models;
using SpotifyWeb.Repository.IRepository;
using System.Net.Http;

namespace SpotifyWeb.Repository
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public ArtistRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
