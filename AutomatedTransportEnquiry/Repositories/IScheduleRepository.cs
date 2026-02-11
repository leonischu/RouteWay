using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Repositories
{
    public interface IScheduleRepository

    {
        Task<IEnumerable<Schedule>> GetAsync();
        Task<IEnumerable<ScheduleDto>> GetAllAsync(string from, string to);
        Task<int>CreateAsync(Schedule schedule);
    }
}
