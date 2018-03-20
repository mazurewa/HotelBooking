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
                        OperationCode.Payment,
                        OperationCode.Reservation,
                        OperationCode.SendingEmail
                    }
                },
                new Hotel
                {
                    HotelId = "002",
                    OperationOrder = new List<string>
                    {
                        OperationCode.RecheckingPrice,
                        OperationCode.Reservation,
                        OperationCode.SendingEmail,
                        OperationCode.Payment
                    }
                },
                new Hotel
                {
                    HotelId = "003",
                    OperationOrder = new List<string>
                    {
                        OperationCode.RecheckingPrice,
                        OperationCode.Payment,
                        OperationCode.Reservation
                    }
                },
                new Hotel
                {
                    HotelId = "004",
                    OperationOrder = new List<string>
                    {
                        OperationCode.Reservation,
                        OperationCode.RecheckingPrice,
                        OperationCode.Payment,
                        OperationCode.SendingEmail
                    }
                }
            };
        }
    }
}
