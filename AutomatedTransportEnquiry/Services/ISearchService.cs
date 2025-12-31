using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<TransportSearchResultDto>>SearchAsync(TransportSearchRequestDto dto);   
    }
}
