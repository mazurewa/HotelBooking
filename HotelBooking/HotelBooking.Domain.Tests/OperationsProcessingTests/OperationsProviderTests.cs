using HotelBooking.Domain.Models;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using Rhino.Mocks;
using HotelBooking.Domain.Enums;
using HotelBooking.Domain.OperationsProcessing;

namespace HotelBooking.App.Tests.OperationsProcessingTests
{
    [TestFixture]
    public class OperationsProviderTests
    {
        OperationsProvider sut;
        private IAllOperationsProvider _allOperationsProvider;

        [Test]
        public void Should_ProvideAllOperationsSpecifiedByHotel_When_EachIncludedInAllOperations()
        {
            var hotel = new Hotel
            {
                HotelId = "001",
                HotelConfiguration = new HotelConfiguration
                {
                    OperationOrder = new List<string>
                        {
                            OperationCode.RecheckingPrice,
                            OperationCode.Payment,
                            OperationCode.Reservation, 
                            OperationCode.SendingEmail
                        }
                }
            };

            var availableOperations = new List<string>
            {
                OperationCode.Payment,
                OperationCode.RecheckingPrice,
                OperationCode.SendingEmail,
                OperationCode.Reservation
            };

            _allOperationsProvider = MockRepository.GenerateMock<IAllOperationsProvider>();
            _allOperationsProvider.Stub(x => x.GetAllOperations()).Return(availableOperations);
            sut = new OperationsProvider(_allOperationsProvider);

            var orderOperations = sut.GetOrderedOperations(hotel);

            orderOperations.Should().HaveCount(4);
        }

        [Test]
        public void Should_ProvideUniqueOperations()
        {
            var hotel = new Hotel
            {
                HotelId = "001",
                HotelConfiguration = new HotelConfiguration
                {
                    OperationOrder = new List<string>
                        {
                            OperationCode.RecheckingPrice, 
                            OperationCode.Payment, 
                            OperationCode.Reservation, 
                            OperationCode.SendingEmail
                        }
                }
            };

            var availableOperations = new List<string>
            {
                OperationCode.Payment,
                OperationCode.RecheckingPrice,
                OperationCode.SendingEmail,
                OperationCode.Reservation
            };

            _allOperationsProvider = MockRepository.GenerateMock<IAllOperationsProvider>();
            _allOperationsProvider.Stub(x => x.GetAllOperations()).Return(availableOperations);
            sut = new OperationsProvider(_allOperationsProvider);

            var orderOperations = sut.GetOrderedOperations(hotel);

            orderOperations.Should().OnlyHaveUniqueItems();
        }

        [Test]
        public void Should_OrderOperations()
        {
            var hotel = new Hotel
            {
                HotelId = "001",
                HotelConfiguration = new HotelConfiguration
                {
                    OperationOrder = new List<string>
                        {
                            OperationCode.RecheckingPrice,
                            OperationCode.Payment,
                            OperationCode.Reservation,
                            OperationCode.SendingEmail
                        }
                }
            };

            var availableOperations = new List<string>
            {
                OperationCode.Payment,
                OperationCode.RecheckingPrice,
                OperationCode.SendingEmail,
                OperationCode.Reservation
            };

            _allOperationsProvider = MockRepository.GenerateMock<IAllOperationsProvider>();
            _allOperationsProvider.Stub(x => x.GetAllOperations()).Return(availableOperations);
            sut = new OperationsProvider(_allOperationsProvider);
            var orderOperations = sut.GetOrderedOperations(hotel);

            orderOperations[0].OperationName.Should().Equals(OperationCode.RecheckingPrice);
            orderOperations[1].OperationName.Should().Equals(OperationCode.Payment);
            orderOperations[2].OperationName.Should().Equals(OperationCode.Reservation);
            orderOperations[3].OperationName.Should().Equals(OperationCode.SendingEmail);
        }

        [Test]
        public void Should_NotContainNulls_When_NotAllAvailableOperationsSpecifiedByHotel()
        {
            var hotel = new Hotel
            {
                HotelId = "001",
                HotelConfiguration = new HotelConfiguration
                {
                    OperationOrder = new List<string>
                        {
                            OperationCode.RecheckingPrice,
                            OperationCode.Payment
                        }
                }
            };

            var availableOperations = new List<string>
            {
                OperationCode.Payment,
                OperationCode.RecheckingPrice,
                OperationCode.SendingEmail,
                OperationCode.Reservation
            };

            _allOperationsProvider = MockRepository.GenerateMock<IAllOperationsProvider>();
            _allOperationsProvider.Stub(x => x.GetAllOperations()).Return(availableOperations);
            sut = new OperationsProvider(_allOperationsProvider);

            var orderOperations = sut.GetOrderedOperations(hotel);

            orderOperations.Should().NotContainNulls();
        }

        [Test]
        public void Should_CallAllOperationsProvider()
        {
            var hotel = new Hotel
            {
                HotelId = "001",
                HotelConfiguration = new HotelConfiguration
                {
                    OperationOrder = new List<string>
                        {
                            OperationCode.RecheckingPrice
                        }
                }
            };

            var availableOperations = new List<string>
            {
                OperationCode.Payment,
                OperationCode.RecheckingPrice,
                OperationCode.SendingEmail,
                OperationCode.Reservation
            };

            _allOperationsProvider = MockRepository.GenerateMock<IAllOperationsProvider>();
            _allOperationsProvider.Expect(x => x.GetAllOperations()).Return(availableOperations);
            sut = new OperationsProvider(_allOperationsProvider);

            var orderOperations = sut.GetOrderedOperations(hotel);

            _allOperationsProvider.VerifyAllExpectations();
        }
    }
}
