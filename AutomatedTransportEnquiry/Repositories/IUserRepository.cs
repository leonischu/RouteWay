using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmail(string email);
        Task Create(User user);
    }
}
