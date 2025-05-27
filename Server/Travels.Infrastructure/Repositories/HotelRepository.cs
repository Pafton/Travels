using Microsoft.EntityFrameworkCore;
using Travels.Domain.Entities;
using Travels.Domain.Interfaces;
using Travels.Infrastructure.Presistance;

namespace Travels.Infrastructure.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly AppDbContext _appDbContext;
        public HotelRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddHotel(Hotel hotel)
        {
            if (hotel == null)
                throw new ArgumentNullException(nameof(hotel));

            await _appDbContext.Hotels.AddAsync(hotel);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task ChangeHotel(Hotel hotel)
        {
            if (hotel == null)
                throw new ArgumentNullException(nameof(hotel));

            _appDbContext.Hotels.Update(hotel);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteHotel(int id)
        {
            var hotel = await _appDbContext.Hotels.FindAsync(id);
            if (hotel == null)
                throw new KeyNotFoundException($"Hotel with id {id} not found.");

            _appDbContext.Hotels.Remove(hotel);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Hotel?> GetHotel(int? id)
        {
            if (id == null)
                return null;

            return await _appDbContext.Hotels
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<Hotel>> GetHotels()
        {
            return await _appDbContext.Hotels.ToListAsync();
        }
    }
}
