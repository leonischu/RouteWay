using AutomatedTransportEnquiry.DTOs;

namespace AutomatedTransportEnquiry.Repositories
{
    public interface ISearchRepository
    {
        Task<IEnumerable<TransportSearchDto>>SearchAsync(string from , string to);  
    }
}
