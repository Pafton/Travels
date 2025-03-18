using AutoMapper;
using Travels.Application.Dtos;
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
            CreateMap<RegisterDto, User>();

            CreateMap<User, LoginDto>();
            CreateMap<LoginDto, User>();
        }
    }
}
