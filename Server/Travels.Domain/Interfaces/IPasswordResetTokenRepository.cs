using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Domain.Entities;

namespace Travels.Domain.Interfaces
{
    public interface IPasswordResetTokenRepository
    {
        Task AddToken(PasswordResetToken token);
        Task<PasswordResetToken> GetToken(string token);
        Task<bool> IsTokenExpired(string token);
        Task MarkTokenAsUsed(string token);
        Task RemoveToken(PasswordResetToken token);
    }
}
