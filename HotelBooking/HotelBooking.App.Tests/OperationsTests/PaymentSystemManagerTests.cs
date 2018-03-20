using HotelBooking.App.Operations;
using HotelBooking.App.Operations.ExternalSystems;
using HotelBooking.Domain.Models;
using NUnit.Framework;
using Rhino.Mocks;

namespace HotelBooking.App.Tests.OperationsTests
{
    [TestFixture]
    public class PaymentSystemManagerTests
    {
        PaymentSystemManager sut;
        ExternalPaymentSystem externalPaymentSystem;

        [SetUp]
        public void SetUp()
        {
            externalPaymentSystem = MockRepository.GenerateMock<ExternalPaymentSystem>();
            sut = new PaymentSystemManager(externalPaymentSystem);
        }

        [Test]
        public void Should_CallExternalEmailSystem_When_Executing()
        {
            var reservation = new Reservation();
            externalPaymentSystem.Expect(x => x.Pay(reservation)).Return(true);
            sut.Execute(reservation);

            externalPaymentSystem.VerifyAllExpectations();
        }
    }
}

