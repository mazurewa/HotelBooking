using HotelBooking.App.Operations;
using HotelBooking.App.Operations.ExternalSystems;
using HotelBooking.Domain.Models;
using NUnit.Framework;
using Rhino.Mocks;

namespace HotelBooking.App.Tests.OperationsTests
{
    [TestFixture]
    public class ExternalReservationSystemManagerTests
    {
        ExternalReservationSystemManager sut;
        IExternalHotelReservationSystem externalHotelReservationSystem;

        [SetUp]
        public void SetUp()
        {
            externalHotelReservationSystem = MockRepository.GenerateStub<IExternalHotelReservationSystem>();
            sut = new ExternalReservationSystemManager(externalHotelReservationSystem);
        }

        [Test]
        public void Should_CallExternalEmailSystem_When_Executing()
        {
            var reservation = new Reservation();
            sut.Execute(reservation);

            externalHotelReservationSystem.AssertWasCalled(x => x.BookReservation(reservation));
        }
    }
}
