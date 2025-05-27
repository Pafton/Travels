using Microsoft.EntityFrameworkCore;
using Travels.Domain.Entities;
using Travels.Domain.Interfaces;
using Travels.Infrastructure.Presistance;

namespace Travels.Infrastructure.Repositories
{
    public class TransportRepository : ITransportRepository
    {
        private readonly AppDbContext _appDbContext;
        public TransportRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddTransport(Transport transport)
        {
            if (transport == null)
                throw new ArgumentNullException(nameof(transport));

            await _appDbContext.Transports.AddAsync(transport);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task ChangeTransport(Transport transport)
        {
            if (transport == null)
                throw new ArgumentNullException(nameof(transport));

            _appDbContext.Transports.Update(transport);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteTransport(int id)
        {
            var transport = await _appDbContext.Transports.FindAsync(id);
            if (transport == null)
                throw new KeyNotFoundException($"Transport with id {id} not found.");

            _appDbContext.Transports.Remove(transport);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Transport?> GetTransport(int? id)
        {
            if (id == null)
                return null;

            return await _appDbContext.Transports
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Transport>> GetTransports()
        {
            return await _appDbContext.Transports.ToListAsync();
        }
    }
}
