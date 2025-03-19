using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Application.Dtos.Travel;
using Travels.Application.Interfaces;
using Travels.Domain.Interfaces;

namespace Travels.Application.Services
{
    public class TravelOfferService : ITravelOfferService
    {
        private readonly ITravelOfferRepository _travelOfferRepository;
        private readonly IMapper _mapper;
        public TravelOfferService(ITravelOfferRepository travelOfferRepository, IMapper mapper)
        {
            _travelOfferRepository = travelOfferRepository;
            _mapper = mapper;
        }
        public async Task<TravelOfferDto?> GetTravel(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException($"Id must be grater by 0 , or This id {id} do not exists");

            var travelOffer = await _travelOfferRepository.GetTravel(id);
            if (travelOffer == null)
                throw new ArgumentNullException("Travel offer not found");

            var travelOfferDto = _mapper.Map<TravelOfferDto>(travelOffer);
            return travelOfferDto;
        }

        public async Task<IEnumerable<TravelOfferDto>> GetTravels()
        {
            var travelOffers =  await _travelOfferRepository.GetTravelOffers();
            var travelOfferDto = _mapper.Map<IEnumerable<TravelOfferDto>>(travelOffers);
            return travelOfferDto;
        }

        public Task NewTravel(TravelOfferDto travelOfferDto)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTravelOffer(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTravelOffer(TravelOfferDto travelOfferDto)
        {
            throw new NotImplementedException();
        }
    }
}
