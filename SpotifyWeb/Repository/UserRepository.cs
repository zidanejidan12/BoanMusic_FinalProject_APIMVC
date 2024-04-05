using SpotifyWeb.Models;
using SpotifyWeb.Repository.IRepository;
using System.Net.Http;

namespace SpotifyWeb.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public UserRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
