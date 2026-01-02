using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Repositories
{
    public interface IBookingRepository
    {
        Task<int> CreateAsync(Booking booking);
        Task<IEnumerable<BookingDto>> GetByPhoneAsync(string phone);

    }
}
