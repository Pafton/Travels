using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Domain.Entities;

namespace Travels.Application.Dtos.Review
{
    public class ReviewDto
    {
        public int Id {  get; set; }
        public string Comment { get; set; } = default!;
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public string? UserName { get; set; } = default!;
        public int TravelOfferId { get; set; }
    }
}
