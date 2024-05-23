using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class CompaniesScore
    {
        [Key]
        public int CompaniesScoreId { get; set; }
        public int CompaniesInfoId { get; set; }

        [ForeignKey("CompaniesInfoId")]
        public CompaniesInfo CompaniesInfo { get; set; }
        public string CompanieStockCode { get; set; }
        public decimal CompanieEScore { get; set; }
        public string CompanieName { get; set; }
        public string ReportLink { get; set; }
		public decimal CompanieSScore { get; set; }

	}
}
