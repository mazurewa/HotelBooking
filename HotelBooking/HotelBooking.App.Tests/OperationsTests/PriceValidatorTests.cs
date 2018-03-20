using HotelBooking.App.Operations;
using HotelBooking.App.Operations.Services;
using HotelBooking.Domain.Models;
using NUnit.Framework;
using Rhino.Mocks;

namespace HotelBooking.App.Tests.OperationsTests
{
    [TestFixture]
    public class PriceValidatorTests
    {
        PriceValidator sut;
        HotelPriceValidator hotelPriceValidator;

        [SetUp]
        public void SetUp()
        {
            hotelPriceValidator = MockRepository.GenerateMock<HotelPriceValidator>();
            sut = new PriceValidator(hotelPriceValidator);
        }

        [Test]
        public void Should_CallEmailSystem_When_Executing()
        {
            var reservation = new Reservation();
            hotelPriceValidator.Expect(x => x.ValidatePrice(reservation)).Return(true);
            sut.Execute(reservation);

            hotelPriceValidator.VerifyAllExpectations();
        }
    }
}
