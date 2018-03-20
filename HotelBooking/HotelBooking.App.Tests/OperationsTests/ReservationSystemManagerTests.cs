using HotelBooking.App.Operations;
using HotelBooking.App.Operations.ExternalSystems;
using HotelBooking.Domain.Models;
using NUnit.Framework;
using Rhino.Mocks;

namespace HotelBooking.App.Tests.OperationsTests
{
    [TestFixture]
    public class ReservationSystemManagerTests
    {
        ReservationSystemManager sut;
        ExternalHotelReservationSystem externalHotelReservationSystem;

        [SetUp]
        public void SetUp()
        {
            externalHotelReservationSystem = MockRepository.GenerateMock<ExternalHotelReservationSystem>();
            sut = new ReservationSystemManager(externalHotelReservationSystem);
        }

        [Test]
        public void Should_CallExternalEmailSystem_When_Executing()
        {
            var reservation = new Reservation();
            externalHotelReservationSystem.Expect(x => x.BookReservation(reservation)).Return(true);
            sut.Execute(reservation);

            externalHotelReservationSystem.VerifyAllExpectations();
        }
    }
}
