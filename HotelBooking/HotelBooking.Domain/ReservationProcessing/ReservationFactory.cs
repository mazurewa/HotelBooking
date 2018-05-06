using System;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.DataAccess;

namespace HotelBooking.Domain.ReservationProcessing
{
    public class ReservationFactory : IReservationFactory
    {
        private IHotelsRepository _hotelRepository;
        private ILogger _logger;

        public ReservationFactory(IHotelsRepository hotelRepository, ILogger logger)
        {
            _hotelRepository = hotelRepository;
            _logger = logger;
        }

        public Reservation CreateReservation(Options options)
        {
            var hotel = _hotelRepository.GetById(options.HotelId);

            if (hotel == null)
            {
                _logger.Write($"Reservation could not be created. Hotel {options.HotelId} not found."); ;
                return null;
            }

            return new Reservation()
            {
                ReservationId = GetReservationId(),
                Hotel = hotel,
                ReservationDate = options.ReservationDate,
                CreditCard = options.CreditCard,
                CustomerEmail = options.CustomerEmail,
                Cost = Convert.ToDecimal(options.Cost)
            };
        }

        protected internal virtual string GetReservationId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
