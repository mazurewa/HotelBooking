using FluentAssertions;
using HotelBooking.AcceptanceTests.Operations;
using HotelBooking.Domain;
using HotelBooking.Domain.DataObjects;
using HotelBooking.Domain.Enums;
using HotelBooking.Domain.OperationsProcessing;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using System.Linq;
using TechTalk.SpecFlow;

namespace HotelBooking.AcceptanceTests
{
    [Binding]
    [Category("email")]
    public sealed class SendingEmail
    {
        private OperationWithWarning operation;
        private ILogger logger;
        private BookingResult bookingResult;

        [Given(@"the email address input is in invalid format")]
        public void GivenTheEmailAddressInputIsInInvalidFormat()
        {
            ScenarioContext.Current["emailAddress"] = "invalidEmail";
        }

        [Given(@"the email has not been delivered")]
        public void GivenTheEmailHasNotBeenDelivered()
        {
            operation = new OperationWithWarning("sendingEmail");
        }
    }
}
