using HotelBooking.Domain.OperationsProcessing;

namespace HotelBooking.Domain.DataObjects
{
    public class OperationResult
    {
        public ExecutionResult ExecutionResult { get; set; }
        public bool ShouldAbortBookingProcess { get; set; }
        public string OperationName { get; set; }
    }
}