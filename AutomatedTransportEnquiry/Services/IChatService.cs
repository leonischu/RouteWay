using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Services
{
    public interface IChatService
    {
        Task<APIResponse> GetReplyAsync(ChatRequestDto dto, int userId);

    }
}
