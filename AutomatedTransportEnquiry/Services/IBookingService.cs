using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Services
{
    public interface IBookingService
    {



        Task<APIResponse> GetByIdAsync(int bookingId);
        Task<APIResponse> GetAllAsync();
        Task<APIResponse> CreateAsync(BookingCreateDto dto);


    }
}
