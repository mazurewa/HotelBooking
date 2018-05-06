using FluentAssertions;
using HotelBooking.Domain;
using HotelBooking.Domain.DataObjects;
using HotelBooking.Domain.Enums;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Operations;
using HotelBooking.Domain.OperationsProcessing;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using System.Collections.Generic;

namespace HotelBooking.App.Tests.OperationsProcessingTests
{
    [TestFixture]
    public class OperationsManagerTests
    {
        OperationsManager sut;
        IOperationsProvider operationsProvider;
        Reservation reservation;
        private ILogger logger;

        [SetUp]
        public void SetUp()
        {
            operationsProvider = MockRepository.GenerateMock<IOperationsProvider>();
            reservation = new Reservation { Hotel = new Hotel() };
            logger = MockRepository.GenerateStub<ILogger>();
            sut = new OperationsManager(operationsProvider, logger);
        }

        [Test]
        public void Should_GetOrderedOperationsForHotel()
        {
            operationsProvider.Expect(x => x.GetOrderedOperations(reservation.Hotel)).Return(new List<IOperation>());

            sut.ProcessOperations(reservation);

            operationsProvider.VerifyAllExpectations();
        }

        [Test]
        public void Should_ProcessAtLeastOneOperation()
        {
            var operation = MockRepository.GenerateMock<IOperation>();
            operation.Stub(x => x.Process(reservation)).Return( new OperationResult { ShouldAbortBookingProcess = false });
         
            var operationsToRun = new List<IOperation> { operation };
            operationsProvider.Stub(x => x.GetOrderedOperations(reservation.Hotel)).Return(operationsToRun);

           var bookingResult = sut.ProcessOperations(reservation);

           operation.AssertWasCalled(x => x.Process(reservation));
        }

        [Test]
        public void Should_NotStopProcessing_When_NoOperationsFailed()
        {
            var operation1 = MockRepository.GenerateMock<IOperation>();
            operation1.Stub(x => x.Process(reservation)).Return(new OperationResult { ShouldAbortBookingProcess = false });
            var operation2 = MockRepository.GenerateMock<IOperation>();
            operation2.Stub(x => x.Process(reservation)).Return(new OperationResult { ShouldAbortBookingProcess = false });

            var operationsToRun = new List<IOperation> { operation1, operation2 };
            operationsProvider.Stub(x => x.GetOrderedOperations(reservation.Hotel)).Return(operationsToRun);

            var bookingResult = sut.ProcessOperations(reservation);

            operation1.AssertWasCalled(x => x.Process(reservation));
            operation2.AssertWasCalled(x => x.Process(reservation));
        }

        [Test]
        public void Should_StopProcessing_When_OperationFailedWithAbort()
        {
            var operation1 = MockRepository.GenerateMock<IOperation>();
            operation1.Stub(x => x.Process(reservation)).Return(new OperationResult { ShouldAbortBookingProcess = false });
            var operation2 = MockRepository.GenerateMock<IOperation>();
            operation2.Stub(x => x.Process(reservation)).Return(new OperationResult { ShouldAbortBookingProcess = false });
            var operation3 = MockRepository.GenerateMock<IOperation>();
            operation3.Stub(x => x.Process(reservation)).Return(new OperationResult { ShouldAbortBookingProcess = true });
            var operation4 = MockRepository.GenerateMock<IOperation>();
            operation4.Stub(x => x.Process(reservation)).Return(new OperationResult { ShouldAbortBookingProcess = false });
            var operationsToRun = new List<IOperation> { operation1, operation2, operation3, operation4 };

            operationsProvider.Stub(x => x.GetOrderedOperations(reservation.Hotel)).Return(operationsToRun);

            var bookingResult = sut.ProcessOperations(reservation);

            operation1.AssertWasCalled(x => x.Process(reservation));
            operation2.AssertWasCalled(x => x.Process(reservation));
            operation3.AssertWasCalled(x => x.Process(reservation));
            operation4.AssertWasNotCalled(x => x.Process(reservation));
        }

        [Test]
        public void Should_LogAborted_When_ProcessAborted()
        {
            var operation1 = MockRepository.GenerateMock<IOperation>();
            operation1.Expect(x => x.Process(reservation)).Return(new OperationResult { ShouldAbortBookingProcess = true });
          
            var operationsToRun = new List<IOperation> { operation1 };
            operationsProvider.Stub(x => x.GetOrderedOperations(reservation.Hotel)).Return(operationsToRun);

            logger.Expect(x => x.Write(Arg<string>.Is.Anything)).Repeat.Once();

            var bookingResult = sut.ProcessOperations(reservation);

            operation1.VerifyAllExpectations();
            logger.VerifyAllExpectations();
        }

        [Test]
        public void Should_NotLogAborted_When_ProcessNotAborted()
        {
            var operation1 = MockRepository.GenerateMock<IOperation>();
            operation1.Stub(x => x.Process(reservation)).Return(new OperationResult { ShouldAbortBookingProcess = false });
            var operation2 = MockRepository.GenerateMock<IOperation>();
            operation2.Stub(x => x.Process(reservation)).Return(new OperationResult { ShouldAbortBookingProcess = false });
            var operation3 = MockRepository.GenerateMock<IOperation>();
            operation3.Stub(x => x.Process(reservation)).Return(new OperationResult { ShouldAbortBookingProcess = false });

            var operationsToRun = new List<IOperation> { operation1,operation2, operation3 };

            operationsProvider.Stub(x => x.GetOrderedOperations(reservation.Hotel)).Return(operationsToRun);

            logger.Expect(x => x.Write(Arg<string>.Matches(Text.Contains("ReservationProcess aborted")))).Repeat.Never();
            var bookingResult = sut.ProcessOperations(reservation);

            logger.VerifyAllExpectations();
        }

        [Test]
        public void Should_ReturnFailure_When_ProcessAbortedDueToRequiredOperationFailure()
        {
            var operation1 = MockRepository.GenerateMock<IOperation>();
            operation1.Stub(x => x.Process(reservation)).Return(new OperationResult { ShouldAbortBookingProcess = true });
           
            var operationsToRun = new List<IOperation> { operation1 };

            operationsProvider.Stub(x => x.GetOrderedOperations(reservation.Hotel)).Return(operationsToRun);

            var bookingResult = sut.ProcessOperations(reservation);

            bookingResult.OverallResult.Should().Equals(Result.Failure);
        }

        [Test]
        public void Should_ReturnSuccess_When_ProcessNotAborted()
        {
            var operation1 = MockRepository.GenerateMock<IOperation>();
            operation1.Stub(x => x.Process(reservation)).Return(new OperationResult { ShouldAbortBookingProcess = false });

            var operationsToRun = new List<IOperation> { operation1 };

            operationsProvider.Stub(x => x.GetOrderedOperations(reservation.Hotel)).Return(operationsToRun);

            var bookingResult = sut.ProcessOperations(reservation);

            bookingResult.OverallResult.Should().Equals(Result.Success);
        }
    }
}
