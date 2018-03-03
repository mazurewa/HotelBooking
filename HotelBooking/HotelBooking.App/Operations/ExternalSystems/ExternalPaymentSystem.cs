using System;
using HotelBooking.Domain.Models;

namespace HotelBooking.App.Operations.ExternalSystems
{
    public class ExternalPaymentSystem : IExternalPaymentSystem
    {
        public bool Pay(Reservation reservation)
        {
            return true;
        }
    }
}
