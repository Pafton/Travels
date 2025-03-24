using Microsoft.AspNetCore.Mvc;
using Travels.Application.Dtos.Auth;
using Travels.Application.Interfaces;

namespace Travels.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                await _authService.Register(registerDto);
                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[AuthCtrl] Exception in Register: {ex.Message}");
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _authService.Login(loginDto);
                if (token == null)
                {
                    return Unauthorized("Invalid email or password");
                }
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[AuthCtrl] Exception in Login: {ex.Message}");
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            try
            {   
                var token = await _authService.SendPasswordResetLink(dto.Email);
                return Ok($"Password reset link sent to email.:{ token }");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[AuthCtrl] Exception in ForgotPassword: {ex.Message}");
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            try
            {
                await _authService.ResetPassword(dto.Token, dto.NewPassword);
                return Ok("Password has been reset.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[AuthCtrl] Exception in ResetPassword: {ex.Message}");
                return BadRequest($"Error: {ex.Message}");
            }
        }

    }
}
