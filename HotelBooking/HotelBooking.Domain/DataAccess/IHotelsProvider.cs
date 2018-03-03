using HotelBooking.Domain.Models;
using System.Collections.Generic;

namespace HotelBooking.Domain.DataAccess
{
    public interface IHotelsProvider
    {
        IEnumerable<Hotel> GetHotels();
    }
}
