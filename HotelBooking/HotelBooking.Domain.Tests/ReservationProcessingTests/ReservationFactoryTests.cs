using FluentAssertions;
using HotelBooking.Domain;
using HotelBooking.Domain.DataAccess;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.ReservationProcessing;
using NUnit.Framework;
using Rhino.Mocks;
using System;

namespace HotelBooking.App.Tests.ReservationProcessingTests
{
    [TestFixture]
    public class ReservationFactoryTests
    {
        private ILogger logger;
        ReservationFactory sut;
        private Options options;
        private IHotelsRepository hotelRepository;

        [SetUp]
        public void SetUp()
        {
            options = TestHelpers.GetOptions();
            hotelRepository = MockRepository.GenerateStub<IHotelsRepository>();
        }

        [Test]
        public void Should_ReturnNotNull_When_HotelFoundInRepository()
        {
            var hotel = new Hotel();
            hotelRepository.Stub(x => x.GetById(options.HotelId)).Return(hotel);
            logger = MockRepository.GenerateStub<ILogger>();
            sut = new ReservationFactory(hotelRepository, logger);

            var reservation = sut.CreateReservation(options);

            reservation.Should().NotBeNull();
        }

        [Test]
        public void Should_ReturnNull_When_HotelNotFoundInRepository()
        {
            hotelRepository.Stub(x => x.GetById(options.HotelId)).Return(null);
            logger = MockRepository.GenerateStub<ILogger>();
            sut = new ReservationFactory(hotelRepository, logger);

            var reservation = sut.CreateReservation(options);

            reservation.Should().BeNull();
        }

        [Test]
        public void Should_ReturnReservationBasedOnOptions_When_HotelFoundInRepository()
        {
            var hotel = new Hotel();
            hotelRepository.Stub(x => x.GetById(options.HotelId)).Return(hotel);
            logger = MockRepository.GenerateStub<ILogger>();
            sut = MockRepository.GeneratePartialMock<ReservationFactory>(hotelRepository, logger);
            sut.Stub(x => x.GetReservationId()).Return("333");

            var expectedReservation = new Reservation
            {
                Cost = 123.34M,
                CreditCard = "1111 2222 3333 4444",
                CustomerEmail = "test@test.pl",
                ReservationDate = new DateTime(2008, 1, 1, 14, 50, 40),
                Hotel = hotel,
                ReservationId = "333"
            };

            var reservation = sut.CreateReservation(options);

            reservation.Should().BeEquivalentTo(expectedReservation);
        }
    }
}
