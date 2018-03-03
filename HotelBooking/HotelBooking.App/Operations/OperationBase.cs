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

        public OperationResult OperationResult { get; set; }

        public void Process(Reservation reservation, BookingResult bookingResult)
        {
            var executionResult = Execute(reservation) ? Result.Success : Result.Failure;

            OperationResult = new OperationResult
            {
                ExecutionResult = executionResult,
                OperationName = OperationName,
                ShouldAbortBookingProcess = IsRequiredToSucceed && executionResult == Result.Failure
            };           
            bookingResult.OperationResults.Add(OperationResult);

            WriteOperationResultToConsole();            
        }

        private void WriteOperationResultToConsole()
        {
            Console.WriteLine(string.Format($"{OperationName} - Result: {OperationResult.ExecutionResult}, Required: {IsRequiredToSucceed}"));
        }
    }
}
