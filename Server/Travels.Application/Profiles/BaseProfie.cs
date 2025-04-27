using AutoMapper;
using Travels.Application.Dtos.Account;
using Travels.Application.Dtos.Reservation;
using Travels.Application.Dtos.Review;
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

            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>
                    src.UserId.HasValue ? src.User.Name : src.NotLogginUser));

            CreateMap<ReviewDto, Review>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.NotLogginUser, opt => opt.Ignore());

            CreateMap<User, UserListItemDto>();
        }
    }
}
