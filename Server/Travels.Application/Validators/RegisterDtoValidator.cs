using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Application.Dtos.Account;
using Travels.Domain.Entities;

namespace Travels.Application.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            /*RuleFor(x => x.Role)
                .Must(role => Enum.IsDefined(typeof(Role), role))
                .WithMessage("Nieprawidłowa rola użytkownika.");*/
        }
    }

}
