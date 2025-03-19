using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Travels.Application.Dtos;
using Travels.Application.Interfaces;

namespace Travels.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        [Route("GetReservation/{id}")]
        [SwaggerOperation(Summary = "Pobiera rezerwację o podanym ID", Description = "Zwraca szczegóły rezerwacji na podstawie identyfikatora.")]
        [SwaggerResponse(200, "Rezerwacja została znaleziona i zwrócona.", typeof(ReservationDto))]
        [SwaggerResponse(400, "Nieprawidłowe ID rezerwacji.")]
        [SwaggerResponse(404, "Rezerwacja nie została znaleziona.")]
        public async Task<IActionResult> GetReservation(int id)
        {
            try
            {
                var reservationDto = await _reservationService.GetReservation(id);
                return Ok(reservationDto);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"{ex.Message}");
                return BadRequest($"Invalid id: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[ReservationCtl] Unhandled exception: {ex.Message}");
                return NotFound($"Reservation not found: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("StartReservation")]
        [SwaggerOperation(Summary = "Tworzy nową rezerwację", Description = "Tworzy nową rezerwację na podstawie przekazanych danych.")]
        [SwaggerResponse(200, "Rezerwacja została pomyślnie utworzona.")]
        [SwaggerResponse(400, "Nieprawidłowe dane rezerwacji.")]
        public async Task<IActionResult> StartReservation([FromBody] ReservationDto reservationDto)
        {
            try
            {
                await _reservationService.StartReservation(reservationDto);
                return Ok(new { message = "Reservation created successfully" });
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"{ex.Message}");
                return BadRequest($"Invalid data: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[ReservationCtl] Unhandled exception: {ex.Message}");
                return BadRequest($"Unexpected error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("CancelReservation/{id}")]
        [SwaggerOperation(Summary = "Anuluje rezerwację o podanym ID", Description = "Anuluje rezerwację na podstawie identyfikatora.")]
        [SwaggerResponse(200, "Rezerwacja została pomyślnie anulowana.")]
        [SwaggerResponse(400, "Nieprawidłowe ID rezerwacji.")]
        [SwaggerResponse(404, "Rezerwacja nie została znaleziona.")]
        public async Task<IActionResult> CancelReservation(int id)
        {
            try
            {
                await _reservationService.CancelReservation(id);
                return Ok(new { message = $"Reservation with id {id} cancelled" });
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"{ex.Message}");
                return BadRequest($"Invalid id: {ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
                return NotFound($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[ReservationCtl] Unhandled exception: {ex.Message}");
                return BadRequest($"Unexpected error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("UpdateReservation/{id}")]
        [SwaggerOperation(Summary = "Aktualizuje rezerwację o podanym ID", Description = "Aktualizuje rezerwację na podstawie identyfikatora i przekazanych danych.")]
        [SwaggerResponse(200, "Rezerwacja została pomyślnie zaktualizowana.")]
        [SwaggerResponse(400, "Nieprawidłowe dane rezerwacji lub ID.")]
        public async Task<IActionResult> UpdateReservation([FromBody] ReservationDto reservationDto, int id)
        {
            try
            {
                await _reservationService.UpdateReservation(reservationDto, id);
                return Ok(new { message = $"Reservation with id {id} updated" });
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"{ex.Message}");
                return BadRequest($"Invalid data: {ex.Message}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"{ex.Message}");
                return BadRequest($"Invalid id: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[ReservationCtl] Unhandled exception: {ex.Message}");
                return BadRequest($"Unexpected error: {ex.Message}");
            }
        }
    }
}