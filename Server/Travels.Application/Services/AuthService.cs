using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Travels.Application.Dtos.Auth;
using Travels.Application.Interfaces;
using Travels.Domain.Entities;
using Travels.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Scholario.Application.Authentication;

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

        public async Task<bool> Register(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetByEmail(registerDto.Email);
            if (existingUser != null)
                throw new Exception("User already exists");

            var user = _mapper.Map<User>(registerDto);
            user.Role = Role.Customer;

            user.Password = _passwordHasher.HashPassword(user, registerDto.Password);
            await _userRepository.AddUser(user);

            return true;
        }

        public async Task<string?> Login(LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmail(loginDto.Email);
            if (user == null)
                throw new Exception("User not found");

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Invalid email or password");

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Name} {user.Surname}"),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(_authenticationSettings.JwtExpireMinutes);

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
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

            var resetLink = $"https://travel/reset-password?token={token}";

            await _emailSender.SendEmailAsync(email, "Password Reset", $"Click here to reset your password: {resetLink}");

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
