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
                    OperationOrder = new List<string>
                    {
                        OperationCode.RecheckingPrice,
                        OperationCode.ExternalPayment,
                        OperationCode.ExternalReservation,
                        OperationCode.SendingEmail
                    }
                },
                new Hotel
                {
                    HotelId = "002",
                    OperationOrder = new List<string>
                    {
                        OperationCode.RecheckingPrice,
                        OperationCode.ExternalReservation,
                        OperationCode.SendingEmail,
                        OperationCode.ExternalPayment
                    }
                },
                new Hotel
                {
                    HotelId = "003",
                    OperationOrder = new List<string>
                    {
                        OperationCode.RecheckingPrice,
                        OperationCode.ExternalPayment,
                        OperationCode.ExternalReservation
                    }
                },
                new Hotel
                {
                    HotelId = "004",
                    OperationOrder = new List<string>
                    {
                        OperationCode.ExternalReservation,
                        OperationCode.RecheckingPrice,
                        OperationCode.ExternalPayment,
                        OperationCode.SendingEmail
                    }
                }
            };
        }
    }
}
