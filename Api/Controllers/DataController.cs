using DataAccessLayer.Concrete;
using DtoLayer.DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly CapstoneContext _context;

        public DataController(CapstoneContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetStockData()
        {
            var stocks = await _context.CompaniesScores
                                    .Select(s => new { s.CompanieStockCode, s.CompanieSScore })
                                    .ToListAsync();
            return Ok(stocks);
        }
    }
}

