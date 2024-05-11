using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_Dr.Sale.Models
{
    public class ProductM
    {
		public int ID { get; set; }
		public string PR_CODE { get; set; }
		public string ITEM_NAME { get; set; }
		public int TYPE_TAX { get; set; }
		public int STOCK { get; set; }
		public string UNIT { get; set; }
		public string BARCODE { get; set; }
		public decimal SALE_PRICE { get; set; }
		public string ACCOUNT { get; set; }
		public string DESCRIPTION { get; set; }
		public decimal COST_PRICE { get; set; }
		public string pr_ACCOUNT { get; set; }
		public decimal pr_COSTPRICE { get; set; }
		public string pr_DESCRIPTION { get; set; }
		public string IDATE { get; set; }
        public string pic { get; set; }
        public string USER_N { get; set; }

	}
}
