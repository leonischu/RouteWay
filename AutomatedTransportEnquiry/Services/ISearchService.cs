using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Services
{
    public interface ISearchService
    {
        Task<APIResponse>SearchAsync(string from, string to);   
    }
}
