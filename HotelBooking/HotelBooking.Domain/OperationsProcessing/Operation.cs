using HotelBooking.Domain.DataObjects;
using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.OperationsProcessing
{
    
    public class Operation
    {
        public string OperationName { get; }

        public Operation(string operationName)
        {
            OperationName = operationName;
        }

        public OperationResult Process(Reservation reservation)
        {
            var executionResult = Execute(reservation);

            var operationResult =  new OperationResult
            {
                ExecutionResult = executionResult,
                OperationName = OperationName
            };

            return operationResult;
        }

        protected internal virtual ExecutionResult Execute(Reservation reservation)
        {
            return ExecutionResult.Success;
            //return ExecutionResult.Warning;
            //return ExecutionResult.Failure;
        }
    }
}
