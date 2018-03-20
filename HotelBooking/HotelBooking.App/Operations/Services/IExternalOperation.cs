using HotelBooking.Domain.Models;
using System;

namespace HotelBooking.App.Operations.ExternalSystems
{
    interface IExternalOperation
    {
        Func<Reservation, bool> Execute { get; }
    }
}
