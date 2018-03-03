using HotelBooking.Domain.Models;

namespace HotelBooking.App.Operations.ExternalSystems
{
    public interface IExternalPaymentSystem
    {
        bool Pay(Reservation reservation);
    }
}
