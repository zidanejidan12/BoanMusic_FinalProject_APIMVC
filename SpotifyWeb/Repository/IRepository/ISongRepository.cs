using SpotifyWeb.Models;
using System.Threading.Tasks;

namespace SpotifyWeb.Repository.IRepository
{
    public interface ISongRepository : IRepository<Song>
    {
        Task<int> GetRandomSongIdAsync(int currentSongId);
    }
}
