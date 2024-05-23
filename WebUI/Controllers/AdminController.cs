using DataAccessLayer.Concrete;
using EntityLayer.Entities;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneOf.Types;
using System.Collections.Generic;
using System.Net.Http;
using Tensorflow.Contexts;

namespace WebUI.Controllers
{
	public class AdminController : Controller
	{
		private readonly CapstoneContext _context;
		private readonly IHttpClientFactory _httpClientFactory;

		public AdminController(CapstoneContext context, IHttpClientFactory httpClientFactory)
		{
			_context = context;
			_httpClientFactory = httpClientFactory;
		}

		[Authorize]
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
								info.CompanieName = reader.GetValue(1).ToString();
								info.Scope1 = Convert.ToDecimal(reader.GetValue(2).ToString());
								info.Scope2 = Convert.ToDecimal(reader.GetValue(3).ToString());
								info.Scope3 = Convert.ToDecimal(reader.GetValue(4).ToString());
								info.WaterUsage = Convert.ToDecimal(reader.GetValue(5).ToString());
								info.EnergyUsage = Convert.ToDecimal(reader.GetValue(6).ToString());
								info.WomenEmployees = Convert.ToDecimal(reader.GetValue(7).ToString());
								info.MenEmployees = Convert.ToDecimal(reader.GetValue(8).ToString());
								info.TotalEmployees = Convert.ToDecimal(reader.GetValue(9).ToString());
								info.RatioOfWomenBoard = Convert.ToDecimal(reader.GetValue(10).ToString());
								info.RenewableEnergy = Convert.ToDecimal(reader.GetValue(11).ToString());
								info.ReportLink = reader.GetValue(12).ToString();

								_context.Add(info);
								await _context.SaveChangesAsync();


							}
						} while (reader.NextResult());
						ViewBag.Message = "Successful";
						CapstoneContext context = new CapstoneContext();
						var list = context.CompanieInfos.OrderByDescending(x => x.CompaniesInfoId).FirstOrDefault();

						var client = _httpClientFactory.CreateClient();
						var responseMessage = await client.GetAsync("https://localhost:7114/api/Prediction/predict");
						var responseMessage2 = await client.GetAsync("https://localhost:7114/api/Prediction/predict2");
						if (responseMessage.IsSuccessStatusCode && responseMessage2.IsSuccessStatusCode)
						{
							var jsonData = await responseMessage.Content.ReadAsStringAsync();
							var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();

							// JSON verisini deserialize edin
							var jsonObject = JObject.Parse(jsonData);
							var jsonObject2 = JObject.Parse(jsonData2);

							// output0 anahtarının altındaki ilk değeri alın
							var companieSScoreValue = (int)jsonObject["output0"][0];
							var companieEScoreValue = (int)jsonObject2["output0"][0];
							//Apiden json formatında geldi ve bunu deserialize ettik.Eğer apiye göndereceksek serialize etmemiz lazım jSON formatına
							var companiesScore = new CompaniesScore
							{
								CompaniesInfoId = list.CompaniesInfoId,
								CompanieEScore = companieEScoreValue,
								CompanieSScore = companieSScoreValue,
								CompanieStockCode = list.CompanieStockCode,
								CompanieName = list.CompanieName,
								ReportLink = list.ReportLink
							};

							context.Add(companiesScore);
							context.SaveChanges(); // Değişiklikleri kaydetmeyi unutmayın
						}

					}

				}
				return View("Index");
			}
			else
				ViewBag.Message = "Unsuccessful";
			return View();
		}
		public IActionResult DownloadExampleExcel()
		{
			// Bilgisayarınızdaki örnek Excel dosyasının tam yolunu alın
			string filePath = @"D:\OrnekExcel\Kitap2.xlsx";

			// İndirme işlemi için PhysicalFileResult dönün
			return PhysicalFile(filePath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SampleeExcelFile.xlsx");
		}



	}


}



