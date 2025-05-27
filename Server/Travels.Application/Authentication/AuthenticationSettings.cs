using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Authentication
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; } = string.Empty;
        public int JwtExpireMinutes { get; set; }
        public string JwtIssuer { get; set; } = string.Empty;
    }
}
