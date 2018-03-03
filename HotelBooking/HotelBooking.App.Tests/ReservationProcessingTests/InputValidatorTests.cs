using NUnit.Framework;
using FluentAssertions;
using HotelBooking.App.ReservationProcessing;

namespace HotelBooking.App.Tests.ReservationProcessingTests
{
    [TestFixture]
    class InputValidatorTests
    {
        InputValidator sut;

        [SetUp]
        public void SetUp()
        {
            sut = new InputValidator();
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
            var arguments = new string[] { "-h", "003", "-d", "01/08/2008 14:50:50.42", "-c", "123.34",
            "-r", "1111 2222 3333 4444", "-e", "test@test.pl" };
            var options = new Options();

            var validated = sut.ValidateInputs(arguments, options);

            options.HotelId.Should().Equals("003");
            options.Cost.Should().Equals(123.34);
            options.CreditCard.Should().Equals("1111 2222 3333 4444");
            options.CustomerEmail.Should().Equals("test@test.pl");
            options.ReservationDate.Should().HaveDay(8);
            options.ReservationDate.Should().HaveMonth(1);
            options.ReservationDate.Should().HaveYear(2008);
            options.ReservationDate.Should().HaveHour(14);
            options.ReservationDate.Should().HaveMinute(50);
            options.ReservationDate.Should().HaveSecond(50);
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
