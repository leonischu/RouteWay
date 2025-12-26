using AutoMapper;
using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using AutomatedTransportEnquiry.Repositories;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Numerics;

namespace AutomatedTransportEnquiry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]

    public class VehicleRouteController : ControllerBase
    {
        private readonly IVehicleRouteRepository _repository;
        private readonly IMapper _mapper;
        private readonly APIResponse _apiResponse;
        public VehicleRouteController(IVehicleRouteRepository repository,IMapper mapper,APIResponse apiResponse)
        {
            _repository = repository;
            _mapper = mapper;
            _apiResponse = apiResponse;
        }


        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAll()
        {

            try
            {
                var routes = await _repository.GetAllAsync();
                _apiResponse.Data = _mapper.Map<IEnumerable<VehicleRouteDto>>(routes);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse>> GetById(int id)
        {
            try {
                var route = await _repository.GetByIdAsync(id);
                if (route == null)
                {
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.Data = null;
                    _apiResponse.Errors.Clear();
                    _apiResponse.Errors.Add($"Route with ID {id} not found.");
                    return NotFound(_apiResponse);
                }

                var routes = await _repository.GetAllAsync();
                _apiResponse.Data = _mapper.Map<IEnumerable<VehicleRouteDto>>(routes);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiResponse);
            }
            catch(Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }
            
            }


        [HttpPost]
        public async Task<ActionResult<APIResponse>Create([FromBody] VehicleRouteCreateDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest();

                VehicleRoute route = _mapper.Map<VehicleRouteDto>(dto);
                var routeAfterCreation = 
                var id = await _repository.CreateAsync(route);
                route.RouteId = id;
                return CreatedAtAction(nameof(GetById), new { id }, route);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }



        }

        [HttpPut]

        public async Task<IActionResult> Update(VehicleRouteUpdateDto dto)
        {
            var route = new VehicleRoute
            {
                RouteId = dto.RouteId,
                Source = dto.Source,
                Destination = dto.Destination,
                Distance = dto.Distance
            };

            var updated = await _repository.UpdateAsync(route);
            if (!updated)
            {
                return NotFound("Route not found");
            }
            return Ok("Route Updated Sucessfully");
        }


        [HttpDelete("{RouteId}")]

        public async Task<IActionResult> Delete(int RouteId)
        {
            var deleted = await _repository.DeleteAsync(RouteId);
            if (!deleted)
                return NotFound("Route Not Found");
            return Ok("Route deleted sucessfully");
        }


    }
}
