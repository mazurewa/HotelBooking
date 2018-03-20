using System.Collections.Generic;
using System.Linq;
using HotelBooking.Domain.Models;
using HotelBooking.App.Operations;

namespace HotelBooking.App.OperationsProcessing
{
    public class OperationsProvider : IOperationsProvider
    {
        private IOperation[] _bookingOperations;

        public OperationsProvider(IOperation[] bookingOperations)
        {
            _bookingOperations = bookingOperations;
        }

        public IList<IOperation> GetOrderedOperations(Hotel hotel)
        {
            var operationsOrder = hotel.OperationOrder;
            var orderedOperations = new List<IOperation>();

            foreach (var operation in operationsOrder)
            {
                var selectedOperation = _bookingOperations.Where(x => x.OperationName == operation).FirstOrDefault();
                if (selectedOperation != null)
                {
                    orderedOperations.Add(selectedOperation);
                }
            }

            if (!ContainsAllRequiredOperations(orderedOperations))
            {
                return new List<IOperation>();
            }

            return orderedOperations;
        }

        public bool ContainsAllRequiredOperations(IEnumerable<IOperation> operationsToRun)
        {
            var requiredOperations = _bookingOperations.Where(x => x.IsRequiredToSucceed).ToList();
            return operationsToRun.Intersect(requiredOperations, new OperationEqualityComparer()).Count() 
                == requiredOperations.Count();
        }
    }
}
