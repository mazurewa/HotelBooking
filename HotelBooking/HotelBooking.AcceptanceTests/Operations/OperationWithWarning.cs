using HotelBooking.Domain.Models;
using HotelBooking.Domain.OperationsProcessing;

namespace HotelBooking.AcceptanceTests.Operations
{
    internal class OperationWithWarning : Operation
    {
        public OperationWithWarning(string name) : base(name)
        { }

        protected internal override ExecutionResult Execute(Reservation reservation)
        {
            return ExecutionResult.Warning;
        }
    }
}
