using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class CompaniesInfoManager : ICompaniesInfosService
	{
		private readonly ICompaniesInfoDal _companiesIfoDal;

		public CompaniesInfoManager(ICompaniesInfoDal companiesIfoDal)
		{
			_companiesIfoDal = companiesIfoDal;
		}

		public void TAdd(CompaniesInfo entity)
		{
			_companiesIfoDal.Add(entity);
			
		}

		public void TDelete(CompaniesInfo entity)
		{
			_companiesIfoDal.Delete(entity);
		}

		public CompaniesInfo TGetById(int id)
		{
			 return _companiesIfoDal.GetById(id);
		}

		public List<CompaniesInfo> TGetListAll()
		{
			return _companiesIfoDal.GetListAll();
		}

		public void TUpdate(CompaniesInfo entity)
		{
			_companiesIfoDal.Update(entity);
		}
	}
}
