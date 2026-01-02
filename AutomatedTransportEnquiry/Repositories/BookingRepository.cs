using AutomatedTransportEnquiry.Data;
using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using Dapper;

namespace AutomatedTransportEnquiry.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DapperContext _context;
        public BookingRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Booking booking)
        {
            var sql = @"INSERT INTO Bookings (ScheduleId,FareId,PassengerName,PassengerPhone,Seats,TotalAmount,BookingStatus,BookingDate,PaymentStatus)
                    VALUES (@ScheduleId,@FareId,@PassengerName,@PassengerPhone,@Seats,@TotalAmount,  'CONFIRMED', GETDATE(),'PENDING');
                    SELECT SCOPE_IDENTITY();";
            using var connection = _context.CreateConnection();
            
            
            
            var parameters = new DynamicParameters();
            parameters.Add("@ScheduleId", booking.ScheduleId);
            parameters.Add("@FareId", booking.FareId);
            parameters.Add("@PassengerName", booking.PassengerName);
            parameters.Add("@PassengerPhone", booking.PassengerPhone);
            parameters.Add("@Seats", booking.Seats);
            parameters.Add("@TotalAmount", booking.TotalAmount);


            return await connection.ExecuteScalarAsync<int>(sql, parameters);
        }

        public async Task<IEnumerable<BookingDto>> GetByPhoneAsync(string phone)
        {
            var sql = @"SELECT b.BookingId,
                            CONCAT(r.Source, ' - ' r.Destination) AS RouteName,
                            v.VehicleName,
                            s.DepartureTime,    
                            b.TotalAmount,
                            b.BookingStatus
                       FROM Bookings b 
                       JOIN Schedule s ON b.ScheduleId = s.ScheduleId
                       JOIN Vehicle v ON s.VehicleId = v.VehicleId
                       JOIN Routes r ON s.RouteId = r.RouteId
                        WHERE b.PassengerPhone = @Phone";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<BookingDto>(sql, new { Phone = phone });
        }
    }
}
