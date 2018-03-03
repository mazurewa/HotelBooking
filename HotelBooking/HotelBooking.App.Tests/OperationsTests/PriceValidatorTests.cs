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
        IExternalHotelPriceValidator externalHotelPriceValidator;

        [SetUp]
        public void SetUp()
        {
            externalHotelPriceValidator = MockRepository.GenerateStub<IExternalHotelPriceValidator>();
            sut = new PriceValidator(externalHotelPriceValidator);
        }

        [Test]
        public void Should_CallExternalEmailSystem_When_Executing()
        {
            var reservation = new Reservation();
            sut.Execute(reservation);

            externalHotelPriceValidator.AssertWasCalled(x => x.ValidatePrice(reservation));
        }
    }
}
