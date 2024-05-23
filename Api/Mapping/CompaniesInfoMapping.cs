using AutoMapper;
using DtoLayer.CompaniesInfoDtos;
using EntityLayer.Entities;

namespace WebApi.Mapping
{
	public class CompaniesInfoMapping : Profile
	{
		public CompaniesInfoMapping()
		{
			CreateMap<CompaniesInfo, ResultCompaniesInfoDto>().ReverseMap();
		}
	}
}
