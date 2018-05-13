using HotelBooking.Domain.Models;
using HotelBooking.Domain.OperationsProcessing;

namespace HotelBooking.AcceptanceTests.Operations
{
    internal class OperationWithFailure : Operation
    {
        public OperationWithFailure(string name) : base(name)
        { }

        protected internal override ExecutionResult Execute(Reservation reservation)
        {
            return ExecutionResult.Failure;
        }
    }
}
