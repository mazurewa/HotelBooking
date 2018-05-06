using HotelBooking.Domain.Models;
using HotelBooking.Domain.DataObjects;
using HotelBooking.Domain.Enums;
using HotelBooking.Domain.OperationsProcessing;

namespace HotelBooking.Domain.Operations
{
    
    public class Operation : IOperation
    {
        public string OperationName { get; }
        bool IsRequiredToSucceed { get; }

        public Operation(OperationConfiguration operationConfiguration)
        {
            OperationName = operationConfiguration.Name;
            IsRequiredToSucceed = operationConfiguration.IsRequiredToSucceed;
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
