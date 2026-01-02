using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Services
{
    public interface IBookingService
    {
        Task<APIResponse> CreateAsync(BookingCreateDto dto);
    }
}
