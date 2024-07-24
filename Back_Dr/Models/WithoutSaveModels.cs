using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_Dr.Models
{
    public class WithoutSaveModels
    {
        public string same;


               

        public int Sr { get; set; }
        public string invoiceID { get; set; }
        public string per { get; set; }
        public string cust_Name { get; set; }

        public string ITEM_NAME { get; set; }

        public decimal SALE_PRICE { get; set; }
        public decimal TotalBill { get; set; }
		public DateTime invdate { get; set; }
		public int Qty { get; set; }

        public decimal Amount { get; set; }
        public decimal discount { get; set; }
    }
}
