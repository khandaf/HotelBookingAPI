namespace HotelBookingAPI.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public string BookingReference { get; set; } = Guid.NewGuid().ToString();
    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int GuestCount { get; set; }
}
