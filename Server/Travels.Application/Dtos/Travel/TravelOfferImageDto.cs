using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travels.Application.Dtos.Travel
{
    public class TravelOfferImageDto
    {
        public int Id { get; set; }
        public byte[] ImageData { get; set; } = Array.Empty<byte>();
        public int TravelOfferId { get; set; }
    }
}
