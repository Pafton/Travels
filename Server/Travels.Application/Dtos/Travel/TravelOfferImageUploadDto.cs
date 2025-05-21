using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travels.Application.Dtos.Travel
{
    public class TravelOfferImageUploadDto
    {
        public int TravelOfferId { get; set; }
        public IFormFile ImageFile { get; set; } = default!;
    }
}
