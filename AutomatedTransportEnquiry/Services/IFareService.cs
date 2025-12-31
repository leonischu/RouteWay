using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using System.Net;

namespace AutomatedTransportEnquiry.Services
{
    public interface IFareService
    {


         Task<APIResponse> GetAllAsync();
         Task<APIResponse> CreateAsync(FareCreateDto dto);



    }
}
