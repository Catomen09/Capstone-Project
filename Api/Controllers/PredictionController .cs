using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static WebApi.PredictionService;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PredictionController : ControllerBase
	{
		private readonly PredictionService _predictionService;
		private readonly PredictionService _predictionService2;
		CapstoneContext context = new CapstoneContext();

		public PredictionController()
		{
			string modelPath = @"D:\AI\model.onnx";
			string modelPath2 = @"D:\AI\environmentAnn.onnx";

			_predictionService = new PredictionService(modelPath);
			_predictionService2 = new PredictionService(modelPath2);
		}

		[HttpGet("predict")]
		public IActionResult Predict()
		{
            float[] minValues = new float[] { 1, 0, 2.82f, 202761, 251.99f, 6, 39, 51, 0, 0 };
            float[] maxValues = new float[] { 9.06514700e+06f, 1.59069200e+06f, 2.00287630e+08f, 3.51040500e+07f, 3.93339215e+09f, 3.22126200e+04f, 8.70933800e+04f, 1.19306000e+05f, 8.30000000e-01f, 6.21041000e+07f };
            float[] exampleValues = new float[] { 140675, 19, 47054, 88, 119793, 18, 380365, 00, 2965199, 86, 85, 00, 951, 00, 1076, 00, 0, 25, 4124184, 00 };

            var list = context.CompanieInfos.OrderByDescending(x => x.CompaniesInfoId).Take(1);
			var inputList = new List<float>();
			foreach (var item in list)
			{

				inputList.Add((float)item.Scope1);
				inputList.Add((float)item.Scope2);
				inputList.Add((float)item.Scope3);
				inputList.Add((float)item.WaterUsage);
				inputList.Add((float)item.EnergyUsage);
				inputList.Add((float)item.WomenEmployees);
				inputList.Add((float)item.MenEmployees);
				inputList.Add((float)item.TotalEmployees);
				inputList.Add((float)item.RatioOfWomenBoard);
				inputList.Add((float)item.RenewableEnergy);
			}
			var input = new ModelInput()
			{
				Inputs = inputList.ToArray()
			};
			try
			{
                var check = _predictionService.MinMaxScale(input.Inputs, minValues, maxValues);
                var input2 = new ModelInput()
                {
                    Inputs = check.ToArray()
                };
                var result = _predictionService.Predict(input2);
                return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal Server Error: " + ex.Message);
			}
		}
		[HttpGet("predict2")]
		public IActionResult Predict2()
		{
			float[] minValues = new float[] { 39, 0};
			float[] maxValues = new float[] { 8.70933800e+04f,  8.30000000e-01f };
			float[] exampleValues = new float[] { 140675,19 ,  47054,88  ,  119793,18  , 380365,00 ,  2965199,86 , 85,00  , 951,00 , 1076,00 , 0,25 ,   4124184,00 };

			var list = context.CompanieInfos.OrderByDescending(x => x.CompaniesInfoId).Take(1);
			var inputList = new List<float>();
			foreach (var item in list)
			{

				inputList.Add((float)item.MenEmployees);
				inputList.Add((float)item.RatioOfWomenBoard);
				
			}
			var input = new ModelInput2()
			{
				Inputs = inputList.ToArray()
			};
			try
			{
				var check = _predictionService2.MinMaxScale(input.Inputs, minValues, maxValues);
				var input2 = new ModelInput2()
				{
					Inputs = check.ToArray()
				};
				var result = _predictionService2.Predict2(input2);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal Server Error: " + ex.Message);
			}
		}
	}
}
