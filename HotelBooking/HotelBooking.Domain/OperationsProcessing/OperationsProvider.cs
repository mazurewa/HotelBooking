using System.Collections.Generic;
using System.Linq;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Operations;

namespace HotelBooking.Domain.OperationsProcessing
{
    public class OperationsProvider : IOperationsProvider
    {
        private IAllOperationsProvider _allOperationsProvider;

        public OperationsProvider(IAllOperationsProvider allOperationsProvider)
        {
            _allOperationsProvider = allOperationsProvider;
        }

        IEnumerable<string> _allOperations;
        public IEnumerable<string> AllOperations
        {
            get
            {
                if (_allOperations == null)
                {
                    _allOperations = _allOperationsProvider.GetAllOperations();
                }
                return _allOperations;
            }
            set
            {
                _allOperations = value;
            }
        }

        public IList<IOperation> GetOrderedOperations(Hotel hotel)
        {
            var hotelOperations = hotel.HotelConfiguration.OperationOrder;
            var orderedOperations = new List<IOperation>();

            foreach (var operationConfiguration in hotelOperations)
            {
                var selectedOperation = AllOperations.Where(x => x == operationConfiguration.Name).FirstOrDefault();
                if (selectedOperation != null)
                {
                    orderedOperations.Add(new Operation(operationConfiguration));
                }
            }

            return orderedOperations;
        }
    }
}
