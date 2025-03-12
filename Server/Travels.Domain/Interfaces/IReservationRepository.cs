using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Domain.Entities;

namespace Travels.Domain.Interfaces
{
    public interface IReservationRepository
    {
        Task AddReservation(Reservation reservation);
        Task<IEnumerable<Reservation>> GetReservations();
        Task<Reservation?> GetReservation(int? id);
        Task ChangeReservation(Reservation reservation);
        Task DeleteReservation(int id);
    }
}
