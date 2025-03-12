using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travels.Domain.Entities
{
    public class Transport
    {
        public int Id { get; set; }
        public string Type { get; set; } = default!;
        public string Company { get; set; } = default!;
        public virtual ICollection<TravelOffer> TravelOffers { get; set; } = default!;
    }
}
