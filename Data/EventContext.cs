using Microsoft.EntityFrameworkCore;
using EventManagement.Models;

namespace EventManagement.Data
{
    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> options) : base(options) { }
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
