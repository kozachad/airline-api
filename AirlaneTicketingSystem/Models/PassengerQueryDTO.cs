namespace AirlaneTicketingSystem.Models
{
    public class PassengerQueryDTO
    {
        public int FlightId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
