using HotelBooking.Domain.Configuration;
using HotelBooking.Domain.DataObjects;
using Ninject;

namespace HotelBooking.Domain.ReservationProcessing
{
    public class ProcessInitiator
    {
        public BookingResult ProcessReservation(string[] args, ILogger logger)
        {
            IKernel kernel = new StandardKernel(new NinjectModule());
            kernel.Bind<ILogger>().ToConstant(logger);

            var reservationManager = kernel.Get<IReservationManager>();
            return reservationManager.ManageReservation(args);
        }
    }
}
