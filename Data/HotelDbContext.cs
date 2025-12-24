using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OtelYonetimMVC.Models;

namespace OtelYonetimMVC.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
