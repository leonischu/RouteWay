using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Repositories
{
    public interface IUserRepository
    {

        Task<User> GetAll();
        Task<User> GetByEmail(string email);
        Task Create(User user);

        Task<User> GetById(int id);
  

    }
}
