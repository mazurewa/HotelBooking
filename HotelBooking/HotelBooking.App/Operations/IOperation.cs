using HotelBooking.App.DataObjects;
using HotelBooking.Domain.Models;
using System;

namespace HotelBooking.App.Operations
{
    public interface IOperation
    {
        void Process(Reservation reservation, BookingResult bookingResult);
        OperationResult OperationResult { get; set; }
        bool IsRequiredToSucceed { get; }
        string OperationName { get; }
        Func<Reservation, bool> Execute { get; }
    }
}