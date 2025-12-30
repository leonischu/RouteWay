using AutomatedTransportEnquiry.Data;
using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using Dapper;

namespace AutomatedTransportEnquiry.Repositories
{
    public class ScheduleRepository :IScheduleRepository
    {
        private readonly DapperContext _context;
        public ScheduleRepository(DapperContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<ScheduleDto>> GetAllAsync(string from,string to)
        {
           
            var query = @"SELECT s.ScheduleId,v.VehicleName,CONCAT(r.Source, ' - ' ,r.Destination) AS RouteName,
                           s.DepartureTime,
                           s.ArrivalTime,
                           s.Price
                          FROM Schedules s
                          JOIN Vehicles v ON s.VehicleId = v.VehicleId
                          JOIN Routes r ON s.RouteId = r.RouteId WHERE r.Source = @From AND r.Destination = @To";
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@From", from);
            parameters.Add("@To", to);
            return await connection.QueryAsync<ScheduleDto>(query,parameters);
        }
        public async Task<int> CreateAsync(Schedule schedule)
        {
          var query = @"
          INSERT INTO Schedules
          (VehicleId, RouteId, DepartureTime, ArrivalTime, Price)
          VALUES
          (@VehicleId, @RouteId, @DepartureTime, @ArrivalTime, @Price);

          SELECT CAST(SCOPE_IDENTITY() AS INT);  ";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(query,schedule);
        }


    }
}
