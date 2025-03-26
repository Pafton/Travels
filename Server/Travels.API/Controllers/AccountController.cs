using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Travels.Application.Dtos.Account;
using Travels.Application.Dtos.Auth;
using Travels.Application.Interfaces;

[Route("Account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("register")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(Summary = "Rejestruje nowego użytkownika -- ADMIN", Description = "Tworzy nowe konto użytkownika na podstawie podanych danych.")]
    [SwaggerResponse(200, "Użytkownik został pomyślnie zarejestrowany.")]
    [SwaggerResponse(400, "Błąd podczas rejestracji.")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            await _accountService.Register(registerDto);
            return Ok("User registered successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during registration: {ex.Message}");
            return BadRequest(ex.Message); // Prosta odpowiedź z samym komunikatem
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(Summary = "Usuwa użytkownika -- ADMIN", Description = "Usuwa użytkownika.")]
    [SwaggerResponse(200, "Użytkownik został pomyślnie usunięty.")]
    [SwaggerResponse(400, "Błąd podczas usuwania.")]
    [SwaggerResponse(404, "Użytkownik nie został znaleziony.")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _accountService.Delete(id);
            return Ok("Użytkownik został pomyślnie usunięty.");
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($">[AccountCtrl] No user found: {ex.Message}");
            return NotFound("Użytkownik nie został znaleziony.");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($">[AccountCtrl] Invalid user ID: {ex.Message}");
            return BadRequest($"Invalid id: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($">[AccountCtrl] Unhandled exception: {ex.Message}");
            return BadRequest($"Unexpected error: {ex.Message}");
        }
    }

    [HttpPost("login")]
    [SwaggerOperation(Summary = "Loguje użytkownika -- USER/ADMIN", Description = "Sprawdza dane logowania i zwraca token uwierzytelniający.")]
    [SwaggerResponse(200, "Zalogowano pomyślnie.", typeof(string))]
    [SwaggerResponse(401, "Nieprawidłowy e-mail lub hasło.")]
    [SwaggerResponse(400, "Błąd podczas logowania.")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var token = await _accountService.Login(loginDto);
            return Ok(new { token });
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($">[AccountCtrl] Unauthorized login attempt: {ex.Message}");
            return Unauthorized("Invalid email or password");
        }
        catch (Exception ex)
        {
            Console.WriteLine($">[AccountCtrl] Login error: {ex.Message}");
            return BadRequest($"Login error: {ex.Message}");
        }
    }

    [HttpPatch("set-activation/{id}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(Summary = "Zmienia status aktywacji użytkownika -- ADMIN", Description = "Administrator może aktywować lub dezaktywować konto użytkownika.")]
    [SwaggerResponse(200, "Status aktywacji został zmieniony.")]
    [SwaggerResponse(400, "Nieprawidłowe dane.")]
    [SwaggerResponse(404, "Użytkownik nie został znaleziony.")]
    public async Task<IActionResult> SetActivationStatus(int id, [FromBody] bool isActive)
    {
        try
        {
            await _accountService.SetActivationStatus(id, isActive);
            return Ok("Status aktywacji został zmieniony.");
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($">[AccountCtrl] User not found: {ex.Message}");
            return NotFound("Użytkownik nie został znaleziony.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($">[AccountCtrl] Error while setting activation status: {ex.Message}");
            return BadRequest($"Error while setting activation status: {ex.Message}");
        }
    }
}
