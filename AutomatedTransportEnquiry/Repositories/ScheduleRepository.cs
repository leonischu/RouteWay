using AutomatedTransportEnquiry.Data;
using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using Dapper;

namespace AutomatedTransportEnquiry.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly DapperContext _context;
        public ScheduleRepository(DapperContext context)
        {
            _context = context;

        }



        public async Task<IEnumerable<ScheduleDto>> GetAllAsync(string from,string to)
        {
           
            var query = @"SELECT s.ScheduleId,v.VehicleType,CONCAT(r.Source, ' - ' ,r.Destination) AS RouteName,
                           s.DepartureTime,
                           s.ArrivalTime,
                           
                           s.TravelDate
                          FROM Schedules s
                          JOIN Vehicles v ON v.VehicleId = s.VehicleId
                          JOIN Routes r ON r.RouteId = s.RouteId WHERE LOWER(r.Source) = LOWER(@From) AND LOWER( r.Destination) = LOWER(@To)";
            using var connection = _context.CreateConnection();
            
            return await connection.QueryAsync<ScheduleDto>(query, new { from, to});
        }
        public async Task<int> CreateAsync(Schedule schedule)
        {
          var query = @"
          INSERT INTO Schedules
          (VehicleId, RouteId, DepartureTime, ArrivalTime, TravelDate)
          VALUES
          (@VehicleId, @RouteId, @DepartureTime, @ArrivalTime,@TravelDate);

          SELECT CAST(SCOPE_IDENTITY() AS INT);  ";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(query,schedule);
        }

        public async Task<IEnumerable<Schedule>> GetAsync()
        {
            var query = "SELECT * FROM Schedules ";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Schedule>(query);
        }
    }
}
