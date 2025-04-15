using Microsoft.EntityFrameworkCore;
using AirlaneTicketingSystem.Models;
using System.Collections.Generic;

namespace AirlaneTicketingSystem.Data
{
    public class AirlineDbContext : DbContext
    {
        public AirlineDbContext(DbContextOptions<AirlineDbContext> options) : base(options) { }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
