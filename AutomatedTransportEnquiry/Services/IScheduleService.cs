using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Services
{
    public interface IScheduleService
    {
        Task<APIResponse> GetAllAsync(string from, string to);
        Task<APIResponse> CreateAsync(ScheduleCreateDto dto);
    }
}
