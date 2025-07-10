namespace HotelBookingAPI.Domain.DTOs;

public class CreateBookingDto
{
    public int HotelId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int GuestCount { get; set; }
}
