using HotelBookingAPI.Domain.DTOs;

namespace HotelBookingAPI.Domain.Services;

public interface IBookingService
{
    Task<BookingDto?> GetBookingByReferenceAsync(string reference);
    Task<BookingDto> BookRoomAsync(CreateBookingDto request);
}
