using AutomatedTransportEnquiry.Data;
using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using Dapper;
using MySqlX.XDevAPI.Common;

namespace AutomatedTransportEnquiry.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DapperContext _context;
        public BookingRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<bool>ScheduleExists(int scheduleId)
        {
            var sql = "SELECT COUNT(1) FROM Schedules WHERE ScheduleId = @ScheduleId";
            using var conn = _context.CreateConnection();
            return await conn.ExecuteScalarAsync<int>(sql, new { ScheduleId = scheduleId }) > 0;
        }

        public async Task<(decimal price,int availableSeats)> GetFareAndSeats(int scheduleId, int fareId)
        {
            var sql = @"SELECT f.price,s.AvailableSeats
                        FROM Fares f 
                        JOIN Schedules s ON s.RouteId = f.RouteId
                        WHERE s.ScheduleId = @ScheduleId AND f.FareId = @FareId";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<(decimal, int)>(sql, new
            {

                //Instead of this we can write var parameters = new DynamicParameters();
                ScheduleId = scheduleId,
                FareId = fareId
            });

     
        }


        public async Task<Booking> GetByIdAsync(int bookingId)
        {
            var sql = @"SELECT BookingId, ScheduleId, Seats, BookingStatus
                FROM Bookings
                WHERE BookingId = @BookingId";

            using var conn = _context.CreateConnection();
            return await conn.QuerySingleOrDefaultAsync<Booking>(sql, new { BookingId = bookingId });
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

        public async Task UpdateSeatsAsync(int scheduleId, int seats)
        {
            var sql = @"
            UPDATE Schedules
            SET AvailableSeats = AvailableSeats - @Seats
            WHERE ScheduleId = @ScheduleId";

            using var conn = _context.CreateConnection();
            await conn.ExecuteAsync(sql, new { ScheduleId = scheduleId, Seats = seats });
        }




        public async Task<IEnumerable<BookingDto>> GetByPhoneAsync(string phone)
        {
            var sql = @"SELECT b.BookingId,
                            CONCAT(r.Source, ' - ',.Destination) AS RouteName,
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

        public async Task<bool> CancelBookingAsync(int bookingId, string? cancellationReason)
        {
            var sql = @"UPDATE Bookings SET BookingStatus = 'CANCELLED' , CancellationReason = @CancellationReason,CancellationDate = GETDATE() 
                        WHERE BookingId = @BookingId";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(sql, new
            {
                BookingId = bookingId,
                CancellationReason = cancellationReason
            })>0;
        }

        public async Task<bool> RestoreSeatsAsync(int scheduleId, int seats)
        {
            var sql = @"UPDATE Schedules 
                SET AvailableSeats = AvailableSeats + @Seats 
                 WHERE ScheduleId = @ScheduleId";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(sql, new
            {
                ScheduleId = scheduleId,
                Seats = seats
            }) > 0;
        }
    }
}
