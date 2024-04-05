using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyAPI.Models;

namespace SpotifyAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAndPasswordAsync(string username, string password);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
