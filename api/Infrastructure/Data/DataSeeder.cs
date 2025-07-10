using HotelBookingAPI.Domain.Entities;

namespace HotelBookingAPI.Infrastructure.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (!db.Hotels.Any())
        {
            var hotel = new Hotel { Name = "Test Hotel" };

            for (int i = 0; i < 2; i++)
                hotel.Rooms.Add(new Room { Type = RoomType.Single });

            for (int i = 0; i < 2; i++)
                hotel.Rooms.Add(new Room { Type = RoomType.Double });

            for (int i = 0; i < 2; i++)
                hotel.Rooms.Add(new Room { Type = RoomType.Deluxe });

            db.Hotels.Add(hotel);
            await db.SaveChangesAsync();
        }
    }

    public static async Task ResetAsync(AppDbContext db)
    {
        db.Bookings.RemoveRange(db.Bookings);
        db.Rooms.RemoveRange(db.Rooms);
        db.Hotels.RemoveRange(db.Hotels);
        await db.SaveChangesAsync();
    }
}
