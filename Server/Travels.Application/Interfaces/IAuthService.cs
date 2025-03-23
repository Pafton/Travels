    using Travels.Application.Dtos.Auth;
    using Travels.Domain.Entities;

    namespace Travels.Application.Interfaces
    {
        public interface IAuthService
        {
            Task<bool> Register(RegisterDto registerDto);
            Task<string?> Login(LoginDto loginDto);
            Task SendPasswordResetLink(string email);
            Task ResetPassword(string token, string newPassword);
    }
}
