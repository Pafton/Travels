using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Application.Dtos.Reservation;
using Travels.Application.Dtos.Review;

namespace Travels.Application.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool IsActivate {  get; set; }
        public ICollection<ReviewDto> Reviews { get; set; } = default!;
        public ICollection<ReservationDto> Reservations { get; set; } = default!;
    }
}
