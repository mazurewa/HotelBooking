using HotelBooking.App.Operations;
using HotelBooking.App.Operations.ExternalSystems;
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
            Bind<IOperation>().To<ExternalPaymentSystemManager>();
            Bind<IOperation>().To<ExternalReservationSystemManager>();
            Bind<IOperation>().To<PriceValidator>();

            Bind<IExternalEmailSystem>().To<ExternalEmailSystem>();
            Bind<IExternalHotelPriceValidator>().To<ExternalHotelPriceValidator>();
            Bind<IExternalHotelReservationSystem>().To<ExternalHotelReservationSystem>();
            Bind<IExternalPaymentSystem>().To<ExternalPaymentSystem>();
        }
    }
}
