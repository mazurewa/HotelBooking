using HotelBooking.Domain.Models;
using System.Collections.Generic;

namespace HotelBooking.Domain.OperationsProcessing
{
    public interface IOperationsProvider
    {
        IList<Operation> GetOrderedOperations(Hotel hotel);
    }
}
