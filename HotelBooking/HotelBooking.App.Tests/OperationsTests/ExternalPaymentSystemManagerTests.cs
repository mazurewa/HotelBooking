using HotelBooking.App.Operations;
using HotelBooking.App.Operations.ExternalSystems;
using HotelBooking.Domain.Models;
using NUnit.Framework;
using Rhino.Mocks;

namespace HotelBooking.App.Tests.OperationsTests
{
    [TestFixture]
    public class ExternalPaymentSystemManagerTests
    {
        ExternalPaymentSystemManager sut;
        IExternalPaymentSystem externalPaymentSystem;

        [SetUp]
        public void SetUp()
        {
            externalPaymentSystem = MockRepository.GenerateStub<IExternalPaymentSystem>();
            sut = new ExternalPaymentSystemManager(externalPaymentSystem);
        }

        [Test]
        public void Should_CallExternalEmailSystem_When_Executing()
        {
            var reservation = new Reservation();
            sut.Execute(reservation);

            externalPaymentSystem.AssertWasCalled(x => x.Pay(reservation));
        }
    }
}

