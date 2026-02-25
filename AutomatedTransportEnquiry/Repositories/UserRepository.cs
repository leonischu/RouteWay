using AutomatedTransportEnquiry.Data;
using AutomatedTransportEnquiry.Models;
using Dapper;
using System.Data;

namespace AutomatedTransportEnquiry.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var sql = "SELECT * FROM Users";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<User>(sql);
        }
        public async Task<User> GetByEmail(string email)
        {
            var sql = "SELECT * FROM Users WHERE Email = @Email";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(
                sql, new { Email = email });
        }

        public async Task Create(User user)
        {
            var sql = @"INSERT INTO Users (FullName, Email, PasswordHash, Role)
                        VALUES (@FullName, @Email, @PasswordHash, @Role)";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, user);
        }
      
    
        

       public async Task<User>GetById(int id)
        {
            var sql = @"
            SELECT 
            UserId , 
            FullName,
            Email,
            PasswordHash,
            Role
            FROM Users
            WHERE UserId = @UserId";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(
                sql, new { UserId = id });

                
            
        }


        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var sql = "SELECT * FROM Users WHERE Email = @Email";

            using var connection = _context.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<User>(
                sql,
                new { Email = email }
            );
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var sql = @"INSERT INTO Users (FullName, Email, PasswordHash, Role)
                VALUES (@FullName, @Email, @PasswordHash, @Role);
                
                SELECT CAST(SCOPE_IDENTITY() as int);";

            using var connection = _context.CreateConnection();

            var id = await connection.QuerySingleAsync<int>(sql, user);

            user.UserId = id;

            return user;
        }


    }
}
