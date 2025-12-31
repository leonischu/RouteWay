using AutomatedTransportEnquiry.DTOs;

namespace AutomatedTransportEnquiry.Repositories
{
    public interface IFareRepository
    {
        Task<IEnumerable<FareDto>> GetAllAsync();
        Task<int> CreateAsync(FareCreateDto dto);
    }
}
