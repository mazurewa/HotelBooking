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
        IOperation operationA;
        IOperation operationB;

        [SetUp]
        public void SetUp()
        {
            operationA = MockRepository.GenerateStub<IOperation>();
            operationA.Stub(x => x.OperationName).Return("name");
            operationB = MockRepository.GenerateStub<IOperation>();
            sut = new OperationEqualityComparer();
        }

        [Test]
        public void Should_BeEqual_When_OperationsNamesEqual()
        {           
            operationB.Stub(x => x.OperationName).Return("name");

            bool equal = sut.Equals(operationA, operationB);

            equal.Should().BeTrue();
        }

        [Test]
        public void Should_BeEqual_When_OperationsNamesEqualAndOperationResultsDifferent()
        {
            operationA.OperationResult = new OperationResult() { ShouldAbortBookingProcess = true };
            operationB.Stub(x => x.OperationName).Return("name");
            operationB.OperationResult = new OperationResult() { ShouldAbortBookingProcess = false };

            bool equal = sut.Equals(operationA, operationB);

            equal.Should().BeTrue();
        }

        [Test]
        public void Should_NotBeEqual_When_OperationsNamesAreDifferent()
        {          
            operationB.Stub(x => x.OperationName).Return("otherName");

            bool equal = sut.Equals(operationA, operationB);

            equal.Should().BeFalse();
        }
    }
}
