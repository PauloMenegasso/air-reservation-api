namespace AirReservationApi.Domain
{
    public class ReturnMessage
    {
        public ReturnMessage(string message)
        {
            this.Message = message;
        }
        public string Message { get; set; }
    }
}