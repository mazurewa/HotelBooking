using HotelBooking.App.Operations;
using HotelBooking.App.Operations.Services;
using HotelBooking.Domain.Models;
using NUnit.Framework;
using Rhino.Mocks;

namespace HotelBooking.App.Tests.OperationsTests
{
    [TestFixture]
    public class ReservationSystemManagerTests
    {
        ReservationSystemManager sut;
        HotelReservationSystem hotelReservationSystem;

        [SetUp]
        public void SetUp()
        {
            hotelReservationSystem = MockRepository.GenerateMock<HotelReservationSystem>();
            sut = new ReservationSystemManager(hotelReservationSystem);
        }

        [Test]
        public void Should_CallEmailSystem_When_Executing()
        {
            var reservation = new Reservation();
            hotelReservationSystem.Expect(x => x.BookReservation(reservation)).Return(true);
            sut.Execute(reservation);

            hotelReservationSystem.VerifyAllExpectations();
        }
    }
}
