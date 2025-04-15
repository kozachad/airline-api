using AirlaneTicketingSystem.Models;
using AirlaneTicketingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class FlightController : ControllerBase
{
    private readonly IFlightService _flightService;

    public FlightController(IFlightService flightService)
    {
        _flightService = flightService;
    }

    [Authorize]
    [HttpPost("add")]
    public async Task<IActionResult> AddFlight([FromBody] AddFlightDTO dto)
    {
        var result = await _flightService.AddFlightAsync(dto);
        return Ok(new { status = result });
    }

    [AllowAnonymous]
    [HttpPost("query")]
    public async Task<IActionResult> QueryFlights([FromBody] FlightQueryDTO query)
    {
        var result = await _flightService.QueryFlightsAsync(query);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("buy")]
    public async Task<IActionResult> BuyTicket([FromBody] BuyTicketDTO dto)
    {
        var result = await _flightService.BuyTicketsAsync(dto);
        return Ok(new { status = result });
    }

    [HttpPost("checkin")]
    public async Task<IActionResult> CheckIn([FromBody] CheckInDTO dto)
    {
        var result = await _flightService.CheckInAsync(dto);
        return Ok(new { result });
    }

    [Authorize]
    [HttpGet("passengers")]
    public async Task<IActionResult> GetPassengerList([FromQuery] int flightId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new PassengerQueryDTO
        {
            FlightId = flightId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _flightService.GetPassengerListAsync(query);
        return Ok(result);
    }
}
