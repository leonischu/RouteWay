using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using AutomatedTransportEnquiry.Repositories;
using System.Net;

namespace AutomatedTransportEnquiry.Services
{
    public class SearchService:ISearchService
    {
        private readonly ISearchRepository _repository;
        public SearchService(ISearchRepository repository)
        {
            _repository = repository;
        }

        public async Task<APIResponse> SearchAsync(TransportSearchRequestDto dto)
        {
            var response = new APIResponse();
            try
            {
                var result = await _repository.SearchAsync(dto);
                response.Data = result;
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                //response.Errors.Add(ex.Message);
                response.Errors.Add("An unexpected error occured");
        
            }


            return response;
        }

    }
}
