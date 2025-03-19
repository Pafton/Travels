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
