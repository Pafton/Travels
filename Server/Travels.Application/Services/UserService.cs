using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Application.Dtos.User;
using Travels.Application.Interfaces;
using Travels.Domain.Entities;
using Travels.Domain.Interfaces;

namespace Travels.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
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

        public async Task<UserDto> GetUserById(int userId)
        {
            if (userId < 0)
                throw new ArgumentOutOfRangeException("User not found");

            var user = await _userRepository.GetById(userId);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetUsers();

            if (users == null)
                throw new ArgumentNullException(nameof(users));

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            return usersDto;
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
