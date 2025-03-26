using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Scholario.Application.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Travels.Application.Dtos.Account;
using Travels.Application.Interfaces;
using Travels.Domain.Entities;
using Travels.Domain.Interfaces;

namespace Travels.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IEmailSender _emailSender;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(IUserRepository userRepository,IPasswordHasher<User> passwordHasher, IConfiguration configuration,IMapper mapper, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _mapper = mapper;
            _emailSender = emailSender;
            _authenticationSettings = _configuration.GetSection("Authentication").Get<AuthenticationSettings>();
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

            var activationLink = $"http://localhost:5190/User/activate-account/{user.Id}";

            await _emailSender.SendEmailAsync(user.Email, "Link activate", $"Click here to activate your account: {activationLink}");

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

        public async Task Delete(int id)
        {
            if(id<0) 
                throw new ArgumentOutOfRangeException(nameof(id));

            var user = _userRepository.GetUser(id);
            if (user == null)
                throw new ArgumentNullException("User not found");

            await _userRepository.DeleteUser(id);
        }

        public async Task SetActivationStatus(int id, bool isActive)
        {
            var user = await _userRepository.GetUser(id);
            if (user == null)
                throw new KeyNotFoundException("Użytkownik nie został znaleziony.");

            user.isActivate = isActive;
            await _userRepository.ChangeUser(user);
        }
    }
}
