using DataAccessLayer.Concrete;
using EntityLayer.Entities;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
	public class AdminController : Controller
	{
		private readonly CapstoneContext _context;

		public AdminController(CapstoneContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ExcelImport(IFormFile file)
		{
			if (file != null && file.Length > 0)
			{
				var uploadFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads\\";
				if (!Directory.Exists(uploadFolder))
				{
					Directory.CreateDirectory(uploadFolder);
				}

				var filePath = Path.Combine(uploadFolder, file.FileName);
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}
				using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
				{

					using (var reader = ExcelReaderFactory.CreateReader(stream))
					{

						do
						{
							bool isHeaderSkipped = false;
							while (reader.Read())
							{
								if (!isHeaderSkipped)
								{
									isHeaderSkipped = true;
									continue;
								}
								CompaniesInfo info = new CompaniesInfo();
								info.CompanieStockCode = reader.GetValue(0).ToString();
								info.Scope1 = Convert.ToDecimal(reader.GetValue(1).ToString());
								info.Scope2 = Convert.ToDecimal(reader.GetValue(2).ToString());
								info.Scope3 = Convert.ToDecimal(reader.GetValue(3).ToString());
								info.WaterUsage = Convert.ToDecimal(reader.GetValue(4).ToString());
								info.EnergyUsage = Convert.ToDecimal(reader.GetValue(5).ToString());
								info.WomenEmployees = Convert.ToDecimal(reader.GetValue(6).ToString());
								info.MenEmployees = Convert.ToDecimal(reader.GetValue(7).ToString());
								info.TotalEmployees = Convert.ToDecimal(reader.GetValue(8).ToString());
								info.RenewableEnergy = Convert.ToDecimal(reader.GetValue(9).ToString());

								_context.Add(info);
								await _context.SaveChangesAsync();


							}
						} while (reader.NextResult());
						ViewBag.Message = "Başarılı";
					}

				}
				return View("Index");
			}
			else
				ViewBag.Message = "Başarısız";
			return View();
		}


	}
}



