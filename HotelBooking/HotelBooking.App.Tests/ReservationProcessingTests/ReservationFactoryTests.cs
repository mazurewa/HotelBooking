using FluentAssertions;
using HotelBooking.App.ReservationProcessing;
using HotelBooking.Domain.DataAccess;
using HotelBooking.Domain.Models;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;

namespace HotelBooking.App.Tests.ReservationProcessingTests
{
    [TestFixture]
    public class ReservationFactoryTests
    {
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
            sut = new ReservationFactory(hotelRepository);

            var reservation = sut.CreateReservation(options);

            reservation.Should().NotBeNull();
        }

        [Test]
        public void Should_ReturnNull_When_HotelNotFoundInRepository()
        {
            hotelRepository.Stub(x => x.GetById(options.HotelId)).Return(null);
            sut = new ReservationFactory(hotelRepository);

            var reservation = sut.CreateReservation(options);

            reservation.Should().BeNull();
        }

        [Test]
        public void Should_ReturnReservationBasedOnOptions_When_HotelFoundInRepository()
        {
            var hotel = new Hotel();
            hotelRepository.Stub(x => x.GetById(options.HotelId)).Return(hotel);
            sut = new ReservationFactory(hotelRepository);

            var reservation = sut.CreateReservation(options);

            reservation.Cost.Should().Equals(123.34);
            reservation.CreditCard.Should().Equals("1111 2222 3333 4444");
            reservation.CustomerEmail.Should().Equals("test@test.pl");
            reservation.ReservationDate.Should().Equals(new DateTime(2008, 1, 1, 14, 50, 40));
        }

        [Test]
        public void Should_MapHotelInformationIntoReservation_When_HotelFoundInRepository()
        {
            var hotel = new Hotel() { OperationOrder = new List<string> { "a", "b", "c"} };
            hotelRepository.Stub(x => x.GetById(options.HotelId)).Return(hotel);
            sut = new ReservationFactory(hotelRepository);

            var reservation = sut.CreateReservation(options);

            reservation.Hotel.HotelId.Should().Equals("003");
            reservation.Hotel.OperationOrder.Count.Equals(3);
            reservation.Hotel.OperationOrder[0].Should().Equals("a");
            reservation.Hotel.OperationOrder[1].Should().Equals("b");
            reservation.Hotel.OperationOrder[2].Should().Equals("c");
        }
    }
}
