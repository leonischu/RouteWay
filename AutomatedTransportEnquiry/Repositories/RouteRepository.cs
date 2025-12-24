
using AutomatedTransportEnquiry.Data;
using AutomatedTransportEnquiry.Models;

using Dapper;
using System.Data;

namespace AutomatedTransportEnquiry.Repositories
{
    public class RouteRepository : IRouteRepository
    {

        private readonly DapperContext _context;
        public RouteRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Route>> GetAllAsync()
        {
            var query = "SELECT * FROM Routes";
            using (var connection = _context.CreateConnection())
            {
                var route = await connection.QueryAsync<Route>(query);
                return route;
            }
        }

        public async Task<Route> CreateAsync(Route route)
        {
            var query = @"INSERT INTO Route (Source,Destination,Distance) VALUES (@Source,@Destination,@Distance);SELECT CAST(SCOPE_IDENTITY() as int);"
            //@ allows to write sql over multiple lines and select cast.--COnverts into integer and scopeIdentity returns last identity value inserted.
            //.. this gets auto generated id.

            //var parameter = new DynamicParameters();
            //DynamicParameters.Add("Source", route.Source, DbType.String);


            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(query, route);









        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        

        public Task<Route> GetIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Route route)
        {
            throw new NotImplementedException();
        }
    }
}
