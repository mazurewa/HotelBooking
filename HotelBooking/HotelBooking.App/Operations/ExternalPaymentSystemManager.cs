using HotelBooking.Domain.Models;
using HotelBooking.App.Operations.ExternalSystems;
using System;
using HotelBooking.Domain.Enums;

namespace HotelBooking.App.Operations
{
    public class ExternalPaymentSystemManager : OperationBase
    {
        private IExternalPaymentSystem _paymentSystem;

        public ExternalPaymentSystemManager(IExternalPaymentSystem paymentSystem)
        {
            _paymentSystem = paymentSystem;
        }

        public override bool IsRequiredToSucceed => true;
        public override string OperationName => OperationCode.ExternalPayment;
        public override Func<Reservation, bool> Execute => Pay;

        private bool Pay(Reservation reservation)
        {
            return _paymentSystem.Pay(reservation);
        }
    }
}
