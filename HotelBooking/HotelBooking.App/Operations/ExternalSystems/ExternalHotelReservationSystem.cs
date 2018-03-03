using HotelBooking.Domain.Models;
using System;

namespace HotelBooking.App.Operations.ExternalSystems
{
    public class ExternalHotelReservationSystem : IExternalHotelReservationSystem
    {
        public bool BookReservation(Reservation reservation)
        {
            return true;
        }
    }
}
