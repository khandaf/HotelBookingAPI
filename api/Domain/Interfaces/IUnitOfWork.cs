namespace HotelBookingAPI.Domain.Interfaces;

public interface IUnitOfWork
{
    Task CompleteAsync();
}
