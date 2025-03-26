using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Application.Interfaces;
using Travels.Domain.Entities;
using Travels.Domain.Interfaces;

namespace Travels.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public async Task AddAdminRole(int userId)
        {
            if(userId < 0)
                throw new ArgumentOutOfRangeException("userId");

            var user = await _userRepository.GetById(userId);
            if (user == null)
                throw new ArgumentNullException("User not found");

            user.Role = Role.Admin;
            await _userRepository.ChangeUser(user);
        }

        public async Task RemoveAdminRole(int userId)
        {
            if (userId < 0)
                throw new ArgumentOutOfRangeException("userId");

            var user = await _userRepository.GetById(userId);
            if (user == null)
                throw new ArgumentNullException("User not found");

            user.Role = Role.Customer;
            await _userRepository.ChangeUser(user);
        }
    }
}
