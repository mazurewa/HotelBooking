using HotelBooking.AcceptanceTests.Operations;
using HotelBooking.Domain.OperationsProcessing;
using System;
using TechTalk.SpecFlow;

namespace HotelBooking.AcceptanceTests.Steps
{
    [Binding]
    public class PaymentFeature
    {
        private Operation operation;

        [Given(@"the customer credit card is not authorized")]
        public void GivenTheCustomerCreditCardIsNotAuthorized()
        {
            operation = new OperationWithFailure("payment");
        }
    }
}
