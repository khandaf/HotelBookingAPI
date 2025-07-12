using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingAPI.Domain.DTOs;
using HotelBookingAPI.Domain.Entities;
using HotelBookingAPI.Domain.Interfaces;
using HotelBookingAPI.Domain.Services;
using Moq;
using Xunit;

namespace HotelBookingAPI.Tests.ServiceTests;
public class HotelServiceTests
{
    private readonly Mock<IHotelRepository> _hotelRepoMock = new();
    private readonly Mock<IBookingRepository> _bookingRepoMock = new();
    private readonly HotelService _service;

    public HotelServiceTests()
    {
        _service = new HotelService(_hotelRepoMock.Object, _bookingRepoMock.Object);
    }

    [Fact]
    public async Task FindHotelByNameAsync_ReturnsHotelDto_WhenHotelExists()
    {
        var hotel = new Hotel { Id = 1, Name = "TestHotel" };
        _hotelRepoMock.Setup(r => r.GetByNameAsync("TestHotel")).ReturnsAsync(hotel);

        var result = await _service.FindHotelByNameAsync("TestHotel");

        Assert.NotNull(result);
        Assert.Equal(hotel.Id, result.Id);
        Assert.Equal(hotel.Name, result.Name);
    }

    [Fact]
    public async Task FindHotelByNameAsync_ReturnsNull_WhenHotelDoesNotExist()
    {
        _hotelRepoMock.Setup(r => r.GetByNameAsync("NoHotel")).ReturnsAsync((Hotel?)null);

        var result = await _service.FindHotelByNameAsync("NoHotel");

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAvailableRoomsAsync_ReturnsRoomAvailabilityDtos()
    {
        var rooms = new List<Room>
        {
            new Room { Id = 1, Type = RoomType.Single },
            new Room { Id = 2, Type = RoomType.Double }
        };
        _bookingRepoMock.Setup(r => r.GetAvailableRoomsAsync(1, It.IsAny<DateTime>(), It.IsAny<DateTime>(), 1))
            .ReturnsAsync(rooms);

        var result = (await _service.GetAvailableRoomsAsync(1, DateTime.Now, DateTime.Now.AddDays(1), 1)).ToList();

        Assert.Equal(2, result.Count);
        Assert.Equal(rooms[0].Id, result[0].RoomId);
        Assert.Equal(rooms[0].Type.ToString(), result[0].RoomType);
        Assert.Equal(rooms[0].Capacity, result[0].Capacity);
    }
}
