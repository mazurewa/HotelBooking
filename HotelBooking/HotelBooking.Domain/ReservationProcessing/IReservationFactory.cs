using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.ReservationProcessing
{
    public interface IReservationFactory
    {
        Reservation CreateReservation(Options options);
    }
}
