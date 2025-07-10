namespace HotelBookingAPI.Domain.Entities;

public class Room
{
    public int Id { get; set; }
    public RoomType Type { get; set; }
    public int HotelId { get; set; }
    public Hotel Hotel { get; set; } = null!;
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public int Capacity => Type switch
    {
        RoomType.Single => 1,
        RoomType.Double => 2,
        RoomType.Deluxe => 4,
        _ => 1
    };
}
