using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Onnx;

namespace WebApi
{
	public class PredictionService
	{
		private readonly MLContext _mlContext;
		private readonly OnnxTransformer _model;
		
		public PredictionService(string modelPath)
		{
			_mlContext = new MLContext();
			try
			{
				DataViewSchema modelSchema;
				if (modelPath.Equals("D:\\AI\\environmentAnn.onnx"))
				{
                    _model = _mlContext.Transforms.ApplyOnnxModel(modelPath).Fit(_mlContext.Data.LoadFromEnumerable(new List<ModelInput2>()));
                }
				else
				_model = _mlContext.Transforms.ApplyOnnxModel(modelPath).Fit(_mlContext.Data.LoadFromEnumerable(new List<ModelInput>()));
				
			}
			catch (Exception ex)
			{
				// Hata ayrıntılarını yazdır
				Console.WriteLine($"Failed to load model: {ex.Message}");
				throw; // Hatanın yukarıya fırlatılması
			}
		}



		public class ModelInput
		{
			[ColumnName("inputs"), VectorType(10)]
			public float[] Inputs { get; set; }

		}
		public class ModelInput2
		{
            [ColumnName("inputs"), VectorType(2)]
            public float[] Inputs { get; set; }
        }
    
		public class ModelOutput
		{
			[ColumnName("output_0")]
			public float[] Output0 { get; set; }
		}


		public ModelOutput Predict(ModelInput input)
		{
			var predictor = _mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(_model);
			return predictor.Predict(input);
		}
        public ModelOutput Predict2(ModelInput2 input2)
        {
            var predictor = _mlContext.Model.CreatePredictionEngine<ModelInput2, ModelOutput>(_model);
            return predictor.Predict(input2);
        }
        public float[] MinMaxScale(float[] values, float[] minValues, float[] maxValues)
		{
			if (values.Length != minValues.Length || values.Length != maxValues.Length)
				throw new Exception("Dizi boyutları eşleşmiyor.");

			float[] scaled = new float[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				scaled[i] = (values[i] - minValues[i]) / (maxValues[i] - minValues[i]);
			}
			return scaled;
		}

	}
}
