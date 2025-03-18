namespace Travels.Application.Dtos
{
    public class ReservationDto
    {
        public int UserId { get; set; }
        public int TravelOfferId { get; set; }
        public DateTime ReservationDate { get; set; }
        public bool Status { get; set; }
    }
}
