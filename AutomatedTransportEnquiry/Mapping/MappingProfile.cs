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


            //Mapping for schedule 
            CreateMap<ScheduleCreateDto, Schedule>();
            CreateMap<ScheduleDto, Schedule>().ReverseMap();

            //Mapping for Fares
            
            CreateMap<FareDto, Fare>().ReverseMap();
            CreateMap<FareCreateDto, Fare>().ReverseMap();
            CreateMap<FareUpdateDto, Fare>().ReverseMap();

            //Mapping for Booking
            CreateMap<BookingCreateDto, Booking>().ReverseMap();
            CreateMap<Booking, BookingDto>().ReverseMap();
        }

    }
}
