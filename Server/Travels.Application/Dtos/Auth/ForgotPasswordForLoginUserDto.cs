using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travels.Application.Dtos.Auth
{
    public class ForgotPasswordForLoginUserDto
    {
        public string oldPassword {  get; set; } = string.Empty;
        public string newPassword { get; set; } = string.Empty;
        public string confirmNewPassword {  get; set; } = string.Empty;
    }
}
