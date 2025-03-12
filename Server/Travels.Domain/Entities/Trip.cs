using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travels.Domain.Entities
{
    public class Trip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public int DestinationId { get; set; }
        public Destination Destination { get; set; } = default!;
        public virtual ICollection<Hotel> Hotels { get; set; } = default!;
        public virtual ICollection<Transport> Transports { get; set; } = default!;
        public virtual ICollection<Review> Reviews { get; set; } = default!;
    }
}
