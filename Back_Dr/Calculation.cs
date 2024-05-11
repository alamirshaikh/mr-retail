using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_Dr.Sale
{
    public   class Calculation
    {


        public decimal sub_amount { get; set; } = 0;
        public   decimal total = 0;
        public   decimal discounts = 0;

        public   decimal SubTotal(string amount)
            { 
            sub_amount = sub_amount + decimal.Parse(amount);

            return sub_amount; 
        }


        public   void afterdiscount(decimal discount,string type)
        {
            if (type == "%")
            {
                discounts = (discount / 100) * sub_amount;
                total = sub_amount - discounts;
            }
            else
            {

                discounts =  sub_amount - discount;
                total = sub_amount - discount;
            }

        }
        




    }
}
