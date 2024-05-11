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
    public partial class Purches_View : UserControl
    {
        public Purches_View()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "")
                {

                    comboBox1.Items.Clear();
                }
                else
                {
                     
                    comboBox1.Items.AddRange(MainEngine_.GetDataScript<string>("select company from Parties").ToArray());

                }

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if((dataGridView2.Rows.Count)-1 > 0)
                {
                    dataGridView2.Rows.Clear();
                    comboBox1.Items.Clear();
                    List<dynamic> list = MainEngine_.GetDataScript<dynamic>("SELECT * FROM Bill JOIN purches_Items ON Bill.BillID = purches_Items.Bill where partiname = '"+comboBox1.Text+"'");
                    foreach (var item in list)
                    {
                        dataGridView2.Rows.Add(item.billdate,item.partiname,item.description,item.rate,item.qty,item.discount,item.amount);

                    }
                    double totalSum = dataGridView2.Rows.Cast<DataGridViewRow>()
            .Sum(row => Convert.ToDouble(row.Cells[6].Value ?? 0));


                    textBox1.Text = $"Rs.{totalSum}";
                }
                else
                {
                    List<dynamic> list = MainEngine_.GetDataScript<dynamic>("SELECT * FROM Bill JOIN purches_Items ON Bill.BillID = purches_Items.Bill where partiname = '" + comboBox1.Text + "'");
                    foreach (var item in list)
                    {
                        dataGridView2.Rows.Add(item.billdate, item.partiname, item.description, item.rate, item.qty, item.discount, item.amount);

                    }

                    double totalSum = dataGridView2.Rows.Cast<DataGridViewRow>()
            .Sum(row => Convert.ToDouble(row.Cells[6].Value ?? 0));


                    textBox1.Text = $"Rs.{totalSum}";

                }

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Purches_View_Load(object sender, EventArgs e)
        {

        }
    }
}
