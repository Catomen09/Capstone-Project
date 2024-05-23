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
    public class CompaniesScoresManager : ICompaniesScoresService
    {
        private readonly ICompaniesScoresDal _companiesScoresDal;

        public CompaniesScoresManager(ICompaniesScoresDal companiesScoresDal)
        {
            _companiesScoresDal = companiesScoresDal;
        }

        public void TAdd(CompaniesScore entity)
        {
            _companiesScoresDal.Add(entity);
        }

        public void TDelete(CompaniesScore entity)
        {
            _companiesScoresDal.Delete(entity);
        }

        public CompaniesScore TGetById(int id)
        {
            return _companiesScoresDal.GetById(id);
        }

        public List<CompaniesScore> TGetListAll()
        {
            return _companiesScoresDal.GetListAll();
        }

        public void TUpdate(CompaniesScore entity)
        {
            _companiesScoresDal.Update(entity);
        }
    }
}
