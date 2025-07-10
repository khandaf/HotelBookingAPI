using HotelBookingAPI.Domain.Interfaces;
using HotelBookingAPI.Infrastructure.Data;

namespace HotelBookingAPI.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    public UnitOfWork(AppDbContext db) => _db = db;

    public async Task CompleteAsync() => await _db.SaveChangesAsync();
}
