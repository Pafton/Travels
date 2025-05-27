using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Application.Dtos.User;
using Travels.Domain.Entities;

namespace Travels.Application.Interfaces
{
    public interface IUserService
    {
        Task AddAdminRole(int userId);
        Task RemoveAdminRole(int userId);
        Task<UserDto> GetUserById(int userId);
        Task<IEnumerable<UserDto>> GetUsers();
    }
}
