using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Repositories
{
    public interface IVehicleRouteRepository
    {
        Task<IEnumerable<VehicleRoute>> GetAllAsync();
        Task<VehicleRoute> GetByIdAsync(int id);
        Task<int> CreateAsync(VehicleRoute route);
        Task<bool> UpdateAsync(VehicleRoute route);
        Task<bool> DeleteAsync(int id);


    }
}
