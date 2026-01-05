using AutoMapper;
using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using AutomatedTransportEnquiry.Repositories;
using System.Net;

namespace AutomatedTransportEnquiry.Services
{
    public class CancelBookingService : ICancelBookingService
    {

        private readonly IBookingRepository _repository;
        private readonly IMapper _mapper;

        public CancelBookingService(IBookingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }





        public async Task<APIResponse> CancelBookingAsync(CancelBookingDto dto)
        {
            var response = new APIResponse();

            try
            {
                var booking = await _repository.GetByIdAsync(dto.BookingId);

                if (booking == null)
                {
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Errors.Add("Booking not found");
                    return response;
                }

                if (booking.BookingStatus == "CANCELLED")
                {
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Errors.Add("Booking already cancelled");
                    return response;
                }

                await _repository.CancelBookingAsync(dto.BookingId, dto.CancellationReason);
                await _repository.RestoreSeatsAsync(booking.ScheduleId, booking.Seats);

                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Data = "Booking cancelled and seats restored successfully";
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

