using Back_Dr.Sale;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalReport.Components
{
    public partial class DayBook : UserControl
    {
        public DayBook()
        {
            InitializeComponent();
        }


        private string sql = "";


        List<dynamic> fetchedData = new List<dynamic>();
        private async void LoadInitialData()
        {

            dataGridView1.Rows.Clear();


            sql = "select * from Transactions where Date_ = '" + DateTime.Today.ToString("yyyy/MM/dd") + "'";
            // Fetch initial set of data
            fetchedData = await Task.Run(() => MainEngine_.GetDataScript<dynamic>(sql));

            // Show the initial set of records in the DataGridView
            DisplayRecords(0, 100); // Display the first 100 records as an example

        }

        private void DisplayRecords(int startIndex, int count)
        {
            for (int i = startIndex; i < Math.Min(startIndex + count, fetchedData.Count); i++)
            {
                dataGridView1.Rows.Add(fetchedData[i].Date_, fetchedData[i].Particular, fetchedData[i].Description, fetchedData[i].voucher_t, fetchedData[i].Amount);
            }
        }


        private void DisplayMoreRecords()
        {
            // Example: Load the next 100 records
            int startIndex = dataGridView1.RowCount; // Index from which to start adding new records
            int recordsToLoad = 10; // Number of records to load at a time

            DisplayRecords(startIndex, recordsToLoad);
        }
        private void DayBook_Load(object sender, EventArgs e)
        {
            try
            {

                LoadInitialData();

               IEnumerable<decimal> tt = MainEngine_.GetDataScript<decimal>("select Amount from Transactions where Date_ = '" + DateTime.Today.ToString("yyyy/MM/dd") + "'").ToList();

                decimal tsp = tt.Sum();
                total.Text = $"Rs.{tsp}";



                IEnumerable<decimal> cash = MainEngine_.GetDataScript<decimal>("select Amount from Transactions WHERE LOWER(voucher_t) LIKE '%cash%' AND  Date_ = '" + DateTime.Today.ToString("yyyy/MM/dd") + "'").ToList();
                decimal cash_amt = cash.Sum();

                IEnumerable<decimal> online = MainEngine_.GetDataScript<decimal>("select Amount from Transactions WHERE LOWER(voucher_t) NOT LIKE '%cash%' AND  Date_ = '" + DateTime.Today.ToString("yyyy/MM/dd") + "'").ToList();
                decimal online_amt = online.Sum();

                textcash.Text = $"Rs.{cash_amt}";
                
                textonline.Text = $"Rs.{online_amt}";
                IEnumerable<decimal> credit = MainEngine_.GetDataScript<decimal>($"select Amount from Transactions WHERE LOWER(voucher_t)  LIKE '%credit%'   AND  Date_ >= '{dateTimePicker1.Text} 00:00:00' AND  Date_ <= '{dateTimePicker2.Text} 23:59:59'").ToList();
                decimal credit_amt = credit.Sum();

 

                creditamt.Text = $"Rs.{credit_amt}";

            }

            catch (Exception ex)
            {
                 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sql = $"select * from Transactions where Date_ >= '{dateTimePicker1.Text} 00:00:00' AND  Date_ <= '{dateTimePicker2.Text} 23:59:59' ";
                List<dynamic> li = MainEngine_.GetDataScript<dynamic>(sql).ToList();
                dataGridView1.Rows.Clear();
                for (int i = 0; i < Math.Min(0 + 100, li.Count); i++)
                {
                    dataGridView1.Rows.Add(li[i].Date_, li[i].Particular, li[i].Description, li[i].voucher_t, li[i].Amount); ;
                }



                IEnumerable<decimal> tt = MainEngine_.GetDataScript<decimal>($"select Amount from Transactions  where Date_ >= '{dateTimePicker1.Text} 00:00:00' AND  Date_ <= '{dateTimePicker2.Text} 23:59:59'").ToList();

                decimal tsp = tt.Sum();
                total.Text = $"Rs.{tsp}";



                IEnumerable<decimal> cash = MainEngine_.GetDataScript<decimal>($"select Amount from Transactions WHERE LOWER(voucher_t) LIKE '%cash%' AND  Date_ >= '{dateTimePicker1.Text} 00:00:00' AND  Date_ <= '{dateTimePicker2.Text} 23:59:59'").ToList();
                decimal cash_amt = cash.Sum();

                IEnumerable<decimal> online = MainEngine_.GetDataScript<decimal>($"select Amount from Transactions WHERE LOWER(voucher_t) NOT LIKE '%cash%' AND LOWER(voucher_t) NOT LIKE '%credit%'  AND  Date_ >= '{dateTimePicker1.Text} 00:00:00' AND  Date_ <= '{dateTimePicker2.Text} 23:59:59'").ToList();
                decimal online_amt = online.Sum();

                IEnumerable<decimal> credit = MainEngine_.GetDataScript<decimal>($"select Amount from Transactions WHERE LOWER(voucher_t)  LIKE '%credit%'   AND  Date_ >= '{dateTimePicker1.Text} 00:00:00' AND  Date_ <= '{dateTimePicker2.Text} 23:59:59'").ToList();
                decimal credit_amt = credit.Sum();


                textcash.Text = $"Rs.{cash_amt}";

                textonline.Text = $"Rs.{online_amt}";

                creditamt.Text = $"Rs.{credit_amt}";



            }
            catch (Exception ex)
            {
                 
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                textcash.Text = $"Rs.0";

                textonline.Text = $"Rs.0";

                creditamt.Text = $"Rs.0";

                sql = $"select * from Transactions where LOWER(voucher_t) LIKE '%{comboBox1.Text}%' AND Date_ >= '{dateTimePicker1.Text} 00:00:00' AND  Date_ <= '{dateTimePicker2.Text} 23:59:59' ";
                List<dynamic> li = MainEngine_.GetDataScript<dynamic>(sql).ToList();
                dataGridView1.Rows.Clear();
                for (int i = 0; i < Math.Min(0 + 100, li.Count); i++)
                {
                    dataGridView1.Rows.Add(li[i].Date_, li[i].Particular, li[i].Description, li[i].voucher_t, li[i].Amount); ;
                }


                IEnumerable<decimal> tt = MainEngine_.GetDataScript<decimal>($"select Amount from Transactions  where LIKE '%{comboBox1.Text}%' AND Date_ >= '{dateTimePicker1.Text} 00:00:00' AND  Date_ <= '{dateTimePicker2.Text} 23:59:59'").ToList();

                decimal tsp = tt.Sum();
                total.Text = $"Rs.{tsp}";


 

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ReportStd rst = new ReportStd(sql, "AMIRSHAKH1234", textcash.Text, textonline.Text,creditamt.Text,dateTimePicker1.Text,dateTimePicker2.Text,total.Text);
                rst.Show();
            }
            catch (Exception EX)
            {
                 
            }
        }
    }
}
