using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Domain.Entities;

namespace Travels.Domain.Interfaces
{
    public interface ITravelOfferRepository
    {
        Task AddTravelOffer(TravelOffer travelOffer);
        Task<IEnumerable<TravelOffer>> GetTravelOffers();
        Task<TravelOffer?> GetTravel(int? id);
        Task ChangeTravelOffer(TravelOffer travelOffer);
        Task DeleteTravelOffer(int id);
    }
}
