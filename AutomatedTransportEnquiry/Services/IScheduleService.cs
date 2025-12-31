using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Services
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleDto>>GetAllAsync(string from, string to);
        Task<APIResponse> CreateAsync(ScheduleCreateDto dto);
    }
}
