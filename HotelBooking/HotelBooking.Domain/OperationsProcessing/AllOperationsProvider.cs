using HotelBooking.Domain.Enums;
using System.Collections.Generic;

namespace HotelBooking.Domain.OperationsProcessing
{
    public class AllOperationsProvider : IAllOperationsProvider
    {
        public IEnumerable<string> GetAllOperations()
        {
            return new List<string>
            {
                OperationCode.RecheckingPrice,
                OperationCode.Payment,
                OperationCode.Reservation,
                OperationCode.SendingEmail
            };
        }
    }
}
