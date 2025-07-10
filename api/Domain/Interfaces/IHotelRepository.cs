using HotelBookingAPI.Domain.Entities;

namespace HotelBookingAPI.Domain.Interfaces;

public interface IHotelRepository
{
    Task<Hotel?> GetByIdAsync(int id);
    Task<Hotel?> GetByNameAsync(string name);
    Task<IEnumerable<Hotel>> GetAllAsync();
}
