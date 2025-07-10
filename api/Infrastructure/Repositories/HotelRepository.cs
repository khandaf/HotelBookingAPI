using HotelBookingAPI.Domain.Entities;
using HotelBookingAPI.Domain.Interfaces;
using HotelBookingAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI.Infrastructure.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly AppDbContext _db;
    public HotelRepository(AppDbContext db) => _db = db;

    public async Task<Hotel?> GetByIdAsync(int id) =>
        await _db.Hotels.Include(h => h.Rooms).FirstOrDefaultAsync(h => h.Id == id);

    public async Task<Hotel?> GetByNameAsync(string name) =>
        await _db.Hotels.Include(h => h.Rooms).FirstOrDefaultAsync(h => h.Name.ToLower() == name.ToLower());

    public async Task<IEnumerable<Hotel>> GetAllAsync() =>
        await _db.Hotels.Include(h => h.Rooms).ToListAsync();
}
