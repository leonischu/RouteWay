using AutoMapper;
using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using AutomatedTransportEnquiry.Repositories;
using System.Net;

namespace AutomatedTransportEnquiry.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;
        private readonly IFareRepository _fareRepository;
        private readonly IMapper _mapper;
        public BookingService(
        IBookingRepository repository,
        IFareRepository fareRepository,
        IMapper mapper)
        {
            _repository = repository;
            _fareRepository = fareRepository;
            _mapper = mapper;
        }
        public async Task<APIResponse> CreateAsync(BookingCreateDto dto)
        {
            var response = new APIResponse();
            try
            {
                var fare = await _fareRepository.GetByIdAsync(dto.FareId);
                if (fare == null)
                {
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Errors.Add("Fare not found");
                    return response;
                }
                var booking = _mapper.Map<Booking>(dto);
                booking.TotalAmount = fare.Price * dto.Seats;

                var bookingId = await _repository.CreateAsync(booking);

                response.Data = bookingId;
                response.Status = true;
                response.StatusCode = HttpStatusCode.Created;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Errors.Add(ex.Message);
            }

            return response;

        }
        
    }
}
