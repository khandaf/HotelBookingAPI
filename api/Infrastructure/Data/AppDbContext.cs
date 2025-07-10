using HotelBookingAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Hotel> Hotels => Set<Hotel>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hotel>().HasMany(h => h.Rooms).WithOne(r => r.Hotel).HasForeignKey(r => r.HotelId);
        modelBuilder.Entity<Room>().HasMany(r => r.Bookings).WithOne(b => b.Room).HasForeignKey(b => b.RoomId);
        base.OnModelCreating(modelBuilder);
    }
}
