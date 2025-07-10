using HotelBookingAPI.Domain.Entities;
using HotelBookingAPI.Domain.Interfaces;
using HotelBookingAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _db;
    public BookingRepository(AppDbContext db) => _db = db;

    public async Task<Booking?> GetByReferenceAsync(string reference) =>
        await _db.Bookings.Include(b => b.Room).FirstOrDefaultAsync(b => b.BookingReference == reference);

    public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(int hotelId, DateTime start, DateTime end, int guests)
    {
        return await _db.Rooms
            .Include(r => r.Bookings)
            .Where(r => r.HotelId == hotelId && r.Capacity >= guests &&
                r.Bookings.All(b => end <= b.StartDate || start >= b.EndDate))
            .ToListAsync();
    }

    public async Task AddAsync(Booking booking)
    {
        await _db.Bookings.AddAsync(booking);
    }
}
