using HotelBooking.App.Operations;
using HotelBooking.App.Operations.Services;
using HotelBooking.App.OperationsProcessing;
using HotelBooking.App.ReservationProcessing;
using HotelBooking.Domain.DataAccess;

namespace HotelBooking.App.Configuration
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

            Bind<IOperation>().To<EmailSender>();
            Bind<IOperation>().To<PaymentSystemManager>();
            Bind<IOperation>().To<ReservationSystemManager>();
            Bind<IOperation>().To<PriceValidator>();

            Bind<EmailSystem>().ToSelf();
            Bind<HotelPriceValidator>().ToSelf();
            Bind<HotelReservationSystem>().ToSelf();
            Bind<PaymentSystem>().ToSelf();
        }
    }
}
