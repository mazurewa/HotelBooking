using HotelBooking.Domain.Models;
using HotelBooking.App.Operations.Services;
using System;
using HotelBooking.Domain.Enums;

namespace HotelBooking.App.Operations
{
    public class PaymentSystemManager : OperationBase
    {
        private PaymentSystem _paymentSystem;

        public PaymentSystemManager(PaymentSystem paymentSystem)
        {
            _paymentSystem = paymentSystem;
        }

        public override bool IsRequiredToSucceed => true;
        public override string OperationName => OperationCode.Payment;
        public override Func<Reservation, bool> Execute => Pay;

        private bool Pay(Reservation reservation)
        {
            return _paymentSystem.Pay(reservation);
        }
    }
}
