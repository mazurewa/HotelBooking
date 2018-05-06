using System.Collections.Generic;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Enums;

namespace HotelBooking.Domain.DataAccess
{
    public class HotelsProvider : IHotelsProvider
    {
        public IEnumerable<Hotel> GetHotels()
        {
            return new List<Hotel>
            {
                new Hotel
                {
                    HotelId = "001",
                    HotelConfiguration = new HotelConfiguration
                    {
                        OperationOrder = new List<OperationConfiguration>
                        {
                        new OperationConfiguration(OperationCode.RecheckingPrice, isRequiredToSucceed: true),
                        new OperationConfiguration(OperationCode.Payment, isRequiredToSucceed: true),
                        new OperationConfiguration(OperationCode.Reservation, isRequiredToSucceed: true),
                        new OperationConfiguration(OperationCode.SendingEmail, isRequiredToSucceed: false)
                        }
                    }
                },
                new Hotel
                {
                    HotelId = "002",
                     HotelConfiguration = new HotelConfiguration
                    {
                        OperationOrder = new List<OperationConfiguration>
                        {
                        new OperationConfiguration(OperationCode.RecheckingPrice, isRequiredToSucceed: false),
                        new OperationConfiguration(OperationCode.Reservation, isRequiredToSucceed: true),
                        new OperationConfiguration(OperationCode.SendingEmail, isRequiredToSucceed: false),
                        new OperationConfiguration(OperationCode.Payment, isRequiredToSucceed: true)
                        }
                    }
                },
                new Hotel
                {
                    HotelId = "003",
                     HotelConfiguration = new HotelConfiguration
                    {
                        OperationOrder = new List<OperationConfiguration>
                        {
                        new OperationConfiguration(OperationCode.RecheckingPrice, isRequiredToSucceed: true),
                        new OperationConfiguration(OperationCode.Reservation, isRequiredToSucceed: true),
                        new OperationConfiguration(OperationCode.Payment, isRequiredToSucceed: true)
                        }
                    }
                },
                new Hotel
                {
                    HotelId = "004",
                    HotelConfiguration = new HotelConfiguration
                    {
                       OperationOrder = new List<OperationConfiguration>
                        {
                        new OperationConfiguration(OperationCode.RecheckingPrice, isRequiredToSucceed: true),
                        new OperationConfiguration(OperationCode.Reservation, isRequiredToSucceed: true),
                        new OperationConfiguration(OperationCode.SendingEmail, isRequiredToSucceed: true),
                        new OperationConfiguration(OperationCode.Payment, isRequiredToSucceed: true)
                        }
                    }
                }
            };
        }
    }
}
