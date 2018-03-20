using HotelBooking.Domain.Models;
using System;

namespace HotelBooking.App.Operations.ExternalSystems
{
    public class ExternalHotelReservationSystem
    {
        public virtual bool BookReservation(Reservation reservation)
        {
            return true;
        }
    }
}
