using HotelBooking.App.DataObjects;
using HotelBooking.Domain.Models;

namespace HotelBooking.App.ReservationProcessing
{
    public interface IReservationManager
    {
        BookingResult ProcessReservation(string[] args);
    }
}