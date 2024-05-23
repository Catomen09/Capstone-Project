using AutoMapper;
using BusinessLayer.Abstract;
using DtoLayer.CompaniesInfoDtos;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CompaniesInfo : ControllerBase
	{
		private readonly ICompaniesInfosService _companiesInfosService;
		private readonly IMapper _mapper;

		public CompaniesInfo(ICompaniesInfosService companiesInfosService, IMapper mapper)
		{
			_companiesInfosService = companiesInfosService;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetCompaniesInfo()
		{
			var list = _mapper.Map<List<ResultCompaniesInfoDto>>(_companiesInfosService.TGetListAll());
			Console.WriteLine(list);
			return Ok(_companiesInfosService.TGetListAll());

		}
	}
}
