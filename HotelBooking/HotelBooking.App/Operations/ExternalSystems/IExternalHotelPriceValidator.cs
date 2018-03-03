using HotelBooking.Domain.Models;

namespace HotelBooking.App.Operations.ExternalSystems
{
    public interface IExternalHotelPriceValidator
    {
        bool ValidatePrice(Reservation reservation);
    }
}
