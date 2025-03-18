using Travels.Application.Dtos;
using Travels.Domain.Entities;

namespace Travels.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Register(RegisterDto registerDto);
        Task<string?> Login(LoginDto loginDto);
    }
}
