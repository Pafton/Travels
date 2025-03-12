using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travels.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = default!;
        public int TravelOfferId { get; set; }
        public TravelOffer TravelOffer { get; set; } = default!;
        public DateTime ReservationDate { get; set; } = DateTime.UtcNow;
    }
}
