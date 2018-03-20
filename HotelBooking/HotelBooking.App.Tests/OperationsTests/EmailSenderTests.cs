using HotelBooking.App.Operations;
using HotelBooking.App.Operations.Services;
using HotelBooking.Domain.Models;
using NUnit.Framework;
using Rhino.Mocks;

namespace HotelBooking.App.Tests.OperationsTests
{
    [TestFixture]
    public class EmailSenderTests
    {
        EmailSender sut;
        EmailSystem emailSystem;

        [SetUp]
        public void SetUp()
        {
            emailSystem = MockRepository.GenerateMock<EmailSystem>();
            sut = new EmailSender(emailSystem);
        }

        [Test]
        public void Should_CallEmailSystem()
        {
            var reservation = new Reservation();
            emailSystem.Expect(x => x.SendConfirmationEmail(reservation)).Return(true);
            sut.Execute(reservation);

            emailSystem.VerifyAllExpectations();
        }
    }
}
