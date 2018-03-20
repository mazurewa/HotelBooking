using HotelBooking.App.Operations;
using HotelBooking.App.ReservationProcessing;
using HotelBooking.Domain.Enums;
using Rhino.Mocks;
using System;

namespace HotelBooking.App.Tests
{
    public static class TestHelpers
    {
        public static IOperation[] GetOperations()
        {
            var operations = new IOperation[4];

            var sendingEmail = MockRepository.GenerateStub<IOperation>();
            sendingEmail.Stub(x => x.OperationName).Return(OperationCode.SendingEmail);
            operations[0] = sendingEmail;

            var payment = MockRepository.GenerateStub<IOperation>();
            payment.Stub(x => x.OperationName).Return(OperationCode.Payment);
            operations[1] = payment;

            var reservation = MockRepository.GenerateStub<IOperation>();
            reservation.Stub(x => x.OperationName).Return(OperationCode.Reservation);
            operations[2] = reservation;

            var priceValidator = MockRepository.GenerateStub<IOperation>();
            priceValidator.Stub(x => x.OperationName).Return(OperationCode.RecheckingPrice);
            operations[3] = priceValidator;

            return operations;
        }

        public static Options GetOptions()
        {
            return new Options
            {
                HotelId = "003",
                ReservationDate = new DateTime(2008, 1, 1, 14, 50, 40),
                Cost = 123.34,
                CreditCard = "1111 2222 3333 4444",
                CustomerEmail = "test@test.pl"
            };
        }
    }
}