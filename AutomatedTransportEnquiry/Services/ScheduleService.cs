using AutoMapper;
using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using AutomatedTransportEnquiry.Repositories;
using System.Net;

namespace AutomatedTransportEnquiry.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _repository;
        private readonly IMapper _mapper;
        public ScheduleService(IScheduleRepository repository, IMapper mapper)
        {

            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ScheduleDto>> GetAllAsync(string from, string to)
        {
            
                var data = await _repository.GetAllAsync(from ,to);
            return _mapper.Map<IEnumerable<ScheduleDto>>(data);

        }
        public async Task<APIResponse> CreateAsync(ScheduleCreateDto dto)
        {
            var response = new APIResponse();
            try
            {
                var schedule = _mapper.Map<Schedule>(dto);
                var id = await _repository.CreateAsync(schedule);
                response.Data = id;
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
