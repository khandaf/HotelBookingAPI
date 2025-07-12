using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingAPI.Domain.DTOs;
using HotelBookingAPI.Domain.Services;
using HotelBookingAPI.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HotelBookingAPI.Tests.ControllerTests;
public class HotelsControllerTests
{
    private readonly Mock<IHotelService> _hotelServiceMock = new();
    private readonly HotelsController _controller;

    public HotelsControllerTests()
    {
        _controller = new HotelsController(_hotelServiceMock.Object);
    }

    [Fact]
    public async Task FindHotel_ReturnsOk_WhenHotelExists()
    {
        var hotelDto = new HotelDto { Id = 1, Name = "TestHotel" };
        _hotelServiceMock.Setup(s => s.FindHotelByNameAsync("TestHotel"))
            .ReturnsAsync(hotelDto);

        var result = await _controller.FindHotel("TestHotel");

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(hotelDto, okResult.Value);
    }

    [Fact]
    public async Task FindHotel_ReturnsNotFound_WhenHotelDoesNotExist()
    {
        _hotelServiceMock.Setup(s => s.FindHotelByNameAsync("NoHotel"))
            .ReturnsAsync((HotelDto?)null);

        var result = await _controller.FindHotel("NoHotel");

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetAvailableRooms_ReturnsOk_WithRooms()
    {
        var rooms = new List<RoomAvailabilityDto>
        {
            new RoomAvailabilityDto { RoomId = 1, RoomType = "Single", Capacity = 1 }
        };
        _hotelServiceMock.Setup(s => s.GetAvailableRoomsAsync(1, It.IsAny<DateTime>(), It.IsAny<DateTime>(), 1))
            .ReturnsAsync(rooms);

        var result = await _controller.GetAvailableRooms(1, DateTime.Now, DateTime.Now.AddDays(1), 1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(rooms, okResult.Value);
    }
}
