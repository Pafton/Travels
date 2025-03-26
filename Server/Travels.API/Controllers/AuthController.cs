using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Travels.Application.Dtos.Account;
using Travels.Application.Dtos.Auth;
using Travels.Application.Interfaces;

[Route("User")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
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
