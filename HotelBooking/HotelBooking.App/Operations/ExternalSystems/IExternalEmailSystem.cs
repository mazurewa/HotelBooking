using HotelBooking.Domain.Models;

namespace HotelBooking.App.Operations.ExternalSystems
{
    public interface IExternalEmailSystem
    {
        bool SendConfirmationEmail(Reservation reservation);
        bool SendRejectionEmail(Reservation reservation);
    }
}
