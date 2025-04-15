using AirlaneTicketingSystem.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;

    private readonly List<User> _users = new()
    {
        new User { Id = 1, Username = "admin", Password = "1234" },
        new User { Id = 2, Username = "user", Password = "1234" }
    };

    public AuthController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDTO request)
    {
        var user = _users.FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);
        if (user == null)
            return Unauthorized("Invalid credentials");

        var token = _jwtService.GenerateToken(user.Username);
        return Ok(new { token });
    }
}
