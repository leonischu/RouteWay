using AutomatedTransportEnquiry.DTOs;

namespace AutomatedTransportEnquiry.Repositories
{
    public interface IFareRepository
    {
        Task<IEnumerable<FareDto>> GetAllAsync();
        Task<FareDto?> GetByIdAsync(int fareId);   

        Task<int> CreateAsync(FareCreateDto dto);

        Task<bool> DeleteAsync(int fareId);
    }
}
