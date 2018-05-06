namespace HotelBooking.Domain.ReservationProcessing
{
    public interface IInputValidator
    {
        bool ValidateInputs(string[] args, Options options);
    }
}
