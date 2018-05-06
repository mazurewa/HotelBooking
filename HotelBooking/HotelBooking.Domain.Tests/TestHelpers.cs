using HotelBooking.Domain.ReservationProcessing;
using System;

namespace HotelBooking.App.Tests
{
    public static class TestHelpers
    {
        public static Options GetOptions()
        {
            return new Options
            {
                HotelId = "003",
                ReservationDate = new DateTime(2008, 1, 1, 14, 50, 40),
                Cost = 123.34,
                CreditCard = "1111 2222 3333 4444",
                CustomerEmail = "test@test.pl"
            };
        }
    }
}