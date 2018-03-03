using HotelBooking.Domain.Models;

namespace HotelBooking.App.Operations.ExternalSystems
{
    public interface IExternalHotelReservationSystem
    {
        bool BookReservation(Reservation reservation);
    }
}
