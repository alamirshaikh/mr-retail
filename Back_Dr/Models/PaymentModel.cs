using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_Dr.Models
{
    public class PaymentModel
    {

		public int ID { get; set; }
		public string name_off { get; set; }
		public string TransactionNo { get; set; }
		public DateTime TranDate { get; set; }
		public string PaymentMode { get; set; }
		public decimal Amount { get; set; }

	}
}
