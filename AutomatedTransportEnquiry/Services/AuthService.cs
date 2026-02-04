using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using AutomatedTransportEnquiry.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AutomatedTransportEnquiry.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _config;


        public AuthService(IUserRepository repo, IConfiguration config)



        {
            _repo = repo;
            _config = config;

        }

        public async Task Register(RegisterDto dto)
        {





            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "User"




            };

            await _repo.Create(user);






        }

        public async Task<string> Login(LoginDto dto)
        {
            var user = await _repo.GetByEmail(dto.Email);
            if (user == null ||
                !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");





            var claims = new[]
             {
                new Claim("id", user.UserId.ToString()),
                new Claim("name", user.FullName),
                 new Claim("email", user.Email),
                 new Claim("role", user.Role)
                    };


            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials:
                    new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task SeedAdmin()
        {
            // Check if admin already exists
            var adminEmail = "admin@example.com"; // set your admin email
            var existingAdmin = await _repo.GetByEmail(adminEmail);
            if (existingAdmin != null) return; // Admin already exists

            var admin = new User
            {
                FullName = "Admin",
                Email = adminEmail,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"), // set your password
                Role = "Admin"
            };

            await _repo.Create(admin);
        }

    }
}