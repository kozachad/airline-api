using AirlaneTicketingSystem.Models;

namespace AirlaneTicketingSystem.Services
{
    public interface IFlightService
    {
        Task<string> AddFlightAsync(AddFlightDTO flightDTO);
        Task<List<Flight>> QueryFlightsAsync(FlightQueryDTO query);
        Task<string> BuyTicketsAsync(BuyTicketDTO dto);
        Task<CheckInResultDTO> CheckInAsync(CheckInDTO dto);
        Task<List<PassengerInfoDTO>> GetPassengerListAsync(PassengerQueryDTO query);
    }
}
