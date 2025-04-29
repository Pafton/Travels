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

    [HttpPost("send-password-reset-link")]
    [SwaggerOperation(Summary = "Wysyła na maila token potrzebny do zmiany hasla", Description = "Wysyła e-mail z linkiem do resetu hasła dla podanego adresu e-mail.")]
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
