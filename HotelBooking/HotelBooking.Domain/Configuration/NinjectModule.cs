using HotelBooking.Domain.OperationsProcessing;
using HotelBooking.Domain.ReservationProcessing;
using HotelBooking.Domain.DataAccess;

namespace HotelBooking.Domain.Configuration
{
    public class NinjectModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IReservationManager>().To<ReservationManager>();
            Bind<IInputValidator>().To<InputValidator>();
            Bind<IReservationFactory>().To<ReservationFactory>();
            Bind<IOperationsManager>().To<OperationsManager>();
            Bind<IOperationsProvider>().To<OperationsProvider>();
            Bind<IHotelsProvider>().To<HotelsProvider>();
            Bind<IHotelsRepository>().To<HotelRepository>();
            Bind<IAllOperationsProvider>().To<AllOperationsProvider>();
        }
    }
}
