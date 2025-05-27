using Travels.Application.Dtos.Account;

namespace Travels.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> SendPasswordResetLink(string email);
        Task ResetPassword(string token, string newPassword);
    }
}
