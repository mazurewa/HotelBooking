using HotelBooking.Domain.DataObjects;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.OperationsProcessing;
using NUnit.Framework;
using Rhino.Mocks;

namespace HotelBooking.Domain.Tests.OperationsTests
{
    [TestFixture]
    public class OperationTests
    {
        Operation sut;
        Reservation reservation;

        [SetUp]
        public void SetUp()
        {
            reservation = new Reservation();
        }

        [Test]
        public void Should_ExecuteOperation()
        {
            sut = MockRepository.GeneratePartialMock<Operation>("OperationName");
            sut.Expect(x => x.Execute(Arg<Reservation>.Is.Anything)).Return(ExecutionResult.Success);

            sut.Process(reservation);

            sut.VerifyAllExpectations();
        }
    }
}