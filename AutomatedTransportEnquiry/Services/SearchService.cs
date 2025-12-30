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

        public async Task<APIResponse> SearchAsync(string from, string to)
        {
            var response = new APIResponse();
            try
            {
                var result = await _repository.SearchAsync(from, to);
                response.Data = result;
                response.Status = true;
                response.StatusCode = HttpStatusCode.InternalServerError;

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
