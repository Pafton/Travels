using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Application.Dtos.Travel;
using Travels.Application.Interfaces;
using Travels.Domain.Entities;
using Travels.Domain.Interfaces;

namespace Travels.Application.Services
{
    public class TravelOfferService : ITravelOfferService
    {
        private readonly ITravelOfferRepository _travelOfferRepository;
        private readonly IDestinationRepository _destinationRepository;
        private readonly IMapper _mapper;
        public TravelOfferService(ITravelOfferRepository travelOfferRepository,IDestinationRepository destinationRepository, IMapper mapper)
        {
            _travelOfferRepository = travelOfferRepository;
            _destinationRepository = destinationRepository;
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

            if(travelOffers == null)
                throw new ArgumentNullException("Not found any travel offer");
            var travelOfferDto = _mapper.Map<IEnumerable<TravelOfferDto>>(travelOffers);
            return travelOfferDto;
        }

        public async Task NewTravel(TravelOfferDto travelOfferDto)
        {
            if(travelOfferDto == null)
                throw new ArgumentNullException(nameof(travelOfferDto));

            var destination = await _destinationRepository.GetDestination(travelOfferDto.DestinationId);
            if(destination == null)
                throw new ArgumentNullException("Destination offer not found");

            var travel = _mapper.Map<TravelOffer>(travelOfferDto);
            await _travelOfferRepository.AddTravelOffer(travel);
        }

        public async Task RemoveTravelOffer(int id)
        {
            if(id < 0)
                throw new ArgumentOutOfRangeException($"Id must be grater by 0 , or This id {id} do not exists");

            var travelOffer = await _travelOfferRepository.GetTravel(id);
            if(travelOffer == null)
                throw new ArgumentNullException("Travel offer not found");

            await _travelOfferRepository.DeleteTravelOffer(id);
        }

        public async Task UpdateTravelOffer(TravelOfferDto travelOfferDto)
        {
            if(travelOfferDto == null)
                throw new ArgumentNullException(nameof(travelOfferDto));

            var travel = _mapper.Map<TravelOffer>(travelOfferDto);
            await _travelOfferRepository.ChangeTravelOffer(travel);
        }
    }
}
