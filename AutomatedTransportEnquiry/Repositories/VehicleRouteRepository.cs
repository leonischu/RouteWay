
using AutomatedTransportEnquiry.Data;
using AutomatedTransportEnquiry.Models;
using Dapper;
using System.Data;

namespace AutomatedTransportEnquiry.Repositories
{
    public class VehicleRouteRepository : IVehicleRouteRepository
    {

        private readonly DapperContext _context;
        public VehicleRouteRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehicleRoute>> GetAllAsync()
        {
            var query = "SELECT * FROM Routes";
            var parameters = new DynamicParameters();
            //parameters.Add("Name", r.Name, DbType.String);


            using (var connection = _context.CreateConnection())
            {
               return await connection.QueryAsync<VehicleRoute>(query);
               
            }
        }


        public async Task<VehicleRoute> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Routes WHERE RouteId = @Id";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<VehicleRoute>(query, new { Id = id });
        }







        public async Task<int> CreateAsync(VehicleRoute route)
        {
            var query = @"INSERT INTO Routes (Source,Destination,Distance) VALUES (@Source,@Destination,@Distance);SELECT CAST(SCOPE_IDENTITY() as int)";
            //@ allows to write sql over multiple lines and select cast.--COnverts into integer and scopeIdentity returns last identity value inserted.
            //.. this gets auto generated id.

            var parameters = new DynamicParameters();
                
             parameters.Add("Source", route.Source, DbType.String);
             parameters.Add("Destination", route.Destination, DbType.String);
             parameters.Add("Distance", route.Distance, DbType.Int32);


            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(query, route);
        }

        public async Task<bool> UpdateAsync(VehicleRoute route)
        {
            var query = "Update Routes SET Source = @Source , Destination = @Destination , Distance = @Distance WHERE RouteId = @RouteId";

            var parameters = new DynamicParameters();

            parameters.Add("RouteId", route.RouteId, DbType.Int32);
            parameters.Add("Source", route.Source, DbType.String);
            parameters.Add("Destination", route.Destination, DbType.String);
            parameters.Add("Distance", route.Distance, DbType.Int32);

            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, route) > 0;

        }



        public async Task<bool> DeleteAsync(int routeId)
        {
            var query = "DELETE FROM Routes Where RouteId = @RouteId";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, new { RouteId = routeId }) > 0;
            
        }
        
    }
}
