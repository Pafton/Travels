using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Application.Dtos.Account;

namespace Travels.Application.Interfaces
{
    public interface IAccountService
    {
        Task<bool> Register(RegisterDto registerDto);
        Task<string?> Login(LoginDto loginDto);
        Task Delete(int id);
        Task SetActivationStatus(int id, bool isActive);
        Task ResetPasswordForLoginUser(ForgotPasswordForLoginUserDto forgotPasswordForLoginUserDto, int id);
        Task<IEnumerable<UserListItemDto>> GetAllUsers();

    }
}
