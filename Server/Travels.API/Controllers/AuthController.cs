using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Travels.Application.Dtos.Auth;
using Travels.Application.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("activate-account/{id}")]
    [SwaggerOperation(Summary = "Aktywuje konto użytkownika", Description = "Aktywuje konto użytkownika na podstawie podanego identyfikatora.")]
    [SwaggerResponse(200, "Konto zostało aktywowane.")]
    [SwaggerResponse(400, "Nieprawidłowy identyfikator użytkownika.")]
    [SwaggerResponse(404, "Użytkownik nie został znaleziony.")]
    [SwaggerResponse(403, "Brak uprawnień do wykonania tej operacji.")]
    public async Task<IActionResult> ActivateAccount([FromRoute] int id)
    {
        try
        {
            await _authService.ActivateAccount(id);
            return Ok("Status aktywacji został zmieniony.");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($">[AuthCtrl] Invalid user ID: {ex.Message}");
            return BadRequest("Nieprawidłowy identyfikator użytkownika.");
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($">[AuthCtrl] User not found: {ex.Message}");
            return NotFound("Użytkownik nie został znaleziony.");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($">[AuthCtrl] Unauthorized user: {ex.Message}");
            return StatusCode(403, "Brak uprawnień do wykonania tej operacji.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($">[AuthCtrl] Error while activating account: {ex.Message}");
            return BadRequest($"Błąd podczas aktywacji konta: {ex.Message}");
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
            return Ok("Link do resetu hasła został wysłany.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($">[AuthCtrl] Error sending password reset link: {ex.Message}");
            return BadRequest($"Błąd podczas wysyłania linku: {ex.Message}");
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
            return Ok("Hasło zostało pomyślnie zresetowane.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($">[AuthCtrl] Error resetting password: {ex.Message}");
            return BadRequest($"Błąd podczas resetowania hasła: {ex.Message}");
        }
    }
}
