using FluentAssertions;
using HotelBooking.App.DataObjects;
using HotelBooking.App.Operations;
using HotelBooking.App.OperationsProcessing;
using NUnit.Framework;
using Rhino.Mocks;

namespace HotelBooking.App.Tests.OperationsProcessingTests
{
    public class OperationEqualityComparerTests
    {
        OperationEqualityComparer sut;

        [Test]
        public void Should_BeEqual_When_OperationsNamesEqual()
        {
            IOperation operationA = MockRepository.GenerateStub<IOperation>();
            operationA.Stub(x => x.OperationName).Return("name");
            IOperation operationB = MockRepository.GenerateStub<IOperation>();
            operationB.Stub(x => x.OperationName).Return("name");
            sut = new OperationEqualityComparer();

            bool equal = sut.Equals(operationA, operationB);

            equal.Should().BeTrue();
        }

        [Test]
        public void Should_BeEqual_When_OperationsNamesEqualAndOperationResultsDifferent()
        {
            IOperation operationA = MockRepository.GenerateStub<IOperation>();
            operationA.Stub(x => x.OperationName).Return("name");
            operationA.OperationResult = new OperationResult() { ShouldAbortBookingProcess = true };
            IOperation operationB = MockRepository.GenerateStub<IOperation>();
            operationB.Stub(x => x.OperationName).Return("name");
            operationB.OperationResult = new OperationResult() { ShouldAbortBookingProcess = false };
            sut = new OperationEqualityComparer();

            bool equal = sut.Equals(operationA, operationB);

            equal.Should().BeTrue();
        }

        [Test]
        public void Should_NotBeEqual_When_OperationsNamesAreDifferent()
        {
            IOperation operationA = MockRepository.GenerateStub<IOperation>();
            operationA.Stub(x => x.OperationName).Return("name");
            IOperation operationB = MockRepository.GenerateStub<IOperation>();
            operationB.Stub(x => x.OperationName).Return("otherName");
            sut = new OperationEqualityComparer();

            bool equal = sut.Equals(operationA, operationB);

            equal.Should().BeFalse();
        }
    }
}
