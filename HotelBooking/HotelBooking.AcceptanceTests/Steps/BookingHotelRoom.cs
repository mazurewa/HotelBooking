using HotelBooking.AcceptanceTests.Operations;
using HotelBooking.Domain.OperationsProcessing;
using TechTalk.SpecFlow;

namespace HotelBooking.AcceptanceTests.Steps
{
    [Binding]
    public class BookingHotelRoom
    {
        private Operation operation;

        [Given(@"No rooms are available")]
        public void GivenNoRoomsAreAvailable()
        {
            operation = new OperationWithFailure("bookingRoom");
        }
    }
}
