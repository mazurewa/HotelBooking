using System;
using HotelBooking.Domain.Models;

namespace HotelBooking.App.Operations.Services
{
    public class PaymentSystem
    {
        public virtual bool Pay(Reservation reservation)
        {
            return true;
        }
    }
}
