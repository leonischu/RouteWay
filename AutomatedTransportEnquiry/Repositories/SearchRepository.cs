using AutomatedTransportEnquiry.Data;
using AutomatedTransportEnquiry.DTOs;
using Dapper;
using System.Text;

namespace AutomatedTransportEnquiry.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly DapperContext _context;
        public SearchRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransportSearchResultDto>> GetAll()
        {
            var sql = new StringBuilder(@"
        SELECT 
            s.ScheduleId,
              f.FareId AS FareId,
            v.VehicleType,
            CONCAT(r.Source, ' - ', r.Destination) AS RouteName,
            s.DepartureTime,
            s.ArrivalTime,
            f.Price
        FROM Schedules s
        JOIN Vehicles v ON s.VehicleId = v.VehicleId
        JOIN Routes r ON s.RouteId = r.RouteId
        JOIN Fares f ON f.RouteId = r.RouteId
       WHERE r.IsDeleted = 0 AND s.IsDeleted = 0 AND v.IsDeleted = 0");

            using var connection = _context.CreateConnection();

            return await connection.QueryAsync<TransportSearchResultDto>(sql.ToString());
        }




        public async Task<IEnumerable<TransportSearchResultDto>> SearchAsync(TransportSearchRequestDto dto)
        {
            var sql =new StringBuilder(@"
                      SELECT 
                     s.ScheduleId,
                     f.FareId AS FareId,
                     v.VehicleType,
                     CONCAT(r.Source, ' - ', r.Destination) AS RouteName,
                     s.DepartureTime,
                     s.ArrivalTime,
                     f.Price
                     FROM Schedules s
                     JOIN Vehicles v ON s.VehicleId = v.VehicleId
                     JOIN Routes r ON s.RouteId = r.RouteId
                      JOIN Fares f ON f.RouteId = r.RouteId
                       WHERE r.IsDeleted = 0 AND s.IsDeleted = 0 AND v.IsDeleted = 0
                     WHERE LOWER(r.Source) = LOWER(@From)
                      AND LOWER(r.Destination) =LOWER(@To)");

            var parameters = new DynamicParameters();
            parameters.Add("From" ,dto.From);
            parameters.Add("To" , dto.To);


            if (dto.TravelDate.HasValue)
            {
                sql.Append(" AND CAST(s.TravelDate AS DATE) = @TravelDate");
                parameters.Add("TravelDate", dto.TravelDate.Value.Date);
            }

            if (dto.StartTime.HasValue)
            {
                sql.Append(" AND s.DepartureTime >= @StartTime");
                parameters.Add("StartTime", dto.StartTime);
            }

            if (dto.EndTime.HasValue)
            {
                sql.Append(" AND s.ArrivalTime <= @EndTime");
                parameters.Add("EndTime", dto.EndTime);
            }
            if (dto.MaxPrice.HasValue)
            {
                sql.Append(" AND f.Price <= @MaxPrice");
                parameters.Add("MaxPrice", dto.MaxPrice);
            }



            using var connection = _context.CreateConnection();

            return await connection.QueryAsync<TransportSearchResultDto>(sql.ToString(),parameters);
        }
    }
}
