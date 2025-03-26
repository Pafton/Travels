using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Travels.Application.Dtos.Auth;
using Travels.Application.Interfaces;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [SwaggerOperation(Summary = "Rejestruje nowego użytkownika", Description = "Tworzy nowe konto użytkownika na podstawie podanych danych.")]
    [SwaggerResponse(200, "Użytkownik został pomyślnie zarejestrowany.")]
    [SwaggerResponse(400, "Błąd podczas rejestracji.")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            await _authService.Register(registerDto);
            return Ok("User registered successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during registration: {ex.Message}");
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("activate-account/{id}")]
    [SwaggerOperation(Summary = "Aktywuje konto użytkownika", Description = "Aktywuje konto użytkownika na podstawie podanego identyfikatora.")]
    [SwaggerResponse(200, "Konto zostało aktywowane.")]
    [SwaggerResponse(400, "Nieprawidłowy identyfikator użytkownika.")]
    [SwaggerResponse(404, "Użytkownik nie został znaleziony.")]
    public async Task<IActionResult> ActivateAccount([FromRoute] int id)
    {
        try
        {
            await _authService.ActivateAccount(id);
            return Ok("Account activated successfully.");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Invalid user ID: {ex.Message}");
            return BadRequest(new { error = "Invalid user ID" });
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"User not found: {ex.Message}");
            return NotFound(new { error = "User not found" });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during account activation: {ex.Message}");
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("login")]
    [SwaggerOperation(Summary = "Loguje użytkownika", Description = "Sprawdza dane logowania i zwraca token uwierzytelniający.")]
    [SwaggerResponse(200, "Zalogowano pomyślnie.", typeof(string))]
    [SwaggerResponse(401, "Nieprawidłowy e-mail lub hasło.")]
    [SwaggerResponse(400, "Błąd podczas logowania.")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var token = await _authService.Login(loginDto);
            return Ok(new { token });
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Unauthorized login attempt: {ex.Message}");
            return Unauthorized(new { error = "Invalid email or password" });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Login error: {ex.Message}");
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("send-password-reset-link")]
    [SwaggerOperation(Summary = "Wysyła link do resetu hasła", Description = "Wysyła e-mail z linkiem do resetu hasła dla podanego adresu e-mail.")]
    [SwaggerResponse(200, "Link do resetu hasła został wysłany.")]
    [SwaggerResponse(400, "Błąd podczas wysyłania linku.")]
    public async Task<IActionResult> SendPasswordResetLink([FromBody] string email)
    {
        try
        {
            var token = await _authService.SendPasswordResetLink(email);
            return Ok("Password reset link sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending password reset link: {ex.Message}");
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("reset-password")]
    [SwaggerOperation(Summary = "Resetuje hasło użytkownika", Description = "Ustawia nowe hasło na podstawie tokena resetującego.")]
    [SwaggerResponse(200, "Hasło zostało pomyślnie zresetowane.")]
    [SwaggerResponse(400, "Błąd podczas resetowania hasła.")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
        try
        {
            await _authService.ResetPassword(resetPasswordDto.Token, resetPasswordDto.NewPassword);
            return Ok("Password reset successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error resetting password: {ex.Message}");
            return BadRequest(new { error = ex.Message });
        }
    }
}
