using HotelBooking.App.Enums;
using System.Collections.Generic;

namespace HotelBooking.App.DataObjects
{
    public class BookingResult
    {
        public Result OverallResult { get; set; }
        public string ReservationId { get; set; }
        public IList<OperationResult> OperationResults { get; set; }

        public BookingResult()
        {
            OverallResult = Result.Failure;
            OperationResults = new List<OperationResult>();
        }
    }
}