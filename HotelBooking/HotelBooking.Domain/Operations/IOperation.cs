using HotelBooking.Domain.DataObjects;
using HotelBooking.Domain.Models;
using System;

namespace HotelBooking.Domain.Operations
{
    public interface IOperation
    {
        OperationResult Process(Reservation reservation);
        string OperationName { get; }
    }
}