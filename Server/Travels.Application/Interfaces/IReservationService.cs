using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Application.Dtos;

namespace Travels.Application.Interfaces
{
    public interface IReservationService
    {
        Task StartReservation(ReservationDto reservationDto);
        Task<ReservationDto?> GetReservation(int id);
        Task UpdateReservation(ReservationDto reservationDto, int id);
        Task CancelReservation(int id);
    }
}
