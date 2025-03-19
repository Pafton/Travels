using Travels.Domain.Entities;

namespace Travels.Domain.Interfaces
{
    public interface IDestinationRepository
    {
        Task AddDestination(Destination destination);
        Task<IEnumerable<Destination>> GetDestinations();
        Task<Destination?> GetDestination(int? id);
        Task ChangeDestination(Destination destination);
        Task DeleteDestination(int id);
    }
}
