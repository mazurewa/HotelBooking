﻿using System;
using HotelBooking.Domain.Models;
using HotelBooking.App.Operations.ExternalSystems;
using HotelBooking.Domain.Enums;

namespace HotelBooking.App.Operations
{
    public class ExternalReservationSystemManager : OperationBase
    {
        private IExternalHotelReservationSystem _hotelReservationSystem;

        public ExternalReservationSystemManager(IExternalHotelReservationSystem hotelReservationSystem)
        {
            _hotelReservationSystem = hotelReservationSystem;
        }

        public override bool IsRequiredToSucceed => true;
        public override string OperationName => OperationCode.ExternalReservation;
        public override Func<Reservation, bool> Execute => BookReservation;

        private bool BookReservation(Reservation reservation)
        {
            return _hotelReservationSystem.BookReservation(reservation);
        }
    }
}
