using Dr.Sale.Components;
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
    public partial class DashBoard : UserControl
    {
        public DashBoard()
        {
            InitializeComponent();
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            textBox1.Text = StoreRoom.TodaysSales();
            textBox2.Text = StoreRoom.MonthSales();
            textBox3.Text = StoreRoom.CustomerCount();
            textBox4.Text = StoreRoom.Balance();


        }
    }
}
