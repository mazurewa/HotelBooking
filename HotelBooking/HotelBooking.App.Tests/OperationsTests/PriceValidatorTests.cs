using HotelBooking.App.Operations;
using HotelBooking.App.Operations.ExternalSystems;
using HotelBooking.Domain.Models;
using NUnit.Framework;
using Rhino.Mocks;

namespace HotelBooking.App.Tests.OperationsTests
{
    [TestFixture]
    public class PriceValidatorTests
    {
        PriceValidator sut;
        ExternalHotelPriceValidator externalHotelPriceValidator;

        [SetUp]
        public void SetUp()
        {
            externalHotelPriceValidator = MockRepository.GenerateMock<ExternalHotelPriceValidator>();
            sut = new PriceValidator(externalHotelPriceValidator);
        }

        [Test]
        public void Should_CallExternalEmailSystem_When_Executing()
        {
            var reservation = new Reservation();
            externalHotelPriceValidator.Expect(x => x.ValidatePrice(reservation)).Return(true);
            sut.Execute(reservation);

            externalHotelPriceValidator.VerifyAllExpectations();
        }
    }
}
