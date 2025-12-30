using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Services
{
    public interface ISearchService
    {
        Task<APIResponse>SearchAsync(TransportSearchRequestDto dto);   
    }
}
