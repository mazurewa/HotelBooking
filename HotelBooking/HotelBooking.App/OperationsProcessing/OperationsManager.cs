using System;
using System.Linq;
using HotelBooking.Domain.Models;
using HotelBooking.App.Operations.ExternalSystems;
using HotelBooking.App.DataObjects;
using HotelBooking.App.Enums;

namespace HotelBooking.App.OperationsProcessing
{
    public class OperationsManager : IOperationsManager
    {
        private IOperationsProvider _operationsProvider;
        private ExternalEmailSystem _emailSender;

        public OperationsManager(IOperationsProvider operationsProvider, ExternalEmailSystem emailSender)
        {
            _operationsProvider = operationsProvider;
            _emailSender = emailSender;
        }

        public BookingResult ProcessOperations(Reservation reservation)
        {
            var orderedOperations = _operationsProvider.GetOrderedOperations(reservation.Hotel);
            var bookingResult = new BookingResult();

            foreach (var operation in orderedOperations)
            {
                operation.Process(reservation, bookingResult);

                if (operation.OperationResult.ShouldAbortBookingProcess)
                {
                    AbortProcess(reservation, operation.OperationName);
                    break;
                }
            }

            return bookingResult;
        }

        private void AbortProcess(Reservation reservation, string operationName)
        {
            _emailSender.SendRejectionEmail(reservation);
            Console.WriteLine($"Reservation process aborted due to required operation {operationName} failure"); ;
        }
    }
}
