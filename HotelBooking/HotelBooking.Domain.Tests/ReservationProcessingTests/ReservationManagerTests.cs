using FluentAssertions;
using HotelBooking.Domain;
using HotelBooking.Domain.DataObjects;
using HotelBooking.Domain.Enums;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.OperationsProcessing;
using HotelBooking.Domain.ReservationProcessing;
using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Generic;

namespace HotelBooking.App.Tests.ReservationProcessingTests
{
    [TestFixture]
    public class ReservationManagerTests
    {
        ReservationManager sut;
        private Options options;
        private Hotel hotel;
        private IInputValidator inputsValidator;
        private IReservationFactory reservationFactory;
        private IOperationsManager operationsManager;
        private ILogger logger;

        [SetUp]
        protected void SetUp()
        {
            options = TestHelpers.GetOptions();
            hotel = new Hotel { HotelId = options.HotelId };
            inputsValidator = MockRepository.GenerateStub<IInputValidator>();
            reservationFactory = MockRepository.GenerateMock<IReservationFactory>();
            operationsManager = MockRepository.GenerateStub<IOperationsManager>();
            logger = MockRepository.GenerateStub<ILogger>();
            sut = new ReservationManager(inputsValidator, reservationFactory, operationsManager, logger);
        }

        [Test]
        public void Should_Fail_When_InputsNotValid()
        {
            inputsValidator.Stub(x => x.ValidateInputs(Arg<string[]>.Is.Anything, Arg<Options>.Is.Anything)).Return(false);

            var bookingResult = sut.ManageReservation(new string[1]);

            bookingResult.OverallResult.Should().Equals(Result.Failure);
        }

        [Test]
        public void Should_ReturnNoOperationsResults_When_InputsNotValid()
        {
            inputsValidator.Stub(x => x.ValidateInputs(Arg<string[]>.Is.Anything, Arg<Options>.Is.Anything)).Return(false);

            var bookingResult = sut.ManageReservation(new string[1]);

            bookingResult.OperationResults.Count.Should().Be(0);
        }

        [Test]
        public void Should_TryCreateReservation_When_InputsValid()
        {
            inputsValidator.Stub(x => x.ValidateInputs(Arg<string[]>.Is.Anything, Arg<Options>.Is.Anything)).Return(true);
            var bookingResult = MockRepository.GenerateStub<BookingResult>();
            bookingResult.Stub(x => x.OperationResults).Return(new List<OperationResult>());
            operationsManager.Stub(x => x.ProcessOperations(Arg<Reservation>.Is.Anything)).Return(bookingResult);
 
            reservationFactory.Expect(x => x.CreateReservation(Arg<Options>.Is.Anything));
            var finalBookingResult = sut.ManageReservation(new string[1]);

            reservationFactory.VerifyAllExpectations();
        }

        [Test]
        public void Should_ProcessOperations_When_NewReservationCreated()
        {
            inputsValidator.Stub(x => x.ValidateInputs(Arg<string[]>.Is.Anything, Arg<Options>.Is.Anything)).Return(true);
            reservationFactory.Stub(x => x.CreateReservation(Arg<Options>.Is.Anything)).Return(new Reservation());
            var bookingResult = MockRepository.GenerateStub<BookingResult>();
            bookingResult.Stub(x => x.OperationResults).Return(new List<OperationResult>());
            operationsManager.Stub(x => x.ProcessOperations(Arg<Reservation>.Is.Anything)).Return(bookingResult);

            operationsManager.Expect(x => x.ProcessOperations(Arg<Reservation>.Is.Anything));

            var finalBookingResult = sut.ManageReservation(new string[1]);

            operationsManager.VerifyAllExpectations();
        }

        [Test]
        public void Should_Succeed_When_NoOperationAbortsProcess()
        {
            inputsValidator.Stub(x => x.ValidateInputs(Arg<string[]>.Is.Anything, Arg<Options>.Is.Anything)).Return(true);
            reservationFactory.Stub(x => x.CreateReservation(Arg<Options>.Is.Anything)).Return(new Reservation());
            var bookingResult = MockRepository.GenerateStub<BookingResult>();
            var succeededOperations = new List<OperationResult>
            {
                new OperationResult {ShouldAbortBookingProcess = false},
                new OperationResult {ShouldAbortBookingProcess = false},
                new OperationResult {ShouldAbortBookingProcess = false}
            };
            bookingResult.Stub(x => x.OperationResults).Return(succeededOperations);
            operationsManager.Stub(x => x.ProcessOperations(Arg<Reservation>.Is.Anything)).Return(bookingResult);

            var finalBookingResult = sut.ManageReservation(new string[1]);

            finalBookingResult.OverallResult.Should().Be(Result.Success);
        }

        [Test]
        public void Should_Fail_When_AtLeastOneOperationAbortsProcess()
        {
            inputsValidator.Stub(x => x.ValidateInputs(Arg<string[]>.Is.Anything, Arg<Options>.Is.Anything)).Return(true);
            reservationFactory.Stub(x => x.CreateReservation(Arg<Options>.Is.Anything)).Return(new Reservation());
            var bookingResult = MockRepository.GenerateStub<BookingResult>();
            var succeededOperations = new List<OperationResult>
            {
                new OperationResult {ShouldAbortBookingProcess = true},
                new OperationResult {ShouldAbortBookingProcess = false},
                new OperationResult {ShouldAbortBookingProcess = false}
            };
            bookingResult.Stub(x => x.OperationResults).Return(succeededOperations);
            operationsManager.Stub(x => x.ProcessOperations(Arg<Reservation>.Is.Anything)).Return(bookingResult);

            var finalBookingResult = sut.ManageReservation(new string[1]);

            finalBookingResult.OverallResult.Should().Be(Result.Failure);
        }
    }
}
