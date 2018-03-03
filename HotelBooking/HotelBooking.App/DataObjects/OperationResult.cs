using HotelBooking.App.Enums;

namespace HotelBooking.App.DataObjects
{
    public class OperationResult
    {
        public Result ExecutionResult { get; set; }
        public bool ShouldAbortBookingProcess { get; set; }
        public string OperationName { get; set; }
    }
}