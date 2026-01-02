using AutomatedTransportEnquiry.DTOs;

namespace AutomatedTransportEnquiry.Services
{
    public interface ICancelBookingService
    {
        Task<APIResponse<CancelBookingResponseDto>>CancelBookingAsync(CancelBookingDto dto);
    }
}
