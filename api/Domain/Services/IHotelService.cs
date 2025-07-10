using HotelBookingAPI.Domain.DTOs;

namespace HotelBookingAPI.Domain.Services;

public interface IHotelService
{
    Task<HotelDto?> FindHotelByNameAsync(string name);
    Task<IEnumerable<RoomAvailabilityDto>> GetAvailableRoomsAsync(int hotelId, DateTime start, DateTime end, int guests);
}
