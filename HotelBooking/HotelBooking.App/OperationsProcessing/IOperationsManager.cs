using HotelBooking.App.DataObjects;
using HotelBooking.Domain.Models;

namespace HotelBooking.App.OperationsProcessing
{
    public interface IOperationsManager
    {
        BookingResult ProcessOperations(Reservation reservation);
    }
}
