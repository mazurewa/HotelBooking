using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.DataAccess
{
    public interface IHotelsRepository
    {
        Hotel GetById(string id);
    }
}
