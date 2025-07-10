using HotelBookingAPI.Domain.Entities;

namespace HotelBookingAPI.Domain.Interfaces;

public interface IBookingRepository
{
    Task<Booking?> GetByReferenceAsync(string reference);
    Task<IEnumerable<Room>> GetAvailableRoomsAsync(int hotelId, DateTime startDate, DateTime endDate, int guests);
    Task AddAsync(Booking booking);
}
