using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travels.Domain.Entities
{
    public class TravelOffer
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public double Price { get; set; }
        public DateOnly Begin {  get; set; }
        public DateOnly End { get; set; }
        public int AvailableSpots { get; set; }
    }
}
