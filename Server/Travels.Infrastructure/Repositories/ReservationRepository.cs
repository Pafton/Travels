using Microsoft.EntityFrameworkCore;
using Travels.Domain.Entities;
using Travels.Domain.Interfaces;
using Travels.Infrastructure.Presistance;

namespace Travels.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _appDbContext;
        public ReservationRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddReservation(Reservation reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation));

            await _appDbContext.Reservations.AddAsync(reservation);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task ChangeReservation(Reservation reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation));

            _appDbContext.Reservations.Update(reservation);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteReservation(int id)
        {
            var reservation = await _appDbContext.Reservations.FindAsync(id);
            if (reservation == null)
                throw new KeyNotFoundException($"Reservation with id {id} not found.");

            _appDbContext.Reservations.Remove(reservation);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Reservation?> GetReservation(int? id)
        {
            if (id == null)
                return null;

            return await _appDbContext.Reservations
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            return await _appDbContext.Reservations.ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserId(int userId)
        {
            return await _appDbContext.Reservations.Where(r => r.UserId == userId).ToListAsync();
        }

    }
}
