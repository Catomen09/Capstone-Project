namespace WebUI.Dtos.CompaniesScoresDtos
{
	public class ResultCompaniesScores
	{
		public int CompaniesScoreId { get; set; }
		public int CompaniesInfoId { get; set; }
		public string CompanieStockCode { get; set; }
		public decimal CompanieEScore { get; set; }
		public string CompanieName { get; set; }
		public string ReportLink { get; set; }
		public decimal CompanieSScore { get; set; }
	}
}
