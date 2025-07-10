using HotelBookingAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly AppDbContext _db;

    public AdminController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("seed")]
    public async Task<IActionResult> Seed()
    {
        await DataSeeder.SeedAsync(_db);
        return Ok("Database seeded");
    }

    [HttpPost("reset")]
    public async Task<IActionResult> Reset()
    {
        await DataSeeder.ResetAsync(_db);
        return Ok("Database reset");
    }
}
