using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Domain.Entities;

namespace Travels.Domain.Interfaces
{
    public interface ITransportRepository
    {
        Task AddTransport(Transport transport);
        Task<IEnumerable<Transport>> GetTransports();
        Task<Transport?> GetTransport(int? id);
        Task ChangeTransport(Transport transport);
        Task DeleteTransport(int id);
    }
}
