using AutoMapper;
using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Models;
using AutomatedTransportEnquiry.Repositories;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AutomatedTransportEnquiry.Services
{
    public class SearchService:ISearchService
    {
        private readonly ISearchRepository _repository;
        private readonly IMapper _mapper;
        public SearchService(ISearchRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransportSearchResultDto>> SearchAsync(TransportSearchRequestDto dto)
        {
           
            
                var result = await _repository.SearchAsync(dto);
            return _mapper.Map<IEnumerable<TransportSearchResultDto>>(result);


        }

    }
}
