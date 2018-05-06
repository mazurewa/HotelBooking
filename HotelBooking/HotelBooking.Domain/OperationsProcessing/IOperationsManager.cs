using HotelBooking.Domain.DataObjects;
using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.OperationsProcessing
{
    public interface IOperationsManager
    {
        BookingResult ProcessOperations(Reservation reservation);
    }
}
