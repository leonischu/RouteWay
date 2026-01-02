using AutoMapper;
using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using AutomatedTransportEnquiry.Repositories;
using Microsoft.AspNetCore.Routing;
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

        public async Task<APIResponse> GetByIdAsync(int fareId)
        {
            var response = new APIResponse();
            try
            {
                var fare = await _repository.GetByIdAsync(fareId);
                if (fare == null)
                {
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Errors.Add($"Fare with ID {fareId} not found.");
                    return response;
                }
                response.Data = _mapper.Map<FareDto>(fare);
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
