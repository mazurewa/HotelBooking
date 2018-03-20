using FluentAssertions;
using HotelBooking.App.DataObjects;
using HotelBooking.App.Enums;
using HotelBooking.App.Operations;
using HotelBooking.App.Operations.Services;
using HotelBooking.App.OperationsProcessing;
using HotelBooking.Domain.Models;
using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Generic;

namespace HotelBooking.App.Tests.OperationsProcessingTests
{
    [TestFixture]
    public class OperationsManagerTests
    {
        OperationsManager sut;
        IOperationsProvider operationsProvider;
        EmailSystem emailSystem;
        Reservation reservation;

        [SetUp]
        public void SetUp()
        {
            operationsProvider = MockRepository.GenerateMock<IOperationsProvider>();
            emailSystem = MockRepository.GenerateMock<EmailSystem>();
            reservation = new Reservation { Hotel = new Hotel() };
            sut = new OperationsManager(operationsProvider, emailSystem);
        }

        [Test]
        public void Should_GetOrderedOperationsForHotel()
        {
            operationsProvider.Expect(x => x.GetOrderedOperations(reservation.Hotel));

            sut.ProcessOperations(reservation);

            operationsProvider.VerifyAllExpectations();
        }

        [Test]
        public void Should_ValidateRequiredOperations_When_HavingHotelOperations()
        {
            var operationsToRun = new List<IOperation>();
            operationsProvider.Stub(x => x.GetOrderedOperations(reservation.Hotel)).Return(operationsToRun);

            operationsProvider.Expect(x => x.ContainsAllRequiredOperations(operationsToRun)).Return(false);

            sut.ProcessOperations(reservation);

            operationsProvider.VerifyAllExpectations();
        }

        [Test]
        public void Should_ReturnFailure_When_DoesNotContainAllRequiredOperations()
        {
            var operationsToRun = new List<IOperation>();
            operationsProvider.Stub(x => x.GetOrderedOperations(reservation.Hotel)).Return(operationsToRun);
            operationsProvider.Stub(x => x.ContainsAllRequiredOperations(operationsToRun)).Return(false);

            var bookingResult = sut.ProcessOperations(reservation);

            bookingResult.OverallResult.Should().Equals(Result.Failure);
        }

        [Test]
        public void Should_NotProcessAnyOperation_When_DoesNotContainAllRequiredOperations()
        {
            var operation = MockRepository.GenerateMock<IOperation>();
            var operationsToRun = new List<IOperation> { operation };
            operationsProvider.Stub(x => x.GetOrderedOperations(reservation.Hotel)).Return(operationsToRun);
            operationsProvider.Stub(x => x.ContainsAllRequiredOperations(operationsToRun)).Return(false);

            var bookingResult = sut.ProcessOperations(reservation);

            operation.AssertWasNotCalled(x => x.Process(reservation, bookingResult));
            bookingResult.OperationResults.Should().BeEmpty();
        }

        [Test]
        public void Should_ProcessAtLeastOneOperation_When_RequiredOperationsValidatedSuccessfully()
        {
            var operation1 = MockRepository.GenerateMock<IOperation>();
            operation1.Stub(x => x.OperationResult).Return( new OperationResult { ShouldAbortBookingProcess = false });
         
            var operationsToRun = new List<IOperation> { operation1 };
            operationsProvider.Stub(x => x.GetOrderedOperations(reservation.Hotel)).Return(operationsToRun);
            operationsProvider.Stub(x => x.ContainsAllRequiredOperations(operationsToRun)).Return(true);

           var bookingResult = sut.ProcessOperations(reservation);

            operation1.AssertWasCalled(x => x.Process(reservation, bookingResult));
        }

        [Test]
        public void Should_NotStopProcessing_When_NoOperationsFailed()
        {
            var operation1 = MockRepository.GenerateMock<IOperation>();
            operation1.Stub(x => x.OperationResult).Return(new OperationResult { ShouldAbortBookingProcess = false });
            var operation2 = MockRepository.GenerateMock<IOperation>();
            operation2.Stub(x => x.OperationResult).Return(new OperationResult { ShouldAbortBookingProcess = false });

            var operationsToRun = new List<IOperation> { operation1, operation2 };
            operationsProvider.Stub(x => x.GetOrderedOperations(reservation.Hotel)).Return(operationsToRun);
            operationsProvider.Stub(x => x.ContainsAllRequiredOperations(operationsToRun)).Return(true);

            var bookingResult = sut.ProcessOperations(reservation);

            operation1.AssertWasCalled(x => x.Process(reservation, bookingResult));
            operation2.AssertWasCalled(x => x.Process(reservation, bookingResult));
        }

