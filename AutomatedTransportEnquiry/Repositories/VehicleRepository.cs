using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        public Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        

        public Task<Vehicle> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<int> CreateAsync(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

       

        
    }
}
