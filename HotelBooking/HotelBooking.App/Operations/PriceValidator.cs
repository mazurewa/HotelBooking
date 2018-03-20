using HotelBooking.Domain.Models;
using HotelBooking.App.Operations.Services;
using System;
using HotelBooking.Domain.Enums;

namespace HotelBooking.App.Operations
{
    public class PriceValidator : OperationBase
    {
        private HotelPriceValidator _hotelPriceChecker;

        public PriceValidator(HotelPriceValidator hotelPriceChecker)
        {
            _hotelPriceChecker = hotelPriceChecker;
        }

        public override bool IsRequiredToSucceed => false;
        public override string OperationName => OperationCode.RecheckingPrice;
        public override Func<Reservation, bool> Execute => ValidatePrice;

        private bool ValidatePrice(Reservation reservation)
        {
            return _hotelPriceChecker.ValidatePrice(reservation);
        }
    }
}
