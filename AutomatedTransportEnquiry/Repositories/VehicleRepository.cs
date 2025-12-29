using AutomatedTransportEnquiry.Data;
using AutomatedTransportEnquiry.Models;
using Dapper;
using System.Data;

namespace AutomatedTransportEnquiry.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {

        private readonly DapperContext _context;

        public VehicleRepository(DapperContext context)
        {
         _context = context;
          
        }
        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            var query = "SELECT * FROM Vehicles";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Vehicle>(query);
        }
        

        public async Task<Vehicle> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Routes WHERE VehicleId = @Id";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Vehicle>(query, new { Id = id });
        }
        public async Task<int> CreateAsync(Vehicle vehicle)
        {
            var query = @"INSERT INTO Vehicle(VehicleNumber,VehicleType,Capacity,RouteId) VALUES(@VehicleNumber,@VehicleType,@Capacity,@RouteId);SELECT CAST(SCOPE_IDENTITY() AS INT)";

            var parameter = new DynamicParameters();
            parameter.Add("VehicleNumber", vehicle.VehicleNumber, DbType.String);
            parameter.Add("VehicleType", vehicle.VehicleType, DbType.String);
            parameter.Add("Capacity", vehicle.Capacity, DbType.String);
            parameter.Add("RouteId", vehicle.RouteId, DbType.Int32);


            
            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(query,vehicle);
        }

        public async Task<bool> UpdateAsync(Vehicle vehicle)
        {
            var query = "UPDATE Vehicle SET VehicleNumber = @VehicleNumber,VehicleType = @VehicleType, Capacity = @Capacity,RouteId=@RouteId WHERE VehicleId=@VehicleID";

            var parameter = new DynamicParameters();
            parameter.Add("VehicleId",vehicle.VehicleId,DbType.Int32);
            parameter.Add("VehicleNumber", vehicle.VehicleNumber, DbType.String);
            parameter.Add("VehicleType", vehicle.VehicleType, DbType.String);
            parameter.Add("Capacity", vehicle.Capacity, DbType.String);
            parameter.Add("RouteId", vehicle.RouteId, DbType.Int32);

            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query,vehicle) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var query = "DELETE FROM Vehicle WHERE VehicleId = @VehicleId";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, new {VehicleId = id }) >0;
        }

       

        
    }
}
