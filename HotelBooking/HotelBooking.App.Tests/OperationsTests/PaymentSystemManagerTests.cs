using HotelBooking.App.Operations;
using HotelBooking.App.Operations.Services;
using HotelBooking.Domain.Models;
using NUnit.Framework;
using Rhino.Mocks;

namespace HotelBooking.App.Tests.OperationsTests
{
    [TestFixture]
    public class PaymentSystemManagerTests
    {
        PaymentSystemManager sut;
        PaymentSystem paymentSystem;

        [SetUp]
        public void SetUp()
        {
            paymentSystem = MockRepository.GenerateMock<PaymentSystem>();
            sut = new PaymentSystemManager(paymentSystem);
        }

        [Test]
        public void Should_CallEmailSystem_When_Executing()
        {
            var reservation = new Reservation();
            paymentSystem.Expect(x => x.Pay(reservation)).Return(true);
            sut.Execute(reservation);

            paymentSystem.VerifyAllExpectations();
        }
    }
}

