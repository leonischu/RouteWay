using AutoMapper;
using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using AutomatedTransportEnquiry.Repositories;
using System.Net;

namespace AutomatedTransportEnquiry.Services
{
    public class VehicleRouteService : IVehicleRouteService
    {
        private readonly IVehicleRouteRepository _repository;
        private readonly IMapper _mapper;

        public VehicleRouteService(
            IVehicleRouteRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<APIResponse> GetAllAsync()
        {
            var response = new APIResponse();

            try
            {
                var routes = await _repository.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<VehicleRouteDto>>(routes);
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

        public async Task<APIResponse> GetByIdAsync(int routeId)
        {
            var response = new APIResponse();

            try
            {
                var route = await _repository.GetByIdAsync(routeId);
                if (route == null)
                {
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Errors.Add($"Route with ID {routeId} not found.");
                    return response;
                }

                response.Data = _mapper.Map<VehicleRouteDto>(route);
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

        public async Task<APIResponse> CreateAsync(VehicleRouteCreateDto dto)
        {
            var response = new APIResponse();

            try
            {
                var route = _mapper.Map<VehicleRoute>(dto);
                var id = await _repository.CreateAsync(route);

                var resultDto = _mapper.Map<VehicleRouteDto>(route);
                resultDto.RouteId = id;

                response.Data = resultDto;
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

        public async Task<APIResponse> UpdateAsync(VehicleRouteUpdateDto dto)
        {
            var response = new APIResponse();

            try
            {
                var route = _mapper.Map<VehicleRoute>(dto);
                var updated = await _repository.UpdateAsync(route);

                if (!updated)
                {
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                response.Data = "Route updated successfully.";
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

        public async Task<APIResponse> DeleteAsync(int routeId)
        {
            var response = new APIResponse();

            try
            {
                var deleted = await _repository.DeleteAsync(routeId);
                if (!deleted)
                {
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                response.Data = "Route deleted successfully.";
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
