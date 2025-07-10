using HotelBookingAPI.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : ControllerBase
{
    private readonly IHotelService _hotelService;
    public HotelsController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    [HttpGet("find")]
    public async Task<IActionResult> FindHotel([FromQuery] string name)
    {
        var hotel = await _hotelService.FindHotelByNameAsync(name);
        if (hotel == null) return NotFound();
        return Ok(hotel);
    }

    [HttpGet("{hotelId}/available-rooms")]
    public async Task<IActionResult> GetAvailableRooms(int hotelId, [FromQuery] DateTime start, [FromQuery] DateTime end, [FromQuery] int guests)
    {
        var rooms = await _hotelService.GetAvailableRoomsAsync(hotelId, start, end, guests);
        return Ok(rooms);
    }
}
