using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
	public class CompaniesInfo
	{
		[Key]
		public int CompaniesInfoId { get; set; }
		public string CompanieStockCode { get; set; }
		public string CompanieName { get; set; }
		public string ReportLink { get; set; }
		public decimal Scope1 { get; set; }
		public decimal Scope2 { get; set; }
		public decimal Scope3 { get; set; }
		public decimal WaterUsage { get; set; }
		public decimal EnergyUsage { get; set; }
		public decimal WomenEmployees { get; set; }
		public decimal MenEmployees { get; set; }
		public decimal TotalEmployees { get; set; }
		public decimal RenewableEnergy { get; set; }
		public decimal RatioOfWomenBoard { get; set; }
	}
}
