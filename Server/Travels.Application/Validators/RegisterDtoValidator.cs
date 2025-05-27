using FluentValidation;
using Travels.Application.Dtos.Account;
using Travels.Domain.Interfaces;

namespace Travels.Application.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator(IUserRepository userRepository)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email jest wymagany.")
                .EmailAddress().WithMessage("Nieprawidłowy format adresu e-mail.")
                .MustAsync(async (email, cancellation) =>
                {
                    var user = await userRepository.GetByEmail(email);
                    return user == null;
                }).WithMessage("Ten adres e-mail jest już używany.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Imię jest wymagane.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Nazwisko jest wymagane.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Hasło jest wymagane.")
                .MinimumLength(6).WithMessage("Hasło musi mieć co najmniej 6 znaków.");
        }
    }
}
