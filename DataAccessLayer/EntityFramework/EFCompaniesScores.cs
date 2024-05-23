using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFCompaniesScores : GenericRepository<CompaniesScore>, ICompaniesScoresDal
    {
        public EFCompaniesScores(CapstoneContext context) : base(context)
        {
        }
    }
}
