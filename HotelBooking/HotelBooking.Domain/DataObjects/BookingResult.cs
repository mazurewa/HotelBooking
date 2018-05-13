using HotelBooking.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Domain.DataObjects
{
    public class BookingResult
    {
        public Result OverallResult => DeduceOverallBookingResult();
        public string ReservationId { get; set; }
        public virtual IList<OperationResult> OperationResults { get; }

        public BookingResult()
        {
            OperationResults = new List<OperationResult>();
        }

        private Result DeduceOverallBookingResult()
        {
            if (OperationResults.Count == 0)
            {
                return Result.Failure;
            }
            var hasAnyOperationFailed = OperationResults.Any(x => x.ExecutionResult 
            == OperationsProcessing.ExecutionResult.Failure);
            return hasAnyOperationFailed ? Result.Failure : Result.Success;
        }
    }
}