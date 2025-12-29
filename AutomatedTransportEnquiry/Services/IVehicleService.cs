using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Services
{
    public interface IVehicleService
    {
        Task<APIResponse> GetAllAsync();
        Task<APIResponse> GetByIdAsync(int vehicleId);
        Task<APIResponse> CreateAsync(VehicleCreateDto dto);
        Task<APIResponse> UpdateAsync(VehicleUpdateDto dto);
        Task<APIResponse> DeleteAsync(int vehicleId);

    }
}
