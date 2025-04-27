using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using Travels.Application.Dtos.Account;
using Travels.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("register")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(Summary = "Rejestruje nowego użytkownika",Description = "Rejestruje użytkownika i wysyła link aktywacyjny na e-mail. Format linka: http://localhost:5190/User/activate-account/{userId}")]
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
            return BadRequest(ex.Message);
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
    [SwaggerOperation(Summary = "Loguje użytkownika", Description = "Sprawdza dane logowania i zwraca token uwierzytelniający.")]
    [SwaggerResponse(200, "Zalogowano pomyślnie.", typeof(string))]
    [SwaggerResponse(401, "Nieprawidłowy e-mail lub hasło.")]
    [SwaggerResponse(400, "Błąd podczas logowania.")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var token = await _accountService.Login(loginDto);
            return Ok(token);
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

    [HttpPost("change-password")]
    [Authorize(Roles = "Customer")]
    [SwaggerOperation(Summary = "Zmienia zmiana hasła dla zalogowanego użytkownika -- CUSTOMER", Description = "Użytkonik może zmienić swoje hasło.")]
    [SwaggerResponse(200, "Hasło zostało zmienione.")]
    [SwaggerResponse(400, "Nieprawidłowe dane.")]
    [SwaggerResponse(404, "Użytkownik nie został znaleziony.")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ForgotPasswordForLoginUserDto forgotPasswordForLoginUserDto)
    {
        try
        {
            var userIdClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID is missing in the token.");
            }
            var customerId = int.Parse(userIdClaim);
            await _accountService.ResetPasswordForLoginUser(forgotPasswordForLoginUserDto, customerId);
            return Ok("User change password");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($">[AuthCtr] Unauthorized access: {ex.Message}");
            return Forbid("You are not authorized to perform this action.");
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($">[AuthCtr] User not found: {ex.Message}");
            return NotFound("User not found in the database.");
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($">[AuthCtr] Received null value: {ex.Message}");
            return BadRequest($"Invalid data: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($">[AuthCtr] Unhandled exception: {ex.Message}");
            return BadRequest($"Unexpected error: {ex.Message}");
        }
    }

    [HttpGet("admin/users")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(Summary = "Pobiera listę wszystkich użytkowników -- ADMIN", Description = "Zwraca ID, email i zahashowane hasło użytkowników.")]
    [SwaggerResponse(200, "Zwrócono listę użytkowników.", typeof(IEnumerable<UserListItemDto>))]
    [SwaggerResponse(403, "Brak dostępu.")]
    [SwaggerResponse(500, "Błąd serwera.")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _accountService.GetAllUsers();
            return Ok(users);
        }
        catch (Exception ex)
        {
            Console.WriteLine($">[AccountCtrl] GetAllUsers error: {ex.Message}");
            return StatusCode(500, "Wystąpił błąd podczas pobierania listy użytkowników.");
        }
    }

}
