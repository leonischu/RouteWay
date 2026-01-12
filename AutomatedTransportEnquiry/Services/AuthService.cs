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
        private readonly IEmailService _emailService;  

        public AuthService(
            IUserRepository repo,
            IConfiguration config,
            IEmailService emailService)  
        {
            _repo = repo;
            _config = config;
            _emailService = emailService;  
        }

        public async Task Register(RegisterDto dto)
        {
            // Check if user already exists
            var existingUser = await _repo.GetByEmail(dto.Email);
            if (existingUser != null)
                throw new InvalidOperationException("Email already registered");

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "User",
                // Email verification fields
                IsEmailVerified = false,
                EmailVerificationToken = Guid.NewGuid().ToString(),
                EmailVerificationTokenExpiry = DateTime.UtcNow.AddHours(24)
            };

            await _repo.Create(user);

            // Send verification 
            await _emailService.SendVerificationEmail(
                user.Email,
                user.EmailVerificationToken
            );
        }

        public async Task<string> Login(LoginDto dto)
        {
            var user = await _repo.GetByEmail(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email or password");

            // Email Verfication
            if (!user.IsEmailVerified)
                throw new UnauthorizedAccessException("Email not verified. Please check your email to verify your account.");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),  
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // NEW METHOD: Verify Email
        public async Task<bool> VerifyEmail(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;

            var user = await _repo.GetByVerificationToken(token);

            if (user == null)
                return false;

            // Check if token expired
            if (user.EmailVerificationTokenExpiry < DateTime.UtcNow)
                return false;

            // Update user verification status
            user.IsEmailVerified = true;
            user.EmailVerificationToken = null;
            user.EmailVerificationTokenExpiry = null;

            await _repo.UpdateVerification(user);
            return true;
        }
    }
}