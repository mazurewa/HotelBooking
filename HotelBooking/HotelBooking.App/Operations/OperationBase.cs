using System;
using HotelBooking.Domain.Models;
using HotelBooking.App.DataObjects;
using HotelBooking.App.Enums;

namespace HotelBooking.App.Operations
{
    public abstract class OperationBase : IOperation
    {
        public abstract bool IsRequiredToSucceed { get; }
        public abstract string OperationName { get; }
        public abstract Func<Reservation, bool> Execute { get; }

        public OperationResult Process(Reservation reservation, BookingResult bookingResult)
        {
            var executionResult = Execute(reservation) ? Result.Success : Result.Failure;

            var operationResult =  new OperationResult
            {
                ExecutionResult = executionResult,
                OperationName = OperationName,
                ShouldAbortBookingProcess = IsRequiredToSucceed && executionResult == Result.Failure
            };           
            WriteOperationResultToConsole(operationResult);

            return operationResult;
        }

        private void WriteOperationResultToConsole(OperationResult operationResult)
        {
            Console.WriteLine(string.Format($"{OperationName} - Result: {operationResult.ExecutionResult}, Required: {IsRequiredToSucceed}"));
        }
    }
}
