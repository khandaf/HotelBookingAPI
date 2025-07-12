using HotelBookingAPI.Domain.DTOs;
using HotelBookingAPI.WebApi.Controllers;
using HotelBookingAPI.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HotelBookingAPI.Tests.ControllerTests;
public class BookingsControllerTests
{
    private readonly Mock<IBookingService> _bookingServiceMock = new();
    private readonly BookingsController _controller;

    public BookingsControllerTests()
    {
        _controller = new BookingsController(_bookingServiceMock.Object);
    }

    [Fact]
    public async Task CreateBooking_ReturnsBookingDto_WhenSuccessful()
    {
        var createDto = new CreateBookingDto
        {
            HotelId = 1,
            StartDate = DateTime.Today.AddDays(1),
            EndDate = DateTime.Today.AddDays(3),
            GuestCount = 1
        };
        var bookingDto = new BookingDto
        {
            BookingReference = "ABC123",
            RoomId = 1,
            RoomType = "Single",
            StartDate = createDto.StartDate,
            EndDate = createDto.EndDate,
            GuestCount = 1
        };

        _bookingServiceMock.Setup(s => s.BookRoomAsync(createDto))
            .ReturnsAsync(bookingDto);

        var result = await _controller.BookRoom(createDto);

        var okResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedBooking = Assert.IsType<BookingDto>(okResult.Value);
        Assert.Equal(bookingDto.BookingReference, returnedBooking.BookingReference);
    }

    [Fact]
    public async Task GetBookingByReference_ReturnsBookingDto_WhenFound()
    {
        var bookingDto = new BookingDto
        {
            BookingReference = "ABC123",
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(2),
            GuestCount = 1
        };

        _bookingServiceMock.Setup(s => s.GetBookingByReferenceAsync("ABC123"))
            .ReturnsAsync(bookingDto);

        var result = await _controller.GetBooking("ABC123");

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(bookingDto, okResult.Value);
    }

    [Fact]
    public async Task GetBookingByReference_ReturnsNotFound_WhenNotFound()
    {
        _bookingServiceMock.Setup(s => s.GetBookingByReferenceAsync("NOTFOUND"))
            .ReturnsAsync((BookingDto?)null);

        var result = await _controller.GetBooking("NOTFOUND");

        Assert.IsType<NotFoundResult>(result);
    }
}
