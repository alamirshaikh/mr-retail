using Back_Dr.Sale;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalReport.Components
{
    public partial class Profit : UserControl
    {
        public Profit()
        {
            InitializeComponent();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var dy = new
                {
                    startDate = dateTimePicker1.Value.ToString("yyyy/MM/dd"),
                    endDate = dateTimePicker2.Value.ToString("yyyy/MM/dd")
                };

                // Fetch data from the stored procedure
                List<dynamic> profit = MainEngine_.GetData<dynamic>("GetProfitAndLossReport", dy).ToList();

                foreach (var item in profit)
                {
                    // Access each column by its name
                    gst.Text = Convert.ToString(item.TotalGSTAmtPurchases);
                    purches.Text = Convert.ToString(item.TotalPurchasesAmount);
                    prd.Text = Convert.ToString(item.TotalPurchasesDiscount);
                    salegst.Text = Convert.ToString(item.TotalGSTAmtSales);
                    sales.Text = Convert.ToString(item.TotalSalesAmount);
                    sale_discount.Text= Convert.ToString(item.TotalSalesDiscount);
                    exp.Text = Convert.ToString(item.TotalKharcha);
                    var totalPurchasesReturnAmount = item.TotalPurchasesReturnAmount;
                    var totalGSTAmtPurchasesReturn = item.TotalGSTAmtPurchasesReturn;

                }


            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
