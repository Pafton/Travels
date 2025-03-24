using Travels.Domain.Entities;

namespace Travels.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<IEnumerable<User>> GetUsers();
        Task<User?> GetUser(int? id);
        Task<User?> GetById(int? userId);
        Task ChangeUser(User user);
        Task DeleteUser(int id);
        Task<User?> GetByEmail(string email);
    }
}
