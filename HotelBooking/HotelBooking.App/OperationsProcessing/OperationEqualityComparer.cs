using HotelBooking.App.Operations;
using System.Collections.Generic;

namespace HotelBooking.App.OperationsProcessing
{
    public class OperationEqualityComparer : IEqualityComparer<IOperation>
    {
        public bool Equals(IOperation x, IOperation y)
        {
            return x.OperationName == y.OperationName;
        }

        public int GetHashCode(IOperation obj)
        {
            return obj.OperationName.GetHashCode();
        }
    }
}
