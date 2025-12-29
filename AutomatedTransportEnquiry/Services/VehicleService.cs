using AutoMapper;
using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using AutomatedTransportEnquiry.Repositories;
using System.Net;

namespace AutomatedTransportEnquiry.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _repository;
        private readonly IMapper _mapper;
        public VehicleService(IVehicleRepository repository,IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<APIResponse> GetAllAsync()
        {
            var response = new APIResponse();
            try
            {
                var vehicle = await _repository.GetAllAsync();
                response.Data = vehicle;
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<APIResponse> GetByIdAsync(int vehicleId)
        {
            var response = new APIResponse();
            try
            {
                var vehicle = await _repository.GetByIdAsync(vehicleId);
                if (vehicle == null)
                {
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Errors.Add($"Vehicle with ID{vehicleId} not found.");
                    return response;
                }
                response.Data = _mapper.Map<VehicleDTO>(vehicle);
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




        public async Task<APIResponse> CreateAsync(VehicleCreateDto dto)
        {
            var response = new APIResponse();
            try
            {
                var vehicle = _mapper.Map<Vehicle>(dto);
                var id = await _repository.CreateAsync(vehicle);
                var resultDto = _mapper.Map<VehicleDTO>(vehicle);
                resultDto.VehicleId = id;
                response.Data=resultDto;
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;
                  
            }
            catch(Exception ex)
                {
                response.Status=false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Errors.Add(ex.Message);
            }
            return response;
        }



        public async Task<APIResponse> UpdateAsync(VehicleUpdateDto dto)
        {
           var response = new APIResponse();
            try
            {
                var vehicle = _mapper.Map<Vehicle>(dto);
                var updated = await _repository.UpdateAsync(vehicle);
                if (!updated)
                {
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;

                }
                response.Data = "Vehicle Updated Sucessfully.";
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





        public async Task<APIResponse> DeleteAsync(int vehicleId)
        {
            var response = new APIResponse();
            try
            {
                var deleted = await _repository.DeleteAsync(vehicleId);
                if (!deleted)
                {
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }
                response.Data = "Vehicle Deleted Sucessfully";
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
