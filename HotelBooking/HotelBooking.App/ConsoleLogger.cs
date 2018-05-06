using HotelBooking.Domain;
using System;

namespace HotelBooking.App
{
    public class ConsoleLogger : ILogger
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
