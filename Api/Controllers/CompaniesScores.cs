using AutoMapper;
using BusinessLayer.Abstract;
using DtoLayer.CompaniesScoresDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesScores : ControllerBase
    {
        private readonly ICompaniesScoresService _companiesScoresService;
        private readonly IMapper _mapper;

        public CompaniesScores(ICompaniesScoresService companiesScoresService, IMapper mapper)
        {
            _companiesScoresService = companiesScoresService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCompaniesScores()
        {
            return Ok(_mapper.Map<List<ResultCompaniesScoresDto>>(_companiesScoresService.TGetListAll()));
        }
    }
}
