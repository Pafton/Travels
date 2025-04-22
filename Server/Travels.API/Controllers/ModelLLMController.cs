using Microsoft.AspNetCore.Mvc;
using Travels.Application.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Travels.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelLLMController : ControllerBase
    {
        private readonly IModelLLMService _modelLLMService;

        public ModelLLMController(IModelLLMService modelLLMService)
        {
            _modelLLMService = modelLLMService;
        }

        // Endpoint do pobierania obiektów turystycznych
        [HttpGet("list")]
        public async Task<IActionResult> GetListFromLLM()
        {
            try
            {
                var list = await _modelLLMService.GetListFromLLMAsync();

                if (list == null || !list.Any())
                {
                    return NotFound("Brak obiektów turystycznych.");
                }

                return Ok(list);  // Zwrócenie listy obiektów
            }
            catch (Exception ex)
            {
                return BadRequest($"Wystąpił błąd: {ex.Message}");
            }
        }

        // Endpoint do pobrania obiektu po ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetObjectById(int id)
        {
            try
            {
                var selectedObject = await _modelLLMService.GetObjectByIdAsync(id);

                if (selectedObject == null)
                {
                    return NotFound("Obiekt o podanym numerze nie został znaleziony.");
                }

                return Ok(selectedObject);
            }
            catch (Exception ex)
            {
                return BadRequest($"Wystąpił błąd: {ex.Message}");
            }
        }

        // Endpoint do filtrowania obiektów turystycznych
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredObjects([FromQuery] string filter)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filter))
                {
                    return BadRequest("Filtr nie może być pusty.");
                }

                var filteredList = await _modelLLMService.GetFilteredObjectsAsync(filter);

                if (filteredList == null || !filteredList.Any())
                {
                    return NotFound("Nie znaleziono obiektów spełniających podane kryterium.");
                }

                return Ok(filteredList);  // Zwrócenie przefiltrowanej listy
            }
            catch (Exception ex)
            {
                return BadRequest($"Wystąpił błąd: {ex.Message}");
            }
        }
    }
}
