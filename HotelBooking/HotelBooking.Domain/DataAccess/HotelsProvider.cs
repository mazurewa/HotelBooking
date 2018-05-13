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
                       OperationOrder = new List<string>
                        {
                            OperationCode.RecheckingPrice,
                            OperationCode.Payment,
                            OperationCode.Reservation,
                            OperationCode.SendingEmail
                        }
                    }
                },
                new Hotel
                {
                    HotelId = "002",
                    HotelConfiguration = new HotelConfiguration
                    {
                       OperationOrder = new List<string>
                        {
                            OperationCode.RecheckingPrice,
                            OperationCode.Reservation,
                            OperationCode.Payment,
                            OperationCode.SendingEmail
                        }
                    }
                },
                new Hotel
                {
                    HotelId = "003",
                     HotelConfiguration = new HotelConfiguration
                     {
                         OperationOrder = new List<string>
                         {
                            OperationCode.RecheckingPrice,
                            OperationCode.Reservation,
                            OperationCode.Payment
                         }
                     }
                },
                new Hotel
                {
                    HotelId = "004",
                    HotelConfiguration = new HotelConfiguration
                    {
                       OperationOrder = new List<string>
                        {
                            OperationCode.RecheckingPrice,
                            OperationCode.Reservation,
                            OperationCode.SendingEmail,
                            OperationCode.Payment
                        }
                    }
                }
            };
        }
    }
}
