using HotelBooking.Domain.Models;
using HotelBooking.App.Operations.ExternalSystems;
using System;
using HotelBooking.Domain.Enums;

namespace HotelBooking.App.Operations
{
    public class EmailSender : OperationBase
    {
        private IExternalEmailSystem _emailSender;

        public EmailSender(IExternalEmailSystem emailSender)
        {
            _emailSender = emailSender;
        }

        public override bool IsRequiredToSucceed => false;
        public override string OperationName => OperationCode.SendingEmail;
        public override Func<Reservation, bool> Execute => SendConfirmationEmail;

        private bool SendConfirmationEmail(Reservation reservation)
        {
            return _emailSender.SendConfirmationEmail(reservation);
        }
    }
}
