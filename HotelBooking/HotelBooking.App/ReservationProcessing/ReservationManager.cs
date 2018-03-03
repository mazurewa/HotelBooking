using System;
using HotelBooking.App.OperationsProcessing;
using HotelBooking.App.DataObjects;
using HotelBooking.App.Enums;

namespace HotelBooking.App.ReservationProcessing
{
    public class ReservationManager : IReservationManager
    {
        private IInputValidator _inputValidator;
        private IReservationFactory _reservationFactory;
        private IOperationsManager _operationsManager;

        public ReservationManager(IInputValidator inputValidator, IReservationFactory reservationFactory, 
            IOperationsManager operationsManager)
        {
            _inputValidator = inputValidator;
            _reservationFactory = reservationFactory;
            _operationsManager = operationsManager;
        }

        public BookingResult ProcessReservation(string[] args)
        {
            var bookingResult = new BookingResult();
            var options = new Options();

            if (_inputValidator.ValidateInputs(args, options))
            {
                var reservation = _reservationFactory.CreateReservation(options);
                if (reservation != null)
                {
                    bookingResult = _operationsManager.ProcessOperations(reservation);
                }
            }

            WriteBookingResultToConsole(bookingResult);
            return bookingResult;
        }       

        private void WriteBookingResultToConsole(BookingResult bookingResult)
        {
            Console.WriteLine();

            if (bookingResult.OverallResult == Result.Success)
            {
                Console.WriteLine("Reservation successful.");
            }
            else if (bookingResult.OverallResult == Result.Failure)
            {
                Console.WriteLine("Reservation failed.");
            }
        }
    }
}