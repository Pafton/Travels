using System.Collections.Generic;
using System.Threading.Tasks;
using Travels.Domain.Entities;

namespace Travels.Application.Interfaces
{
    public interface IModelLLMService
    {
        Task<List<MyModel>> GetListFromLLMAsync();
        Task<MyModel> GetObjectByIdAsync(int id);
        Task<List<MyModel>> GetFilteredObjectsAsync(string filter);
    }
}
