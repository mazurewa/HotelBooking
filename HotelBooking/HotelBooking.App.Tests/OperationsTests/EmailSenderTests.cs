using HotelBooking.App.Operations;
using HotelBooking.App.Operations.ExternalSystems;
using HotelBooking.Domain.Models;
using NUnit.Framework;
using Rhino.Mocks;

namespace HotelBooking.App.Tests.OperationsTests
{
    [TestFixture]
    public class EmailSenderTests
    {
        EmailSender sut;
        IExternalEmailSystem externalEmailSystem;

        [SetUp]
        public void SetUp()
        {
            externalEmailSystem = MockRepository.GenerateStub<IExternalEmailSystem>();
            sut = new EmailSender(externalEmailSystem);
        }

        [Test]
        public void Should_CallExternalEmailSystem_When_Executing()
        {
            var reservation = new Reservation();
            sut.Execute(reservation);

            externalEmailSystem.AssertWasCalled(x => x.SendConfirmationEmail(reservation));
        }
    }
}
