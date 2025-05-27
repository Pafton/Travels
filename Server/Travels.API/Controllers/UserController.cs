using Microsoft.AspNetCore.Mvc;
using Travels.Application.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Travels.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add-admin-role/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAdminRole([FromRoute] int userId)
        {
            try
            {
                await _userService.AddAdminRole(userId);
                return Ok("Rola administratora została przypisana.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($">[UserCtrl] Invalid user ID: {ex.Message}");
                return BadRequest("Nieprawidłowy identyfikator użytkownika.");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($">[UserCtrl] User not found: {ex.Message}");
                return NotFound("Użytkownik nie został znaleziony.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[UserCtrl] Error while adding admin role: {ex.Message}");
                return BadRequest($"Błąd podczas przypisywania roli administratora: {ex.Message}");
            }
        }


        [HttpPost("remove-admin-role/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveAdminRole([FromRoute] int userId)
        {
            try
            {
                await _userService.RemoveAdminRole(userId);
                return Ok("Rola administratora została usunięta.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($">[UserCtrl] Invalid user ID: {ex.Message}");
                return BadRequest("Nieprawidłowy identyfikator użytkownika.");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($">[UserCtrl] User not found: {ex.Message}");
                return NotFound("Użytkownik nie został znaleziony.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[UserCtrl] Error while removing admin role: {ex.Message}");
                return BadRequest($"Błąd podczas usuwania roli administratora: {ex.Message}");
            }
        }
    }
}
