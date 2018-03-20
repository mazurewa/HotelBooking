using System;
using HotelBooking.Domain.Models;

namespace HotelBooking.App.Operations.ExternalSystems
{
    public class ExternalPaymentSystem
    {
        public virtual bool Pay(Reservation reservation)
        {
            return true;
        }
    }
}
