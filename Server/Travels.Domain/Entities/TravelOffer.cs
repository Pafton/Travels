namespace Travels.Domain.Entities
{
    public class TravelOffer
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public double Price { get; set; }
        public DateOnly Begin { get; set; }
        public DateOnly End { get; set; }
        public int AvailableSpots { get; set; }
        public int DestinationId { get; set; }
        public virtual Destination Destination { get; set; } = default!;
        public virtual ICollection<TravelOfferImage> TravelOfferImages { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; } = default!;
        public virtual ICollection<Hotel> Hotels { get; set; } = default!;
        public virtual ICollection<Transport> Transports { get; set; } = default!;
        public virtual ICollection<Review> Reviews { get; set; } = default!;
    }
}
