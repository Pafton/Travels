using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Application.Dtos.Reservation;
using Travels.Application.Dtos.Travel;

namespace Travels.Application.Interfaces
{
    public interface ITravelOfferService
    {
        Task NewTravel(TravelOfferDto travelOfferDto);
        Task<TravelOfferDto?> GetTravel(int id);
        Task<IEnumerable<TravelOfferDto>> GetTravels();
        Task UpdateTravelOffer(TravelOfferDto travelOfferDto);
        Task RemoveTravelOffer(int id);
    }
}
