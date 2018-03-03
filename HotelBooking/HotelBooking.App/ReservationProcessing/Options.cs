using CommandLine;
using System;

namespace HotelBooking.App.ReservationProcessing
{
    public class Options
    {
        [Option('h', "hotelId", Required = true, HelpText = "Hotel Id.")]
        public string HotelId { get; set; }
        [Option('d', "date", Required = true, HelpText = "Reservation date.")]
        public DateTime ReservationDate { get; set; }
        [Option('c', "cost", Required = true, HelpText = "Total cost.")]
        public double Cost { get; set; }
        [Option('r', "creditCard", Required = true, HelpText = "Customer's credit card number.")]
        public string CreditCard { get; set; }
        [Option('e', "email", Required = true, HelpText = "Customer's email.")]
        public string CustomerEmail { get; set; }
    }
}