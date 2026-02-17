using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Services
{
    public interface IUserService
    {
        Task<APIResponse> GetAllAsync();
    }
}
