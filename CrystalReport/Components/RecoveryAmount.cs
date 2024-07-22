using Back_Dr.Sale;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalReport.Components
{
    public partial class RecoveryAmount : UserControl
    {
        public RecoveryAmount()
        {
            InitializeComponent();
        }

        private void RecoveryAmount_Load(object sender, EventArgs e)
        {
            try
            {
                city.Items.AddRange(MainEngine_.GetDataScript<string>("select DISTINCT pcity from Customer where pcity IS not  null ").ToArray());



            
            }
            catch (Exception ex)
            {
                 
            }
        }

        private void city_SelectedIndexChanged(object sender, EventArgs e)
        {
             try
            {

                area.Items.AddRange(MainEngine_.GetDataScript<string>("select DISTINCT cust_addres from Customer where pcity = '"+city.Text+"'").ToArray());

                List<dynamic> cust_list = MainEngine_.GetDataScript<dynamic>("select * from Customer where Balance < 0 AND pcity = '"+city.Text+"'  ");

                datacust.Rows.Clear();

                foreach (var item in cust_list)
                {
                    datacust.Rows.Add(item.id,item.cust_name,item.cust_phone,item.cust_addres,item.cust_date,item.pcity,item.Balance);
                }


                double totalSum = datacust.Rows.Cast<DataGridViewRow>()
              .Sum(row => Convert.ToDouble(row.Cells[6].Value ?? 0));

                totalamt.Text = totalSum.ToString();



            }
            catch (Exception ex)
            { 
            }
        }

        private void area_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<dynamic> cust_list = MainEngine_.GetDataScript<dynamic>("select * from Customer where Balance  < 0 AND pcity = '" + city.Text + "' AND cust_addres = '"+area.Text+"' ");


            datacust.Rows.Clear();
            foreach (var item in cust_list)
            {
                datacust.Rows.Add(item.id, item.cust_name, item.cust_phone, item.cust_addres, item.cust_date, item.pcity, item.Balance);

            }


            double totalSum = datacust.Rows.Cast<DataGridViewRow>()
            .Sum(row => Convert.ToDouble(row.Cells[6].Value ?? 0));

            totalamt.Text = totalSum.ToString();

        }

        private void datacust_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {


               pbalance.Text =  datacust.Rows[e.RowIndex].Cells[6].Value.ToString();
               ids.Text =  datacust.Rows[e.RowIndex].Cells[0].Value.ToString();
            label9.Text = datacust.Rows[e.RowIndex].Cells[1].Value.ToString();

            }
            catch (Exception ex)
            {
                 //tsimple 
            }
        }

        private void recived_TextChanged(object sender, EventArgs e)
        {
            try
            {
                 
               decimal balance = MainEngine_.GetDataScript<decimal>("select Balance from Customer where id = "+Convert.ToInt32(ids.Text)+"").FirstOrDefault();
                pbalance.Text = (balance + Convert.ToDecimal(recived.Text)).ToString();

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                decimal prs = MainEngine_.GetDataScript<decimal>("select Balance from Customer where id = "+Convert.ToInt32(ids.Text)+"").FirstOrDefault();

                
                int s = MainEngine_.GetDataScript<decimal>("select * from CustomerTransactions").Count();


                string idge = $"REC/{s+1}/{recived.Text}";


                MainEngine_.GetDataScript<dynamic>("update Customer set Balance = "+ Convert.ToDecimal(pbalance.Text) + " where id = "+Convert.ToInt32(ids.Text)+" ");
                string sql = "INSERT INTO CustomerTransactions (Cust_ID, Cust_Name, Last_Date, PayMode, Paid,InvoiceId,Amount,Quantity) " +
               $"VALUES ({Convert.ToInt32(ids.Text)}, '{label9.Text}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{reviedby.Text}', {Convert.ToDecimal(recived.Text)},'{idge}',{0},{0})";

                MainEngine_.GetDataScript<dynamic>(sql);

                MessageBox.Show("Balance Update Succesfully!","Balance Qur",MessageBoxButtons.OK,MessageBoxIcon.Information);

                totalamt.Text = (Convert.ToDecimal(totalamt.Text) + Convert.ToDecimal(recived.Text)).ToString();
                pbalance.Text = "0";
                ids.Text = "0";
                label9.Text = "";
                recived.Text = "0";
                reviedby.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                
                List<dynamic> cust_list = MainEngine_.GetDataScript<dynamic>("select * from Customer where Balance < 0 AND cust_name = '"+comboBox1.Text+"'");

                datacust.Rows.Clear();

                foreach (var item in cust_list)
                {
                    datacust.Rows.Add(item.id, item.cust_name, item.cust_phone, item.cust_addres, item.cust_date, item.pcity, item.Balance);
                }


                double totalSum = datacust.Rows.Cast<DataGridViewRow>()
              .Sum(row => Convert.ToDouble(row.Cells[6].Value ?? 0));

                totalamt.Text = totalSum.ToString();



            }
            catch (Exception ex)
            {
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            var test = new { cust_name = comboBox1.Text };

            List<dynamic> list = MainEngine_.GetData<dynamic>("sp_getCustomer", test).ToList();

            foreach (var item in list.Take(10))
            {
                comboBox1.Items.Add(item.cust_name);

            }
        }
    }
}
