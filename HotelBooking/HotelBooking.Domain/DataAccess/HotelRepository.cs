using HotelBooking.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Domain.DataAccess
{
    public class HotelRepository : IHotelsRepository
    {
        IEnumerable<Hotel> _hotels;
        private IHotelsProvider _hotelsProvider;

        public HotelRepository(IHotelsProvider hotelsProvider)
        {
            _hotelsProvider = hotelsProvider;
        }

        public IEnumerable<Hotel> Hotels
        {
            get
            {
                if (_hotels == null)
                {
                    _hotels = _hotelsProvider.GetHotels();
                }
                return _hotels;
            }          
        }

        public Hotel GetById(string id)
        {
            return Hotels.FirstOrDefault(h => h.HotelId.Equals(id));
        }
    }
}
