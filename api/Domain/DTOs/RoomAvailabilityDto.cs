namespace HotelBookingAPI.Domain.DTOs;

public class RoomAvailabilityDto
{
    public int RoomId { get; set; }
    public string RoomType { get; set; } = string.Empty;
    public int Capacity { get; set; }
}
