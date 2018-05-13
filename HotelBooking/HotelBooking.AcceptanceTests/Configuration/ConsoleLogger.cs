using HotelBooking.Domain;
using System;

namespace HotelBooking.AcceptanceTests.Configuration
{
    public class ConsoleLogger : ILogger
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
