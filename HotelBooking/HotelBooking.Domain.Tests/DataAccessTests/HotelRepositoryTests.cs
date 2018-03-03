using FluentAssertions;
using HotelBooking.Domain.DataAccess;
using HotelBooking.Domain.Enums;
using HotelBooking.Domain.Models;
using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Domain.Tests.DataAccessTests
{
    [TestFixture]
    public class HotelRepositoryTests
    {
        HotelRepository sut;
        private List<Hotel> hotels;
        IHotelsProvider hotelsProvider;

        [SetUp]
        public void SetUp()
        {
            hotels = new List<Hotel>
            {
                new Hotel
                {
                    HotelId = "0008",
                    OperationOrder = new List<string>
                    {
                        OperationCode.RecheckingPrice,
                        OperationCode.ExternalPayment,
                        OperationCode.ExternalReservation,
                        OperationCode.SendingEmail
                    }
                }
            };
            hotelsProvider = MockRepository.GenerateStub<IHotelsProvider>();
            hotelsProvider.Stub(x => x.GetHotels()).Return(hotels);

            sut = new HotelRepository(hotelsProvider);
        }


        [Test]
        public void Should_ReturnNotNull_When_HotelExistsInRepository()
        {
            var hotel = sut.GetById("0008");

            hotel.Should().NotBeNull();
        }

        [Test]
        public void Should_ReturnHotel_When_HotelExistsInRepository()
        {
            var hotel = sut.GetById("0008");

            hotel.Should().Be(hotels.First());
        }

        [Test]
        public void Should_ReturnNull_When_HotelDoesNotExistInRepository()
        {
            var hotel = sut.GetById("0009");

            hotel.Should().BeNull();
        }
    }
}