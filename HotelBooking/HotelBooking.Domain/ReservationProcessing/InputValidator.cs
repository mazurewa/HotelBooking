using System;
using System.Text.RegularExpressions;

namespace HotelBooking.Domain.ReservationProcessing
{
    internal class InputValidator : IInputValidator
    {
        private ILogger _logger;

        public InputValidator(ILogger logger)
        {
            _logger = logger;
        }

        public bool ValidateInputs(string[] args, Options options)
        {
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                return ValidateEmail(options.CustomerEmail) && ValidateReservationDate(options.ReservationDate);
            }
            else
            {
                _logger.Write("At least one of the inputs to the booking module has invalid format");
                return false;
            }
        }

        private bool ValidateEmail(string customerEmail)
        {
            var emailPattern = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            var match = emailPattern.Match(customerEmail);
            if (!match.Success)
            {
                _logger.Write("Customer Email has invalid format");
                return false;
            }
            return true;
        }

        private bool ValidateReservationDate(DateTime reservationDate)
        {
            if (reservationDate < DateTime.Now)
            {
                return true;
            }
            _logger.Write("Reservation Date should not be in the future.");
            return false;
        }
    }
}