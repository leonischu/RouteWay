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
                



                if (!await _repository.ScheduleExists(dto.ScheduleId))
                {
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Errors.Add("Invalid Schedule");
                    return response;
                }

                var (price, availableSeats) =
                                        await _repository.GetFareAndSeats(dto.ScheduleId, dto.FareId);


                if (availableSeats < dto.Seats)
                {
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Errors.Add("Not enough seats available");
                    return response;
                }



                var booking = _mapper.Map<Booking>(dto);
                booking.TotalAmount = price * dto.Seats;
                booking.BookingStatus = "CONFIRMED";

                var bookingId = await _repository.CreateAsync(booking);
                await _repository.UpdateSeatsAsync(dto.ScheduleId, dto.Seats);

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

        public async Task<APIResponse> GetByIdAsync(int bookingId)
        {
            var response = new APIResponse();
            try
            {
                var booking = await _repository.GetByIdAsync(bookingId);
                if(booking == null)
                {
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Errors.Add($"Booking with ID{bookingId} not found.");
                    return response;
                }
                response.Data = _mapper.Map<BookingDto>(booking);
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;
            } 
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.OK;
                response.Errors.Add(ex.Message);
            }
            return response;



        }
    }
}
