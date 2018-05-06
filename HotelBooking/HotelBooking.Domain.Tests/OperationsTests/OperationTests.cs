using FluentAssertions;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Operations;
using NUnit.Framework;
using Rhino.Mocks;

namespace HotelBooking.Domain.Tests.OperationsTests
{
    [TestFixture]
    public class OperationTests
    {
        Operation sut;
        Reservation reservation;
        OperationConfiguration operationConfiguration;

        [SetUp]
        public void SetUp()
        {
            reservation = new Reservation();
            operationConfiguration = MockRepository.GenerateStub<OperationConfiguration>
                ("OperationName", true);
        }

        [Test]
        public void Should_ExecuteOperation()
        {
            sut = MockRepository.GeneratePartialMock<Operation>(operationConfiguration);
            sut.Expect(x => x.Execute(Arg<Reservation>.Is.Anything)).Return(true);

            sut.Process(reservation);

            sut.VerifyAllExpectations();
        }

        [Test]
        public void Should_Abort_When_OperationRequiredToSucceedAndFailed()
        {
            var operationConfiguration = new OperationConfiguration("OperationName", isRequiredToSucceed: true);
            sut = MockRepository.GeneratePartialMock<Operation>(operationConfiguration);
            sut.Stub(x => x.Execute(reservation)).Return(false);
            
            var operationResult = sut.Process(reservation);

            operationResult.ShouldAbortBookingProcess.Should().Be(true);
        }

        [Test]
        public void Should_NotAbort_When_OperationNotRequiredToSucceedAndFailed()
        {
            var operationConfiguration = new OperationConfiguration("OperationName", isRequiredToSucceed: false);
            sut = MockRepository.GeneratePartialMock<Operation>(operationConfiguration);
            sut.Stub(x => x.Execute(reservation)).Return(false);

            var operationResult = sut.Process(reservation);

            operationResult.ShouldAbortBookingProcess.Should().Be(false);
        }

        [Test]
        public void Should_NotAbort_When_OperationNotRequiredToSucceedAndSucceeded()
        {
            var operationConfiguration = new OperationConfiguration("OperationName", isRequiredToSucceed: true);
            sut = MockRepository.GeneratePartialMock<Operation>(operationConfiguration);
            sut.Stub(x => x.Execute(reservation)).Return(true);
            
            var operationResult = sut.Process(reservation);

            operationResult.ShouldAbortBookingProcess.Should().Be(false);
        }
    }
}