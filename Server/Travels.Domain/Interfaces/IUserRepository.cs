using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Domain.Entities;

namespace Travels.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<IEnumerable<User>> GetUsers();
        Task<User?> GetUser(int? id);
        Task ChangeUser(User user);
        Task DeleteUser(int id);
    }
}
