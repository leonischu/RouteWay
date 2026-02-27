using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Repositories
{
    public interface IUserRepository
    {

        Task<IEnumerable<UserDetailDto>> GetAll();
        Task<User> GetByEmail(string email);
        Task Create(User user);

        Task<User> GetById(int id);


        //For Login With Google 
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> CreateUserAsync(User user);


    }
}
