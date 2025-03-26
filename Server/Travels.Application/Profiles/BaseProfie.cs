using AutoMapper;
using Travels.Application.Dtos.Account;
using Travels.Application.Dtos.Reservation;
using Travels.Application.Dtos.Travel;
using Travels.Application.Services;
using Travels.Domain.Entities;

namespace Travels.Application.Profiles
{
    public class BaseProfie : Profile
    {
        public BaseProfie()
        {
            CreateMap<ReservationDto, Reservation>();
            CreateMap<Reservation, ReservationDto>();

            CreateMap<User, RegisterDto>();
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<TravelOffer, TravelOfferDto>();
            CreateMap<TravelOfferDto, TravelOffer>();


        }
    }
}
