using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_Dr.Models
{
    public class ItemModels
    {
        public int ID { get; set; }
        public string ITEM_NAME { get; set; }

        public decimal COST_PRICE { get; set; }
        public decimal SALE_PRICE { get; set; }

		public int STOCK { get; set; }
		public string UNIT { get; set; }
		public string BARCODE { get; set; }
		public string DESCRIPTION { get; set; }
		public DateTime IDATE { get; set; }
	 

	}
}
