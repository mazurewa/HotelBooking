using HotelBooking.Domain.DataObjects;
using HotelBooking.Domain.ReservationProcessing;

namespace HotelBooking.App
{
    public class Program
    {
        // -h hotelId -d reservationDate -c cost -cc creditCard -e email
        public static void Main(string[] args)
        {
            var processInitiator = new ProcessInitiator();
            BookingResult result = processInitiator.ProcessReservation(args, new ConsoleLogger());            
        }
    }
}
