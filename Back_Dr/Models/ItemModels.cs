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
        public decimal MRP { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }
         

        public decimal STOCK { get; set; }
		public string UNIT { get; set; }
		public string BARCODE { get; set; }
		public string DESCRIPTION { get; set; }
		public string IDATE { get; set; }

        public string Color { get; set; }
        public string Style { get; set; }



    }
}
