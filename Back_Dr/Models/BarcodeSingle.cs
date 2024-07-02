using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_Dr.Models
{
   public class BarcodeSingle
    {

        public double Width { get; set; }
        public double Height { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public string ShopName { get; set; }
            
        public int CODE { get; set; }

    }
}
