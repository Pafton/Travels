using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Application.Dtos;
using Travels.Application.Interfaces;
using Travels.Domain.Entities;
using Travels.Domain.Interfaces;

namespace Travels.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITravelOfferRepository _travelOfferRepository;
        private readonly IMapper _mapper;
        public ReservationService(IReservationRepository reservationRepository,  IUserRepository userRepository, ITravelOfferRepository travelOfferRepository ,IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _travelOfferRepository = travelOfferRepository;
            _mapper = mapper;
        }

        public async Task CancelReservation(int id)
        {
            if(id < 0)
                throw new ArgumentOutOfRangeException($"Id must be grater by 0 , or This id {id} do not exists");

            var reservation = await _reservationRepository.GetReservation(id);
            if (reservation == null)
                throw new KeyNotFoundException("Reservation not found");

            await _reservationRepository.DeleteReservation(id);
        }

        public async Task StartReservation(ReservationDto reservationDto)
        {
            if (reservationDto == null)
                throw new ArgumentNullException(nameof(reservationDto));

            var user = await _userRepository.GetUser(reservationDto.UserId);
            if (user == null)
                throw new Exception("User not found");

            var offer = await _travelOfferRepository.GetTravel(reservationDto.TravelOfferId);
            if (offer == null)
                throw new Exception("Offer not found");

            var reservation = _mapper.Map<Reservation>(reservationDto);
            await _reservationRepository.AddReservation(reservation);
        }

        public async Task UpdateReservation(ReservationDto reservationDto, int id)
        {
            if(reservationDto == null)
                throw new ArgumentNullException(nameof(reservationDto));

            if (id < 0)
                throw new ArgumentOutOfRangeException($"Id must be grater by 0 , or This id {id} do not exists");

            var reservation = await _reservationRepository.GetReservation(id);
            if (reservation == null)
                throw new Exception("Reservation not found");


            reservation.ReservationDate = reservationDto.ReservationDate;
            reservation.TravelOfferId = reservationDto.TravelOfferId;
            reservation.Status = reservationDto.Status;
            reservation.TotalAmount = reservationDto.TotalAmount;

            await _reservationRepository.ChangeReservation(reservation);
        }
    }
}
