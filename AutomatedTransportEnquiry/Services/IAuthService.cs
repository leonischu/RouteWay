using AutomatedTransportEnquiry.DTOs;

namespace AutomatedTransportEnquiry.Services
{
    public interface IAuthService
    {
        Task Register(RegisterDto dto);
        Task<string> Login(LoginDto dto);
   
    }
}
