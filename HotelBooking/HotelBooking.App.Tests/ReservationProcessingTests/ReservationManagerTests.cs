using FluentAssertions;
using HotelBooking.App.DataObjects;
using HotelBooking.App.Enums;
using HotelBooking.App.OperationsProcessing;
using HotelBooking.App.ReservationProcessing;
using HotelBooking.Domain.Models;
using NUnit.Framework;
using Rhino.Mocks;

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

        [SetUp]
        protected void SetUp()
        {
            options = TestHelpers.GetOptions();
            hotel = new Hotel { HotelId = options.HotelId };
            inputsValidator = MockRepository.GenerateStub<IInputValidator>();
            reservationFactory = MockRepository.GenerateMock<IReservationFactory>();
            operationsManager = MockRepository.GenerateStub<IOperationsManager>();
            sut = new ReservationManager(inputsValidator, reservationFactory, operationsManager);
        }

        [Test]
        public void Should_Fail_When_InputsNotValid()
        {
            inputsValidator.Stub(x => x.ValidateInputs(Arg<string[]>.Is.Anything, Arg<Options>.Is.Anything)).Return(false);

            var bookingResult = sut.ProcessReservation(new string[1]);

            bookingResult.OverallResult.Should().Equals(Result.Failure);
        }

        [Test]
        public void Should_ReturnNoOperationsResults_When_InputsNotValid()
        {
            inputsValidator.Stub(x => x.ValidateInputs(Arg<string[]>.Is.Anything, Arg<Options>.Is.Anything)).Return(false);

            var bookingResult = sut.ProcessReservation(new string[1]);

            bookingResult.OperationResults.Count.Should().Be(0);
        }

        [Test]
        public void Should_TryCreateReservation_When_InputsValid()
        {
            inputsValidator.Stub(x => x.ValidateInputs(Arg<string[]>.Is.Anything, Arg<Options>.Is.Anything)).Return(true);
            var bookingResult = MockRepository.GenerateStub<BookingResult>();
            operationsManager.Stub(x => x.ProcessOperations(Arg<Reservation>.Is.Anything)).Return(bookingResult);
 
            reservationFactory.Expect(x => x.CreateReservation(Arg<Options>.Is.Anything));
            var finalBookingResult = sut.ProcessReservation(new string[1]);

            reservationFactory.VerifyAllExpectations();
        }

        [Test]
        public void Should_ProcessOperations_When_NewReservationCreated()
        {
            inputsValidator.Stub(x => x.ValidateInputs(Arg<string[]>.Is.Anything, Arg<Options>.Is.Anything)).Return(true);
            reservationFactory.Stub(x => x.CreateReservation(Arg<Options>.Is.Anything)).Return(new Reservation());
            var bookingResult = MockRepository.GenerateStub<BookingResult>();
            operationsManager.Stub(x => x.ProcessOperations(Arg<Reservation>.Is.Anything)).Return(bookingResult);

            operationsManager.Expect(x => x.ProcessOperations(Arg<Reservation>.Is.Anything));

            var finalBookingResult = sut.ProcessReservation(new string[1]);

            operationsManager.VerifyAllExpectations();
        }

        [Test]
        public void Should_Succeed_When_OperationsSucceeded()
        {
            inputsValidator.Stub(x => x.ValidateInputs(Arg<string[]>.Is.Anything, Arg<Options>.Is.Anything)).Return(true);
            reservationFactory.Stub(x => x.CreateReservation(Arg<Options>.Is.Anything)).Return(new Reservation());
            var bookingResult = MockRepository.GenerateStub<BookingResult>();
            bookingResult.Stub(x => x.OverallResult).Return(Result.Success);
            operationsManager.Stub(x => x.ProcessOperations(Arg<Reservation>.Is.Anything)).Return(bookingResult);

            var finalBookingResult = sut.ProcessReservation(new string[1]);

            finalBookingResult.OverallResult.Should().Be(Result.Success);
        }

        [Test]
        public void Should_Fail_When_OperationsFailed()
        {
            inputsValidator.Stub(x => x.ValidateInputs(Arg<string[]>.Is.Anything, Arg<Options>.Is.Anything)).Return(true);
            reservationFactory.Stub(x => x.CreateReservation(Arg<Options>.Is.Anything)).Return(new Reservation());
            var bookingResult = MockRepository.GenerateStub<BookingResult>();
            bookingResult.Stub(x => x.OverallResult).Return(Result.Failure);
            operationsManager.Stub(x => x.ProcessOperations(Arg<Reservation>.Is.Anything)).Return(bookingResult);

            var finalBookingResult = sut.ProcessReservation(new string[1]);

            finalBookingResult.OverallResult.Should().Be(Result.Failure);
        }
    }
}
