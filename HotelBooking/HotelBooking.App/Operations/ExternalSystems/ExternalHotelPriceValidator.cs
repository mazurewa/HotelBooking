using HotelBooking.Domain.Models;
using System;

namespace HotelBooking.App.Operations.ExternalSystems
{
    public class ExternalHotelPriceValidator
    {
        public virtual bool ValidatePrice(Reservation reservation)
        {
            //var currentPrice = reservation.Cost;
            var currentPrice = reservation.Cost + 10;

            return currentPrice == reservation.Cost;
        }       
    }
}
