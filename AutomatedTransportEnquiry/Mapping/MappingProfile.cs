using AutoMapper;
using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<VehicleRoute, VehicleRouteDto>().ReverseMap();
            CreateMap<VehicleRouteCreateDto, VehicleRoute>().ReverseMap();
            CreateMap<VehicleRouteUpdateDto, VehicleRoute>().ReverseMap();
        }

    }
}
