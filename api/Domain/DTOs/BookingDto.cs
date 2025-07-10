namespace HotelBookingAPI.Domain.DTOs;

public class BookingDto
{
    public string BookingReference { get; set; } = string.Empty;
    public int RoomId { get; set; }
    public string RoomType { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int GuestCount { get; set; }
}
