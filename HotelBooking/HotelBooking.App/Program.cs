using HotelBooking.App.Configuration;
using HotelBooking.App.DataObjects;
using HotelBooking.App.ReservationProcessing;
using Ninject;

namespace HotelBooking.App
{
    public class Program
    {
        // -h hotelId -d reservationDate -c cost -cc creditCard -e email
        public static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new NinjectModule());
            var reservationManager = kernel.Get<IReservationManager>();

            BookingResult result = reservationManager.ProcessReservation(args);            
        }
    }
}
