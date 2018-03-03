using HotelBooking.Domain.Models;

namespace HotelBooking.App.ReservationProcessing
{
    public interface IReservationFactory
    {
        Reservation CreateReservation(Options options);
    }
}
