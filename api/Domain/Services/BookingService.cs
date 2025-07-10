using HotelBookingAPI.Domain.DTOs;
using HotelBookingAPI.Domain.Entities;
using HotelBookingAPI.Domain.Interfaces;

namespace HotelBookingAPI.Domain.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepo;
    private readonly IHotelRepository _hotelRepo;
    private readonly IUnitOfWork _unitOfWork;

    public BookingService(IBookingRepository bookingRepo, IHotelRepository hotelRepo, IUnitOfWork unitOfWork)
    {
        _bookingRepo = bookingRepo;
        _hotelRepo = hotelRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<BookingDto?> GetBookingByReferenceAsync(string reference)
    {
        var booking = await _bookingRepo.GetByReferenceAsync(reference);
        if (booking == null) return null;

        return new BookingDto
        {
            BookingReference = booking.BookingReference,
            RoomId = booking.RoomId,
            RoomType = booking.Room.Type.ToString(),
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            GuestCount = booking.GuestCount
        };
    }

    public async Task<BookingDto> BookRoomAsync(CreateBookingDto request)
    {
        if (request.EndDate <= request.StartDate)
            throw new ArgumentException("EndDate must be after StartDate");

        var hotel = await _hotelRepo.GetByIdAsync(request.HotelId);
        if (hotel == null)
            throw new KeyNotFoundException("Hotel not found");

        var availableRooms = await _bookingRepo.GetAvailableRoomsAsync(request.HotelId, request.StartDate, request.EndDate, request.GuestCount);

        if (!availableRooms.Any())
            throw new InvalidOperationException("No available rooms for the given criteria");

        var room = availableRooms.First();

        var booking = new Booking
        {
            RoomId = room.Id,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            GuestCount = request.GuestCount
        };

        await _bookingRepo.AddAsync(booking);
        await _unitOfWork.CompleteAsync();

        return new BookingDto
        {
            BookingReference = booking.BookingReference,
            RoomId = room.Id,
            RoomType = room.Type.ToString(),
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            GuestCount = booking.GuestCount
        };
    }
}
