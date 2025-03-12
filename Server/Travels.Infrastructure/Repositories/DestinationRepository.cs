using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Domain.Entities;
using Travels.Domain.Interfaces;
using Travels.Infrastructure.Presistance;

namespace Travels.Infrastructure.Repositories
{
    public class DestinationRepository : IDestinationRepository
    {
        private readonly AppDbContext _appDbContext;
        public DestinationRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddDestination(Destination destination)
        {
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            await _appDbContext.Destinations.AddAsync(destination);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task ChangeDestination(Destination destination)
        {
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            _appDbContext.Destinations.Update(destination);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteDestination(int id)
        {
            var destination = await _appDbContext.Destinations.FindAsync(id);
            if (destination == null)
                throw new KeyNotFoundException($"Destination with id {id} not found.");

            _appDbContext.Destinations.Remove(destination);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Destination?> GetDestination(int? id)
        {
            if (id == null)
                return null;

            return await _appDbContext.Destinations
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Destination>> GetDestinations()
        {
            return await _appDbContext.Destinations.ToListAsync();
        }
    }
}
