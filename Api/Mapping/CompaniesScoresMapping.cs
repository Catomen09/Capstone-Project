using AutoMapper;
using DtoLayer.CompaniesInfoDtos;
using DtoLayer.CompaniesScoresDtos;
using EntityLayer.Entities;

namespace WebApi.Mapping
{
    public class CompaniesScoresMapping : Profile
    {
        public CompaniesScoresMapping()
        {
            CreateMap<CompaniesScore, ResultCompaniesScoresDto>().ReverseMap();
        }
    }
}
