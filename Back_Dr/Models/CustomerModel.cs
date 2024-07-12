using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_Dr.Sale.Models
{
	public class CustomerModel 
{
	public int id { get; set; }
	public string cust_name { get; set; }
	public string cust_phone { get; set; }
        public string pstate { get; set; }
        public string pcity { get; set; }
}

}
