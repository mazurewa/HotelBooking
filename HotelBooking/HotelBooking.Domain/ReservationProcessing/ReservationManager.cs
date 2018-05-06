using HotelBooking.Domain.OperationsProcessing;
using HotelBooking.Domain.DataObjects;
using HotelBooking.Domain.Enums;

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
                bookingResult.ReservationId = reservation.ReservationId;

                if (reservation != null)
                {
                    bookingResult = _operationsManager.ProcessOperations(reservation);
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
        }
    }
}