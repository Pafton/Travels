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

        [HttpPost]
        [SwaggerOperation(Summary = "Dodaje recenzję do oferty wycieczki", Description = "Umożliwia dodanie recenzji dla wybranej oferty wycieczki. Recenzja zawiera komentarz, ocenę oraz identyfikator użytkownika.")]
        [SwaggerResponse(200, "Recenzja została pomyślnie dodana.")]
        [SwaggerResponse(400, "Nieprawidłowe dane wejściowe lub brak wymaganych pól w żądaniu.")]
        [SwaggerResponse(404, "Nie znaleziono oferty wycieczki o podanym identyfikatorze.")]
        [SwaggerResponse(500, "Wystąpił błąd serwera podczas próby dodania recenzji.")]
        public async Task<IActionResult> AddReview([FromBody] ReviewDto reviewDto)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim != null)
                {
                    var userId = int.Parse(userIdClaim);
                    await _reviewservice.AddReview(reviewDto, userId);
                }
                else
                {
                    var guestUserId = Guid.NewGuid().ToString();
                    reviewDto.UserName = guestUserId;
                    await _reviewservice.AddReview(reviewDto, null);
                }

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

        [HttpGet]
        [SwaggerOperation(Summary = "Pobiera recenzje dla wybranej oferty wycieczki", Description = "Umożliwia pobranie wszystkich recenzji dla oferty wycieczki. Recenzje zawierają komentarze, oceny, użytkowników oraz daty dodania.")]
        [SwaggerResponse(200, "Recenzje zostały pomyślnie pobrane.")]
        [SwaggerResponse(400, "Nieprawidłowe dane wejściowe.")]
        [SwaggerResponse(404, "Nie znaleziono recenzji dla tej oferty.")]
        [SwaggerResponse(500, "Wystąpił błąd serwera podczas pobierania recenzji.")]
        public async Task<IActionResult> GetReviews()
        {
            try
            {
                var reviews = await _reviewservice.GetReviews();

                if (reviews == null || !reviews.Any())
                {
                    return NotFound("Recenzje nie zostały znalezione.");
                }

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[ReviewCtl] Unhandled exception: {ex.Message}");
                return BadRequest($"Unexpected error: {ex.Message}");
            }
        }
    }
}
