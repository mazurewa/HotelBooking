using System.Collections.Generic;

namespace HotelBooking.Domain.OperationsProcessing
{
    public interface IAllOperationsProvider
    {
        IEnumerable<string> GetAllOperations();
    }
}