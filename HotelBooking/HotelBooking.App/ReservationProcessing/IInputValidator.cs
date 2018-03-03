using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.App.ReservationProcessing
{
    public interface IInputValidator
    {
        bool ValidateInputs(string[] args, Options options);
    }
}
