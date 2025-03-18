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
        public virtual User User { get; set; } = default!;
        public int TravelOfferId { get; set; }
        public virtual TravelOffer TravelOffer { get; set; } = default!;
        public DateTime ReservationDate { get; set; }
        public bool Status { get; set; } = false;
    }
}
