﻿using HotelBooking.Domain.Models;
using System;

namespace HotelBooking.App.Operations.ExternalSystems
{
    public class ExternalEmailSystem
    {
        public virtual bool SendConfirmationEmail(Reservation reservation)
        {
            Console.WriteLine($"Confirmation email sent");
            return true;
        }

        public virtual bool SendRejectionEmail(Reservation reservation)
        {
            Console.WriteLine($"Rejection email sent");
            return true;
        }
    }
}
