namespace AirlaneTicketingSystem.Models
{
    public class FlightQueryDTO
    {
        public DateTime DateFrom {  get; set; }
        public DateTime DateTo { get; set; }
        public string AirportFrom { get; set; }
        public string AirportTo { get; set; }
        public int NumberOfPeople { get; set; }
        public bool IsRoundTrip { get; set; }

        //Paging
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
