using AutomatedTransportEnquiry.Data;
using AutomatedTransportEnquiry.DTOs;
using Dapper;

namespace AutomatedTransportEnquiry.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly DapperContext _context;
        public SearchRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TransportSearchDto>> SearchAsync(string from, string to)
        {
            var query = @"
                      SELECT 
                     s.ScheduleId,
                     v.VehicleName,
                     CONCAT(r.Source, ' - ', r.Destination) AS RouteName,
                     s.DepartureTime,
                     s.ArrivalTime,
                     s.Price
                     FROM Schedules s
                     JOIN Vehicles v ON s.VehicleId = v.VehicleId
                     JOIN Routes r ON s.RouteId = r.RouteId
                     WHERE r.Source = @From
                      AND r.Destination = @To";

            var parameters = new DynamicParameters();
            parameters.Add("@From" ,from);
            parameters.Add("@To" , to);
            using var connection = _context.CreateConnection();

            return await connection.QueryAsync<TransportSearchDto>(query,parameters);
        }
    }
}
