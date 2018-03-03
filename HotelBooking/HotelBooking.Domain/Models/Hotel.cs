using System.Collections.Generic;

namespace HotelBooking.Domain.Models
{
    public class Hotel
    {
        public string HotelId { get; set; }
        public List<string> OperationOrder { get; set; }
    }
}