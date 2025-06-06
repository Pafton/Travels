using Travels.Application.Dtos.Reservation;

namespace Travels.Application.Interfaces
{
    public interface IReservationService
    {
        Task StartReservation(ReservationDto reservationDto);
        Task<ReservationDto?> GetReservation(int id);
        Task<IEnumerable<ReservationDto?>> GetReservationsByUserId(int userId);
        Task UpdateReservation(ReservationDto reservationDto, int id);
        Task CancelReservation(int id);
    }
}
