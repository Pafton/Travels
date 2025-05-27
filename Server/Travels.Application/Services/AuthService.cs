using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Travels.Application.Interfaces;
using Travels.Domain.Entities;
using Travels.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Scholario.Application.Authentication;
using Travels.Application.Dtos.Account;

namespace Travels.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IEmailSender _emailSender;
        private readonly IResetTokenService _resetTokenService;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IConfiguration configuration,
            IResetTokenService resetTokenService,
            IMapper mapper,
            IEmailSender emailSender,
            IPasswordResetTokenRepository passwordResetTokenRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _resetTokenService = resetTokenService;
            _mapper = mapper;
            _emailSender = emailSender;
            _authenticationSettings = _configuration.GetSection("Authentication").Get<AuthenticationSettings>();
            _passwordResetTokenRepository = passwordResetTokenRepository;
        }


        public async Task<string> SendPasswordResetLink(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user == null)
                throw new Exception("User not found");

            var token = Guid.NewGuid().ToString();

            var resetToken = new PasswordResetToken
            {
                Token = token,
                ExpirationDate = DateTime.UtcNow.AddMinutes(30),  
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                IsUsed = false
            };
            await _passwordResetTokenRepository.AddToken(resetToken);

            var resetLink = $"{token}";

            await _emailSender.SendEmailAsync(email, "Password Reset", $"This is token necessery to reset password: {resetLink}");

            return token;
        }

        public async Task ResetPassword(string token, string newPassword)
        {
            var resetToken = await _passwordResetTokenRepository.GetToken(token);
            if (resetToken == null || resetToken.IsUsed || resetToken.ExpirationDate < DateTime.UtcNow)
                throw new Exception("Invalid or expired token");

            var user = await _userRepository.GetById(resetToken.UserId);
            if (user == null)
                throw new Exception("User not found");

            user.Password = _passwordHasher.HashPassword(user, newPassword);

            await _userRepository.ChangeUser(user);
            await _passwordResetTokenRepository.MarkTokenAsUsed(token);
        }
    }
}
