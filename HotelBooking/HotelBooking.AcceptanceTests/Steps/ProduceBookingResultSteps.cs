using FluentAssertions;
using HotelBooking.AcceptanceTests.Configuration;
using HotelBooking.AcceptanceTests.DataAccess;
using HotelBooking.App.Configuration;
using HotelBooking.App.DataObjects;
using HotelBooking.App.Enums;
using HotelBooking.App.ReservationProcessing;
using HotelBooking.Domain.DataAccess;
using Ninject;
using System;
using TechTalk.SpecFlow;

namespace HotelBooking.AcceptanceTests.Steps
{
    [Binding]
    public class ProduceBookingResultSteps
    {
        IKernel kernel;
        IReservationManager reservationManager;
        string[] arguments;
        IHotelsProvider hotelsProvider;
        BookingResult bookingResult;
        string hotelId;
        string emailAddress;

        [BeforeScenario]
        public void SetUp()
        {
            kernel = new StandardKernel(new NinjectModule(), new TestNinjectModule());
            reservationManager = kernel.Get<IReservationManager>();
            hotelId = "008";
            emailAddress = "test@test.pl";
        }

        [Given(@"the hotel is in database")]
        public void GivenTheHotelIsInDatabase()
        {
            hotelId = "008";
        }

        [Given(@"the hotel is not in database")]
        public void GivenTheHotelIsNotInDatabase()
        {
            hotelId = "001234";
        }

        [Given(@"the email address input is in invalid format")]
        public void GivenTheEmailAddressInputIsInInvalidFormat()
        {
            emailAddress = "test.test.pl";
        }

        [Given(@"the hotel does not have all required operations included")]
        public void GivenTheHotelDoesNotHaveAllRequiredOperationsIncluded()
        {
            hotelId = "009";
        }

        [When(@"I request for hotel reservation")]
        public void WhenIRequestForHotelReservation()
        {
            arguments = new string[] { "-h", hotelId , "-d", "01/08/2008 14:50:50.42", "-c", "123.34",
            "-r", "1111 2222 3333 4444", "-e", emailAddress };
            hotelsProvider = new FakeHotelsProvider();
            bookingResult = reservationManager.ProcessReservation(arguments);
        }

        [Then(@"Booking result includes all opeartions results")]
        public void ThenBookingResultIncludesAllOpeartionsResults()
        {
            bookingResult.OperationResults.Count.Should().Be(4);
        }

        [Then(@"Booking result has success overall result")]
        public void ThenBookingResultHasSuccessOverallResult()
        {
            bookingResult.OverallResult = Result.Success;
        }

        [Then(@"Booking result includes no operations results")]
        public void ThenBookingResultIncludesNoOperationsResults()
        {
            bookingResult.OperationResults.Count.Should().Be(0);
        }

        [Then(@"Booking result has failure overall result")]
        public void ThenBookingResultHasFailureOverallResult()
        {
            bookingResult.OverallResult = Result.Failure;
        }
    }
}
