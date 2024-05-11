using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_Dr.Sale.Models
{
    public class SalesBillModel
    {
		public int id { get; set; }
		public string invoiceID { get; set; }
		public string cust_Name { get; set; }
		public int items { get; set; }
		public decimal sub_total { get; set; }
		public decimal perdis { get; set; }
		public decimal discount { get; set; }
		public decimal other { get; set; }
		public decimal TotalBill { get; set; }
	
	}
}
