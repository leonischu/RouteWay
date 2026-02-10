using AutoMapper;
using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using AutomatedTransportEnquiry.Repositories;
using AutomatedTransportEnquiry.Services;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Numerics;

namespace AutomatedTransportEnquiry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]


    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]

    public class VehicleRouteController : ControllerBase
    {
        private readonly IVehicleRouteService _service;
        public VehicleRouteController(IVehicleRouteService service)
        {
            _service = service;

        }


        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAll()
        {



            var response = await _service.GetAllAsync();
            return StatusCode((int)response.StatusCode, response);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse>> GetById(int id)
        {
            var response = await _service.GetByIdAsync(id);
            return StatusCode((int)response.StatusCode, response);

        }


        [HttpPost]
        public async Task<ActionResult<APIResponse>> Create(VehicleRouteCreateDto dto)
        {
            var response = await _service.CreateAsync(dto);
            return StatusCode((int)response.StatusCode, response);

        }

        [HttpPut("{routeId}")]

        public async Task<ActionResult<APIResponse>> Update(int routeId, [FromBody]  VehicleRouteUpdateDto dto)
        {
            dto.RouteId = routeId;
         
            var response = await _service.UpdateAsync(dto);
            return StatusCode((int)response.StatusCode, response);

        }


        [HttpDelete("{routeId}")]

        public async Task<ActionResult<APIResponse>>Delete(int routeId)
        {
            var response = await _service.DeleteAsync(routeId);
            return StatusCode((int)response.StatusCode,response);
        }

    }
}
