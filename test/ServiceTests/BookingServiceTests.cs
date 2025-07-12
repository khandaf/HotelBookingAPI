using FluentAssertions;
using HotelBookingAPI.Domain.DTOs;
using HotelBookingAPI.Domain.Entities;
using HotelBookingAPI.Domain.Interfaces;
using HotelBookingAPI.Domain.Services;
using Moq;

namespace HotelBookingAPI.Tests.ServiceTests;
public class BookingServiceTests
{
    private readonly Mock<IBookingRepository> _bookingRepoMock = new();
    private readonly Mock<IHotelRepository> _hotelRepoMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    private BookingService CreateService() => new(_bookingRepoMock.Object, _hotelRepoMock.Object, _unitOfWorkMock.Object);

    [Fact]
    public async Task BookRoomAsync_ShouldReturnBookingDto_WhenRoomAvailable()
    {
        var hotel = new Hotel { Id = 1, Name = "Test Hotel" };
        _hotelRepoMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(hotel);

        var availableRooms = new List<Room>
        {
            new Room { Id = 1, Type = RoomType.Single }
        };
        _bookingRepoMock.Setup(x => x.GetAvailableRoomsAsync(1, It.IsAny<DateTime>(), It.IsAny<DateTime>(), 1))
            .ReturnsAsync(availableRooms);

        _bookingRepoMock.Setup(x => x.AddAsync(It.IsAny<Booking>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        var service = CreateService();
        var request = new CreateBookingDto
        {
            HotelId = 1,
            StartDate = DateTime.Today.AddDays(1),
            EndDate = DateTime.Today.AddDays(2),
            GuestCount = 1
        };

        var result = await service.BookRoomAsync(request);

        result.Should().NotBeNull();
        result.RoomId.Should().Be(1);
        result.GuestCount.Should().Be(1);
    }

    [Fact]
    public async Task BookRoomAsync_ShouldThrow_WhenNoRoomsAvailable()
    {
        _hotelRepoMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(new Hotel { Id = 1, Name = "Hotel" });
        _bookingRepoMock.Setup(x => x.GetAvailableRoomsAsync(1, It.IsAny<DateTime>(), It.IsAny<DateTime>(), 1))
            .ReturnsAsync(new List<Room>());

        var service = CreateService();
        var request = new CreateBookingDto
        {
            HotelId = 1,
            StartDate = DateTime.Today.AddDays(1),
            EndDate = DateTime.Today.AddDays(2),
            GuestCount = 1
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.BookRoomAsync(request));
    }
}