        [Test]
        public void Should_StopProcessing_When_OperationFailedWithAbort()
        {
            var operation1 = MockRepository.GenerateMock<IOperation>();
            operation1.Stub(x => x.OperationResult).Return(new OperationResult { ShouldAbortBookingProcess = false });
            var operation2 = MockRepository.GenerateMock<IOperation>();
            operation2.Stub(x => x.OperationResult).Return(new OperationResult { ShouldAbortBookingProcess = false });
            var operation3 = MockRepository.GenerateMock<IOperation>();
            operation3.Stub(x => x.OperationResult).Return(new OperationResult { ShouldAbortBookingProcess = true });
            var operation4 = MockRepository.GenerateMock<IOperation>();
            operation4.Stub(x => x.OperationResult).Return(new OperationResult { ShouldAbortBookingProcess = false });
            var operationsToRun = new List<IOperation> { operation1, operation2, operation3, operation4 };

            operationsProvider.Stub(x => x.GetOrderedOperations(reservation.Hotel)).Return(operationsToRun);
            operationsProvider.Stub(x => x.ContainsAllRequiredOperations(operationsToRun)).Return(true);

            var bookingResult = sut.ProcessOperations(reservation);

            operation1.AssertWasCalled(x => x.Process(reservation, bookingResult));
            operation2.AssertWasCalled(x => x.Process(reservation, bookingResult));
            operation3.AssertWasCalled(x => x.Process(reservation, bookingResult));
            operation4.AssertWasNotCalled(x => x.Process(reservation, bookingResult));
        }

        [Test]
        public void Should_SendRejectionEmail_When_ProcessAborted()
        {
            var operation1 = MockRepository.GenerateMock<IOperation>();
            operation1.Stub(x => x.OperationResult).Return(new OperationResult { ShouldAbortBookingProcess = true });
          
            var operationsToRun = new List<IOperation> { operation1 };

            operationsProvider.Stub(x => x.GetOrderedOperations(reservation.Hotel)).Return(operationsToRun);
            operationsProvider.Stub(x => x.ContainsAllRequiredOperations(operationsToRun)).Return(true);

            operation1.Expect(x => x.Process(Arg<Reservation>.Is.Anything, Arg<BookingResult>.Is.Anything));
            emailSystem.Expect(x => x.SendRejectionEmail(reservation)).Return(true);

            var bookingResult = sut.ProcessOperations(reservation);

            emailSystem.VerifyAllExpectations();
            operation1.VerifyAllExpectations();
        }

        [Test]
        public void Should_NotSendRejectionEmail_When_ProcessNotAborted()
        {
            var operation1 = MockRepository.GenerateMock<IOperation>();
            operation1.Stub(x => x.OperationResult).Return(new OperationResult { ShouldAbortBookingProcess = false });
            var operation2 = MockRepository.GenerateMock<IOperation>();
            operation2.Stub(x => x.OperationResult).Return(new OperationResult { ShouldAbortBookingProcess = false });
            var operation3 = MockRepository.GenerateMock<IOperation>();
            operation3.Stub(x => x.OperationResult).Return(new OperationResult { ShouldAbortBookingProcess = false });

            var operationsToRun = new List<IOperation> { operation1,operation2, operation3 };

            operationsProvider.Stub(x => x.GetOrderedOperations(reservation.Hotel)).Return(operationsToRun);
            operationsProvider.Stub(x => x.ContainsAllRequiredOperations(operationsToRun)).Return(true);

            emailSystem.Expect(x => x.SendRejectionEmail(reservation)).Repeat.Never();
            var bookingResult = sut.ProcessOperations(reservation);

            emailSystem.VerifyAllExpectations();
        }

        [Test]
        public void Should_ReturnFailure_When_ProcessAbortedDueToRequiredOperationFailure()
        {
            var operation1 = MockRepository.GenerateMock<IOperation>();
            operation1.Stub(x => x.OperationResult).Return(new OperationResult { ShouldAbortBookingProcess = true });
           
            var operationsToRun = new List<IOperation> { operation1 };

            operationsProvider.Stub(x => x.GetOrderedOperations(reservation.Hotel)).Return(operationsToRun);
            operationsProvider.Stub(x => x.ContainsAllRequiredOperations(operationsToRun)).Return(true);

            var bookingResult = sut.ProcessOperations(reservation);

            bookingResult.OverallResult.Should().Equals(Result.Failure);
        }

        [Test]
        public void Should_ReturnSuccess_When_ProcessNotAborted()
        {
            var operation1 = MockRepository.GenerateMock<IOperation>();
            operation1.Stub(x => x.OperationResult).Return(new OperationResult { ShouldAbortBookingProcess = false });

            var operationsToRun = new List<IOperation> { operation1 };

            operationsProvider.Stub(x => x.GetOrderedOperations(reservation.Hotel)).Return(operationsToRun);
            operationsProvider.Stub(x => x.ContainsAllRequiredOperations(operationsToRun)).Return(true);

            var bookingResult = sut.ProcessOperations(reservation);

            bookingResult.OverallResult.Should().Equals(Result.Success);
        }
    }
}
