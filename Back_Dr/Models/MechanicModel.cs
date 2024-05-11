using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_Dr.Models
{
   public class MechanicModel
    {	public int ID { get; set; }
	public string Mechanic_Name { get; set; }
	public string Item_Name { get; set; }
	public decimal Item_Cost { get; set; }
	public decimal Item_price { get; set; }
	public DateTime Date { get; set; }
    }
}
