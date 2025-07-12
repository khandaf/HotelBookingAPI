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
   
        var rooms = await _db.Rooms
            .Where(r => r.HotelId == hotelId)
            .ToListAsync(); // Materialize the query, so the rest runs in memory  
 
        var availableRooms = rooms.Where(r => r.Capacity >= guests &&
            r.Bookings.All(b => end <= b.StartDate || start >= b.EndDate));

        return availableRooms;
    }

    public async Task AddAsync(Booking booking)
    {
        await _db.Bookings.AddAsync(booking);
    }
}
