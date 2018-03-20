using System;
using HotelBooking.Domain.Models;
using HotelBooking.App.Operations.Services;
using HotelBooking.Domain.Enums;

namespace HotelBooking.App.Operations
{
    public class ReservationSystemManager : OperationBase
    {
        private HotelReservationSystem _hotelReservationSystem;

        public ReservationSystemManager(HotelReservationSystem hotelReservationSystem)
        {
            _hotelReservationSystem = hotelReservationSystem;
        }

        public override bool IsRequiredToSucceed => true;
        public override string OperationName => OperationCode.Reservation;
        public override Func<Reservation, bool> Execute => BookReservation;

        private bool BookReservation(Reservation reservation)
        {
            return _hotelReservationSystem.BookReservation(reservation);
        }
    }
}
