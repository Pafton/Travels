using Microsoft.AspNetCore.Mvc;
using Travels.Application.Interfaces;
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

        // Endpoint do pobrania odpowiedzi z Gemini
        [HttpGet("response")]
        public async Task<IActionResult> GetLLMResponse()
        {
            try
            {
                // Używamy właściwej metody GetResponseFromGeminiAsync
                var prompt = "Podaj mi 10 najciekawszych miejsc na podróż. Odpowiedz zwroc w postacji json. Kazde miejse ma miec id";
                var response = await _modelLLMService.GetResponseFromGeminiAsync(prompt);
                return Ok(response);  // Zwracamy odpowiedź jako tekst
            }
            catch (Exception ex)
            {
                return BadRequest($"Wystąpił błąd: {ex.Message}");
            }
        }

        [HttpGet("response/{id}")]
        public async Task<IActionResult> GetObjectById(int id)
        {
            try
            {
                // Pobranie listy obiektów
                var list = await _modelLLMService.GetListFromLLMAsync();

                // Wyszukiwanie obiektu po ID (w tym przypadku po indeksie)
                var selectedObject = list.FirstOrDefault(x => x.Index == id);

                if (selectedObject == null)
                {
                    return NotFound("Obiekt o podanym numerze nie został znaleziony.");
                }

                return Ok(selectedObject);  // Zwracamy znaleziony obiekt
            }
            catch (Exception ex)
            {
                return BadRequest($"Wystąpił błąd: {ex.Message}");
            }
        }

        [HttpGet("response/filter")]
        public async Task<IActionResult> GetFilteredObjects([FromQuery] string filter)
        {
            try
            {
                // Pobranie listy obiektów
                var list = await _modelLLMService.GetListFromLLMAsync();

                // Filtrowanie obiektów na podstawie typu (przykład z filtrowaniem po 'Type')
                var filteredList = list.Where(x => x.Type.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();

                if (!filteredList.Any())
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
