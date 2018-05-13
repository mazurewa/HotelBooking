using HotelBooking.Domain.OperationsProcessing;
using HotelBooking.Domain.DataObjects;
using HotelBooking.Domain.Enums;
using System.Linq;
using System.Collections.Generic;

namespace HotelBooking.Domain.ReservationProcessing
{
    public class ReservationManager : IReservationManager
    {
        private IInputValidator _inputValidator;
        private IReservationFactory _reservationFactory;
        private IOperationsManager _operationsManager;
        private ILogger _logger;

        public ReservationManager(IInputValidator inputValidator, IReservationFactory reservationFactory,
            IOperationsManager operationsManager, ILogger logger)
        {
            _inputValidator = inputValidator;
            _reservationFactory = reservationFactory;
            _operationsManager = operationsManager;
            _logger = logger;
        }

        public BookingResult ManageReservation(string[] args)
        {
            var bookingResult = new BookingResult();
            var options = new Options();

            if (_inputValidator.ValidateInputs(args, options))
            {
                var reservation = _reservationFactory.CreateReservation(options);

                if (reservation != null)
                {
                    bookingResult = _operationsManager.ProcessOperations(reservation);
                    bookingResult.ReservationId = reservation.Id;
                }
            }

            LogBookingResult(bookingResult);
            return bookingResult;
        }

        private void LogBookingResult(BookingResult bookingResult)
        {
            if (bookingResult.OverallResult == Result.Success)
            {
                _logger.Write("Reservation successful.");
            }
            else if (bookingResult.OverallResult == Result.Failure)
            {
                _logger.Write("Reservation failed.");
            }

            var warnings = bookingResult.OperationResults.Where(x => x.ExecutionResult == ExecutionResult.Warning);
            if (warnings.Count() > 0)
            {
                LogWarnings(warnings);
            }
        }

        private void LogWarnings(IEnumerable<OperationResult> warnings)
        {
            foreach( var warning in warnings)
            {
                _logger.Write($"Warning when processing {warning.OperationName}");
            }
        }
    }
}