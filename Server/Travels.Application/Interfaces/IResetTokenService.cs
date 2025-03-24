using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travels.Application.Interfaces
{
    public interface IResetTokenService
    {
        Task AddToken(string token, string email);
        Task<string?> GetEmailByToken(string token);
        Task RemoveToken(string token);
    }
}
