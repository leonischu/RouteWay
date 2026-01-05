using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Repositories
{
    public interface IBookingRepository
    {
        Task<int> CreateAsync(Booking booking);
        Task<Booking> GetByIdAsync(int bookingId);
        Task<IEnumerable<BookingDto>> GetByPhoneAsync(string phone);
        Task UpdateSeatsAsync(int scheduleId, int seats);
        Task<(decimal price, int availableSeats)> GetFareAndSeats(int scheduleId, int fareId);
        Task<bool> ScheduleExists(int scheduleId);
        Task<bool> CancelBookingAsync(int bookingId, string? cancellationReason);
        Task<bool> RestoreSeatsAsync(int scheduleId, int seats);
    }
}
