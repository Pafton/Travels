using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travels.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; } = default!;
        public int Rating { get; set; } 
        public DateTime Date { get; set; }
        public string UserIdentifier { get; set; } = default!;
        public int TravelOfferId { get; set; }
        public virtual TravelOffer TravelOffer { get; set; } = default!;
        public bool IsEditable { get; set; }
    }
}
