using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Services
{
    public interface IVehicleRouteService
    {
        Task<APIResponse> GetAllAsync();
        Task<APIResponse> GetByIdAsync(int RouteId);

        Task<APIResponse> CreateAsync(VehicleRouteCreateDto dto);
        Task<APIResponse> UpdateAsync(VehicleRouteUpdateDto dto);
        Task<APIResponse> DeleteAsync(int routeId);
    }
}
