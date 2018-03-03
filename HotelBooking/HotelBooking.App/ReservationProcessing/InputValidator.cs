using System;
using System.Net.Mail;

namespace HotelBooking.App.ReservationProcessing
{
    internal class InputValidator : IInputValidator
    {
        public bool ValidateInputs(string[] args, Options options)
        {
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                return ValidateEmail(options.CustomerEmail) && ValidateReservationDate(options.ReservationDate);
            }
            else
            {
                Console.Write("At least one of the inputs to the booking module has invalid format");
                return false;
            }
        }

        private bool ValidateEmail(string customerEmail)
        {
            try
            {
                MailAddress m = new MailAddress(customerEmail);
                return true;
            }
            catch (FormatException)
            {
                Console.Write("Customer Email has invalid format");
                return false;
            }
        }

        private bool ValidateReservationDate(DateTime reservationDate)
        {
            if (reservationDate < DateTime.Now)
            {
                return true;
            }
            Console.Write("Reservation Date should not be in the future.");
            return false;
        }
    }
}