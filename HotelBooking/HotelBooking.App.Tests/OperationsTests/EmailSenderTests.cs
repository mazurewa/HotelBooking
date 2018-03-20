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
        ExternalEmailSystem externalEmailSystem;

        [SetUp]
        public void SetUp()
        {
            externalEmailSystem = MockRepository.GenerateMock<ExternalEmailSystem>();
            sut = new EmailSender(externalEmailSystem);
        }

        [Test]
        public void Should_CallExternalEmailSystem()
        {
            var reservation = new Reservation();
            externalEmailSystem.Expect(x => x.SendConfirmationEmail(reservation)).Return(true);
            sut.Execute(reservation);

            externalEmailSystem.VerifyAllExpectations();
        }
    }
}
