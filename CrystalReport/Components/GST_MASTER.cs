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
    public partial class GST_MASTER : Form
    {
        public GST_MASTER()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            main_datagrid.Rows.Clear();
            List<dynamic> dataTable = MainEngine_.GetDataScript<dynamic>("select purches_tax, sale_tax from TaxSettings where id = 1").ToList();
            foreach (var item in dataTable)
            {
                main_datagrid.Rows.Add(item.purches_tax, item.sale_tax);
            }

        }


        private void GST_MASTER_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;


            // Assuming GetDataScript returns a DataTable
            LoadData();




        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {


                var update = MainEngine_.GetDataScript<string>("update TaxSettings set purches_tax = '"+comboBox1.Text+ "', sale_tax = '"+comboBox2.Text+"'");
                MessageBox.Show("Updated ");
                LoadData();
            }
            catch (Exception ex)
            {
                 
            }
        }
    }
}
