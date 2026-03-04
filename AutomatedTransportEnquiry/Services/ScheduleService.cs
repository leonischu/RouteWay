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
        private readonly ICacheService _cacheService;

        private const string ALL_SCHEDULE_CACHE_KEY = "all_schedules";

        public ScheduleService(
            IScheduleRepository repository,
            IMapper mapper,
            ICacheService cacheService)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        //  Cached
        public async Task<IEnumerable<Schedule>> GetAsync()
        {
            return await _cacheService.GetOrSetAsync(
                ALL_SCHEDULE_CACHE_KEY,
                async () => await _repository.GetAsync(),
                TimeSpan.FromMinutes(10)
            );
        }

        //  Cached (with dynamic key)
        public async Task<IEnumerable<ScheduleDto>> GetAllAsync(string from, string to)
        {
            var cacheKey = $"schedule_{from}_{to}";

            return await _cacheService.GetOrSetAsync(
                cacheKey,
                async () =>
                {
                    var data = await _repository.GetAllAsync(from, to);
                    return _mapper.Map<IEnumerable<ScheduleDto>>(data);
                },
                TimeSpan.FromMinutes(10)
            );
        }

        public async Task<APIResponse> CreateAsync(ScheduleCreateDto dto)
        {
            var response = new APIResponse();

            try
            {
                var schedule = _mapper.Map<Schedule>(dto);
                var id = await _repository.CreateAsync(schedule);

                //  Clear main cache after insert
                _cacheService.Remove(ALL_SCHEDULE_CACHE_KEY);

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

        public async Task<APIResponse> DeleteAsync(int scheduleId)
        {
            var response = new APIResponse();

            try
            {
                var deleted = await _repository.DeleteAsync(scheduleId);

                if (!deleted)
                {
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Errors.Add("Schedule not found.");
                    return response;
                }

                //  Clear main cache after delete
                _cacheService.Remove(ALL_SCHEDULE_CACHE_KEY);

                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Data = "Schedule Deleted Successfully";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Errors.Add(ex.Message);
            }

            return response;
        }
    }
}