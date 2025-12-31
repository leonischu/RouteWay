using AutoMapper;
using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using AutomatedTransportEnquiry.Repositories;
using System.Net;

namespace AutomatedTransportEnquiry.Services
{
    public class FareService : IFareService
    {
        private readonly IFareRepository _repository;
        private readonly IMapper _mapper;
        public FareService(IFareRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<APIResponse> GetAllAsync()
        {
            var response = new APIResponse();
            var result = await _repository.GetAllAsync();
            response.Data = _mapper.Map<IEnumerable<FareDto>>(result);
            response.Status = true;
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        public async Task<APIResponse> CreateAsync(FareCreateDto dto)
        {
            var response = new APIResponse();
            try
            {

                var fare = _mapper.Map<Fare>(dto);
                var id = await _repository.CreateAsync(dto);

                fare.FareId = id;

                response.Data = _mapper.Map<FareDto>(fare);
                response.Status = true;
                response.StatusCode = HttpStatusCode.Created;
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
