using System.Collections.Generic;
using System.Linq;
using HotelBooking.Domain.Models;

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

        public IList<Operation> GetOrderedOperations(Hotel hotel)
        {
            var hotelOperations = hotel.HotelConfiguration.OperationOrder;
            var orderedOperations = new List<Operation>();

            foreach (var operationName in hotelOperations)
            {
                var selectedOperation = AllOperations.Where(x => x == operationName).FirstOrDefault();
                if (selectedOperation != null)
                {
                    orderedOperations.Add(new Operation(selectedOperation));
                }
            }

            return orderedOperations;
        }
    }
}
