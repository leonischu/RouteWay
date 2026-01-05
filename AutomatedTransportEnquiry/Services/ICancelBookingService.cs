using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
namespace AutomatedTransportEnquiry.Services
{
    public interface ICancelBookingService
    {
        Task<APIResponse>CancelBookingAsync(CancelBookingDto dto);
    }
}
