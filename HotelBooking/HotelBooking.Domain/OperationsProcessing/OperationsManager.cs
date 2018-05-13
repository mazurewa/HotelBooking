using HotelBooking.Domain.Models;
using HotelBooking.Domain.DataObjects;

namespace HotelBooking.Domain.OperationsProcessing
{
    public class OperationsManager : IOperationsManager
    {
        private IOperationsProvider _operationsProvider;
        private ILogger _logger;

        public OperationsManager(IOperationsProvider operationsProvider, ILogger logger)
        {
            _operationsProvider = operationsProvider;
            _logger = logger;
        }

        public BookingResult ProcessOperations(Reservation reservation)
        {
            var orderedOperations = _operationsProvider.GetOrderedOperations(reservation.Hotel);
            var bookingResult = new BookingResult();

            foreach (var operation in orderedOperations)
            {
                var operationResult = operation.Process(reservation);
                LogOperationResult(operationResult);
                bookingResult.OperationResults.Add(operationResult);

                if (operationResult.ExecutionResult == ExecutionResult.Failure)
                {
                    AbortProcess(reservation, operation.OperationName);
                    break;
                }
            }

            return bookingResult;
        }

        private void LogOperationResult(OperationResult operationResult)
        {
            _logger.Write(string.Format($"{operationResult.OperationName} - Result: {operationResult.ExecutionResult}"));
        }

        private void AbortProcess(Reservation reservation, string operationName)
        {
            _logger.Write($"Reservation process aborted due to {operationName} operation failure"); ;
        }
    }
}
