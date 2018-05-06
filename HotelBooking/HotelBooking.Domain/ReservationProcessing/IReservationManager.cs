using HotelBooking.Domain.DataObjects;

namespace HotelBooking.Domain.ReservationProcessing
{
    public interface IReservationManager
    {
        BookingResult ManageReservation(string[] args);
    }
}