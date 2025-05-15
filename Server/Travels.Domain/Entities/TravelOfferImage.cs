using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travels.Domain.Entities
{
    public class TravelOfferImage
    {
        public int Id { get; set; }
        public string Image {  get; set; } = string.Empty;
        public int TravelOfferId { get; set; }
        public virtual TravelOffer TravelOffer { get; set; } = default!;
    }
}
