namespace AirlaneTicketingSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public string PassengerName { get; set; }
        public string? SeatNumber { get; set; }
        public bool IsCheckedIn { get; set; } = false;
        public DateTime PurchasedAt { get; set; }
    }
}
