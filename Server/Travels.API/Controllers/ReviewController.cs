using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using Travels.Application.Dtos.Reservation;
using Travels.Application.Dtos.Review;
using Travels.Application.Interfaces;
using Travels.Application.Services;

namespace Travels.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewservice;

        public ReviewController(IReviewService reviewservice)
        {
            _reviewservice = reviewservice;
        }

        [HttpPost] // TODO: id użytkownika pobierane z tokenu
        [SwaggerOperation(Summary = "Dodaje recenzję do oferty wycieczki", Description = "Umożliwia dodanie recenzji dla wybranej oferty wycieczki. Recenzja zawiera komentarz, ocenę oraz identyfikator użytkownika.")]
        [SwaggerResponse(200, "Recenzja została pomyślnie dodana.")]
        [SwaggerResponse(400, "Nieprawidłowe dane wejściowe lub brak wymaganych pól w żądaniu.")]
        [SwaggerResponse(404, "Nie znaleziono oferty wycieczki o podanym identyfikatorze.")]
        [SwaggerResponse(500, "Wystąpił błąd serwera podczas próby dodania recenzji.")]
        public async Task<IActionResult> AddReview([FromBody] ReviewDto reviewDto)
        {
            try
            {
                var userIdClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var userId = int.Parse(userIdClaim);
                await _reviewservice.AddReview(reviewDto,userId);
                return Ok(new { message = "Review created successfully" });
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"{ex.Message}");
                return BadRequest($"Invalid data: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[ReviewCtl] Unhandled exception: {ex.Message}");
                return BadRequest($"Unexpected error: {ex.Message}");
            }
        }

    }
}
