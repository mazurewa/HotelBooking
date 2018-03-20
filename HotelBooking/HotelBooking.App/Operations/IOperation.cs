using HotelBooking.App.DataObjects;
using HotelBooking.Domain.Models;
using System;

namespace HotelBooking.App.Operations
{
    public interface IOperation
    {
        OperationResult Process(Reservation reservation, BookingResult bookingResult);
        bool IsRequiredToSucceed { get; }
        string OperationName { get; }
        Func<Reservation, bool> Execute { get; }
    }
}