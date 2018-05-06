using HotelBooking.Domain.Operations;
using HotelBooking.Domain.Models;
using System.Collections.Generic;

namespace HotelBooking.Domain.OperationsProcessing
{
    public interface IOperationsProvider
    {
        IList<IOperation> GetOrderedOperations(Hotel hotel);
    }
}
