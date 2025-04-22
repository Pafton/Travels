using System.Collections.Generic;
using System.Threading.Tasks;
using Travels.Domain.Entities;

namespace Travels.Application.Interfaces
{
    public interface IModelLLMService
    {
        Task<List<MyModel>> GetListFromLLMAsync();  // Pobiera listę obiektów turystycznych
        Task<MyModel> GetObjectByIdFromLLMAsync(int id);  // Pobiera obiekt na podstawie ID
        Task<List<MyModel>> GetFilteredObjectsFromLLMAsync(string filter);  // Filtrowanie obiektów na podstawie typu
        Task<string> GetResponseFromGeminiAsync(string prompt);  // Wysyłanie zapytania do Gemini
    }
}
