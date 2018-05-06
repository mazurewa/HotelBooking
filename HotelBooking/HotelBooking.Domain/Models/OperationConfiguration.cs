namespace HotelBooking.Domain.Models
{
    public class OperationConfiguration
    {
        public string Name { get; set; }
        public bool IsRequiredToSucceed { get; set; }

        public OperationConfiguration(string name, bool isRequiredToSucceed)
        {
            Name = name;
            IsRequiredToSucceed = isRequiredToSucceed;
        }
    }
}