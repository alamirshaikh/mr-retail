using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Back_Dr.Sale.Models;

namespace Back_Dr.Sale.Inventory
{
    public  class ProductAdd
    {
        public async Task AddProduct(dynamic  models)
        {
            await MainEngine_.Add(models, "dbo.spProductItem");

        }

        
    }
}
