using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Domain.Entities;

namespace Travels.Domain.Interfaces
{
    public interface IHotelRepository
    {
        Task AddHotel(Hotel hotel);
        Task<IEnumerable<Hotel>> GetHotels();
        Task<Hotel?> GetHotel(int? id);
        Task ChangeHotel(Hotel hotel);
        Task DeleteHotel(int id);
    }
}
