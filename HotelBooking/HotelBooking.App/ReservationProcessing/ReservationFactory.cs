using System;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.DataAccess;

namespace HotelBooking.App.ReservationProcessing
{
    public class ReservationFactory : IReservationFactory
    {
        private IHotelsRepository _hotelRepository;

        public ReservationFactory(IHotelsRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public Reservation CreateReservation(Options options)
        {
            var hotel = _hotelRepository.GetById(options.HotelId);

            if (hotel == null)
            {
                Console.WriteLine($"Reservation could not be created. Hotel {options.HotelId} not found."); ;
                return null;
            }

            return new Reservation()
            {
                ReservationId = Guid.NewGuid().ToString(),
                Hotel = hotel,
                ReservationDate = options.ReservationDate,
                CreditCard = options.CreditCard,
                CustomerEmail = options.CustomerEmail,
                Cost = Convert.ToDecimal(options.Cost)
            };
        }
    }
}
