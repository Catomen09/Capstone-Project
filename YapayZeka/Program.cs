using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Onnx;
using System.Collections.Generic;
using System;
using DataAccessLayer.Concrete;
using EntityLayer.Entities;

public class ModelInput
{
	[ColumnName("inputs"), VectorType(10)]
	public float[] Inputs { get; set; }
}

public class ModelOutput
{
	[ColumnName("output_0")]
	public float[] Output0 { get; set; }
}

class Program
{
	static void Main(string[] args)
	{
		CapstoneContext context = new CapstoneContext();
		int i = 157;
		while (i < 158)
		{
			var list = context.CompanieInfos.Where(x => x.CompaniesInfoId == i).ToList();
			var inputList = new List<float>();
			foreach (var item in list)
			{

				inputList.Add((float)item.RenewableEnergy);
				inputList.Add((float)item.TotalEmployees);
				inputList.Add((float)item.Scope3);
				inputList.Add((float)item.WomenEmployees);
				inputList.Add((float)item.EnergyUsage);
				inputList.Add((float)item.Scope1);
				inputList.Add((float)item.Scope2);
				inputList.Add((float)item.WaterUsage);
				inputList.Add((float)item.MenEmployees);
				inputList.Add(0.25f);
			}
			var mlContext = new MLContext();
			string modelPath = @"D:\YabayZeka/model2.onnx";

			// Modelinizi yükleyin
			var model = mlContext.Transforms.ApplyOnnxModel(modelPath)
						.Fit(mlContext.Data.LoadFromEnumerable(new List<ModelInput>()));

			// Tahmin motorunu oluşturun
			var predictor = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);

			// Tahmin yapmak için girdi verinizi hazırlayın
			var input = new ModelInput()
			{
				Inputs = inputList.ToArray()
			};

			// Tahmini alın
			ModelOutput result = predictor.Predict(input);

			// Tahmini yazdırın
			Console.WriteLine($"Tahmin: {string.Join(", ", result.Output0)}");


			var companiesScore = new CompaniesScore
			{
				CompaniesInfoId = list.Select(x => x.CompaniesInfoId).FirstOrDefault(),
				CompanieEScore = (decimal)result.Output0[0],
				CompanieStockCode = list.Select(x => x.CompanieStockCode).FirstOrDefault(),
				CompanieName = list.Select(x => x.CompanieName).FirstOrDefault(),
				ReportLink = list.Select(x => x.ReportLink).FirstOrDefault()


			};

			context.Add(companiesScore);
			context.SaveChanges(); // Değişiklikleri kaydetmeyi unutmayın
			i++;
		}

	}
}