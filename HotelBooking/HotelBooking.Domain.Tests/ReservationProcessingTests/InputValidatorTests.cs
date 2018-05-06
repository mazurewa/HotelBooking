using NUnit.Framework;
using FluentAssertions;
using HotelBooking.Domain.ReservationProcessing;
using Rhino.Mocks;
using HotelBooking.Domain;
using System;

namespace HotelBooking.App.Tests.ReservationProcessingTests
{
    [TestFixture]
    class InputValidatorTests
    {
        private ILogger logger;
        InputValidator sut;

        [SetUp]
        public void SetUp()
        {
            logger = MockRepository.GenerateStub<ILogger>();
            sut = new InputValidator(logger);
        }

        [Test]
        public void Should_ReturnTrue_When_ValidInputs()
        {
            var arguments = new string[] { "-h", "003", "-d", "01/08/2008 14:50:50.42", "-c", "123.34",
            "-r", "1111 2222 3333 4444", "-e", "test@test.pl" };
            var options = new Options();

            var validated = sut.ValidateInputs(arguments, options);

            validated.Should().BeTrue();
        }

        [Test]
        public void Should_ConvertToOptions_When_ValidInputs()
        {
            var arguments = new string[] { "-h", "003", "-d", "01/08/2008 14:50:50", "-c", "123.34",
            "-r", "1111 2222 3333 4444", "-e", "test@test.pl" };
            var options = new Options();
            var expectedOptions = new Options
            {
                HotelId = "003",
                Cost = 123.34,
                CreditCard = "1111 2222 3333 4444",
                CustomerEmail = "test@test.pl",
                ReservationDate = new DateTime(2008, 1, 8, 14, 50, 50)
            };

            var validated = sut.ValidateInputs(arguments, options);

            options.Should().BeEquivalentTo(expectedOptions);
        }

        [Test]
        public void Should_ConvertDateTime_When_ValidInputs()
        {
            var arguments = new string[] { "-h", "003", "-d", "01/08/2008 14:50:50.42", "-c", "123.34",
            "-r", "1111 2222 3333 4444", "-e", "test@test.pl" };
            var expectedDateTime = new DateTime(2008, 1, 8, 14, 50, 50);
            var options = new Options();

            var validated = sut.ValidateInputs(arguments, options);

            options.ReservationDate.Should().BeSameDateAs(expectedDateTime);
        }

        [Test]
        public void Should_ReturnTrue_When_DifferentOrderOfArgs()
        {
            var arguments = new string[] { "-d", "01/08/2008 14:50:50.42", "-h", "003", "-c", "123.34",
            "-r", "1111 2222 3333 4444", "-e", "test@test.pl" };
            var options = new Options();

            var validated = sut.ValidateInputs(arguments, options);

            validated.Should().BeTrue();
        }

        [Test]
        public void Should_ReturnFalse_When_InvalidArgumentFormat()
        {
            var arguments = new string[] { "-d", "invalid argument", "-h", "003", "-c", "123.34",
            "-r", "1111 2222 3333 4444", "-e", "test@test.pl" };
            var options = new Options();

            var validated = sut.ValidateInputs(arguments, options);

            validated.Should().BeFalse();
        }

        [Test]
        public void Should_ReturnFalse_When_InvalidEmailFormat()
        {
            var arguments = new string[] { "-d", "01/08/2008 14:50:50.42", "-h", "003", "-c", "123.34",
            "-r", "1111 2222 3333 4444", "-e", "test.test.pl" };
            var options = new Options();

            var validated = sut.ValidateInputs(arguments, options);

            validated.Should().BeFalse();
        }


        [Test]
        public void Should_ReturnFalse_When_ReservationDateInTheFuture()
        {
            var arguments = new string[] { "-d", "01/08/2090 14:50:50.42", "-h", "003", "-c", "123.34",
            "-r", "1111 2222 3333 4444", "-e", "test@test.pl" };
            var options = new Options();

            var validated = sut.ValidateInputs(arguments, options);

            validated.Should().BeFalse();
        }
    }
}
