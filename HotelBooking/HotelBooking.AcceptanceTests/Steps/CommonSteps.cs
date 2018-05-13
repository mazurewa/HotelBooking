using FluentAssertions;
using HotelBooking.AcceptanceTests.Configuration;
using HotelBooking.Domain;
using HotelBooking.Domain.Configuration;
using HotelBooking.Domain.DataObjects;
using HotelBooking.Domain.Enums;
using HotelBooking.Domain.OperationsProcessing;
using HotelBooking.Domain.ReservationProcessing;
using Ninject;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using System.Linq;
using TechTalk.SpecFlow;

namespace HotelBooking.AcceptanceTests
{
    [Binding]
    public class CommonSteps
    {
        IKernel kernel;
        IReservationManager reservationManager;
        string[] arguments;
        BookingResult bookingResult;
        ILogger logger;

        [BeforeScenario]
        public void SetUp()
        {
            kernel = new StandardKernel(new NinjectModule(), new TestNinjectModule());
            logger = new ConsoleLogger();
            kernel.Bind<ILogger>().ToConstant(logger);
            reservationManager = kernel.Get<IReservationManager>();
            ScenarioContext.Current["emailAddress"] = "test@test.com";
        }

        [When(@"I request for hotel reservation")]
        public void WhenIRequestForHotelReservation()
        {
            arguments = new string[] { "-h", "001" , "-d", "01/08/2008 14:50:50.42", "-c", "123.34",
            "-r", "1111 2222 3333 4444", "-e", ScenarioContext.Current["emailAddress"].ToString() };
            bookingResult = reservationManager.ManageReservation(arguments);
            ScenarioContext.Current["bookingResult"] = bookingResult;
        }

        [Then(@"Operation fails")]
        public void ThenOperationFails()
        {
            bookingResult.OperationResults.Single(x => x.OperationName == "bookingRoom").ExecutionResult
                .Should().Be(ExecutionResult.Failure);
        }

        [Then(@"Booking fails")]
        public void ThenBookingFails()
        {
            bookingResult.OverallResult.Should().Be(Result.Failure);
        }

        [Then(@"Abortion is logged")]
        public void ThenAbortionIsLogged()
        {
            logger.AssertWasCalled(x => x.Write(Arg<string>.Matches(Text.Contains("aborted"))));
        }


        [Then(@"Booking result includes no operations results")]
        public void ThenBookingResultIncludesNoOperationsResults()
        {

            ((BookingResult)ScenarioContext.Current["bookingResult"]).OperationResults.Should().BeEmpty();
        }

        [Then(@"Booking result has failure overall result")]
        public void ThenBookingResultHasFailureOverallResult()
        {
            ((BookingResult)ScenarioContext.Current["bookingResult"]).OverallResult.Should().Be(Result.Failure);
        }

        [Then(@"Operations succeeds with warning")]
        public void ThenOperationsSucceedsWithWarning()
        {
            ((BookingResult)ScenarioContext.Current["bookingResult"]).OperationResults.Single(x => x.OperationName == "sendingEmail").ExecutionResult
                .Should().Be(ExecutionResult.Warning);
        }

        [Then(@"Booking succeeds")]
        public void ThenBookingSucceeds()
        {
            bookingResult.OverallResult.Should().Be(Result.Success);
        }

        [Then(@"Warning is logged")]
        public void ThenWarningIsLogged()
        {
            logger.AssertWasCalled(x => x.Write(Arg<string>.Matches(Text.Contains("warning"))));
        }
    }
}
