using System;

namespace HotelBooking.Domain.Models
{
    public class Reservation
    {
        public string ReservationId { get; set; }
        public Hotel Hotel { get; set; }
        public DateTime ReservationDate { get; set; }
        public decimal Cost { get; set; }
        public string CreditCard { get; set; }
        public string CustomerEmail { get; set; }
    }
}
