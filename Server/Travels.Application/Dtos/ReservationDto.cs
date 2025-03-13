using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travels.Application.Dtos
{
    public class ReservationDto
    {
        public int UserId { get; set; }
        public int TravelOfferId { get; set; }
        public DateTime ReservationDate { get; set; }
        public bool Status { get; set; }
        public int TotalAmount { get; set; }
    }

}
