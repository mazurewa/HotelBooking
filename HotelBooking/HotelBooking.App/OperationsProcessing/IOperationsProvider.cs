using HotelBooking.App.Operations;
using HotelBooking.Domain.Models;
using System.Collections.Generic;

namespace HotelBooking.App.OperationsProcessing
{
    public interface IOperationsProvider
    {
        IList<IOperation> GetOrderedOperations(Hotel hotel);
        bool ContainsAllRequiredOperations(IEnumerable<IOperation> operationsToRun);
    }
}
