using Microsoft.EntityFrameworkCore;
using Travels.Domain.Entities;
using Travels.Domain.Interfaces;
using Travels.Infrastructure.Presistance;

namespace Travels.Infrastructure.Repositories
{
    public class TravelOfferRepository : ITravelOfferRepository
    {
        private readonly AppDbContext _appDbContext;
        public TravelOfferRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddTravelOffer(TravelOffer travelOffer)
        {
            if (travelOffer == null)
                throw new ArgumentNullException(nameof(travelOffer));
            await _appDbContext.TravelOffers.AddAsync(travelOffer);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task ChangeTravelOffer(TravelOffer travelOffer)
        {
            if (travelOffer == null)
                throw new ArgumentNullException(nameof(travelOffer));
            _appDbContext.TravelOffers.Update(travelOffer);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteTravelOffer(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            var travelOffer = await _appDbContext.TravelOffers.FirstOrDefaultAsync(to => to.Id == id);
            if (travelOffer == null)
                throw new KeyNotFoundException($"User with id {id} not found.");

            _appDbContext.TravelOffers.Remove(travelOffer);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<TravelOffer?> GetTravel(int? id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            return await _appDbContext.TravelOffers.FirstOrDefaultAsync(to => to.Id == id);
        }

        public async Task<IEnumerable<TravelOffer>> GetTravelOffers()
        {
            return await _appDbContext.TravelOffers.ToListAsync();
        }
        public async Task<IEnumerable<Destination>> GetDestinations()
        {
            return await _appDbContext.Destinations.ToListAsync();
        }
    }
}
