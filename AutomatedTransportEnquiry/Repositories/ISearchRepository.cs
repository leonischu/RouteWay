using AutomatedTransportEnquiry.DTOs;

namespace AutomatedTransportEnquiry.Repositories
{
    public interface ISearchRepository
    {
        Task<IEnumerable<TransportSearchResultDto>>SearchAsync(TransportSearchRequestDto dto);  
    }
}
