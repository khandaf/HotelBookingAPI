using HotelBookingAPI.Domain.DTOs;
using HotelBookingAPI.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet("{reference}")]
    public async Task<IActionResult> GetBooking(string reference)
    {
        var booking = await _bookingService.GetBookingByReferenceAsync(reference);
        if (booking == null) return NotFound();
        return Ok(booking);
    }

    [HttpPost]
    public async Task<IActionResult> BookRoom([FromBody] CreateBookingDto bookingRequest)
    {
        var booking = await _bookingService.BookRoomAsync(bookingRequest);
        return CreatedAtAction(nameof(GetBooking), new { reference = booking.BookingReference }, booking);
    }
}
