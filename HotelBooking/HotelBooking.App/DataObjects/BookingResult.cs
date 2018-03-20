using HotelBooking.App.Enums;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.App.DataObjects
{
    public class BookingResult
    {
        public virtual Result OverallResult => DeduceOverallBookingResult();
        public string ReservationId { get; set; }
        public IList<OperationResult> OperationResults { get; set; }

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
            var hasAnyRequiredOperationFailed = OperationResults.Any(x => x.ShouldAbortBookingProcess);
            return hasAnyRequiredOperationFailed ? Result.Failure : Result.Success;
        }
    }
}