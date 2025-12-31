using AutomatedTransportEnquiry.Data;
using AutomatedTransportEnquiry.DTOs;
using Dapper;

namespace AutomatedTransportEnquiry.Repositories
{
    public class FareRepository : IFareRepository
    {
        private readonly DapperContext _context;
        public FareRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<FareDto>> GetAllAsync()
        {
            var sql = @"SELECT f.FareId,CONCAT(r.source, ' - ' ,r.Destination) AS RouteName,
                        f.Price From Fares f 
                        JOIN Routes r ON r.RouteId = f.RouteId";
                using var connection = _context.CreateConnection();
                return await connection.QueryAsync<FareDto>(sql);
        }
        public async Task<int> CreateAsync(FareCreateDto dto)
        {
            var sql = @"INSERT INTO Fares (RouteId,Price)
                        VALUES(@RouteId,@Price);
                        SELECT SCOPE_IDENTITY();";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(sql,dto);
        }
    }
}
