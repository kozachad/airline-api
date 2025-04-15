using AirlaneTicketingSystem.Models;
using AirlaneTicketingSystem.Services;
using AirlaneTicketingSystem.Data;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


public class FlightService : IFlightService
{
    private readonly AirlineDbContext _context;

    public FlightService(AirlineDbContext context)
    {
        _context = context;
    }

    public async Task<string> AddFlightAsync(AddFlightDTO dto)
    {
        var flight = new Flight
        {
            DateFrom = dto.DateFrom,
            DateTo = dto.DateTo,
            AirportFrom = dto.AirportFrom,
            AirportTo = dto.AirportTo,
            Duration = dto.Duration,
            Capacity = dto.Capacity,
            AvailableSeats = dto.Capacity
        };

        _context.Flights.Add(flight);
        var affected = await _context.SaveChangesAsync();

        return affected > 0 ? "Flight Added" : "Save Failed";
    }

    public async Task<List<Flight>> QueryFlightsAsync(FlightQueryDTO query)
    {
        var flightsQuery = _context.Flights
            .Where(f =>
                f.DateFrom >= query.DateFrom &&
                f.DateTo <= query.DateTo &&
                f.AirportFrom == query.AirportFrom &&
                f.AirportTo == query.AirportTo &&
                f.AvailableSeats >= query.NumberOfPeople)
            .OrderBy(f => f.DateFrom);

        var pagedResult = await flightsQuery
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        return pagedResult;
    }


    public async Task<string> BuyTicketsAsync(BuyTicketDTO dto)
    {
        var flight = await _context.Flights.FirstOrDefaultAsync(f => f.Id == dto.FlightId);

        if (flight == null)
            return "Flight not found";

        if (flight.AvailableSeats <= 0)
            return "No seats available";

        flight.AvailableSeats--;

        var ticket = new Ticket
        {
            FlightId = dto.FlightId,
            PassengerName = dto.PassengerName,
            PurchasedAt = DateTime.UtcNow
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        return "Ticket purchased";
    }

    public async Task<CheckInResultDTO> CheckInAsync(CheckInDTO dto)
    {
        var ticket = await _context.Tickets
            .FirstOrDefaultAsync(t => t.FlightId == dto.FlightId && t.PassengerName == dto.PassengerName);

        if (ticket == null || ticket.IsCheckedIn)
            return null;

        int seatCount = await _context.Tickets
            .Where(t => t.FlightId == dto.FlightId && t.IsCheckedIn)
            .CountAsync();

        ticket.SeatNumber = $"A{seatCount + 1}";
        ticket.IsCheckedIn = true;

        await _context.SaveChangesAsync();

        return new CheckInResultDTO
        {
            PassengerName = ticket.PassengerName,
            SeatNumber = ticket.SeatNumber,
            IsCheckedIn = true
        };
    }



    public async Task<List<PassengerInfoDTO>> GetPassengerListAsync(PassengerQueryDTO query)
    {
        var allPassengers = await _context.Tickets
            .Where(t => t.FlightId == query.FlightId)
            .Select(t => new PassengerInfoDTO
            {
                PassengerName = t.PassengerName,
                SeatNumber = t.SeatNumber
            })
            .ToListAsync();

        var pagedResult = allPassengers
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        return pagedResult;
    }

}
