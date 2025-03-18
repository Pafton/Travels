using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Application.Dtos;
using Travels.Domain.Entities;

namespace Travels.Application.Profiles
{
    public class BaseProfie : Profile
    {
        public BaseProfie() 
        {
            CreateMap<ReservationDto, Reservation>();
            CreateMap<Reservation, ReservationDto>();
        }
    }
}
