using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Repositories
{
    public interface IBookingRepository
    {
        Task<int> CreateAsync(Booking booking);
        Task<IEnumerable<BookingDto>> GetByPhoneAsync(string phone);
        Task UpdateSeatsAsync(int scheduleId, int seats);
        Task<(decimal price, int availableSeats)> GetFareAndSeats(int scheduleId, int fareId);
        Task<bool> ScheduleExists(int scheduleId);

    }
}
