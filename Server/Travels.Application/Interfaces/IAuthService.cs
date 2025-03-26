using Travels.Application.Dtos.Auth;

namespace Travels.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Register(RegisterDto registerDto);
        Task<string?> Login(LoginDto loginDto);
        Task<string> SendPasswordResetLink(string email);
        Task ResetPassword(string token, string newPassword);
        Task ActivateAccount(int id);
    }
}
