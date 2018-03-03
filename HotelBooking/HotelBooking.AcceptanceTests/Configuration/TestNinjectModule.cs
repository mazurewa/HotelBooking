using HotelBooking.AcceptanceTests.DataAccess;
using HotelBooking.Domain.DataAccess;
using Ninject.Modules;

namespace HotelBooking.AcceptanceTests.Configuration
{
    public class TestNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Rebind<IHotelsProvider>().To<FakeHotelsProvider>();
        }
    }
}
