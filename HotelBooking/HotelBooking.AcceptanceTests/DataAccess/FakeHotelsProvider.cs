using HotelBooking.Domain.DataAccess;
using HotelBooking.Domain.Enums;
using HotelBooking.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.AcceptanceTests.DataAccess
{
    public class FakeHotelsProvider : IHotelsProvider
    {
        public IEnumerable<Hotel> GetHotels()
        {
            return new List<Hotel>
            {
                new Hotel
                {
                    HotelId = "008",
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
                    HotelId = "009",
                    OperationOrder = new List<string>
                    {
                        OperationCode.RecheckingPrice,
                        OperationCode.SendingEmail
                    }
                },
                new Hotel
                {
                    HotelId = "0010",
                    OperationOrder = new List<string>
                    {
                        OperationCode.RecheckingPrice,
                        OperationCode.ExternalPayment,
                        OperationCode.SendingEmail,
                        OperationCode.ExternalReservation
                    }
                },
                new Hotel
                {
                    HotelId = "0011",
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
