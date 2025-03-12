using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travels.Domain.Entities
{
    public class Customer : User
    {
        public ICollection<Reservation> Reservations { get; set; } = default!;
    }
}
