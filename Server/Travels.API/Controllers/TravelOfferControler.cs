using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Travels.Application.Dtos.Travel;
using Travels.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Travels.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TravelOfferController : ControllerBase
    {
        private readonly ITravelOfferService _travelOfferService;

        public TravelOfferController(ITravelOfferService travelOfferService)
        {
            _travelOfferService = travelOfferService;
        }

        [HttpGet("GetTravel/{id}")]
        [SwaggerOperation(Summary = "Pobiera ofertę podróży o podanym ID", Description = "Zwraca szczegóły oferty podróży na podstawie identyfikatora.")]
        [SwaggerResponse(200, "Oferta podróży została znaleziona i zwrócona.", typeof(TravelOfferDto))]
        [SwaggerResponse(400, "Nieprawidłowe ID oferty podróży.")]
        [SwaggerResponse(404, "Oferta podróży nie została znaleziona.")]
        public async Task<IActionResult> GetTravel(int id)
        {
            try
            {
                var travelOfferDto = await _travelOfferService.GetTravel(id);
                return Ok(travelOfferDto);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("GetAllTravels")]
        [SwaggerOperation(Summary = "Pobiera wszystkie oferty podróży", Description = "Zwraca wszystkie dostępne oferty podróży.")]
        [SwaggerResponse(200, "Oferty podróży zostały zwrócone.", typeof(IEnumerable<TravelOfferDto>))]
        [SwaggerResponse(404, "Brak dostępnych ofert podróży.")]
        public async Task<IActionResult> GetAllTravels()
        {
            try
            {
                var travelOffersDto = await _travelOfferService.GetTravels();
                return Ok(travelOffersDto);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("CreateTravel")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Tworzy nową ofertę podróży -- ADMIN", Description = "Tworzy nową ofertę podróży na podstawie przekazanych danych.")]
        [SwaggerResponse(201, "Oferta podróży została pomyślnie utworzona.")]
        [SwaggerResponse(400, "Nieprawidłowe dane oferty podróży.")]
        [SwaggerResponse(404, "Nie znaleziono docelowej destynacji.")]
        public async Task<IActionResult> CreateTravel([FromBody] TravelOfferDto travelOfferDto)
        {
            try
            {
                await _travelOfferService.NewTravel(travelOfferDto);
                return StatusCode(201, new { message = "Travel offer created successfully" });
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("DeleteTravel/{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Usuwa ofertę podróży o podanym ID -- ADMIN", Description = "Usuwa ofertę podróży na podstawie identyfikatora.")]
        [SwaggerResponse(200, "Oferta podróży została pomyślnie usunięta.")]
        [SwaggerResponse(400, "Nieprawidłowe ID oferty podróży.")]
        [SwaggerResponse(404, "Oferta podróży nie została znaleziona.")]
        public async Task<IActionResult> DeleteTravel(int id)
        {
            try
            {
                await _travelOfferService.RemoveTravelOffer(id);
                return Ok(new { message = $"Travel offer with id {id} deleted" });
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("UpdateTravel/{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Aktualizuje ofertę podróży o podanym ID -- ADMIN", Description = "Aktualizuje ofertę podróży na podstawie przekazanych danych.")]
        [SwaggerResponse(200, "Oferta podróży została pomyślnie zaktualizowana.")]
        [SwaggerResponse(400, "Nieprawidłowe dane oferty podróży.")]
        public async Task<IActionResult> UpdateTravel([FromBody] TravelOfferDto travelOfferDto, int id)
        {
            try
            {
                await _travelOfferService.UpdateTravelOffer(travelOfferDto);
                return Ok(new { message = $"Travel offer with id {id} updated" });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
