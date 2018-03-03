using FluentAssertions;
using HotelBooking.App.DataObjects;
using HotelBooking.App.Enums;
using HotelBooking.App.Operations;
using HotelBooking.Domain.Models;
using NUnit.Framework;
using Rhino.Mocks;

namespace HotelBooking.App.Tests.OperationsTests
{
    [TestFixture]
    public class OperationBaseTests
    {
        OperationBase sut;
        MockRepository mocks;
        Reservation reservation;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            sut = (OperationBase)mocks.PartialMock(typeof(OperationBase));
            reservation = new Reservation();
        }

        [Test]
        public void Should_ExecuteOperation_When_Called()
        {
            sut.Stub(x => x.OperationName).Return("name");
            sut.Stub(x => x.IsRequiredToSucceed).Return(true);

            Expect.Call(sut.Execute(Arg<Reservation>.Is.Anything)).Return(true);
            mocks.ReplayAll();

            var bookingResult = new BookingResult();                
            sut.Process(reservation, bookingResult);

            mocks.VerifyAll();
        }

        [Test]
        public void Should_SetShouldAbortProcessToTrue_When_OperationRequiredToSucceedAndFailed()
        {
            sut.Stub(x => x.OperationName).Return("name");
            sut.Stub(x => x.IsRequiredToSucceed).Return(true);
            Expect.Call(sut.Execute(Arg<Reservation>.Is.Anything)).Return(false);

            mocks.ReplayAll();

            var bookingResult = new BookingResult();
            sut.Process(reservation, bookingResult);

            sut.OperationResult.ShouldAbortBookingProcess.Should().Be(true);
        }

        [Test]
        public void Should_SetShouldAbortProcessToFalse_When_OperationNotRequiredToSucceedAndFailed()
        {
            sut.Stub(x => x.OperationName).Return("name");
            sut.Stub(x => x.IsRequiredToSucceed).Return(false);
            Expect.Call(sut.Execute(Arg<Reservation>.Is.Anything)).Return(false);

            mocks.ReplayAll();

            var bookingResult = new BookingResult();
            sut.Process(reservation, bookingResult);

            sut.OperationResult.ShouldAbortBookingProcess.Should().Be(false);
        }

        [Test]
        public void Should_SetShouldAbortProcessToFalse_When_OperationNotRequiredToSucceedAndSucceeded()
        {
            sut.Stub(x => x.OperationName).Return("name");
            sut.Stub(x => x.IsRequiredToSucceed).Return(false);
            Expect.Call(sut.Execute(Arg<Reservation>.Is.Anything)).Return(true);

            mocks.ReplayAll();

            var bookingResult = new BookingResult();
            sut.Process(reservation, bookingResult);

            sut.OperationResult.ShouldAbortBookingProcess.Should().Be(false);
        }

        [Test]
        public void Should_SetExecutionResultToSuccess_When_ExecutionSucceeded()
        {
            sut.Stub(x => x.OperationName).Return("name");
            sut.Stub(x => x.IsRequiredToSucceed).Return(false);
            Expect.Call(sut.Execute(Arg<Reservation>.Is.Anything)).Return(true);

            mocks.ReplayAll();

            var bookingResult = new BookingResult();
            sut.Process(reservation, bookingResult);

            sut.OperationResult.ExecutionResult.Should().Be(Result.Success);
        }

        [Test]
        public void Should_SetExecutionResultToFailure_When_ExecutionFailed()
        {
            sut.Stub(x => x.OperationName).Return("name");
            sut.Stub(x => x.IsRequiredToSucceed).Return(false);
            Expect.Call(sut.Execute(Arg<Reservation>.Is.Anything)).Return(false);

            mocks.ReplayAll();

            var bookingResult = new BookingResult();
            sut.Process(reservation, bookingResult);

            sut.OperationResult.ExecutionResult.Should().Be(Result.Failure);
        }

        [Test]
        public void Should_AddOperationResultToBookingResult_When_Called()
        {
            sut.Stub(x => x.OperationName).Return("name");
            sut.Stub(x => x.IsRequiredToSucceed).Return(false);
            Expect.Call(sut.Execute(Arg<Reservation>.Is.Anything)).Return(true);

            mocks.ReplayAll();

            var bookingResult = new BookingResult();
            sut.Process(reservation, bookingResult);

            bookingResult.OperationResults.Count.Should().Be(1);
            bookingResult.OperationResults[0].OperationName.Should().Be("name");
            bookingResult.OperationResults[0].ExecutionResult.Should().Be(Result.Success);
            bookingResult.OperationResults[0].ShouldAbortBookingProcess.Should().Be(false);
        }
    }
}