using AutoMapper;
using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;

namespace AutomatedTransportEnquiry.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {

            //Mapping for VehicleRoute

            CreateMap<VehicleRoute, VehicleRouteDto>().ReverseMap();
            CreateMap<VehicleRouteCreateDto, VehicleRoute>().ReverseMap();
            CreateMap<VehicleRouteUpdateDto, VehicleRoute>().ReverseMap();



            //Mapping for vehicles 
            CreateMap<Vehicle, VehicleDTO>().ReverseMap();
            CreateMap<VehicleCreateDto, Vehicle>().ReverseMap();
            CreateMap<VehicleUpdateDto, Vehicle>().ReverseMap();
        }

    }
}
