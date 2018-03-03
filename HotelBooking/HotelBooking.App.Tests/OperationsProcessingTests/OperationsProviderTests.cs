using HotelBooking.App.Operations;
using HotelBooking.Domain.Models;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using Rhino.Mocks;
using System.Linq;
using HotelBooking.App.OperationsProcessing;
using HotelBooking.Domain.Enums;

namespace HotelBooking.App.Tests.OperationsProcessingTests
{
    [TestFixture]
    public class OperationsProviderTests
    {
        OperationsProvider sut;
        Hotel hotel;
        IOperation[] bookingOperations;

        [OneTimeSetUp]
        protected void TestFixtureSetUp()
        {
            bookingOperations = TestHelpers.GetOperations();
            sut = new OperationsProvider(bookingOperations);
            hotel = new Hotel();
        }

        [Test]
        public void Should_ReturnFourOperations_When_FourOperationsSpecifiedByHotelAndAllAvailable()
        {
            var hotel = new Hotel
            {
                OperationOrder = new List<string>
                    {
                        OperationCode.RecheckingPrice,
                        OperationCode.ExternalPayment,
                        OperationCode.ExternalReservation,
                        OperationCode.SendingEmail
                    }
            };
            var orderOperations = sut.GetOrderedOperations(hotel);

            orderOperations.Should().HaveCount(4);
        }

        [Test]
        public void Should_ReturnThreeOperations_When_ThreeOperationsSpecifiedByHotelAndAllAvailable()
        {
            var hotel = new Hotel
            {
                OperationOrder = new List<string>
                    {
                        OperationCode.RecheckingPrice,
                        OperationCode.ExternalPayment,
                        OperationCode.SendingEmail
                    }
            };
            var orderOperations = sut.GetOrderedOperations(hotel);

            orderOperations.Should().HaveCount(3);
        }

        [Test]
        public void Should_OnlyHaveUniqueItems_When_FourOperationsSpecifiedByHotel()
        {
            var hotel = new Hotel
            {
                OperationOrder = new List<string>
                    {
                        OperationCode.RecheckingPrice,
                        OperationCode.ExternalPayment,
                        OperationCode.ExternalReservation,
                        OperationCode.SendingEmail
                    }
            };
            var orderOperations = sut.GetOrderedOperations(hotel);

            orderOperations.Should().OnlyHaveUniqueItems();
        }

        [Test]
        public void Should_OrderFourOperations_When_FourOperationsSpecifiedByHotelAndAllAvailable()
        {
            var hotel = new Hotel
            {
                OperationOrder = new List<string>
                    {
                        OperationCode.RecheckingPrice,
                        OperationCode.ExternalPayment,
                        OperationCode.ExternalReservation,
                        OperationCode.SendingEmail
                    }
            };
            var orderOperations = sut.GetOrderedOperations(hotel);

            orderOperations[0].OperationName.Should().Equals(OperationCode.RecheckingPrice);
            orderOperations[1].OperationName.Should().Equals(OperationCode.ExternalPayment);
            orderOperations[2].OperationName.Should().Equals(OperationCode.ExternalReservation);
            orderOperations[3].OperationName.Should().Equals(OperationCode.SendingEmail);
        }

        [Test]
        public void Should_NotContainNulls_When_ThreeOperationsSpecifiedByHotelAndFourAvailable()
        {
            var hotel = new Hotel
            {
                OperationOrder = new List<string>
                    {
                        OperationCode.RecheckingPrice,
                        OperationCode.ExternalPayment,
                        OperationCode.ExternalReservation
                    }
            };
            var orderOperations = sut.GetOrderedOperations(hotel);

            orderOperations.Should().NotContainNulls();
        }

        [Test]
        public void Should_BeValid_When_AllRequiredOperationsIncluded()
        {
            bookingOperations[0].Stub(x => x.IsRequiredToSucceed).Return(true);
            bookingOperations[1].Stub(x => x.IsRequiredToSucceed).Return(true);
            bookingOperations[2].Stub(x => x.IsRequiredToSucceed).Return(true);
            bookingOperations[3].Stub(x => x.IsRequiredToSucceed).Return(false);

            var operationsToRun = TestHelpers.GetOperations();
            
            var valid = sut.ContainsAllRequiredOperations(operationsToRun);

            valid.Should().BeTrue();
        }

        [Test]
        public void Should_NotBeValid_When_NotAllRequiredOperationsIncluded()
        {
            bookingOperations[0].Stub(x => x.IsRequiredToSucceed).Return(true);
            bookingOperations[1].Stub(x => x.IsRequiredToSucceed).Return(true);
            bookingOperations[2].Stub(x => x.IsRequiredToSucceed).Return(true);
            bookingOperations[3].Stub(x => x.IsRequiredToSucceed).Return(false);

            var operationsToRun = TestHelpers.GetOperations().ToList();
            operationsToRun.RemoveAt(0);

            var valid = sut.ContainsAllRequiredOperations(operationsToRun);

            valid.Should().BeFalse();
        }
    }
}
