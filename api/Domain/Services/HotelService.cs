using HotelBookingAPI.Domain.DTOs;
using HotelBookingAPI.Domain.Interfaces;

namespace HotelBookingAPI.Domain.Services;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepo;
    private readonly IBookingRepository _bookingRepo;

    public HotelService(IHotelRepository hotelRepo, IBookingRepository bookingRepo)
    {
        _hotelRepo = hotelRepo;
        _bookingRepo = bookingRepo;
    }

    public async Task<HotelDto?> FindHotelByNameAsync(string name)
    {
        var hotel = await _hotelRepo.GetByNameAsync(name);
        if (hotel == null) return null;
        return new HotelDto { Id = hotel.Id, Name = hotel.Name };
    }

    public async Task<IEnumerable<RoomAvailabilityDto>> GetAvailableRoomsAsync(int hotelId, DateTime start, DateTime end, int guests)
    {
        var rooms = await _bookingRepo.GetAvailableRoomsAsync(hotelId, start, end, guests);
        return rooms.Select(r => new RoomAvailabilityDto
        {
            RoomId = r.Id,
            RoomType = r.Type.ToString(),
            Capacity = r.Capacity
        });
    }
}
