using AutoMapper;
using AutomatedTransportEnquiry.Models;
using AutomatedTransportEnquiry.Repositories;
using System.Net;

namespace AutomatedTransportEnquiry.Services
{
    public class UserService : IUserService
    {

     
        private readonly IUserRepository  _repository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }


        public async Task<APIResponse> GetAllAsync()
        {
            var response = new APIResponse();
            try
            {
                var users = await _repository.GetAll();
                response.Data = users;
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Errors.Add(ex.Message);
            }
            return response;
        }
    }
}
